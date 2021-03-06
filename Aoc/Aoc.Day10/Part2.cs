﻿
namespace Aoc.Day10
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using LanguageExt;
    using Aoc.Misc;
    using System.Net.Http.Headers;

    public static class Part2
    {
        static readonly Dictionary<string, long> cache = new();

        static long GetArrangements(List<int> adapters)
        {
            if (adapters.Count == 1) return 1;

            long arrangements = 0;
            int index = 1;
            int current = adapters[0];

            while (adapters.Count > index && adapters[index] - current < 4)
            {
                var span = adapters.Skip(index).Take(adapters.Count - 1).ToList();
                var spanStr = string.Join(',', span);

                if (cache.ContainsKey(spanStr))
                {
                    arrangements += cache[spanStr];
                }
                else
                {
                    long spanArrangements = GetArrangements(span);
                    cache[spanStr] = spanArrangements;
                    arrangements += spanArrangements;
                }

                index++;
            }

            return arrangements;
        }

        public static long Solve(IEnumerable<int> adapters) =>
            adapters
                .Prepend(0).Append(adapters.Last() + 3)
                .ToList()
                .Benchmark(GetArrangements)
                .Apply(x => x.elapsedTime.Printfn(x => x.TotalMilliseconds + " ms").Apply(_ => x.result));

    }
}
