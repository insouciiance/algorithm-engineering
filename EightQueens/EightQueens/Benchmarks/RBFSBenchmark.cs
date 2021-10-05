using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EightQueens.Heuristics;
using EightQueens.Tree;

namespace EightQueens.Benchmarks
{
    public class RBFSBenchmark<T> : IBenchmark<T>
    {
        public T Root { get; }
        public IHeuristic<T> Heuristic { get; }

        public RBFSBenchmark(T root, IHeuristic<T> heuristic)
        {
            Root = root;
            Heuristic = heuristic;
        }

        public BenchmarkResult<T> Run(ITreeNode<T> rootNode)
        {
            List<ITreeNode<T>> visitedNodes = new ();
            ITreeNode<T> root = rootNode;

            int iterationsCount = 0;
            int nodesGenerated = 0;

            (ITreeNode<T> result, _) = Traverse(root, int.MaxValue);

            return new RBFSBenchmarkResult<T>(result, iterationsCount, nodesGenerated);

            (ITreeNode<T> result, int resultHeuristic) Traverse(ITreeNode<T> node, int fLimit)
            {
                iterationsCount++;

                if (!visitedNodes.Contains(node))
                {
                    visitedNodes.Add(node);
                }

                if (node.IsNodeCompliant())
                {
                    return (node, 0);
                }

                (ITreeNode<T> child, int heuristic)[] children = node
                    .GetChildren()
                    .Where(n => !visitedNodes.Any(visitedNode => n.State.Equals(visitedNode.State)))
                    .OrderBy(n => Math.Max(Heuristic.Evaluate(n.State), Heuristic.Evaluate(node.State)))
                    .Select(child => (child, Math.Max(Heuristic.Evaluate(child.State), Heuristic.Evaluate(node.State))))
                    .Take(2)
                    .ToArray();

                nodesGenerated += children.Length;

                while (true)
                {
                    children = children
                        .OrderBy(child => child.heuristic)
                        .ToArray();

                    (ITreeNode<T> best, int bestHeuristic) = children[0];

                    if (bestHeuristic > fLimit)
                    {
                        return (null, bestHeuristic);
                    }

                    int alternative = children[1].heuristic;

                    (ITreeNode<T> result, int resultHeuristic) =
                        Traverse(best, Math.Min(alternative, fLimit));

                    children[0] = (best, resultHeuristic);

                    if (result is not null)
                    {
                        return (result, 0);
                    }
                }
            }
        }
    }
}
