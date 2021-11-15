using System;
using System.Linq;

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

        public static Route GenerateGreedyRoute(Graph graph)
        {
            int[] vertices = new int[graph.VerticesCount];

            for(int i = 0; i < graph.VerticesCount; i++)
            {
                vertices[i] = -1;
            }

            int currentIndex = Random.Next(0, graph.VerticesCount);

            for(int i = 0; i < graph.VerticesCount; i++)
            {
                vertices[i] = currentIndex;

                for(int j = 0; j < graph.VerticesCount; j++)
                {
                    if (!vertices.Any(v => v == j))
                    {
                        currentIndex = j;
                        break;
                    }
                }

                for(int j = 0; j < graph.VerticesCount; j++)
                {
                    if (i == j) continue;

                    if (!vertices.Any(v => v == j) && graph.AdjacencyMatrix[vertices[i], j] < graph.AdjacencyMatrix[vertices[i], currentIndex])
                    {
                        currentIndex = j;
                    }
                }
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