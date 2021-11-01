using System;
using GraphColoring.Data;
using GraphColoring.Services;

namespace GraphColorerTest
{
    class Program
    {
        static void Main(string[] args)
        {   
            Graph graph = new Graph(new bool[3, 3] {
                { false, true, true },
                { true, false, true },
                { true, true, false },
            });

            foreach(Vertex v in graph.Vertices)
            {
                Console.WriteLine(v);
            }

            ColoredGraph g = new GraphColorer(graph).Color();
        }
    }
}
