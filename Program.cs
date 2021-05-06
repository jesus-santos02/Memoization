using System;
using System.Collections.Concurrent;
using System.Diagnostics;

namespace Memoization
{
    static class Program
    {
        static void Main(string[] args)
        {
            Func<long, long> factorial = null;
            factorial = (n)=> n > 1? n * factorial(n - 1): 1;

            var sw = Stopwatch.StartNew();
            for(int i = 0; i < 200000000; i++){ factorial(15);} 
            System.Console.WriteLine(sw.ElapsedMilliseconds);

            var factorialM = factorial.Memoize();
            var swM = Stopwatch.StartNew();
            for(int i = 0; i < 200000000; i++){factorial(15);}
            System.Console.WriteLine(swM.ElapsedMilliseconds);
            
        }

        static Func<T, TResult> Memoize<T, TResult>(this Func<T, TResult> func)
        {
            var cache = new ConcurrentDictionary<T, TResult>();
            return (a) => cache.GetOrAdd(a, func);
        }
    }
}
