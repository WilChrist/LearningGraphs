using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphTheory
{
    public class SimpleGraphWAL
    {
        public bool IsUnDirectedGraph { get; set; } = false;
        public bool IsInNeedForSpeedMode { get; set; } = false;
        public List<Vertex> Vertexes { get; set; }
        public SimpleGraphWAL()
        {
            Vertexes = new List<Vertex>();
        }
        public void AddVertex(int id, object value)
        {
            if (!IsInNeedForSpeedMode)
            {
                foreach (var v in Vertexes)
                {
                    if(v.Id == id)
                    {
                        throw new Exception($"Adding Vertex Exception: vertex with Id = {id} already exists in the graph");
                    }
                }
            }
            
            Vertexes.Add(new Vertex(id, value));
        }

        public void AddEdge(int firstVertexId, int secondVertexId, int weight = 0)
        {
            AddEdgeInDirection(firstVertexId, secondVertexId, weight);
            if (IsUnDirectedGraph)
            {
                AddEdgeInDirection(secondVertexId, firstVertexId, weight);
            }
        }
        void AddEdgeInDirection(int firstVertexId, int secondVertexId, int weight = 0)
        {
            Vertex firstVertex = Vertexes.First(v => v.Id == firstVertexId);
            Vertex secondVertex = Vertexes.First(v => v.Id == secondVertexId);

            if (!IsInNeedForSpeedMode) {
                foreach (var vt in firstVertex.AdjacencyList)
                {
                    if (vt.Item1.Id == secondVertexId)
                    {
                        throw new Exception($"Adding Edge Exception: edge between with firstVertexId = {firstVertexId} and secondVertexId = {secondVertexId} already exists in the graph");
                    }
                }
            }
            
            firstVertex.AdjacencyList.Add(new Tuple<Vertex, int>(secondVertex, weight));
        }
    }
}
