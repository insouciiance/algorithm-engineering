using System;
using GraphColoring.Data;
using GraphColoring.Services;

namespace GraphColoringTest
{
    class Program
    {
        static void Main(string[] args)
        {
            ColoredGraph graph = ColoredGraphGenerator.GenerateFromFile("graph.txt");
            GraphColorer colorer = new (graph);

            GraphColorerBenchmark benchmark = new(colorer);
            benchmark.Run(1000);

            Console.ReadKey();
        }
    }
}
