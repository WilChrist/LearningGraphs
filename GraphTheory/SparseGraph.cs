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
        public void DFS_Explore(int startingVertexID=-1) {
            if (startingVertexID != -1)
            {
                Vertex startingVertex;
                startingVertex = InitializeAndFindStartingVertex(startingVertexID);
                DFS(startingVertex);
            }

            foreach (var v in Vertexes)
            {
                if (v.Color == VERTEXCOLOR.WHITE)
                    DFS(v);
            }


        }
        public void DFS(Vertex vertex) {
            vertex.Color = VERTEXCOLOR.GRAY;
            vertex.Distance++;
            foreach (var v in vertex.Neighbors)
            {
                if(v.Item1.Color==VERTEXCOLOR.WHITE)
                    DFS(v.Item1);
            }
            vertex.Color = VERTEXCOLOR.BLACK;
        }


        public void BFS_Explore(int startingVertexID)
        {
            Vertex startingVertex;
            startingVertex = InitializeAndFindStartingVertex(startingVertexID);

            if (startingVertex == null)
            {
                throw new Exception($"Find Starting Vertex Exception: starting vertex with Id = {startingVertexID} can't be found in the graph");
            }

            Queue<Vertex> vertexQueue = new Queue<Vertex>();

            startingVertex.Color = VERTEXCOLOR.GRAY;
            startingVertex.Distance = 0;
            vertexQueue.Enqueue(startingVertex);
            while (vertexQueue.Count > 0)
            {
                Vertex currentVertex = vertexQueue.Dequeue();
                foreach (var v in currentVertex.Neighbors)
                {
                    if (v.Item1.Color == VERTEXCOLOR.WHITE)
                    {
                        v.Item1.Color = VERTEXCOLOR.GRAY;
                        v.Item1.Distance = currentVertex.Distance + 6;
                        v.Item1.Parent = currentVertex;
                        vertexQueue.Enqueue(v.Item1);
                    }
                }
                currentVertex.Color = VERTEXCOLOR.BLACK;
            }

        }
        Vertex InitializeAndFindStartingVertex( int startingVertexID) {
            Vertex vertex = null;
            if (!IsInNeedForSpeedMode)
            {
                foreach (var v in Vertexes)
                {
                    v.Color = VERTEXCOLOR.WHITE;
                    v.Distance = -1;
                    v.Parent = null;
                    if (v.Id == startingVertexID)
                    {
                        vertex = v;
                    }
                }
            }
            else
            {
                vertex = Vertexes[startingVertexID - 1];  // ToDo: verify this assumption for the problem you'll be trying to solve
            }

            return vertex;
        }
        public bool IsVertexInGraph(int id)
        {
            bool answer = false;
            foreach (var v in Vertexes)
            {
                if (v.Id == id)
                {
                    answer = true;
                }
                
            }

            return answer;
        }
        public void AddVertex(int id, object value=null)
        {
            bool isDoAdd = true;
            if (!IsInNeedForSpeedMode)
            {
                isDoAdd = !IsVertexInGraph(id);
            }
            if(isDoAdd)
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
            Vertex firstVertex;
            Vertex secondVertex;

            if (!IsInNeedForSpeedMode)
            {
                firstVertex = Vertexes.First(v => v.Id == firstVertexId);
                secondVertex = Vertexes.First(v => v.Id == secondVertexId);
                foreach (var vt in firstVertex.Neighbors)
                {
                    if (vt.Item1.Id == secondVertexId)
                    {
                        throw new Exception($"Adding Edge Exception: edge between with firstVertexId = {firstVertexId} and secondVertexId = {secondVertexId} already exists in the graph");
                    }
                }
            }
            else
            {
                firstVertex = Vertexes[firstVertexId - 1]; // ToDo: verify this assumption for the problem you'll be trying to solve
                secondVertex = Vertexes[secondVertexId - 1];
            }

            firstVertex.Neighbors.Add(new Tuple<Vertex, int>(secondVertex, weight));
        }
    }
}
