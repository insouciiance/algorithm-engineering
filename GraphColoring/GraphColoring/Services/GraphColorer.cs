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
            List<OnlookerBee> onlookers = new();

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
                        return;
                    }

                    EmployedBee employedBee;
                    ColoredVertex randomVertex;
                    do
                    {
                        int randomVertexIndex = Random.Next(0, graph.VerticesCount);
                        randomVertex = graph.Vertices[randomVertexIndex];

                        employedBee = new(randomVertex);
                    } while(dancingBees.Contains(employedBee) ||
                            closedVertices.Contains(randomVertex));

                    dancingBees.Add(employedBee);
                }
            }

            void RunOnlookerPhase()
            {
                int maxOnlookersCount = dancingBees
                    .Select(bee => bee.Vertex)
                    .Sum(vertex => vertex.UncoloredDegree); 

                int totalNectar = dancingBees.Sum(b => b.Nectar);

                for (int i = 0; i < onlookerBeesCount; i++)
                {
                    if (onlookers.Count >= maxOnlookersCount)
                    {
                        break;
                    }

                    bool isOnlookerPlaced = false;

                    while (!isOnlookerPlaced)
                    {
                        int randomNectar = Random.Next(0, totalNectar + 1);
                        int currentNectar = 0;

                        foreach (EmployedBee employedBee in dancingBees)
                        {
                            if (randomNectar > employedBee.Nectar + currentNectar)
                            {
                                currentNectar += employedBee.Nectar;
                                continue;
                            }

                            int sourceMaxOnlookersCount = employedBee.Vertex.UncoloredDegree;
                            int sourceOnlookersCount = onlookers
                                .Count(o => o.Vertex.Equals(employedBee.Vertex));

                            if (sourceOnlookersCount >= sourceMaxOnlookersCount)
                            {
                                break;
                            }

                            ColoredVertex adjacentVertex = employedBee.Vertex.AdjacentVertices
                                .First(v => !onlookers
                                    .Any(o => o.Vertex
                                        .Equals(employedBee.Vertex) && 
                                        o.AdjacentVertex.Equals(v)) && 
                                    v.Color is null);

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
                    ColoredVertex foodSource = employedBee.Vertex;
                    OnlookerBee sourceBee = onlookers
                        .FirstOrDefault(onlooker => onlooker.Vertex.Equals(foodSource));

                    if (sourceBee is null || employedBee.Vertex.UncoloredDegree != 0)
                    {
                        continue;
                    }

                    employedBeesCount++;

                    List<int> forbiddenColors = foodSource.ForbiddenColors;

                    int newColor = 0;
                    while (forbiddenColors.Contains(newColor))
                    {
                        newColor++;
                    }

                    foodSource.Color = newColor;

                    closedVertices.Add(foodSource);
                }

                dancingBees.Clear();
                onlookers.Clear();
            }
        }
    }
}