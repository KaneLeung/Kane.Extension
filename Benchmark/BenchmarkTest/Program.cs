using BenchmarkDotNet.Running;
using System;
using System.Collections.Generic;
using System.Text;

namespace BenchmarkTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var summary = BenchmarkRunner.Run<Benchmarks>();
            Console.ReadKey();
        }
    }
}