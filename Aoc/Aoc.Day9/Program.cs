
namespace Aoc.Day9
{
    using System;

    using Aoc.Misc;

    using LanguageExt;

    class Program
    {
        static void Main(string[] _) =>
            "input.txt"
                .Apply(Part1.Solve(25))
                .Printn()
                .Apply(Part2.Solve("input.txt"))
                .Printn();
    }
}
