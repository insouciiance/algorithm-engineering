using System;
using System.Linq;
using TravelingSalesman.Services;

namespace TravelingSalesman.ABC
{
    public class Hive
    {
        private static readonly Random Random = new();

        public const int ScoutBeesCount = 5;
        public const int ActiveBeesCount = 50;
        public const int NectarSourcesCount = 15;
        public const int IterationsCount = 300;
        public const double MistakeProbability = 0.05d;
        public const double PersuasionProbability = 0.9d;

        public Graph Graph { get; }

        public Hive(Graph graph)
        {
            Graph = graph;
        }

        public Route Solve(bool logResults = false)
        {
            Route[] nectarSources = new Route[NectarSourcesCount];

            for(int i = 0; i < NectarSourcesCount; i++)
            {
                nectarSources[i] = RouteGenerator.GenerateRandomRoute(Graph);
            }

            int maxPossibleScoutsCount = Math.Min(ScoutBeesCount, NectarSourcesCount);
            
            ScoutBee[] scoutBees;
            ActiveBee[] activeBees = new ActiveBee[ActiveBeesCount];

            Route bestRoute = null;

            for (int i = 1; i <= IterationsCount; i++)
            {
                RunScoutPhase();
                DoWaggleDance();
                RunActivePhase();

                foreach (ScoutBee scout in scoutBees)
                {
                    if (scout.Route.TotalCost < nectarSources[scout.NectarSourceId].TotalCost)
                    {
                        double randomProbability = Random.NextDouble();

                        if (randomProbability < PersuasionProbability)
                        {
                            nectarSources[scout.NectarSourceId] = scout.Route;
                        }
                    }
                }

                bestRoute = (from nectarSource in nectarSources
                    orderby nectarSource.TotalCost
                    select nectarSource).First();

                if (logResults && (i % 5 == 0 || i == 1))
                {
                    Console.WriteLine($"Iteration: {i, -3} cost: {bestRoute.TotalCost}");
                }
            }

            return bestRoute;

            void RunScoutPhase()
            {
                scoutBees = new ScoutBee[maxPossibleScoutsCount];

                for(int i = 0; i < maxPossibleScoutsCount; i++)
                {
                    Route nectarSource;
                    int randomSourceIndex;
                    do
                    {
                        randomSourceIndex = Random.Next(0, NectarSourcesCount);
                        nectarSource = nectarSources[randomSourceIndex];
                    } while(scoutBees.Any(s => s is not null && nectarSource.Equals(s.Route)));

                    scoutBees[i] = new ScoutBee(nectarSource, randomSourceIndex);
                }
            }

            void DoWaggleDance()
            {
                double nectarSum = scoutBees.Sum(s => 1d / s.Route.TotalCost);

                for(int i = 0; i < ActiveBeesCount; i++)
                {
                    double randomNectar = Random.NextDouble() * nectarSum;
                    double currentNectarSum = 0;
                    
                    foreach(ScoutBee scout in scoutBees)
                    {
                        double currentNectar = 1d / scout.Route.TotalCost;
                        currentNectarSum += currentNectar;

                        if (randomNectar < currentNectarSum)
                        {
                            activeBees[i] = new ActiveBee(scout);
                            break;
                        }
                    }
                }
            }

            void RunActivePhase()
            {
                foreach(ActiveBee active in activeBees)
                {
                    Route adjacentRoute = active.GenerateAdjacentRoute();

                    double randomProbability = Random.NextDouble();

                    if (adjacentRoute.TotalCost < active.Initiator.Route.TotalCost && randomProbability > MistakeProbability)
                    {
                        active.Initiator.Route = adjacentRoute;
                    }

                    if (adjacentRoute.TotalCost >= active.Initiator.Route.TotalCost && randomProbability < MistakeProbability)
                    {
                        active.Initiator.Route = adjacentRoute;
                    }
                }
            }
        }
    }
}