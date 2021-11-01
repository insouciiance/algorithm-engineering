using System.Collections.Generic;

namespace GraphColoring.Data
{
    public class Graph : IGraph<Vertex>
    {
        public List<Vertex> Vertices { get; }
        
        public int VerticesCount => Vertices.Count;

        public Graph(bool[,] adjacencyMatrix)
        {
            int verticesCount = adjacencyMatrix.GetLength(0);

            Vertices = new List<Vertex>();

            for(int i = 0; i < adjacencyMatrix.GetLength(0); i++)
            {
                Vertices.Add(new Vertex(i));
            }

            for(int i = 0; i < adjacencyMatrix.GetLength(0); i++)
            {
                for(int j = 0; j < adjacencyMatrix.GetLength(1); j++)
                {
                    if (adjacencyMatrix[i, j])
                    {
                        Vertices[i].AdjacentVertices.Add(Vertices[j]);
                    }
                }
            }
        }
    }
}