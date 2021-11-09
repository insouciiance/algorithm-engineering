using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelingSalesman
{
    public interface IOptimizable
    {
        double TotalCost { get; }
        bool IsAscending { get; }
    }
}
