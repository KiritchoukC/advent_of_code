
namespace Aoc.Misc
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.Json;

    public static class Extensions
    {
        public static B Apply<A, B>(this A source, Func<A, B> f) => f(source);
        public static bool Apply<A>(this A source, Action<A> f) { f(source); return true; }
        public static bool IsContainedIn<T>(this T candidate, IEnumerable<T> potentialContainer) => potentialContainer.Contains(candidate);
        public static bool IsNullOrEmpty<T>(this IEnumerable<T> source) => source == null || source.Any() == false;
        public static A Log<A>(this A source) { Console.WriteLine(source); return source; }
        public static A Log<A, B>(this A source, Func<A, B> f) { Console.WriteLine(f(source)); return source; }
        public static A LogSerialize<A>(this A source) { Console.WriteLine(JsonSerializer.Serialize(source)); return source; }
        public static A LogSerialize<A, B>(this A source, Func<A, B> f) { Console.WriteLine(JsonSerializer.Serialize(f(source))); return source; }
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
}
