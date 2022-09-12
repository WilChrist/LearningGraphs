using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphTheory
{
    public class SimpleGraphWAM
    {
        public int NumberOfVerticles { get; set; }
        public int NumberOfEdges { get; set; }
        public int [,] VertexMatrix { get; set; }

        public SimpleGraphWAM( int numberOfVerticles = 10, int defaultWeight = int.MaxValue)
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
