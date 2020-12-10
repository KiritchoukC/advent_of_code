
namespace Aoc.Day7
{
    using System.Collections.Generic;

    public record BagRule(string Color, int Count);
    public record Rule(string Color, List<BagRule> Rules);
}
