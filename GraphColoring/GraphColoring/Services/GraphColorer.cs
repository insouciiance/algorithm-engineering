using System;
using System.Collections.Generic;
using System.Linq;
using GraphColoring.Data;
using GraphColoring.Data.ABC;

namespace GraphColoring.Services
{
    public class GraphColorer
    {
        private static readonly Random Random = new();

        public ColoredGraph ColoredGraph;

        public GraphColorer(ColoredGraph graph)
        {
            ColoredGraph = new ColoredGraph(graph.AdjacencyMatrix);
        }

        public ColoredGraph Color()
        {
            List<ColoredVertex> closedVertices = new();
            List<EmployedBee> dancingBees = new();

            ColoredGraph graph = new (ColoredGraph.AdjacencyMatrix);
            int employedBeesCount = 5;
            int onlookerBeesCount = 55;

            do
            {
                RunEmployedPhase();
                RunOnlookerPhase();
                RunScoutPhase();
            } while (graph.ChromaticNumber < 0);

            ColoredGraph = graph;

            return ColoredGraph;

            void RunEmployedPhase()
            {
                for (int i = 0; i < employedBeesCount; i++)
                {
                    if (dancingBees.Count + closedVertices.Count >= graph.VerticesCount)
                    {
                        break;
                    }

                    while (true)
                    {
                        int randomVertexIndex = Random.Next(0, graph.VerticesCount);
                        ColoredVertex randomVertex = graph.Vertices[randomVertexIndex];

                        EmployedBee employedBee = new(randomVertex);

                        if (!dancingBees.Contains(employedBee) &&
                            !closedVertices.Contains(randomVertex))
                        {
                            dancingBees.Add(employedBee);
                            break;
                        }
                    }
                }
            }

            void RunOnlookerPhase()
            {
                List<OnlookerBee> onlookers = new();

                int nectarAmount = dancingBees.Sum(b => b.Nectar);

                for (int i = 0; i < onlookerBeesCount; i++)
                {
                    if (onlookers.Count >= dancingBees.Select(bee => bee.Vertex).Sum(v => v.UncoloredDegree))
                    {
                        break;
                    }

                    bool isOnlookerPlaced = false;

                    while (!isOnlookerPlaced)
                    {
                        int randomNectar = Random.Next(0, nectarAmount + 1);

                        int currentNectar = 0;

                        foreach (EmployedBee employedBee in dancingBees)
                        {
                            if (randomNectar > employedBee.Nectar + currentNectar)
                            {
                                currentNectar += employedBee.Nectar;
                                continue;
                            }

                            if (employedBee.Vertex.UncoloredDegree <= onlookers.Count(o => o.Vertex.Equals(employedBee.Vertex))) break;

                            ColoredVertex adjacentVertex = employedBee.Vertex.AdjacentVertices
                                .First(v => !onlookers.Any(o => o.Vertex.Equals(employedBee.Vertex) && o.AdjacentVertex.Equals(v)) && v.Color is null);

                            OnlookerBee onlooker = new(employedBee.Vertex, adjacentVertex);
                            onlookers.Add(onlooker);
                            isOnlookerPlaced = true;
                            break;
                        }
                    }
                }

                foreach (OnlookerBee onlooker in onlookers)
                {
                    ColoredVertex uncoloredVertex = onlooker.AdjacentVertex;

                    List<int> forbiddenColors = uncoloredVertex.ForbiddenColors;

                    int newColor = 0;

                    while (forbiddenColors.Contains(newColor))
                    {
                        newColor++;
                    }

                    uncoloredVertex.Color = newColor;
                }
            }

            void RunScoutPhase()
            {
                foreach (EmployedBee employedBee in dancingBees)
                {
                    if (employedBee.Vertex.UncoloredDegree != 0) continue;

                    employedBeesCount++;

                    ColoredVertex foodSource = employedBee.Vertex;

                    List<int> forbiddenColors = foodSource.ForbiddenColors;

                    int newColor = 0;

                    while (forbiddenColors.Contains(newColor))
                    {
                        newColor++;
                    }

                    foodSource.Color = newColor;

                    if (!closedVertices.Contains(foodSource))
                    {
                        closedVertices.Add(foodSource);
                    }
                }

                dancingBees.Clear();
            }
        }
    }
}