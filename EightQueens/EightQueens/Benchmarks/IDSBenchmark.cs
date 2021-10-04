using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EightQueens.Models;
using EightQueens.Tree;

namespace EightQueens.Benchmarks
{
    public class IDSBenchmark<T> : IBenchmark<T>
    {
        public T Root { get; }

        public IDSBenchmark(T root)
        {
            Root = root;
        }

        public BenchmarkResult<T> Run(ITreeNode<T> rootNode)
        {
            int maxDepth = 0;

            while (true)
            {
                BenchmarkResult<T> solution = RunDFS(rootNode, maxDepth);

                if (solution.ResultNode is not null)
                {
                    return solution;
                }

                maxDepth++;
            }
        }

        private BenchmarkResult<T> RunDFS(ITreeNode<T> rootNode, int maxDepth)
        {
            ITreeNode<T> root = rootNode;

            int iterationsCount = 0;
            int stuckCount = 0;
            int nodesGenerated = 0;

            ITreeNode<T> result = Traverse(root);

            return new IDSBenchmarkResult<T>(result, iterationsCount, nodesGenerated, stuckCount);

            ITreeNode<T> Traverse(ITreeNode<T> node)
            {
                iterationsCount++;

                if (node.IsNodeCompliant())
                {
                    return node;
                }

                if (node.Depth >= maxDepth)
                {
                    stuckCount++;
                    return null;
                }

                IEnumerable<ITreeNode<T>> nodes = node.GetChildren();

                foreach (ITreeNode<T> child in nodes)
                {
                    nodesGenerated++;

                    ITreeNode<T> result = Traverse(child);
                    if (result is not null)
                    {
                        return result;
                    }
                }

                stuckCount++;
                return null;
            }
        }
    }
}
