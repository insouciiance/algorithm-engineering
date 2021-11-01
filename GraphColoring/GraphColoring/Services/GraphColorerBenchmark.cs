using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GraphColoring.Data;

namespace GraphColoring.Services
{
    public class GraphColorerBenchmark
    {
        public GraphColorer GraphColorer { get; }

        public GraphColorerBenchmark(GraphColorer colorer)
        {
            GraphColorer = colorer;
        }

        public void Run(int iterationsCount)
        {
            ColoredGraph result = null;

            for (int i = 1; i <= iterationsCount; i++)
            {
                ColoredGraph currentResult = GraphColorer.Color();

                if (result is null || result.ChromaticNumber > currentResult.ChromaticNumber)
                {
                    result = currentResult;
                }

                if (i % 20 == 0 || i == 1)
                {
                    Console.WriteLine($"iteration: {i, -3} chromatic number: {result.ChromaticNumber}");
                }
            }
        }
    }
}
