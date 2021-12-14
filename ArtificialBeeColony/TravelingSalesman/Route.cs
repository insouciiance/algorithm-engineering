using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using ArtificialBeeColony;

namespace TravelingSalesman
{
    public class Route : IOptimizable
    {
        Graph Graph { get; }

        public int[] Vertices { get; }

        public double TotalCost
        {
            get
            {
                int totalCost = 0;

                for(int i = 0; i < Vertices.Length - 1; i++)
                {
                    int currentVertex = Vertices[i];
                    int nextVertex = Vertices[i + 1];

                    int currentWeight = Graph.AdjacencyMatrix[currentVertex, nextVertex];

                    totalCost += currentWeight;
                }

                totalCost += Graph.AdjacencyMatrix[Vertices[^1], Vertices[0]];

                return totalCost;
            }
        }

        public Route(Graph graph, IEnumerable<int> vertices)
        {
            Graph = graph;
            Vertices = vertices?.ToArray() ?? throw new ArgumentNullException(nameof(vertices));
        }

        public bool BetterThan(IOptimizable other)
        {
            if (other is null) throw new ArgumentNullException();

            if (other is not Route) throw new InvalidCastException();

            return this.TotalCost < other.TotalCost;
        }

        public int CompareTo(IOptimizable other)
        {
            if (other is null) throw new ArgumentNullException();

            if (other is not Route) throw new InvalidCastException();

            return (int)(this.TotalCost - other.TotalCost);
        }

        public override string ToString()
        {
            StringBuilder sb = new();

            sb.Append("Route: ");

            foreach(int vertex in Vertices)
            {
                sb.Append($"{vertex} ");
            }

            sb.AppendLine($"\nTotalCost: {TotalCost}");

            return sb.ToString();
        }
    }
}