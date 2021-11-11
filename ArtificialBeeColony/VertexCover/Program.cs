using ArtificialBeeColony.Core;
using VertexCover.Services;

namespace VertexCover
{
    public class Program
    {
        public static void Main()
        {
            Graph graph = GraphGenerator.Generate(150, 1, 5);

            Hive<VertexCover, Graph> hive = new(
                graph, 
                VertexCoverGenerator.GenerateRandomCover, 
                VertexCoverGenerator.GenerateAdjacentCover);

            hive.Solve(true);
        }
    }
}