
namespace Aoc.Day8
{
    using System;

    using Aoc.Misc;

    using LanguageExt;

    class Program
    {
        static void Main(string[] _) =>
            "input.txt"
                .Apply(Part1.Solve)
                .Printn()
                .Apply(_ => "input.txt")
                .Apply(Part2.Solve)
                .Printn();
    }
}
