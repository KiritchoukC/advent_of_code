
namespace Aoc.Day8
{
    using System;
    using System.Collections.Generic;
    using System.Collections.Immutable;
    using System.Linq;

    using Aoc.Misc;

    using static Aoc.Misc.Tools;

    public static class Part2
    {
        private enum Status { None, Looped, OutOfRange, Terminated }

        private record Instruction(string Action, int Value, Status Status = Status.None)
        {
            public Instruction Done()   => this with { Status = Status.Looped };
            public Instruction Fix()    => this with { Action = Action is "jmp" ? "nop" : "jmp" };

            public static Instruction OutOfRange() => new("", 0, Status.OutOfRange);
            public static Instruction Terminated() => new("", 0, Status.Terminated);
        }

        private record State(
            ImmutableList<Instruction> Instructions, 
            int Result, 
            int Index, 
            ImmutableList<int> FixedIndices, 
            ImmutableList<Instruction> InitialInstructions)
        {
            public Instruction CurrentInstruction =>
                Index < Instructions.Count ? Instructions[Index] :
                Index > Instructions.Count ? Instruction.OutOfRange() :
                                             Instruction.Terminated();

            public State Accumulate()   => this with { Result = Result + CurrentInstruction.Value };
            public State Jump()         => this with { Index = Index + CurrentInstruction.Value };
            public State Next()         => this with { Index = Index + 1 };
            public State Restart()      => this with { Index = 0, Result = 0, Instructions = InitialInstructions };
            public State Fix(int index) => this with { Instructions = Instructions.Select((inst, i) => i == index ? inst.Fix() : inst).ToImmutableList(), FixedIndices = FixedIndices.Add(index) };
            public State Done()         => this with { Instructions = Instructions.Select((inst, i) => Index == i ? inst.Done() : inst).ToImmutableList() };
            public State NextFix()      => Instructions.FindIndex(FixedIndices.Max() + 1, i => i.Action is "jmp" or "nop" && i.Value != 0).Apply(Restart().Fix);

            public static State Init(IEnumerable<Instruction> instructions) =>
                new(
                    Instructions: instructions.ToImmutableList(),
                    Result: 0,
                    Index: 0,
                    FixedIndices: Lst(-1).ToImmutableList(),
                    InitialInstructions: instructions.ToImmutableList());
        }
        private static (string, string) ParseLine(string line) => (line[..3], line[4..]);
        private static Instruction ToDomain((string action, string value) parsedData) =>
            new(parsedData.action, int.Parse(parsedData.value));

        // Doesn't work for large input because c# does not optimize tail recursion
        private static int RunRec(State state) =>
            state.CurrentInstruction switch
            {
                ("nop", _, Status.None)         => state.Done().Next().Apply(RunRec),
                ("acc", _, Status.None)         => state.Done().Accumulate().Next().Apply(RunRec),
                ("jmp", _, Status.None)         => state.Done().Jump().Apply(RunRec),
                (_    , _, Status.OutOfRange)   => state.NextFix().Apply(RunRec),
                (_    , _, Status.Looped)       => state.NextFix().Apply(RunRec),
                (_    , _, Status.Terminated)   => state.Result,
                _ => throw new InvalidOperationException($"Unhandled status {state.CurrentInstruction.Status}")
            };

        // It works but it uses mutating variables and loop
        private static int Run(State initialState)
        {
            var state = initialState;

            while (state.CurrentInstruction.Status != Status.Terminated)
            {
                state = state.CurrentInstruction switch
                {
                    ("nop", _, Status.None)         => state.Done().Next(),
                    ("acc", _, Status.None)         => state.Done().Accumulate().Next(),
                    ("jmp", _, Status.None)         => state.Done().Jump(),
                    (_    , _, Status.OutOfRange)   => state.NextFix(),
                    (_    , _, Status.Looped)       => state.NextFix(),
                    (_    , _, Status.Terminated)   => state,
                    _ => throw new InvalidOperationException($"Unhandled status {state.CurrentInstruction.Status}")
                };
            }

            return state.Result;
        }

        private static int Start(IEnumerable<Instruction> instructions) => State.Init(instructions).Apply(Run);

        public static int Solve(string path) =>
            path
                .Apply(ReadLines)
                .Select(ParseLine)
                .Select(ToDomain)
                .Apply(Start);
    }
}
