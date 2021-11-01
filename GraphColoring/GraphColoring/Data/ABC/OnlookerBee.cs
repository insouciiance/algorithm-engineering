using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphColoring.Data.ABC
{
    public class OnlookerBee : IEquatable<OnlookerBee>
    {
        public ColoredVertex Vertex { get; }

        public ColoredVertex AdjacentVertex { get; }

        public OnlookerBee(ColoredVertex vertex, ColoredVertex adjacentVertex)
        {
            Vertex = vertex;
            AdjacentVertex = adjacentVertex;
        }

        public bool Equals(OnlookerBee other)
        {
            if (other is null)
            {
                return false;
            }

            return this.Vertex.Equals(other.Vertex);
        }
    }
}
