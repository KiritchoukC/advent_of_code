
namespace Aoc.Day8
{
    using System;

    using Aoc.Misc;

    class Program
    {
        static void Main(string[] _) =>
            "input.txt"
                .Apply(Part1.Solve)
                .Apply(Console.WriteLine)
                .Apply(_ => "input.txt")
                .Apply(Part2.Solve)
                .Apply(Console.WriteLine);
    }
}
