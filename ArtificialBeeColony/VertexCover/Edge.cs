using System;

namespace VertexCover
{
    public class Edge
    {
        public Vertex FirstVertex { get; }

        public Vertex SecondVertex { get; }

        public Edge(Vertex firstVertex, Vertex secondVertex)
        {
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