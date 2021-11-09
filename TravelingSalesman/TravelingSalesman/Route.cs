using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;

namespace TravelingSalesman
{
    public class Route : IOptimizable
    {
        public Vertex[] Vertices { get; }

        public bool IsAscending => false;

        public double TotalCost
        {
            get
            {
                int totalCost = 0;

                for(int i = 0; i < Vertices.Length - 1; i++)
                {
                    Vertex currentVertex = Vertices[i];
                    Vertex nextVertex = Vertices[i + 1];

                    Edge currentEdge = currentVertex.GetAdjacentEdge(nextVertex);

                    totalCost += currentEdge.Weight;
                }

                totalCost += Vertices[^1].GetAdjacentEdge(Vertices[0]).Weight;

                return totalCost;
            }
        }

        public Route(IEnumerable<Vertex> vertices)
        {
            Vertices = vertices?.ToArray() ?? throw new ArgumentNullException(nameof(vertices));
        }

        public override string ToString()
        {
            StringBuilder sb = new();

            sb.Append("Route: ");

            foreach(Vertex vertex in Vertices)
            {
                sb.Append($"{vertex.Index} ");
            }

            sb.AppendLine($"\nTotalCost: {TotalCost}");

            return sb.ToString();
        }
    }
}