using System;
using ArtificialBeeColony;

namespace Function
{
    public class FunctionTuple : IOptimizable
    {
        public double X { get; }
        public double Y { get; }
        public double TotalCost => Math.Abs(Math.Sin(X) + Math.Sin(Y)) / (Math.Abs(X) + Math.Abs(Y) + 1) * 10;

        public FunctionTuple(double x, double y) => (X, Y) = (x, y);
     
        public bool BetterThan(IOptimizable other)
        {
            if (other is null) throw new ArgumentNullException();

            if (other is not FunctionTuple otherTuple) throw new InvalidCastException();

            return this.TotalCost > otherTuple.TotalCost;
        }

        public int CompareTo(IOptimizable other)
        {
            if (other is null) throw new ArgumentNullException();

            if (other is not FunctionTuple otherTuple) throw new InvalidCastException();

            return (int)(otherTuple.TotalCost - this.TotalCost);
        }

        public override string ToString()
        {
            return $"x=={Math.Round(X, 2)} y=={Math.Round(X, 2)} f={Math.Round(TotalCost, 2)}";
        }
    }
}