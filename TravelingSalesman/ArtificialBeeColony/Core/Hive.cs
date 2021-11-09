using System;
using System.Linq;

namespace ArtificialBeeColony.Core
{
    public class Hive<T> where T : IOptimizable
    {
        private static readonly Random Random = new();

        public const int ScoutBeesCount = 5;
        public const int ActiveBeesCount = 50;
        public const int NectarSourcesCount = 15;
        public const int IterationsCount = 300;
        public const double MistakeProbability = 0.05d;
        public const double PersuasionProbability = 0.9d;

        public Graph Graph { get; }

        public Func<Graph, T> InitialSourceGenerator { get; }
        public Func<T, T> AdjacentSourceGenerator { get; }

        public Hive(Graph graph, Func<Graph, T> initialSourceGenerator, Func<T, T> adjacentSourceGenerator)
        {
            Graph = graph;
            InitialSourceGenerator = initialSourceGenerator;
            AdjacentSourceGenerator = adjacentSourceGenerator;
        }

        public T Solve(bool logResults = false)
        {
            T[] nectarSources = new T[NectarSourcesCount];

            for(int i = 0; i < NectarSourcesCount; i++)
            {
                nectarSources[i] = InitialSourceGenerator(Graph);
            }

            int maxPossibleScoutsCount = Math.Min(ScoutBeesCount, NectarSourcesCount);
            
            ScoutBee<T>[] scoutBees;
            ActiveBee<T>[] activeBees = new ActiveBee<T>[ActiveBeesCount];

            T bestSource = default;

            for (int i = 1; i <= IterationsCount; i++)
            {
                RunScoutPhase();
                DoWaggleDance();
                RunActivePhase();

                foreach (ScoutBee<T> scout in scoutBees)
                {
                    if (scout.NectarSource.TotalCost < nectarSources[scout.NectarSourceId].TotalCost)
                    {
                        double randomProbability = Random.NextDouble();

                        if (randomProbability < PersuasionProbability)
                        {
                            nectarSources[scout.NectarSourceId] = scout.NectarSource;
                        }
                    }
                }

                bestSource = (from nectarSource in nectarSources
                    orderby nectarSource.TotalCost
                    select nectarSource).First();

                if (logResults && (i % 5 == 0 || i == 1))
                {
                    Console.WriteLine($"Iteration: {i, -3} cost: {bestSource.TotalCost}");
                }
            }

            return bestSource;

            void RunScoutPhase()
            {
                scoutBees = new ScoutBee<T>[maxPossibleScoutsCount];

                for(int i = 0; i < maxPossibleScoutsCount; i++)
                {
                    T nectarSource;
                    int randomSourceIndex;
                    do
                    {
                        randomSourceIndex = Random.Next(0, NectarSourcesCount);
                        nectarSource = nectarSources[randomSourceIndex];
                    } while(scoutBees.Any(s => s is not null && nectarSource.Equals(s.NectarSource)));

                    scoutBees[i] = new ScoutBee<T>(nectarSource, randomSourceIndex);
                }
            }

            void DoWaggleDance()
            {
                double nectarSum = scoutBees.Sum(s => 1d / s.NectarSource.TotalCost);

                for(int i = 0; i < ActiveBeesCount; i++)
                {
                    double randomNectar = Random.NextDouble() * nectarSum;
                    double currentNectarSum = 0;
                    
                    foreach(ScoutBee<T> scout in scoutBees)
                    {
                        double currentNectar = 1d / scout.NectarSource.TotalCost;
                        currentNectarSum += currentNectar;

                        if (randomNectar < currentNectarSum)
                        {
                            activeBees[i] = new ActiveBee<T>(scout, AdjacentSourceGenerator);
                            break;
                        }
                    }
                }
            }

            void RunActivePhase()
            {
                foreach(ActiveBee<T> active in activeBees)
                {
                    T adjacentRoute = active.AdjacentSourceGenerator.Invoke(active.NectarSource);

                    double randomProbability = Random.NextDouble();

                    if (adjacentRoute.TotalCost < active.Initiator.NectarSource.TotalCost && randomProbability > MistakeProbability)
                    {
                        active.Initiator.NectarSource = adjacentRoute;
                    }

                    if (adjacentRoute.TotalCost >= active.Initiator.NectarSource.TotalCost && randomProbability < MistakeProbability)
                    {
                        active.Initiator.NectarSource = adjacentRoute;
                    }
                }
            }
        }
    }
}