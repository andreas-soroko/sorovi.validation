using BenchmarkDotNet.Running;

namespace sorovi.Validation.Benchmark
{
    class Program
    {
        static void Main(string[] args)
        {
            var summary = BenchmarkRunner.Run<SoroviValidation>();
        }
    }
}