using System;
using System.Collections.Generic;
using System.Text;

namespace Seidel_s_Algorithm
{
    static class Algorithm
    {
        public static void RunSeidelsAlgo(int[,] matrix, int nodesCount)
        {
            int[,] Z = MultiplyMatrices(matrix, matrix);

        }

        public static void ComputeShortestPaths(int[,] matrix)
        {

        }
        
        

        public static int[,] MultiplyMatrices(int[,] A, int[,] B)
        {
            if(A.GetLength(1) != B.GetLength(0))
            {
                throw new IncompatibleMatricesException($"Columns count of matrix A ({A.GetLength(1)}) must be equal to count of rows of matrix B ({B.GetLength(0)})!");
            }
            int[,] Z = new int[A.GetLength(0), B.GetLength(1)];
            for (int i = 0; i < A.GetLength(0); i++)
            {
                for (int j = 0; j < B.GetLength(1); j++)
                {
                    for (int k = 0; k < B.GetLength(0); k++)
                    {
                        Z[i, j] += A[i, k] * B[k, j];
                    }
                }
            }
            return Z;
        }
    }

    class IncompatibleMatricesException : Exception
    {
        public IncompatibleMatricesException(string message) : base(message) { }
    }
}
