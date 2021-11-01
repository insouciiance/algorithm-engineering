using System;
using System.Collections.Generic;

namespace GraphColoring.Data
{
    public interface IVertex<T> : IEquatable<IVertex<T>> where T : IVertex<T>
    {
        int Index { get; }
        List<T> AdjacentVertices { get; set; }
        int Degree { get; }
    }
}