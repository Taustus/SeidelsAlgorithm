using System;
using System.Collections.Generic;
using System.Text;

namespace Seidel_s_Algorithm
{
    readonly struct Matrix
    {
        private readonly int[,] _matrix;

        public Matrix(int[,] matrix)
        {
            _matrix = new int[matrix.GetLength(0), matrix.GetLength(1)];
            Array.Copy(matrix, 0, _matrix, 0, matrix.Length);
        }

        public Matrix(Graph graph)
        {
            _matrix = new int[graph.NodesCount, graph.NodesCount];

            for (int i = 0; i < _matrix.GetLength(0); i++)
            {
                for (int l = 0; l < _matrix.GetLength(1); l++)
                {
                    _matrix[i, l] = graph.Nodes[i].Edges.Exists(edge => edge.SecondNode.Equals(graph.Nodes[l])) ? 1 : 0;
                }
            }
        }

        public static Matrix operator *(Matrix A, Matrix B)
        {
            if (A.GetLength(1) != B.GetLength(0))
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
                        Z[i, j] += A._matrix[i, k] * B._matrix[k, j];
                    }
                }
            }
            return new Matrix(Z);
        }

        public int GetLength(int dimension)
        {
            return _matrix.GetLength(dimension);
        }
        public int[,] GetMultidimensionalArray()
        {
            int[,] toReturn = new int[_matrix.GetLength(0), _matrix.GetLength(1)];
            Array.Copy(_matrix, 0, toReturn, 0, _matrix.Length);
            return toReturn;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < _matrix.GetLength(0); i++)
            {
                for (int l = 0; l < _matrix.GetLength(1); l++)
                {
                    sb.Append($"{_matrix[i, l]} ");
                }
                if (i != _matrix.GetLength(0) - 1)
                {
                    sb.Append("\n");
                }
            }
            return base.ToString();
        }

    }
}
