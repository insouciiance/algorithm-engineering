using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GraphColoring.Data
{
    public class Vertex : IVertex<Vertex>
    {
        public int Index { get; }

        public List<Vertex> AdjacentVertices { get; set; }

        public int Degree => AdjacentVertices.Count;

        public Vertex(int index, IEnumerable<Vertex> adjacentVertices = null)
        {
            Index = index;
            AdjacentVertices = adjacentVertices?.ToList() ?? new List<Vertex>();
        }

        public override string ToString()
        {
            StringBuilder sb = new ();
            sb.AppendLine($"Vertex index: {Index}");
            foreach(Vertex v in AdjacentVertices)
            {
                sb.AppendLine($"Adjacent vertex index: {v.Index}");
            }

            return sb.ToString();
        }

        public bool Equals(IVertex<Vertex> other)
        {
            Vertex otherColored = other as Vertex;

            if (other is null)
            {
                return false;
            }

            return  this.Index == otherColored.Index &&
                    this.Degree == otherColored.Degree &&
                    Enumerable.SequenceEqual(this.AdjacentVertices, otherColored.AdjacentVertices);
        }
    }
}