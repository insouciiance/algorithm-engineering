using System;
using ArtificialBeeColony;

namespace TravelingSalesman.Services
{
    public static class RouteGenerator
    {
        private static readonly Random Random = new();

        public static Route GenerateRandomRoute(Graph graph)
        {
            if (graph is null)
            {
                throw new ArgumentNullException(nameof(graph));
            }

            Vertex[] vertices = new Vertex[graph.VerticesCount];

            graph.Vertices.CopyTo(vertices);

            for(int i = 1; i < graph.VerticesCount; i++)
            {
                int randomIndex = Random.Next(1, graph.VerticesCount);

                Vertex temp = vertices[i];
                vertices[i] = vertices[randomIndex];
                vertices[randomIndex] = temp;
            }

            return new Route(vertices);
        }

        public static Route GenerateAdjacentRoute(Graph _, Route route)
        {
            if (route.Vertices.Length < 3)
            {
                return route;
            }

            Vertex[] vertices = new Vertex[route.Vertices.Length];

            route.Vertices.CopyTo(vertices, 0);

            int firstSwapIndex = Random.Next(0, vertices.Length);

            int secondSwapIndex;
            do
            {
                secondSwapIndex = Random.Next(0, vertices.Length);
            } while (firstSwapIndex == secondSwapIndex);

            Vertex temp = vertices[firstSwapIndex];
            vertices[firstSwapIndex] = vertices[secondSwapIndex];
            vertices[secondSwapIndex] = temp;

            return new Route(vertices);
        }
    }
}