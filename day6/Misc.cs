
namespace day6
{
    using System;
    using System.Collections.Generic;

    public static class Extensions
    {
        public static B Apply<A, B>(this A source, Func<A, B> f) => f(source);
        public static bool Apply<A>(this A source, Action<A> f) { f(source); return true; }
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
    }

    public static class Tools
    {
        public static IEnumerable<string> ReadFile(string path) => System.IO.File.ReadLines(path);
    }
}
