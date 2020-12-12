
namespace Aoc.Day9
{
    using System;
    using System.Collections.Generic;
    using System.Collections.Immutable;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using Aoc.Misc;
    using static Aoc.Misc.Tools;
    using static System.Linq.Enumerable;

    public class Part1
    {
        private record State(ImmutableList<long> Numbers, int Preamble);

        private static IEnumerable<long> GetSums(State state, int index) =>
            state.Numbers
                .Skip(index)
                .Take(state.Preamble)
                .SelectMany(x => state.Numbers.Skip(index).Take(state.Preamble).Where(y => y != x).Select(y => x + y));

        private static bool IsValid(State state, long number, int index) =>
            GetSums(state, index)
                .Contains(number);

        private static long Run(State state) =>
            state.Numbers.Skip(state.Preamble)
                .Where((num, i) => IsValid(state, num, i) == false)
                .FirstOrDefault();

        public static Func<string, long> Solve(int preamble) =>
            path =>
            path
                .Apply(ReadLines)
                .Select(long.Parse)
                .Apply(lines => Run(new(lines.ToImmutableList(), preamble)));
    }
}
