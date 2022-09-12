using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphTheory
{
    /// <summary>
    /// A graph in which the number of edges is much less than the possible number of edges.
    /// Typically, a sparse (connected) graph has about as many edges as vertices
    /// This is an adjacency list representation of a graph, which consists of n lists one for each vertex v_i, 1<=i<=n, which gives the vertices to which v_i is adjacent.
    /// Note: A directed graph can have at most n(n-1) edges, where n is the number of vertices. An undirected graph can have at most n(n-1)/2 edges.
    /// </summary>
    public class SparseGraph
    {
        public bool IsUnDirectedGraph { get; set; } = false;
        /// <summary>
        /// when true, allows to skip the checking of already existing vertex with a certain ID before doing the operation with non existence of that ID as a prerequisite.
        /// skipping that verification step make the execution faster (from O(N) to O(1)) but more hard to debug.
        /// It's recommended to set it to true only in prod environnement.
        /// </summary>
        public bool IsInNeedForSpeedMode { get; set; } = false;
        public List<Vertex> Vertexes { get; set; }
        public SparseGraph()
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
                foreach (var vt in firstVertex.Neighbors)
                {
                    if (vt.Item1.Id == secondVertexId)
                    {
                        throw new Exception($"Adding Edge Exception: edge between with firstVertexId = {firstVertexId} and secondVertexId = {secondVertexId} already exists in the graph");
                    }
                }
            }
            
            firstVertex.Neighbors.Add(new Tuple<Vertex, int>(secondVertex, weight));
        }
    }
}
