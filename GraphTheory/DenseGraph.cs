using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphTheory
{
    /// <summary>
    /// A graph in which the number of edges is close to the possible number of edges.
    /// Typically, a dense graph has nearly the maximum number of edges.
    /// This is an adjacency matrix representation of a graph with n vertices, which consists of  using an n × n matrix, where the entry at (i,j) is 1 if there is an edge from vertex i to vertex j; otherwise the entry is 0.
    /// A weighted graph may be represented using the weight as the entry.
    /// An undirected graph may be represented using the same entry in both (i,j) and (j,i)
    /// Note: A directed graph can have at most n(n-1) edges, where n is the number of vertices. An undirected graph can have at most n(n-1)/2 edges.
    /// </summary>
    public class DenseGraph
    {
        public bool IsUnDirectedGraph { get; set; } = false;
        public int NumberOfVerticles { get; set; }
        public int NumberOfEdges { get; set; }
        public int NumberOfEdgesWithDuplicate { get; set; }
        public int [,] VertexMatrix { get; set; }
        public int[] Distances { get; set; }
        public int[] Fathers { get; set; }
        public bool[] Visited { get; set; }

        public DenseGraph( int numberOfVerticles = 10, int defaultWeight = int.MaxValue)
        {
            NumberOfEdges = 0;
            NumberOfEdgesWithDuplicate = 0;
            NumberOfVerticles = numberOfVerticles;

            VertexMatrix = new int[numberOfVerticles, numberOfVerticles];

            for (int i = 0; i < numberOfVerticles; i++)
            {
                for (int j = 0; j < numberOfVerticles; j++)
                {
                    VertexMatrix[i, j] = defaultWeight;
                }
            }
        }

        public void AddEdge(int i, int j, int weight, int defaultWeight = int.MaxValue)
        {
            if (VertexMatrix[i, j] == defaultWeight)
            {
                NumberOfEdges++;
            }
            VertexMatrix[i, j] = weight;
            if (IsUnDirectedGraph)
            {
                VertexMatrix[j, i] = weight;
            }
            NumberOfEdgesWithDuplicate++;
        }
        public void RemoveEdge(int i, int j, int defaultWeight = int.MaxValue)
        {
            if (VertexMatrix[i, j] != defaultWeight)
            {
                NumberOfEdges--;
            }
            VertexMatrix[i, j] = defaultWeight;
            if (IsUnDirectedGraph)
            {
                VertexMatrix[j, i] = defaultWeight;
            }
        }
        /// <summary>
        /// print the graph to the console for debugging purpose
        /// </summary>
        /// <param name="inError">when true printting will be done in the error console</param>
        public void PrintGraph(bool inError = true) {
            for (int i = 0; i < NumberOfVerticles; i++)
            {
                for (int j = 0; j < NumberOfVerticles; j++)
                {
                    if (inError)
                    {
                        Console.Error.Write($"{VertexMatrix[i, j]} ");
                    }
                    else
                    {
                        Console.Write($"{VertexMatrix[i, j]} ");
                    }
                    
                }
                if (inError)
                {
                    Console.Error.WriteLine($"[{i}]");
                }
                else
                {
                    Console.WriteLine($"[{i}]");
                }
            }
        }
    
        public void BFS_Explore(int startingVertexID, int defaultDistance = 1, int defaultFather= -1)
        {
            Queue<int> vertexIdQueue = new Queue<int>();
            vertexIdQueue.Enqueue(startingVertexID);

            Distances[startingVertexID] = 0;

            while (vertexIdQueue.Count > 0)
            {
                int currentVertexId = vertexIdQueue.Dequeue();
                List<int> adjacentVertexes = GetAdjacentVertexesOf(currentVertexId);

                foreach (int avId in adjacentVertexes)
                {
                    if (!Visited[avId])
                    {
                        Distances[avId] = Math.Min(Distances[currentVertexId] + defaultDistance, Distances[avId]);
                        // if avId doesn't have a father yet, we give him currentVertexId as father, else we look, between currentVertexId and the current father of avId, who is the closest from root, and we make that one the father of avId
                        /* in non ternary form it means
                         * if(Fathers[avId] < defaultFather + 1 || Distances[currentVertexId] < Distances[Fathers[avId]])
                        {
                            Fathers[avId] = currentVertexId;
                        }
                        else
                        {
                            Fathers[avId] = currentVertexId;
                        }*/
                        Fathers[avId] = Fathers[avId] < defaultFather+1 ? currentVertexId : Distances[currentVertexId] < Distances[Fathers[avId]] ? currentVertexId : Fathers[avId];
                        vertexIdQueue.Enqueue(avId);

                        Visited[avId] = true;// to be faster
                    }
                }
                Visited[currentVertexId] = true;
            }
        }
        public void BFS_VariablesInitialization(int defaultDistance = -1, int defaultFather=-1)
        {
            Distances = new int[NumberOfVerticles];
            Fathers = new int[NumberOfVerticles];
            Visited = new bool[NumberOfVerticles];
            for (int i = 0; i < NumberOfVerticles; i++)
            {
                Distances[i] = defaultDistance;
                Fathers[i] = defaultFather;
                Visited[i] = false;
            }
        }
        public List<int> GetAdjacentVertexesOf(int vertexId)
        {
            List<int> adjacents = new List<int>();

            for (int i = 0; i < VertexMatrix.GetLength(1); i++)
            {
                if (VertexMatrix[vertexId, i] > 0)
                {
                    adjacents.Add(i);
                }
            }

            return adjacents;
        }
    }
}
