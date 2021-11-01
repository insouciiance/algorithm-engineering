using System.Collections.Generic;
using System.Linq;

namespace GraphColoring.Data
{
    public class ColoredGraph : IGraph<ColoredVertex>
    {
        public List<ColoredVertex> Vertices { get; }

        public int VerticesCount => Vertices.Count;

        public int ChromaticNumber => GetChromaticNumber();

        public ColoredGraph(Graph graph)
        {
            Vertices = new List<ColoredVertex>();

            foreach(Vertex vertex in graph.Vertices)
            {
                IEnumerable<ColoredVertex> adjacentVertices = 
                    vertex.AdjacentVertices.Select(v => new ColoredVertex(v.Index));
                Vertices.Add(new ColoredVertex(vertex.Index, adjacentVertices));
            }
        }

        public ColoredGraph(bool[,] adjacencyMatrix) : this(new Graph(adjacencyMatrix)) {}

        private int GetChromaticNumber()
        {
            List<Color> usedColors = new ();

            foreach(ColoredVertex vertex in Vertices)
            {
                Color? vertexColor = vertex.Color;

                if (vertexColor is null)
                {
                    return -1;
                }

                if (!usedColors.Contains(vertexColor.Value))
                {
                    usedColors.Add(vertexColor.Value);
                }
            }

            return usedColors.Count;
        }
    }
}