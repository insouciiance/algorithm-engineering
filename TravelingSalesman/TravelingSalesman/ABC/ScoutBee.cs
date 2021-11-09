namespace TravelingSalesman.ABC
{
    public class ScoutBee : Bee
    {
        public int NectarSourceId { get; }

        public sealed override Route Route { get; set; }

        public ScoutBee(Route route, int nectarSourceId)
        {
            NectarSourceId = nectarSourceId;
            Route = route;
        }
    }
}