using System;

namespace TravelingSalesman.Services
{
    public static class GraphGenerator
    {
        private static readonly Random Random = new();

        public static Graph GenerateFullGraph(int verticesCount, int minWeight, int maxWeight)
        {
            Vertex[] vertices = new Vertex[verticesCount];

            for(int i = 0; i < verticesCount; i++)
            {
                vertices[i] = new Vertex(i);
            }

            for(int i = 0; i < verticesCount; i++)
            {
                for(int j = i + 1; j < verticesCount; j++)
                {
                    int randomWeight = Random.Next(minWeight, maxWeight);

                    Vertex firstVertex = vertices[i];
                    Vertex secondVertex = vertices[j];

                    Edge edge = new(randomWeight, firstVertex, secondVertex);

                    firstVertex.Edges.Add(edge);
                    secondVertex.Edges.Add(edge);
                }
            }

            Graph graph = new(vertices);
            
            return graph;
        }

        public static Graph GenerateFullGraph(int verticesCount, Func<Vertex, Vertex, int> weightFunc)
        {
            if (weightFunc is null)
            {
                throw new ArgumentNullException(nameof(weightFunc));
            }

            Vertex[] vertices = new Vertex[verticesCount];

            for(int i = 0; i < verticesCount; i++)
            {
                vertices[i] = new Vertex(i);
            }

            for(int i = 0; i < verticesCount; i++)
            {
                for(int j = i + 1; j < verticesCount; j++)
                {
                    Vertex firstVertex = vertices[i];
                    Vertex secondVertex = vertices[j];

                    int weight = weightFunc(firstVertex, secondVertex);

                    Edge edge = new(weight, firstVertex, secondVertex);

                    firstVertex.Edges.Add(edge);
                    secondVertex.Edges.Add(edge);
                }
            }

            Graph graph = new(vertices);
            
            return graph;
        }
    } 
}