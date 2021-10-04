using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EightQueens.Tree;

namespace EightQueens.Benchmarks
{
    public abstract class BenchmarkResult<T>
    {
        public abstract string Algorithm { get; }
        public ITreeNode<T> ResultNode { get; }
        public int IterationsCount { get; }
        public int NodesGenerated { get; }

        protected BenchmarkResult(
            ITreeNode<T> resultNode,
            int iterationsCount,
            int nodesGenerated)
        {
            ResultNode = resultNode;
            IterationsCount = iterationsCount;
            NodesGenerated = nodesGenerated;
        }

        public override string ToString()
        {
            StringBuilder builder = new();

            builder.Append($"{Algorithm} result:\n");
            builder.Append($"Iterations count: {IterationsCount}\n");
            builder.Append($"Nodes generated: {NodesGenerated}\n");
            if (ResultNode is not null)
            {
                builder.Append($"Depth: {ResultNode.Depth}\n");
                builder.Append(ResultNode.State);
            }

            return builder.ToString();
        }
    }
}
