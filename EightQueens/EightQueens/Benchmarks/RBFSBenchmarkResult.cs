using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EightQueens.Tree;

namespace EightQueens.Benchmarks
{
    public class RBFSBenchmarkResult<T> : BenchmarkResult<T>
    {
        public override string Algorithm => "RBFS";

        public RBFSBenchmarkResult(
            ITreeNode<T> resultNode,
            int iterationsCount,
            int nodesGenerated) : base(resultNode, iterationsCount, nodesGenerated) { }
    }
}
