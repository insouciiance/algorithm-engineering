using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EightQueens.Tree;

namespace EightQueens.Benchmarks
{
    public interface IBenchmark<T>
    {
        BenchmarkResult<T> Run(ITreeNode<T> rootNode);
    }
}
