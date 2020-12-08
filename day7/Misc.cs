namespace day7
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public static class Extensions
    {
        public static B Apply<A, B>(this A source, Func<A, B> f) => f(source);
        public static bool Apply<A>(this A source, Action<A> f) { f(source); return true; }
        public static A Log<A>(this A source) { Console.WriteLine(System.Text.Json.JsonSerializer.Serialize(source)); return source; }
        public static A Log<A, B>(this A source, Func<A, B> f) { Console.WriteLine(f(source)); return source; }
        public static IEnumerable<IEnumerable<T>> SplitBy<T>(this IEnumerable<T> source, Func<T, bool> f)
        {
            var result = new List<T>();

            foreach (var item in source)
            {
                if (f(item))
                {
                    yield return result;

                    result = new List<T>();
                }
                else
                {
                    result.Add(item);
                }
            }

            yield return result;
        }

        public static bool IsNullOrEmpty<T>(this IEnumerable<T> source) => source == null || source.Any() == false;
    }

    public static class Tools
    {
        public static IEnumerable<string> ReadLines(string path) => System.IO.File.ReadLines(path);
        public static T[] Arr<T>(params T[] items) => new List<T>(items).ToArray();
        public static bool NotNull(object o) => o != null;
        public static bool IsTrue(bool b) => b;
    }
}

