using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace VertexCover.Services
{
    public static class GraphSerializer
    {
        public static async Task SerializeAsync(Graph graph, string fileName)
        {
            StringBuilder sb = new ();
            using StreamWriter writer = new (fileName);

            for(int i = 0; i < graph.VerticesCount; i++)
            {
                sb = new();

                for(int j = 0; j < graph.VerticesCount; j++)
                {
                    if (graph.Vertices[i].AdjacentEdges.Any(e => e.GetAdjacentVertex(graph.Vertices[i]).Equals(graph.Vertices[j])))
                    {
                        sb.Append(1);
                    }
                    else
                    {
                        sb.Append(0);
                    }
                    sb.Append(" ");
                }

                await writer.WriteLineAsync(sb.ToString());
            }
        }
    }
}