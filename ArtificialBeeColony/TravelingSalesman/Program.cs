using System;
using ArtificialBeeColony.Core;
using TravelingSalesman.Services;

namespace TravelingSalesman
{
    class Program
    {
        static void Main(string[] args)
        {
            Graph graph = GraphGenerator.GenerateFullGraph(300, 5, 150);

            Hive<Route, Graph> hive = new(graph, RouteGenerator.GenerateRandomRoute, RouteGenerator.GenerateAdjacentRoute);

            hive.Solve(true);
        }
    }
}
