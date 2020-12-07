
namespace day6
{
    using System;

    public class Program
    {
        private static string filePath = "./input.txt";

        static void Main(string[] _) =>
            filePath
                .Apply(Part1.Solve)
                .Apply(Console.WriteLine)
                .Apply(_ => filePath)
                .Apply(Part2.Solve)
                .Apply(Console.WriteLine);
    }
}
