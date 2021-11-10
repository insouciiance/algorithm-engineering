using System;

namespace Function
{
    public static class TupleGenerator
    {
        private static readonly Random Random = new();

        public static FunctionTuple GenerateRandomTuple(FunctionDomain domain)
        {
            double randomX = Random.NextDouble() * (domain.MaxX - domain.MinX) + domain.MinX;
            double randomY = Random.NextDouble() * (domain.MaxY - domain.MinY) + domain.MinY;

            return new FunctionTuple(randomX, randomY);
        }

        public static FunctionTuple GenerateAdjacentTuple(FunctionDomain domain, FunctionTuple tuple)
        {
            double randomDeltaX = Random.NextDouble() - 0.5d;
            double randomDeltaY = Random.NextDouble() - 0.5d;
            double newX = tuple.X + randomDeltaX;
            double newY = tuple.Y + randomDeltaY;
    
            if (newX < domain.MinX)
            {
                newX = domain.MinX;
            }

            if (newX > domain.MaxX)
            {
                newX = domain.MaxX;
            }

            if (newY < domain.MinY)
            {
                newY = domain.MinY;
            }

            if (newY > domain.MaxY)
            {
                newY = domain.MaxY;
            }

            return new FunctionTuple(newX, newY);
        }
    }
}