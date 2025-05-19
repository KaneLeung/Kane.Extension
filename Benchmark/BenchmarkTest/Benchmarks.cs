using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;

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