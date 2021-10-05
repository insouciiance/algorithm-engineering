using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EightQueens.Heuristics
{
    public interface IHeuristic<T>
    {
        int Evaluate(T item);
    }
}
