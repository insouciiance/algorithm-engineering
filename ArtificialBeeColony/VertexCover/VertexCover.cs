using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using ArtificialBeeColony;

namespace VertexCover
{
    public class VertexCover : IOptimizable
    {
        public Graph Graph { get; }

        public Vertex[] Vertices { get; }

        public VertexCover(IEnumerable<Vertex> vertices)
        {
            Vertices = vertices.ToArray();
        }

        public double TotalCost => Vertices.Length;

        public bool BetterThan(IOptimizable other)
        {
            if (other is null)
            {
                throw new ArgumentNullException(nameof(other));
            }

            if (other is not VertexCover)
            {
                throw new InvalidCastException();
            }

            return this.TotalCost < other.TotalCost;
        }

        public int CompareTo(IOptimizable other)
        {
            if (other is null)
            {
                throw new ArgumentNullException(nameof(other));
            }

            if (other is not VertexCover)
            {
                throw new InvalidCastException();
            }

            return (int)(this.TotalCost - other.TotalCost);
        }

        public override string ToString()
        {
            return Vertices.Length.ToString();
        }
    }
}