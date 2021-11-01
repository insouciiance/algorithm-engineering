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

        private int _employedBeesCount = 5;
        private int _onlookerBeesCount = 55;

        public GraphColorer(ColoredGraph graph)
        {
            ColoredGraph = (ColoredGraph)graph.Clone();
        }

        public ColoredGraph Color(int iterationsCount = 1)
        {
            List<ColoredVertex> closedVertices = new();
            List<EmployedBee> dancingBees = new();

            ColoredGraph graph = (ColoredGraph)ColoredGraph.Clone();

            for (int i = 0; i < iterationsCount; i++)
            {
                while (graph.ChromaticNumber < 0)
                {
                    dancingBees.Clear();
                    closedVertices.Clear();

                    RunEmployedPhase();
                    RunOnlookerPhase();
                    RunScoutPhase();
                }

                if (graph.ChromaticNumber < ColoredGraph.ChromaticNumber || ColoredGraph.ChromaticNumber < 0)
                {
                    ColoredGraph = graph;
                }
            }

            return ColoredGraph;

            void RunEmployedPhase()
            {
                for (int i = 0; i < _employedBeesCount; i++)
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

                for (int i = 0; i < _onlookerBeesCount; i++)
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
                                .First(v => !onlookers.Any(o => o.Vertex.Equals(employedBee.Vertex) && o.AdjacentVertex.Equals(v)));

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

                foreach (EmployedBee dancingBee in dancingBees)
                {
                    ColoredVertex foodSource = dancingBee.Vertex;

                    List<int> forbiddenColors = foodSource.ForbiddenColors;

                    int newColor = 0;

                    while (forbiddenColors.Contains(newColor))
                    {
                        newColor++;
                    }

                    foodSource.Color = newColor;
                }
            }

            void RunScoutPhase()
            {
                foreach (EmployedBee employedBee in dancingBees)
                {
                    if (employedBee.Vertex.UncoloredDegree != 0) continue;

                    _employedBeesCount++;

                    if (!closedVertices.Contains(employedBee.Vertex))
                    {
                        closedVertices.Add(employedBee.Vertex);
                    }
                }
            }
        }
    }
}