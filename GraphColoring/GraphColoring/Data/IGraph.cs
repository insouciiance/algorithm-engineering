using System.Collections.Generic;

namespace GraphColoring.Data
{
    public interface IGraph<T> where T : IVertex<T>
    {
        List<T> Vertices { get; } 
        int VerticesCount { get; }
    }
}