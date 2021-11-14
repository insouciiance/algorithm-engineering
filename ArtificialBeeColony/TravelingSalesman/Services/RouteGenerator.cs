using System;
using ArtificialBeeColony;

namespace TravelingSalesman.Services
{
    public static class RouteGenerator
    {
        private static readonly Random Random = new();

        public static Route GenerateRandomRoute(Graph graph)
        {
            int[] vertices = new int[graph.VerticesCount];

            for(int i = 0; i < graph.VerticesCount; i++)
            {
                vertices[i] = i;
            }

            for(int i = 0; i < graph.VerticesCount; i++)
            {
                int randomVertexIndex = Random.Next(0, vertices.Length);
                int temp = vertices[randomVertexIndex];
                vertices[randomVertexIndex] = vertices[i];
                vertices[i] = temp;
            }

            return new Route(graph, vertices);
        }

        public static Route GenerateAdjacentRoute(Graph graph, Route route)
        {
            int[] vertices = new int[graph.VerticesCount];

            for(int i = 0; i < graph.VerticesCount; i++)
            {
                vertices[i] = route.Vertices[i];
            }

            int firstRandomIndex = Random.Next(0, graph.VerticesCount);
            int secondRandomIndex = Random.Next(0, graph.VerticesCount);

            int temp = vertices[firstRandomIndex];
            vertices[firstRandomIndex] = vertices[secondRandomIndex];
            vertices[secondRandomIndex] = temp;

            return new Route(graph, vertices);
        }
    }
}