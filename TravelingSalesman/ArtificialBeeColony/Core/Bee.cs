namespace ArtificialBeeColony.Core
{
    public abstract class Bee<T> where T : IOptimizable
    {
        public abstract T NectarSource { get; set; }
    }
}