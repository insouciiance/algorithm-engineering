using System;
using GraphColoring.Data;
using GraphColoring.Services;

namespace GraphColoringTest
{
    class Program
    {
        static void Main(string[] args)
        {
            ColoredGraph graph = ColoredGraphGenerator.GenerateFile(300, 2, 30, "graph.txt");
            GraphColorer colorer = new (graph);

            for (int i = 1; i < 1000; i++)
            {
                ColoredGraph g = colorer.Color(i);

                Console.WriteLine(g.ChromaticNumber);
            }

            Console.WriteLine("Success");

            Console.ReadKey();
        }
    }
}
