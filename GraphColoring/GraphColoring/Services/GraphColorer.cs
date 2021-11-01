using System;
using System.Collections.Generic;
using GraphColoring.Data;

namespace GraphColoring.Services
{
    public class GraphColorer
    {
        private static readonly Random Random = new ();

        public Graph Graph { get; }

        private int _employedBeesCount = 1;
        private int _onlookerBeesCount = 4;

        public GraphColorer(Graph graph)
        {
            Graph = graph;
        }

        public ColoredGraph Color()
        {
            ColoredGraph coloredGraph = new(Graph);
            List<ColoredVertex> closedVertices = new();
            List<ColoredVertex> nectarSources = new();

            RunEmployedPhase();

            foreach(ColoredVertex vertex in nectarSources)
            {
                Console.WriteLine(vertex);
            }

            return coloredGraph;

            void RunEmployedPhase()
            {
                for(int i = 0; i < _employedBeesCount; i++)
                {
                    if (nectarSources.Count + closedVertices.Count >= coloredGraph.VerticesCount)
                    {
                        break;
                    }

                    while(true)
                    {
                        int randomVertexIndex = Random.Next(0, coloredGraph.VerticesCount);
                        ColoredVertex randomVertex = coloredGraph.Vertices[randomVertexIndex];
                        
                        if (!nectarSources.Contains(randomVertex) &&
                            !closedVertices.Contains(randomVertex))
                        {
                            nectarSources.Add(randomVertex);
                            break;
                        }
                    }
                }
            }

            void RunOnlookerBeePhase()
            {
                for (int i = 0; i < _onlookerBeesCount; i++)
                {
                    
                }
            }
        }
    }
}