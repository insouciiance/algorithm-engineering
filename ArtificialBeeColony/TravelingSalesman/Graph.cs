using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TravelingSalesman
{
    public class Graph
    {
        public int[,] AdjacencyMatrix { get; }

        public int VerticesCount => AdjacencyMatrix.GetLength(0);

        public Graph(int[,] adjacencyMatrix)
        {
            AdjacencyMatrix = adjacencyMatrix;
        }

        public override string ToString()
        {
            StringBuilder sb = new();
            
            sb.AppendLine($"Vertices count: {VerticesCount}");

            return sb.ToString();
        }
    }
}