
namespace day7
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public static class Part2
    { 
        private static Func<Rule, IEnumerable<int>> CountBagsContained(List<Rule> rules) =>
            rule =>
                rule == null ? Enumerable.Empty<int>() :
                rule.Rules
                    .Select(r => r.Count + r.Count * CountBagsContained(rules)(rules.FirstOrDefault(x => x.Color == r.Color)).Sum());

        public static int Solve(string path) =>
            Data.Get(path)
                .Apply(rules =>
                    rules.FirstOrDefault(r => r.Color == "shiny gold")
                    .Apply(CountBagsContained(rules)))
                .Sum();
    }
}
