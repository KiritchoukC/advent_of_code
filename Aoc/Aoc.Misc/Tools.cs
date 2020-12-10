
namespace Aoc.Misc
{
    using System.Collections;
    using System.Collections.Generic;

    public static class Tools
    {
        public static IEnumerable<string> ReadLines(string path) => System.IO.File.ReadLines(path);

        // Functions

        // Constructors
        public static T[] Arr<T>(params T[] items) => new List<T>(items).ToArray();
        public static List<T> Lst<T>(params T[] items) => new(items);

        // Validators
        public static bool NotNull(object o) => o != null;
        public static bool IsTrue(bool b) => b;
    }
}
