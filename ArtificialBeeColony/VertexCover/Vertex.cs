using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VertexCover
{
    public class Vertex : IEquatable<Vertex>
    {
        public int Index { get; }

        public List<Edge> AdjacentEdges { get; }

        public int Degree => AdjacentEdges.Count;

        public Vertex(int index, List<Edge> adjacentEdges = null)
        {
            Index = index;
            AdjacentEdges = adjacentEdges ?? new List<Edge>();
        }

        public bool Equals(Vertex other)
        {
            return this.Index == other?.Index;
        }

        public override string ToString()
        {
            StringBuilder sb = new();

            sb.AppendLine($"Vertex index: [{Index}]");

            sb.AppendLine("Adjacent vertices:");

            foreach(Edge edge in AdjacentEdges)
            {
                sb.AppendLine($"[{edge.GetAdjacentVertex(this)}]");
            } 

            return sb.ToString();
        }
    }
}