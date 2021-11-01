using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GraphColoring.Data;

namespace GraphColoring.Services
{
    public static class ColoredGraphGenerator
    {
        private static readonly Random Random = new();

        public static ColoredGraph Generate(int verticesCount, int minDegree, int maxDegree)
        {
            List<ColoredVertex> vertices = new();

            for (int i = 0; i < verticesCount; i++)
            {
                vertices.Add(new ColoredVertex(i));
            }

            for (int i = 0; i < verticesCount; i++)
            {
                ColoredVertex currentVertex = vertices[i];

                int adjacentVerticesCount = Random.Next(minDegree, maxDegree + 1) - currentVertex.Degree;

                for (int j = 0; j < adjacentVerticesCount; j++)
                {
                    ColoredVertex adjacentVertex;
                    int randomAdjacentVertexIndex;

                    do
                    {
                        randomAdjacentVertexIndex = Random.Next(0, verticesCount);
                        adjacentVertex = vertices[randomAdjacentVertexIndex];

                    } while (randomAdjacentVertexIndex == i || currentVertex.AdjacentVertices.Contains(adjacentVertex));

                    currentVertex.AdjacentVertices.Add(adjacentVertex);
                    adjacentVertex.AdjacentVertices.Add(currentVertex);
                }
            }

            return new ColoredGraph(vertices);
        }

        public static ColoredGraph GenerateFile(int verticesCount, int minDegree, int maxDegree, string fileName)
        {
            ColoredGraph graph = Generate(verticesCount, minDegree, maxDegree);

            using StreamWriter writer = new(fileName);
            
            writer.WriteLine(verticesCount);
            
            StringBuilder matrixBuilder = new();

            for (int i = 0; i < verticesCount; i++)
            {
                int[] adjacentVertices = new int[verticesCount];

                for (int j = 0; j < verticesCount; j++)
                {
                    if (graph.Vertices[i].AdjacentVertices.Contains(graph.Vertices[j]))
                    {
                        adjacentVertices[j] = 1;
                    }
                    else
                    {
                        adjacentVertices[j] = 0;
                    }
                }

                matrixBuilder.AppendLine(string.Join(' ', adjacentVertices));
            }

            writer.WriteLine(matrixBuilder);

            return graph;
        }
    }
}
