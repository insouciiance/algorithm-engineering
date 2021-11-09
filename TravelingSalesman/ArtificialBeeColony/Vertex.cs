using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArtificialBeeColony
{
    public class Vertex : IEquatable<Vertex>
    {
        public int Index { get; }

        public List<Edge> Edges { get; }

        public Vertex(int index, List<Edge> edges = null)
        {
            Index = index;
            Edges = edges ?? new List<Edge>();
        }

        public Edge GetAdjacentEdge(Vertex other)
        {
            return Edges.FirstOrDefault(e => e.GetAdjacentVertex(this).Equals(other));
        }

        public bool Equals(Vertex other)
        {
            return this.Index == other?.Index;
        }

        public override string ToString()
        {
            StringBuilder sb = new();

            sb.AppendLine($"Vertex index: [{Index}]");

            sb.AppendLine("Edges:");

            foreach(Edge edge in Edges)
            {
                Vertex adjacentVertex = edge.GetAdjacentVertex(this);

                sb.AppendLine($"---{edge.Weight}---[{adjacentVertex.Index}]");
            } 

            return sb.ToString();
        }
    }
}