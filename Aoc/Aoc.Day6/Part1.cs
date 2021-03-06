﻿
namespace Aoc.Day6
{
    using System.Collections.Generic;
    using System.Linq;

    using Aoc.Misc;

    using static Misc.Tools;

    public static class Part1
    {
        private static string Distinct(IEnumerable<string> source) =>
            string.Join("", source)
                .Distinct()
                .Select(x => x.ToString())
                .Aggregate((acc, val) => $"{acc}{val}");

        public static int Solve(string path) =>
            ReadLines(path)
                .SplitBy(x => x == "")
                .Select(Distinct)
                .Select(x => x.Length)
                .Sum();
    }
}
