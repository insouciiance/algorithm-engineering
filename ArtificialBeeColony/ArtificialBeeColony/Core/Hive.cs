using System;
using System.Linq;

namespace ArtificialBeeColony.Core
{
    public class Hive<T, R> where T : IOptimizable
    {
        private static readonly Random Random = new();

        public const int ScoutBeesCount = 5;
        public const int ActiveBeesCount = 50;
        public const int NectarSourcesCount = 500;
        public const int IterationsCount = 10000;
        public const double MistakeProbability = 0.05d;
        public const double PersuasionProbability = 0.9d;

        public R Seed { get; }

        public Func<R, T> InitialSourceGenerator { get; }
        public Func<R, T, T> AdjacentSourceGenerator { get; }

        public Hive(R seed, Func<R, T> initialSourceGenerator, Func<R, T, T> adjacentSourceGenerator)
        {
            Seed = seed;
            InitialSourceGenerator = initialSourceGenerator;
            AdjacentSourceGenerator = adjacentSourceGenerator;
        }

        public T Solve(bool sortAscending = true, bool logResults = false)
        {
            T[] nectarSources = new T[NectarSourcesCount];

            for(int i = 0; i < NectarSourcesCount; i++)
            {
                nectarSources[i] = InitialSourceGenerator(Seed);
            }

            Array.Sort(nectarSources);

            int maxPossibleScoutsCount = Math.Min(ScoutBeesCount, NectarSourcesCount);
            
            ScoutBee<T>[] scoutBees;
            ActiveBee<R, T>[] activeBees = new ActiveBee<R, T>[ActiveBeesCount];

            T bestSource = default;

            for (int i = 1; i <= IterationsCount; i++)
            {
                RunScoutPhase();
                DoWaggleDance();
                RunActivePhase();

                foreach (ScoutBee<T> scout in scoutBees)
                {
                    if (scout.NectarSource.BetterThan(nectarSources[scout.NectarSourceId]))
                    {
                        double randomProbability = Random.NextDouble();

                        if (randomProbability < PersuasionProbability)
                        {
                            nectarSources[scout.NectarSourceId] = scout.NectarSource;
                        }
                    }
                }

                Array.Sort(nectarSources);

                bestSource = nectarSources[0];

                if (logResults && (i % 100 == 0 || i == 1))
                {
                    Console.WriteLine($"Iteration: {i, -3} {bestSource}");
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
                double nectarSum = scoutBees.Sum(s => sortAscending ? s.NectarSource.TotalCost : 1d / s.NectarSource.TotalCost);

                for(int i = 0; i < ActiveBeesCount; i++)
                {
                    double randomNectar = Random.NextDouble() * nectarSum;
                    double currentNectarSum = 0;
                    
                    foreach(ScoutBee<T> scout in scoutBees)
                    {
                        double currentNectar = sortAscending ? scout.NectarSource.TotalCost : 1d / scout.NectarSource.TotalCost;
                        currentNectarSum += currentNectar;

                        if (randomNectar < currentNectarSum)
                        {
                            activeBees[i] = new ActiveBee<R, T>(scout, AdjacentSourceGenerator);
                            break;
                        }
                    }
                }
            }

            void RunActivePhase()
            {
                foreach(ActiveBee<R, T> active in activeBees)
                {
                    T adjacentRoute = active.AdjacentSourceGenerator.Invoke(Seed, active.NectarSource);

                    double randomProbability = Random.NextDouble();

                    if (adjacentRoute.BetterThan(active.Initiator.NectarSource) && randomProbability > MistakeProbability)
                    {
                        active.Initiator.NectarSource = adjacentRoute;
                    }

                    if (!adjacentRoute.BetterThan(active.Initiator.NectarSource) && randomProbability < MistakeProbability)
                    {
                        active.Initiator.NectarSource = adjacentRoute;
                    }
                }
            }
        }
    }
}