using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VertexCover
{
    public class Vertex : IEquatable<Vertex>
    {
        public int Index { get; }

        public List<Vertex> AdjacentVertices { get; }

        public int Degree => AdjacentVertices.Count;

        public Vertex(int index, List<Vertex> adjacentVertices = null)
        {
            Index = index;
            AdjacentVertices = adjacentVertices ?? new List<Vertex>();
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

            foreach(Vertex vertex in AdjacentVertices)
            {
                sb.AppendLine($"[{vertex.Index}]");
            } 

            return sb.ToString();
        }
    }
}