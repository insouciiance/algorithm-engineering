using System;
using ArtificialBeeColony.Core;
using TravelingSalesman.Services;

namespace TravelingSalesman
{
    class Program
    {
        static void Main(string[] args)
        {
            Graph graph = GraphGenerator.GenerateFullGraph(5, 1, 20);

            Console.WriteLine(graph);

            Hive<Route, Graph> hive = new(graph, RouteGenerator.GenerateRandomRoute, RouteGenerator.GenerateAdjacentRoute);

            Console.WriteLine(hive.Solve(true));
        }
    }
}
