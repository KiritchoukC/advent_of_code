
namespace Aoc.Day8
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Cryptography.X509Certificates;
    using System.Text;
    using System.Threading.Tasks;

    using Aoc.Misc;

    using static Aoc.Misc.Tools;

    public static class Part1
    {
        private record Instruction(string Action, int Value, bool Done = false);
        private record ExecutionResult(int Jumps, int Accumulator);

        private static (string, string) ParseLine(string line) => (line[0..3], line[3..^0]);
        private static Instruction ToDomain((string action, string value) parsedData) =>
            new(parsedData.action, int.Parse(parsedData.value));

        private static List<Instruction> MarkInstructionDone(List<Instruction> instructions, int index) =>
            instructions.Select((inst, i) => index == i ? inst with { Done = true } : inst).ToList();

        private static ExecutionResult Execute(Instruction instruction, int currentResult) =>
            instruction switch
            {
                ("nop", _, _) => new(1, currentResult),
                ("acc", _, _) => new(1, currentResult + instruction.Value),
                ("jmp", _, _) => new(instruction.Value, currentResult),
                _ => throw new InvalidOperationException($"Unhandled action {instruction.Action}")
            };

        private static int Next(int index, List<Instruction> instructions, int result) =>
            instructions[index] switch
            {
                (_, _, true) => result,
                _ => Execute(instructions[index], result)
                       .Apply(x => Next(index + x.Jumps, MarkInstructionDone(instructions, index), x.Accumulator))
            };

        private static int Start(IEnumerable<Instruction> instructions) => Next(0, instructions.ToList(), 0);

        public static int Solve(string path) =>
            path
                .Apply(ReadLines)
                .Select(ParseLine)
                .Select(ToDomain)
                .Apply(Start);
    }
}
