using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VertexCover.Services
{
    public static class GraphGenerator
    {
        private static readonly Random Random = new();

        public static Graph Generate(int verticesCount, int minDegree, int maxDegree)
        {
            List<Vertex> vertices = new();

            do
            {
                vertices.Clear();

                for (int i = 0; i < verticesCount; i++)
                {
                    vertices.Add(new Vertex(i));
                }

                for (int i = 0; i < verticesCount; i++)
                {
                    Vertex currentVertex = vertices[i];

                    int upperBound = currentVertex.Degree > maxDegree ? 0 : maxDegree - currentVertex.Degree;
                    int lowerBound = currentVertex.Degree < minDegree ? minDegree - currentVertex.Degree : 0;

                    int adjacentVerticesCount = Random.Next(lowerBound, upperBound + 1);

                    for (int j = 0; j < adjacentVerticesCount; j++)
                    {
                        Vertex adjacentVertex;

                        do
                        {
                            int randomAdjacentVertexIndex = Random.Next(0, verticesCount);
                            adjacentVertex = vertices[randomAdjacentVertexIndex];
                        } while (currentVertex.Equals(adjacentVertex) 
                                 || currentVertex.AdjacentVertices.Contains(adjacentVertex) 
                                 || adjacentVertex.Degree >= maxDegree);

                        currentVertex.AdjacentVertices.Add(adjacentVertex);
                        adjacentVertex.AdjacentVertices.Add(currentVertex);
                    }
                }
            } while (vertices.Any(v => v.Degree < minDegree || v.Degree > maxDegree));

            return new Graph(vertices);
        }
    }
}