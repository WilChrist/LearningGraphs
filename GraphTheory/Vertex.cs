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
    public class Vertex
    {
        public int Id { get; set; }
        public object Value { get; set; }
        public List<Tuple<Vertex, int>> AdjacencyList { get; set; }

        public VERTEXCOLOR Color { get; set; } = VERTEXCOLOR.WHITE;
        public int Distance { get; set; } = int.MaxValue;
        public Vertex Parent { get; set; }

        public Vertex()
        {

        }
        public Vertex(int id, object value = null)
        {
            Id = id;
            Value = value;

            AdjacencyList = new List<Tuple<Vertex, int>>();
        }
    }
}
