using ArtificialBeeColony.Core;

namespace Function
{
    public class Program
    {
        public static void Main()
        {
            FunctionDomain domain = new FunctionDomain(-50, 50, -50, 50);

            Hive<FunctionTuple, FunctionDomain> hive = new Hive<FunctionTuple, FunctionDomain>(
                domain,
                TupleGenerator.GenerateRandomTuple,
                TupleGenerator.GenerateAdjacentTuple);

            hive.Solve(true);
        }
    }
} 