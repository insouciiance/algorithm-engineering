using System;

namespace TravelingSalesman
{
    public class Edge
    {
        public int Weight { get; }

        public Vertex FirstVertex { get; }

        public Vertex SecondVertex { get; }

        public Edge(int weight, Vertex firstVertex, Vertex secondVertex)
        {
            Weight = weight;
            FirstVertex = firstVertex ?? throw new ArgumentNullException(nameof(firstVertex));
            SecondVertex = secondVertex ?? throw new ArgumentNullException(nameof(secondVertex));
        }

        public Vertex GetAdjacentVertex(Vertex vertex)
        {
            if (FirstVertex.Equals(vertex))
            {
                return SecondVertex;
            }

            if (SecondVertex.Equals(vertex))
            {
                return FirstVertex;
            }

            return null;
        }
    }
}