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

            System.Console.WriteLine(graph);
            return new VertexCover(sources);

            bool EnoughVerticesToCover()
            {
                List<Edge> coveredEdges = new();

                foreach(Vertex vertex in sources)
                {
                    foreach(Edge edge in vertex.AdjacentEdges)
                    {
                        if (!coveredEdges.Contains(edge))
                        {
                            coveredEdges.Add(edge);
                        }
                    }
                }

                return graph.EdgesCount == coveredEdges.Count;
            }
        }

        public static VertexCover GenerateAdjacentCover(Graph graph, VertexCover current)
        {
            List<Vertex> newVertices = new(current.Vertices);

            OptimizeVertexCover();

            return new VertexCover(newVertices);

            void OptimizeVertexCover()
            {
                int randomVertexIndex = Random.Next(0, current.Vertices.Length);
                Vertex currentVertex = newVertices[randomVertexIndex];

                newVertices.Remove(currentVertex);

                if (!EnoughVerticesToCover())
                {
                    newVertices.Add(currentVertex);
                }
            }

            bool EnoughVerticesToCover()
            {
                List<Edge> coveredEdges = new();

                foreach(Vertex vertex in newVertices)
                {
                    foreach(Edge edge in vertex.AdjacentEdges)
                    {
                        if (!coveredEdges.Contains(edge))
                        {
                            coveredEdges.Add(edge);
                        }
                    }
                }

                return graph.EdgesCount == coveredEdges.Count;
            }
        }
    }
}