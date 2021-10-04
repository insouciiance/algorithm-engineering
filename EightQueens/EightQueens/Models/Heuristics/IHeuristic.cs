using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EightQueens.Models
{
    public interface IHeuristic<T>
    {
        int Evaluate(T item);
    }
}
