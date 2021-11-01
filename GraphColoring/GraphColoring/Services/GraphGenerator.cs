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

            do
            {
                vertices.Clear();

                for (int i = 0; i < verticesCount; i++)
                {
                    vertices.Add(new ColoredVertex(i));
                }

                for (int i = 0; i < verticesCount - 1; i++)
                {
                    ColoredVertex currentVertex = vertices[i];

                    int upperBound = currentVertex.Degree > maxDegree ? 0 : maxDegree - currentVertex.Degree;
                    int lowerBound = currentVertex.Degree < minDegree ? minDegree - currentVertex.Degree : 0;

                    int adjacentVerticesCount = Random.Next(lowerBound, upperBound + 1);

                    for (int j = 0; j < adjacentVerticesCount; j++)
                    {
                        ColoredVertex adjacentVertex;

                        do
                        {
                            int randomAdjacentVertexIndex = Random.Next(0, verticesCount);
                            adjacentVertex = vertices[randomAdjacentVertexIndex];
                        } while (currentVertex.Equals(adjacentVertex) || currentVertex.AdjacentVertices.Contains(adjacentVertex) || adjacentVertex.Degree >= maxDegree);

                        currentVertex.AdjacentVertices.Add(adjacentVertex);
                        adjacentVertex.AdjacentVertices.Add(currentVertex);
                    }
                }
            } while (vertices.Any(v => v.Degree < minDegree || v.Degree > maxDegree));

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
