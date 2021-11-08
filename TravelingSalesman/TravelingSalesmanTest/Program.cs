using System;
using TravelingSalesman;
using TravelingSalesman.Services;

namespace TravelingSalesmanTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Graph graph = GraphGenerator.GenerateFullGraph(4, (v1, v2) => v2.Index - v1.Index);

            Console.WriteLine(graph); 
        }
    }
}
