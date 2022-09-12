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
        public int NumberOfVerticles { get; set; }
        public int NumberOfEdges { get; set; }
        public int [,] VertexMatrix { get; set; }

        public DenseGraph( int numberOfVerticles = 10, int defaultWeight = int.MaxValue)
        {
            NumberOfEdges = 0;

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
        }
        public void RemoveEdge(int i, int j, int defaultWeight = int.MaxValue)
        {
            if (VertexMatrix[i, j] != defaultWeight)
            {
                NumberOfEdges--;
            }
            VertexMatrix[i, j] = defaultWeight;
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
    }
}
