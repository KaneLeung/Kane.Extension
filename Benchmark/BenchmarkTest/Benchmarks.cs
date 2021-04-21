using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;
using System;
using System.Collections.Generic;
using System.Text;

namespace BenchmarkTest
{
    [RankColumn]
    [Orderer(SummaryOrderPolicy.FastestToSlowest)]
    [MemoryDiagnoser]
    public class Benchmarks
    {
        [Benchmark]
        public void UseAAA()
        {
        }

        [Benchmark]
        public void UseBBB()
        {
        }
    }
}