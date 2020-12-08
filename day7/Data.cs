
namespace day7
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using static Tools;

    public static class Data
    {
        static List<string> Parse(string rule) =>
            rule
                .Split(Arr("bags contain", "bags,", "bag,", "bags.", "bag."), StringSplitOptions.TrimEntries)
                [0..^1]
                .Select(x => x.Trim())
                .ToList();

        static int GetNbRules(List<string> parsedRule) => parsedRule.Count - 1;

        static BagRule GetBagRule(string parsedRule) =>
            parsedRule == "no other" 
                ? null
                : new BagRule(
                    Color: parsedRule[1..^0].Trim(),
                    Count: int.Parse(parsedRule[0].ToString()));

        static Rule ToRule(List<string> parsedRule) =>
            parsedRule.ToList()
                .Apply(GetNbRules)
                .Apply(nbRules => new Rule(
                    Color: parsedRule[0],
                    Rules: Enumerable.Range(1, nbRules)
                            .Select(x => GetBagRule(parsedRule[x]))
                            .Where(NotNull)
                            .ToList()
                    )
                );

        public static List<Rule> Get(string path) =>
            path
                .Apply(ReadLines)
                .Select(Parse)
                .Select(ToRule)
                .ToList();
    }
}
