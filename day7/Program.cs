using System;

namespace day7
{
    class Program
    {
        static void Main(string[] args) =>
            "input.txt"
                .Apply(Part1.Solve)
                .Apply(Console.WriteLine)
                .Apply(_ => "input.txt")
                .Apply(Part2.Solve)
                .Apply(Console.WriteLine);
    }
}
