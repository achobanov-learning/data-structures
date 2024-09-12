using BenchmarkDotNet;
using BenchmarkDotNet.Attributes;
using System;

namespace BTrees.Benchmark;
public class Benchmarks
{
    public Benchmarks()
    {
        
    }

    [Benchmark(Baseline = true)]
    public void Baselline()
    {
        // Implement your benchmark here
    }

    [Benchmark]
    public void Scenario2()
    {
        // Implement your benchmark here
    }
}
