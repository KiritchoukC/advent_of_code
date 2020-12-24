
namespace Aoc.Misc
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Text.Json;

    using LanguageExt;

    public static class Extensions
    {
        public static (B result, TimeSpan elapsedTime) Benchmark<A, B>(this A source, Func<A, B> f)
        {
            var sw = Stopwatch.StartNew();
            var result= f(source);
            sw.Stop();
            return (result, sw.Elapsed);
        }
        public static bool IsContainedIn<T>(this T candidate, IEnumerable<T> potentialContainer) => potentialContainer.Contains(candidate);
        public static bool IsNullOrEmpty<T>(this IEnumerable<T> source) => source == null || source.Any() == false;
        public static A Printn<A>(this A source) { Console.WriteLine(source); return source; }
        public static A Printfn<A, B>(this A source, Func<A, B> f) { Console.WriteLine(f(source)); return source; }
        public static A LogSerialize<A>(this A source) { Console.WriteLine(JsonSerializer.Serialize(source)); return source; }
        public static A LogSerialize<A, B>(this A source, Func<A, B> f) { Console.WriteLine(JsonSerializer.Serialize(f(source))); return source; }
        public static IEnumerable<T> Order<T>(this IEnumerable<T> source) => source.OrderBy(x=> x);
        public static IEnumerable<(T, T)> PairWise<T>(this IEnumerable<T> source) => source.Zip(source.Skip(1), (a, b) => (a, b));
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
