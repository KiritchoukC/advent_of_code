
namespace Aoc.Day6
{
    using System.Collections.Generic;
    using System.Linq;

    using Aoc.Misc;

    using static Misc.Tools;

    public static class Part2
    {
        private static int GroupAnswers(IEnumerable<string> source) =>
            string.Join("", source)
                .GroupBy(x => x)
                .Select(x => x.Count() == source.Count() ? 1 : 0)
                .Sum();

        public static int Solve(string path) =>
            ReadLines(path)
                .SplitBy(x => x == "")
                .Select(GroupAnswers)
                .Sum();
    }
}
