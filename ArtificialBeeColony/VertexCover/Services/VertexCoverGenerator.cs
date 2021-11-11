using System;
using System.Linq;
using System.Collections.Generic;

namespace VertexCover.Services
{
    public static class VertexCoverGenerator
    {
        private static Random Random = new ();

        public static VertexCover GenerateRandomCover(Graph graph)
        {
            List<Vertex> sources = new();

            while(!EnoughVerticesToCover())
            {
                int randomVertexIndex;

                do
                {
                    randomVertexIndex = Random.Next(0, graph.VerticesCount);
                }while(sources.Contains(graph.Vertices[randomVertexIndex]));

                sources.Add(graph.Vertices[randomVertexIndex]);
            }

            return new VertexCover(sources);

            bool EnoughVerticesToCover()
            {
                int verticesCount = graph.VerticesCount;

                List<Vertex> coveredVertices = new();

                foreach(Vertex vertex in sources)
                {
                    if (!coveredVertices.Contains(vertex))
                    {
                        coveredVertices.Add(vertex);
                    }

                    foreach(Vertex adjacentVertex in vertex.AdjacentVertices)
                    {
                        if (!coveredVertices.Contains(adjacentVertex))
                        {
                            coveredVertices.Add(adjacentVertex);
                        }
                    }
                }

                return verticesCount == coveredVertices.Count;
            }
        }

        public static VertexCover GenerateAdjacentCover(Graph graph, VertexCover current)
        {
            int vertexToDeleteIndex = Random.Next(0, current.Vertices.Length);
            int vertexToAddIndex = Random.Next(0, graph.VerticesCount);

            List<Vertex> newVertices = new();

            newVertices.AddRange(current.Vertices.Where(v => v.Index != vertexToDeleteIndex));
            newVertices.Add(graph.Vertices.First(v => v.Index == vertexToAddIndex));

            OptimizeVertexCover();

            return new VertexCover(newVertices);

            void OptimizeVertexCover()
            {
                bool optimized;

                do {
                    optimized = false;

                    for(int i = 0; i < newVertices.Count; i++)
                    {
                        Vertex currentVertex = newVertices[i];

                        newVertices.Remove(currentVertex);

                        if (!EnoughVerticesToCover())
                        {
                            newVertices.Add(currentVertex);
                            continue;
                        }

                        optimized = true;
                    }
                }while(optimized);
            }

            bool EnoughVerticesToCover()
            {
                int verticesCount = graph.VerticesCount;

                List<Vertex> coveredVertices = new();

                foreach(Vertex vertex in newVertices)
                {
                    if (!coveredVertices.Contains(vertex))
                    {
                        coveredVertices.Add(vertex);
                    }

                    foreach(Vertex adjacentVertex in vertex.AdjacentVertices)
                    {
                        if (!coveredVertices.Contains(adjacentVertex))
                        {
                            coveredVertices.Add(adjacentVertex);
                        }
                    }
                }
                
                return verticesCount == coveredVertices.Count;
            }
        }
    }
}