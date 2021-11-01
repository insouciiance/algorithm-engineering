using System;
using GraphColoring.Data;
using GraphColoring.Services;

namespace GraphColoringTest
{
    class Program
    {
        static void Main(string[] args)
        {
            ColoredGraph graph = ColoredGraphGenerator.GenerateFile(250, 2, 30, "graph.txt");

            ColoredGraph g = new GraphColorer(graph).Color();

            foreach (ColoredVertex v in g.Vertices)
            {
                Console.WriteLine(v);
            }

            Console.WriteLine(g.ChromaticNumber);

            Console.WriteLine("Success");

            Console.ReadKey();
        }
    }
}
