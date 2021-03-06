using System;
using GraphColoring.Data;
using GraphColoring.Services;

namespace GraphColoringTest
{
    class Program
    {
        static void Main(string[] args)
        {
            ColoredGraph graph = ColoredGraphGenerator.Generate(300, 1, 30);
            GraphColorer colorer = new (graph);

            GraphColorerBenchmark benchmark = new(colorer);
            benchmark.Run(1000);

            Console.ReadKey();
        }
    }
}
