
namespace day6
{
    using System.Collections.Generic;
    using System.Linq;

    using static Tools;

    public static class Part2
    {
        private static int GroupAnswers(IEnumerable<string> source) =>
            string.Join("", source)
                .GroupBy(x => x)
                .Select(x => x.Count() == source.Count() ? 1 : 0)
                .Sum();

        public static int Solve(string path) =>
            ReadFile(path)
                .SplitBy(x => x == "")
                .Select(GroupAnswers)
                .Sum();
    }
}
