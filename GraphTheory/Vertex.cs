using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphTheory
{
    public enum VERTEXCOLOR
    {
        WHITE = 0,
        GRAY = 1,
        BLACK = 2
    }
    /// <summary>
    /// "Vertex" is a synonym for a node of a graph, i.e., one of the points on which the graph is defined and which may be connected by graph edges.
    /// </summary>
    public class Vertex
    {
        /// <summary>
        /// unique identifier of a vertex
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// object representing the content of the graph, may need to be parsed
        /// </summary>
        public object Value { get; set; }
        /// <summary>
        /// list of tuple for each vertex neighbor and its weight
        /// </summary>
        public List<Tuple<Vertex, int>> Neighbors { get; set; }

        public VERTEXCOLOR Color { get; set; } = VERTEXCOLOR.WHITE;
        public int Distance { get; set; } = int.MaxValue;
        public Vertex Parent { get; set; }

        public Vertex()
        {
            Neighbors = new List<Tuple<Vertex, int>>();
        }
        public Vertex(int id, object value = null)
        {
            Id = id;
            Value = value;

            Neighbors = new List<Tuple<Vertex, int>>();
            Parent = null;
        }
    }
}
