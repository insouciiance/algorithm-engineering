using System;
using System.Collections.Generic;
using System.Linq;

namespace GraphColoring.Data
{
    public class ColoredGraph
    {
        public List<ColoredVertex> Vertices { get; }

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
        }

        public ColoredGraph(List<ColoredVertex> vertices)
        {
            Vertices = vertices;
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
    }
}