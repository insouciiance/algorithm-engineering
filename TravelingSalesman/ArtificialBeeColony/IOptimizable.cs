namespace ArtificialBeeColony
{
    public interface IOptimizable
    {
        double TotalCost { get; }
        bool IsAscending { get; }
    }
}
