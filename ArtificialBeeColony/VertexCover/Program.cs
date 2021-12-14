using ArtificialBeeColony.Core;
using VertexCover.Services;

namespace VertexCover
{
    public class Program
    {
        public static void Main()
        {
            Graph graph = GraphGenerator.Generate(300, 2, 30);

            Hive<VertexCover, Graph> hive = new(
                graph, 
                VertexCoverGenerator.GenerateRandomCover, 
                VertexCoverGenerator.GenerateAdjacentCover);

            hive.Solve(true);
        }
    }
}