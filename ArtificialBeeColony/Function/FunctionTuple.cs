using System;
using ArtificialBeeColony;

using static System.Math;

namespace FunctionOptimization
{
    public class FunctionTuple : IOptimizable
    {
        public double X { get; }
        public double Y { get; }
        public double TotalCost => Sqrt(X * X + Y * Y) + 3 * Cos(Sqrt(X * X + Y * Y));

        public FunctionTuple(double x, double y) => (X, Y) = (x, y);
     
        public bool BetterThan(IOptimizable other)
        {
            if (other is null) throw new ArgumentNullException(nameof(other));

            if (other is not FunctionTuple) throw new InvalidCastException();

            return this.TotalCost > other.TotalCost;
        }

        public int CompareTo(IOptimizable other)
        {
            if (other is null) throw new ArgumentNullException(nameof(other));

            if (other is not FunctionTuple) throw new InvalidCastException();

            return (int)(other.TotalCost - this.TotalCost);
        }

        public override string ToString()
        {
            return $"x=={Math.Round(X, 2)} y=={Math.Round(X, 2)} f={Math.Round(TotalCost, 2)}";
        }
    }
}