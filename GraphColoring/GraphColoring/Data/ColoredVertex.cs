using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GraphColoring.Data
{
    public class ColoredVertex : IEquatable<ColoredVertex>
    {
        public int Index { get; }

        public int? Color { get; set; }

        public List<ColoredVertex> AdjacentVertices { get; set; }

        public int Degree => AdjacentVertices.Count;

        public int UncoloredDegree => AdjacentVertices.Count(v => v.Color is null);

        public List<int> ForbiddenColors => GetForbiddenColors(); 

        public ColoredVertex(int index, IEnumerable<ColoredVertex> adjacentVertices = null)
        {
            Index = index;
            AdjacentVertices = adjacentVertices?.ToList() ?? new List<ColoredVertex>();
        }

        private List<int> GetForbiddenColors()
        {
            List<int> forbiddenColors = new();

            foreach(ColoredVertex vertex in AdjacentVertices)
            {
                int? vertexColor = vertex.Color;

                if (vertexColor is null || forbiddenColors.Contains(vertexColor.Value))
                {
                    continue;
                }

                forbiddenColors.Add(vertexColor.Value);
            }

            return forbiddenColors;
        }

        public override string ToString()
        {
            StringBuilder sb = new ();
            sb.AppendLine($"Vertex index: {Index}");
            foreach(ColoredVertex v in AdjacentVertices)
            {
                sb.AppendLine($"Adjacent vertex index: {v.Index}");
            }
            sb.AppendLine($"Color: {this.Color}");

            return sb.ToString();
        }

        public bool Equals(ColoredVertex other)
        {
            if (other is null)
            {
                return false;
            }

            return this.Color == other.Color &&
                   this.Index == other.Index &&
                   this.Degree == other.Degree;
        }
    }
}