using System;

namespace ArtificialBeeColony.Core
{
    public class ActiveBee<R, T> : Bee<T> where T : IOptimizable
    {
        public sealed override T NectarSource { get; set; }

        public ScoutBee<T> Initiator { get; set; }

        public ActiveBee(ScoutBee<T> initiator, Func<R, T, T> adjacentSourceGenerator)
        {
            Initiator = initiator;
            NectarSource = initiator.NectarSource;
            AdjacentSourceGenerator = adjacentSourceGenerator;
        }

        public Func<R, T, T> AdjacentSourceGenerator { get; }
    }
}