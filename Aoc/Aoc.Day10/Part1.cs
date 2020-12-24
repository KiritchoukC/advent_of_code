
namespace Aoc.Day10
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Aoc.Misc;

    using LanguageExt;

    using static LanguageExt.Prelude;


    public static class Part1
    {
        record AdaptersCount(int Ones, int Twos, int Threes);

        static Func<int, Func<IEnumerable<IGrouping<int, int>>, int>> Count =
            number =>
            diffs =>
            diffs.FirstOrDefault(x => x.Key == number).Apply(Optional)
                .Match(
                    x => x.Count() + 1,
                    () => 0);

        static AdaptersCount CountAdapters(IEnumerable<IGrouping<int, int>> diffs) =>
            new(
                Ones:   diffs.Apply(Count(1)),
                Twos:   diffs.Apply(Count(2)),
                Threes: diffs.Apply(Count(3)));

        static int GetResult(AdaptersCount adaptersCount) => adaptersCount.Ones * adaptersCount.Threes;

        public static int Solve(IEnumerable<int> adapters) =>
            adapters
                .PairWise()
                .Select(x => x.Item2 - x.Item1)
                .GroupBy(identity)
                .Apply(CountAdapters)
                .Apply(GetResult);
    }
}
