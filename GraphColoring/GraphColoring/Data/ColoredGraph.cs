using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GraphColoring.Data
{
    public class ColoredGraph
    {
        public List<ColoredVertex> Vertices { get; }

        public bool[,] AdjacencyMatrix { get; }

        public int VerticesCount => Vertices.Count;

        public int ChromaticNumber => GetChromaticNumber();

        public ColoredGraph(bool[,] adjacencyMatrix)
        {
            int verticesCount = adjacencyMatrix.GetLength(0);

            Vertices = new List<ColoredVertex>();

            for (int i = 0; i < verticesCount; i++)
            {
                Vertices.Add(new ColoredVertex(i));
            }

            for (int i = 0; i < verticesCount; i++)
            {
                for (int j = 0; j < verticesCount; j++)
                {
                    if (adjacencyMatrix[i, j])
                    {
                        Vertices[i].AdjacentVertices.Add(Vertices[j]);
                    }
                }
            }

            AdjacencyMatrix = adjacencyMatrix;
        }

        public ColoredGraph(List<ColoredVertex> vertices)
        {
            Vertices = vertices;
            int verticesCount = vertices.Count;
            
            bool[,] adjacencyMatrix = new bool[verticesCount, verticesCount];

            for (int i = 0; i < verticesCount; i++)
            {
                for (int j = 0; j < verticesCount; j++)
                {
                    if (Vertices[i].AdjacentVertices.Contains(Vertices[j]))
                    {
                        adjacencyMatrix[i, j] = true;
                    }
                    else
                    {
                        adjacencyMatrix[i, j] = false;
                    }
                }
            }

            AdjacencyMatrix = adjacencyMatrix;
        }

        private int GetChromaticNumber()
        {
            List<int> usedColors = new ();

            foreach(ColoredVertex vertex in Vertices)
            {
                int? vertexColor = vertex.Color;

                if (vertexColor is null)
                {
                    return -1;
                }

                if (!usedColors.Contains(vertexColor.Value))
                {
                    usedColors.Add(vertexColor.Value);
                }
            }

            return usedColors.Count;
        }

        public override string ToString()
        {
            StringBuilder sb = new();

            sb.AppendLine($"Vertices count: {VerticesCount}");
            sb.AppendLine($"Chromatic number: {ChromaticNumber}");

            foreach (ColoredVertex coloredVertex in Vertices)
            {
                sb.AppendLine();
                sb.Append(coloredVertex);
            }

            return sb.ToString();
        }
    }
}