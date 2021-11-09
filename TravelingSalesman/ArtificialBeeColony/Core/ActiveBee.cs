using System;

namespace ArtificialBeeColony.Core
{
    public class ActiveBee<T> : Bee<T> where T : IOptimizable
    {
        public sealed override T NectarSource { get; set; }

        public ScoutBee<T> Initiator { get; set; }

        public ActiveBee(ScoutBee<T> initiator, Func<T, T> adjacentSourceGenerator)
        {
            Initiator = initiator;
            NectarSource = initiator.NectarSource;
            AdjacentSourceGenerator = adjacentSourceGenerator;
        }

        public Func<T, T> AdjacentSourceGenerator { get; }
    }
}