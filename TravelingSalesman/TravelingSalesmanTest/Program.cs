using System;
using System.Threading.Tasks;
using TravelingSalesman;
using TravelingSalesman.ABC;
using TravelingSalesman.Services;

namespace TravelingSalesmanTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Graph graph = GraphGenerator.GenerateFullGraph(300, 5, 150);

            Hive<Route> hive = new(graph, RouteGenerator.GenerateRandomRoute, RouteGenerator.GenerateAdjacentRoute);

            hive.Solve(true);
        }
    }
}
