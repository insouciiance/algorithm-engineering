using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EightQueens.Tree;

namespace EightQueens.Benchmarks
{
    public class IDSBenchmarkResult<T> : BenchmarkResult<T>
    {
        public override string Algorithm => "IDS";
        public int StuckCount { get; }

        public IDSBenchmarkResult(
            ITreeNode<T> resultNode,
            int iterationsCount,
            int nodesGenerated,
            int stuckCount) : base(resultNode, iterationsCount, nodesGenerated)
        {
            StuckCount = stuckCount;
        }

        public override string ToString()
        {
            StringBuilder builder = new();
            builder.Append(base.ToString());
            builder.Append($"Stuck count: {StuckCount}");
            return builder.ToString();
        }
    }
}
