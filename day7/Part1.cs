
namespace day7
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using static day7.Tools;

    public static class Part1
    {
        static bool CanHoldThisBagRec(string color, List<BagRule> bagRules, List<Rule> rules) =>
            bagRules.IsNullOrEmpty()
                ? false
                : bagRules.Any(r =>
                    r.Color == color || CanHoldThisBagRec(color, rules.FirstOrDefault(x => x.Color == r.Color)?.Rules, rules));

        static Func<Rule, bool> CanHoldThisBag(string color, List<Rule> rules) =>
            rule => CanHoldThisBagRec(color, rule.Rules, rules);

        public static int Solve(string path) =>
            Data.Get(path)
                .Apply(rules => rules.Select(CanHoldThisBag("shiny gold", rules)))
                .Where(IsTrue)
                .Count();
    }
}
