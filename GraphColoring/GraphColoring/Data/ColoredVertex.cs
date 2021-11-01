using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GraphColoring.Data
{
    public class ColoredVertex : IVertex<ColoredVertex>
    {
        public int Index { get; }

        public Color? Color { get; set; }

        public List<ColoredVertex> AdjacentVertices { get; set; }

        public int Degree => AdjacentVertices.Count;

        public List<Color> ForbiddenColors => GetForbiddenColors(); 

        public ColoredVertex(int index, IEnumerable<ColoredVertex> adjacentVertices = null)
        {
            Index = index;
            AdjacentVertices = adjacentVertices?.ToList() ?? new List<ColoredVertex>();
        }

        private List<Color> GetForbiddenColors()
        {
            List<Color> forbiddenColors = new();

            foreach(ColoredVertex vertex in AdjacentVertices)
            {
                Color? vertexColor = vertex.Color;

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

        public bool Equals(IVertex<ColoredVertex> other)
        {
            ColoredVertex otherColored = other as ColoredVertex;

            if (other is null)
            {
                return false;
            }

            return  this.Color == otherColored.Color &&
                    this.Index == otherColored.Index &&
                    this.Degree == otherColored.Degree &&
                    Enumerable.SequenceEqual(this.AdjacentVertices, otherColored.AdjacentVertices);
        }
    }
}