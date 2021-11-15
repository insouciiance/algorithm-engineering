using System;
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

            for(int i = 0; i < VerticesCount; i++)
            {
                for(int j = 0; j < VerticesCount; j++)
                {
                    sb.Append(AdjacencyMatrix[i, j] + " ");
                }

                sb.AppendLine();
            }

            return sb.ToString();
        }
    }
}