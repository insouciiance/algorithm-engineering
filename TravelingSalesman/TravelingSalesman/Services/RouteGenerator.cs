using System;
using System.Collections.Generic;

namespace TravelingSalesman.Services
{
    public static class RouteGenerator
    {
        private static readonly Random Random = new();

        public static Route GenerateRandomRoute(Graph graph)
        {
            if (graph is null)
            {
                throw new ArgumentNullException();
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
    }
}