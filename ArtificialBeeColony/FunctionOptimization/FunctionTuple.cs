using System;
using ArtificialBeeColony;

using static System.Math;

namespace FunctionOptimization
{
    public class FunctionTuple : IOptimizable
    {
        public double X { get; }
        public double Y { get; }
        public double TotalCost => Abs(Cos(Sqrt((X * X + Y * Y))) / (Sqrt(X * X + Y * Y) + 1)) * 10;

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

            return (other.TotalCost - this.TotalCost) >= 0 ? 1 : -1;
        }

        public override string ToString()
        {
            return $"x=={Math.Round(X, 2)} y=={Math.Round(Y, 2)} f={Math.Round(TotalCost, 2)}";
        }
    }
}