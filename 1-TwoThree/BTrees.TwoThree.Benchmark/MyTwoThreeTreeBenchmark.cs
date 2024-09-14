using _01.Two_Three;
using _01.Two_Three.MySolution;
using BenchmarkDotNet.Attributes;
using Common;
using System;
using System.Linq;

namespace BTrees.Benchmark;

public class MyTwoThreeTreeBenchmark
{
    private IntWrapper[] _sequence;

    public MyTwoThreeTreeBenchmark()
    {
        var random = new Random();
        _sequence = Enumerable.Repeat(random.Next(1, 1000), 100_000).Select(x => new IntWrapper(x)).ToArray();
    }

    [Benchmark(Baseline = true)]
    public void Baseline()
    {
        var tree = new SimoTwoThreeTree<IntWrapper>();
        for (var i = 0; i < _sequence.Length; i++)
        {
            tree.Insert(_sequence[i]);
        }
    }

    [Benchmark]
    public void Scenario2()
    {
        var tree = new TwoThreeTree<IntWrapper>();
        for (var i = 0; i < _sequence.Length; i++)
        {
            tree.Insert(_sequence[i]);
        }
    }
}
