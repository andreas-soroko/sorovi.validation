using BenchmarkDotNet.Running;

namespace sorovi.Validation.Benchmark
{
    class Program
    {
        static void Main(string[] args)
        {
            //BenchmarkRunner.Run<SoroviValidationBenchmark>();
            //BenchmarkRunner.Run<DawnGuardBenchmark>();
            BenchmarkRunner.Run<EnsureThatBenchmark>();
        }
    }
}