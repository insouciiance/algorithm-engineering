namespace ArtificialBeeColony.Core
{
    public class ScoutBee<T> : Bee<T> where T : IOptimizable
    {
        public int NectarSourceId { get; }

        public sealed override T NectarSource { get; set; }

        public ScoutBee(T nectarSource, int nectarSourceId)
        {
            NectarSourceId = nectarSourceId;
            NectarSource = nectarSource;
        }
    }
}