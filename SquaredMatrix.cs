using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Seidel_s_Algorithm
{
    readonly struct SquaredMatrix
    {
        private readonly int[,] _matrix;
        public readonly int Order;

        public SquaredMatrix(SquaredMatrix B)
        {
            Order = B.Order;
            _matrix = new int[B.Order, B.Order];
            for (int i = 0; i < B.Order; i++)
            {
                for (int l = 0; l < B.Order; l++)
                {
                    this[i, l] = B[i, l];
                }
            }
        }

        public SquaredMatrix(int[,] matrix)
        {
            if (matrix.GetLength(0) != matrix.GetLength(1))
            {
                throw new NonSquaredMatrixException($"Matrix is non-squared! Columns count is {matrix.GetLength(0)} and rows count is {matrix.GetLength(1)}.");
            }
            _matrix = new int[matrix.GetLength(0), matrix.GetLength(0)];
            Array.Copy(matrix, 0, _matrix, 0, matrix.Length);
            Order = _matrix.GetLength(0);
        }

        public SquaredMatrix(Graph graph)
        {
            Order = graph.NodesCount;
            _matrix = new int[Order, Order];

            for (int i = 0; i < Order; i++)
            {
                for (int l = 0; l < Order; l++)
                {
                    this[i, l] = graph.Nodes[i].Edges.Exists(edge => edge.SecondNode.Equals(graph.Nodes[l])) ? 1 : 0;
                }
            }

        }

        public static SquaredMatrix operator *(SquaredMatrix A, SquaredMatrix B)
        {
            if (A.Order != B.Order)
            {
                throw new DifferentOrdersOfMatricesException($"Different orders of matrices! Order of matrix A is {A.Order} and order of matrix B is {B.Order}.");
            }
            int[,] Z = new int[A.Order, A.Order];
            for (int i = 0; i < A.Order; i++)
            {
                for (int j = 0; j < A.Order; j++)
                {
                    for (int k = 0; k < A.Order; k++)
                    {
                        Z[i, j] += A[i, k] * B[k, j];
                    }
                }
            }
            return new SquaredMatrix(Z);
        }

        public static SquaredMatrix operator +(SquaredMatrix A, SquaredMatrix B)
        {
            if (A.Order != B.Order)
            {
                throw new DifferentOrdersOfMatricesException($"Different orders of matrices! Order of matrix A is {A.Order} and order of matrix B is {B.Order}.");
            }
            int[,] Z = new int[A.Order, B.Order];
            for (int i = 0; i < A.Order; i++)
            {
                for (int j = 0; j < A.Order; j++)
                {
                    Z[i, j] = A[i, j] + B[i, j];
                }
            }
            return new SquaredMatrix(Z);
        }

        public static SquaredMatrix operator -(SquaredMatrix A, SquaredMatrix B)
        {
            if (A.Order != B.Order)
            {
                throw new DifferentOrdersOfMatricesException($"Different orders of matrices! Order of matrix A is {A.Order} and order of matrix B is {B.Order}.");
            }
            int[,] Z = new int[A.Order, B.Order];
            for (int i = 0; i < A.Order; i++)
            {
                for (int j = 0; j < A.Order; j++)
                {
                    Z[i, j] = A[i, j] - B[i, j];
                }
            }
            return new SquaredMatrix(Z);
        }

        public int GetLength(int dimension)
        {
            return _matrix.GetLength(dimension);
        }

        public int this[int i, int l]
        {
            get { return _matrix[i, l]; }
            private set { _matrix[i, l] = value; }
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < Order; i++)
            {
                for (int l = 0; l < Order; l++)
                {
                    sb.Append($"{this[i, l]} ");
                }
                if (i != Order - 1)
                {
                    sb.Append("\n");
                }
            }
            return sb.ToString();
        }

        /// <summary>
        /// Writes matrix in console with different colors
        /// </summary>
        /// <param name="graph"></param>
        public void WriteToConsoleWithColors(Graph graph)
        {
            if(graph.Nodes.Count != Order)
            {
                throw new DifferentOrdersOfMatricesException($"Different orders of matrices! Order of matrix A is {Order} and order of matrix B is {graph.AdjacencyMatrix.Order}.");
            }

            int spacesCount = graph.Nodes.Max(node => node.Name.Length) + 1;
            Console.Write($"{new string(' ', spacesCount + 1)}");

            for (var i = 0; i < graph.Nodes.Count; i++)
            {
                // Starts with name
                Console.Write($"{graph.Nodes[i].Name}{new string(' ', spacesCount - (graph.Nodes[i].Name.Length - 1))}");
            }

            Console.WriteLine();

            for (var i = 0; i < Order; i++)
            {
                Console.Write($"{graph.Nodes[i].Name}{new string(' ', spacesCount - (graph.Nodes[i].Name.Length - 1))}");

                for (int l = 0; l < Order; l++)
                {
                    ConsoleColor color = this[i, l] == 0 ? ConsoleColor.Red : ConsoleColor.Green;
                    Console.ForegroundColor = color;
                    Console.Write($"{this[i, l]}{new string(' ', spacesCount - (this[i,l].ToString().Length - 1))}");
                }
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine();
            }
        }

    }

    class NonSquaredMatrixException : Exception
    {
        public NonSquaredMatrixException(string message) : base(message) { }
    }
    class DifferentOrdersOfMatricesException : Exception
    {
        public DifferentOrdersOfMatricesException(string message) : base(message) { }
    }
}
