
namespace Aoc.Day9
{
    using System;
    using System.Collections.Generic;
    using System.Collections.Immutable;
    using System.Linq;

    using Aoc.Misc;

    using static System.Linq.Enumerable;
    using static Aoc.Misc.Tools;

    public class Part2
    {
        private enum Status { Lower, Greater, Terminated, OnlyOneNumber }

        private record State(
            ImmutableList<long> Numbers,
            long InvalidNumber,
            ImmutableList<int> Indices,
            long Result,
            int Index)
        {
            public Status Status =>
                Result < InvalidNumber ? Status.Lower :
                Result > InvalidNumber ? Status.Greater :
                Indices.Count == 1     ? Status.OnlyOneNumber :
                                         Status.Terminated;

            public State AddIndice() => this with { Indices = Indices.Add(Index) };
            public State SetResult() => this with { Result = Numbers[Index] + Result };
            public State Next() => this with { Index = Index + 1 };
            public State Reset() => this with { Index = Index - Indices.Count + 1, Indices = ImmutableList<int>.Empty, Result = 0 };

            public static State Init(IEnumerable<long> numbers, long invalidNumber) =>
                new(numbers.ToImmutableList(), invalidNumber, ImmutableList<int>.Empty, 0, 0);
        }

        // Recursive
        private static State FindContiguousSetRec(State state) =>
            state.Status switch
            {
                    Status.Lower            => state.AddIndice().SetResult().Next().Apply(FindContiguousSetRec),
                    Status.Greater          => state.Reset().Next().Apply(FindContiguousSetRec),
                    Status.OnlyOneNumber    => state.Reset().Next().Apply(FindContiguousSetRec),
                    Status.Terminated       => state,
                    _ => throw new Exception($"Unhandled status {state.Status}")
            };

        // Mutation + loop
        private static State FindContiguousSet(State state)
        {
            while(state.Status != Status.Terminated)
            {
                state = state.Status switch
                {
                    Status.Lower            => state.AddIndice().SetResult().Next(),
                    Status.Greater          => state.Reset().Next(),
                    Status.OnlyOneNumber    => state.Reset().Next(),
                    Status.Terminated       => state,
                    _ => throw new Exception($"Unhandled status {state.Status}")
                };
            }

            return state;
        }


        public static Func<long, long> Solve(string path) =>
            invalidNumber =>
            path
                .Apply(ReadLines)
                .Select(long.Parse)
                .Apply(lines => FindContiguousSet(State.Init(lines, invalidNumber)))
                .Apply(AddSmallestAndGreatest);

        private static long AddSmallestAndGreatest(State state) => 
            state.Indices
                .Select(i => state.Numbers[i])
                .Apply(ns => ns.Min() + ns.Max());
    }
}
