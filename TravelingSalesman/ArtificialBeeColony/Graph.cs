using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArtificialBeeColony
{
    public class Graph
    {
        public List<Vertex> Vertices { get; }

        public int VerticesCount => Vertices.Count;

        public Graph(IEnumerable<Vertex> vertices)
        {
            Vertices = vertices?.ToList() ?? throw new ArgumentNullException(nameof(vertices));
        }

        public override string ToString()
        {
            StringBuilder sb = new();
            
            sb.AppendLine($"Vertices count: {VerticesCount}");

            foreach(Vertex vertex in Vertices)
            {
                sb.AppendLine(vertex.ToString());
            }

            return sb.ToString();
        }
    }
}