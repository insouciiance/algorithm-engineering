using System;

namespace ArtificialBeeColony
{
    public interface IOptimizable : IComparable<IOptimizable>
    {
        double TotalCost { get; }
        bool BetterThan(IOptimizable other);
    }
}
