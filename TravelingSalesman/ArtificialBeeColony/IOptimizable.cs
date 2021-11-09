using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtificialBeeColony
{
    public interface IOptimizable
    {
        double TotalCost { get; }
        bool IsAscending { get; }
    }
}
