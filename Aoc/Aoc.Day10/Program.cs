using System;
using System.Linq;

using Aoc.Misc;

using LanguageExt;

using static LanguageExt.Prelude;

namespace Aoc.Day10
{
    class Program
    {
        static void Main(string[] args)
        {
            var input =
                "input.txt"
                .Apply(Tools.ReadLines)
                .Select(int.Parse)
                .Order();

            input
                .Apply(Part1.Solve)
                .Printn();

            input
                .Apply(Part2.Solve)
                .Printn();
        }
    }
}