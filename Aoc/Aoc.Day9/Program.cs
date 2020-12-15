
namespace Aoc.Day9
{
    using System;

    using Aoc.Misc;

    class Program
    {
        static void Main(string[] _) =>
            "input.txt"
                .Apply(Part1.Solve(25))
                .Apply(Console.WriteLine)
                .Apply(Part2.Solve("input.txt"))
                .Apply(Console.WriteLine);
    }
}
