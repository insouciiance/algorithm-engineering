using System;

namespace TravelingSalesman.ABC
{
    public class ActiveBee : Bee
    {
        private static readonly Random Random = new(); 

        public sealed override Route Route { get; set; }

        public ScoutBee Initiator { get; set; }

        public ActiveBee(ScoutBee initiator)
        {
            Initiator = initiator;
            Route = initiator.Route;
        }

        public Route GenerateAdjacentRoute()
        {
            if (Route.Vertices.Length < 3)
            {
                return Route;
            }

            Vertex[] vertices = new Vertex[Route.Vertices.Length];

            Route.Vertices.CopyTo(vertices, 0);

            int firstSwapIndex = Random.Next(0, vertices.Length);

            int secondSwapIndex;
            do
            {
                secondSwapIndex = Random.Next(0, vertices.Length);
            } while (firstSwapIndex == secondSwapIndex);

            Vertex temp = vertices[firstSwapIndex];
            vertices[firstSwapIndex] = vertices[secondSwapIndex];
            vertices[secondSwapIndex] = temp;

            return new Route(vertices);
        }
    }
}