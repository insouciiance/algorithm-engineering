using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphColoring.Data.ABC
{
    public class EmployedBee : IBee, IEquatable<EmployedBee>
    {
        public ColoredVertex Vertex { get; }

        public int Nectar => Vertex.Degree;

        public EmployedBee(ColoredVertex vertex)
        {
            Vertex = vertex;
        }

        public bool Equals(EmployedBee other)
        {
            if (other is null)
            {
                return false;
            }

            return this.Vertex.Equals(other.Vertex)
                   && this.Nectar == other.Nectar;
        }

        public override string ToString()
        {
            StringBuilder sb = new ();

            sb.Append(Vertex);
            sb.Append($"Nectar amount: {Nectar}");

            return sb.ToString();
        }
    }
}
