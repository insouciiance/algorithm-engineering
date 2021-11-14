using System;
using ArtificialBeeColony;

namespace TravelingSalesman.Services
{
    public static class GraphGenerator
    {
        private static readonly Random Random = new();

        public static Graph GenerateFullGraph(int verticesCount, int minWeight, int maxWeight)
        {
            int[,] adjacencyMatrix = new int[verticesCount, verticesCount];

            for(int i = 0; i < verticesCount; i++)
            {
                for(int j = i + 1; j < verticesCount; j++)
                {
                    int randomWeight = Random.Next(minWeight, maxWeight + 1);
                    adjacencyMatrix[i, j] = randomWeight;
                    adjacencyMatrix[j, i] = randomWeight;
                }
            }
            
            Graph graph = new(adjacencyMatrix);

            return graph;
        }

        public static Graph GenerateFullGraph(int verticesCount, Func<int, int, int> weightFunc)
        {
            int[,] adjacencyMatrix = new int[verticesCount, verticesCount];

            for(int i = 0; i < verticesCount; i++)
            {
                for(int j = i + 1; j < verticesCount; j++)
                {
                    int randomWeight = weightFunc(i, j);
                    adjacencyMatrix[i, j] = randomWeight;
                    adjacencyMatrix[j, i] = randomWeight;
                }
            }
            
            Graph graph = new(adjacencyMatrix);

            return graph;
        }
    } 
}