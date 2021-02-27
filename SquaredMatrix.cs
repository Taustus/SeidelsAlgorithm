using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Seidel_s_Algorithm
{
    readonly struct SquaredMatrix
    {
        // Squared matrix itself
        private readonly int[,] _matrix;

        // Order of matrix
        public readonly int Order;

        /// <summary>
        /// Creates new instance of SquaredMatrix structure
        /// </summary>
        /// <param name="B"> Matrix to copy values from it </param>
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

        /// <summary>
        /// Creates new instance of SquaredMatrix structure
        /// </summary>
        /// <param name="matrix"> Multidimensional array with values </param>
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

        /// <summary>
        /// Creates new instance of SquaredMatrix structure
        /// </summary>
        /// <param name="graph"> Graph instance with adjacency matrix </param>
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

        /// <summary>
        /// * operator for matrices subtraction
        /// </summary>
        /// <param name="A"> First operand </param>
        /// <param name="B"> Second operand </param>
        /// <returns> The result of the matrices multiplication </returns>
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

        /// <summary>
        /// + operator for matrices subtraction
        /// </summary>
        /// <param name="A"> First operand </param>
        /// <param name="B"> Second operand </param>
        /// <returns> The result of the sum of matrices </returns>
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

        /// <summary>
        /// - operator for matrices subtraction
        /// </summary>
        /// <param name="A"> First operand </param>
        /// <param name="B"> Second operand </param>
        /// <returns> Result of matrices subtraction </returns>
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

        /// <summary>
        /// Get length of the specific dimension
        /// </summary>
        /// <param name="dimension"> Dimension № </param>
        /// <returns> Length of the specific dimension </returns>
        public int GetLength(int dimension)
        {
            return _matrix.GetLength(dimension);
        }

        /// <summary>
        /// Indexer for _matrix
        /// </summary>
        /// <param name="i"> Column number </param>
        /// <param name="l"> Row number </param>
        /// <returns> Value of specified element of _matrix </returns>
        public int this[int i, int l]
        {
            get { return _matrix[i, l]; }
            private set { _matrix[i, l] = value; }
        }

        /// <summary>
        /// Generate string with matrix's values
        /// </summary>
        /// <returns> String with matrix's values </returns>
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
        /// Override for Equals(obj)
        /// </summary>
        /// <param name="obj"></param>
        /// <returns> 
        /// Returns true if all elements are equal to the 
        /// values ​​of the second matrix and false if opposite
        /// </returns>
        public override bool Equals(object obj)
        {
            if (!(obj is SquaredMatrix))
            {
                return false;
            }

            SquaredMatrix B = (SquaredMatrix)obj;
            if (Order != B.Order)
            {
                throw new DifferentOrdersOfMatricesException($"Different orders of matrices! Order of matrix A is {Order} and order of matrix B is {B.Order}.");
            }

            bool flag = true;
            for (int i = 0; i < Order; i++)
            {
                for (int l = 0; l < Order; l++)
                {
                    if(this[i, l] != B[i, l])
                    {
                        flag = false;
                        return flag;
                    }
                }
            }
            return flag;
        }

        /// <summary>
        /// Writes the result of ToString () with the node names at the top and left corners of the matrix
        /// </summary>
        /// <param name="graph"> Graph instance with adjacency matrix </param>
        public void WriteToConsoleWithColors(Graph graph)
        {
            if (graph.Nodes.Count != Order)
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
                    Console.Write($"{this[i, l]}{new string(' ', spacesCount - (this[i, l].ToString().Length - 1))}");
                }
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine();
            }
        }

        /// <summary>
        /// Override for GetHashCode()
        /// </summary>
        /// <returns> Hash code of current SquaredMatrix instance </returns>
        public override int GetHashCode()
        {
            return HashCode.Combine(_matrix, Order);
        }
    }

    class NonSquaredMatrixException : Exception
    {
        /// <summary>
        /// The number of columns is not equal to the number of rows during instantiation of SquareMatrix
        /// </summary>
        /// <param name="message"> Output message </param>
        public NonSquaredMatrixException(string message) : base(message) { }
    }
    class DifferentOrdersOfMatricesException : Exception
    {
        /// <summary>
        /// The operation between matrices could not be completed due to the wrong number of rows or columns
        /// </summary>
        /// <param name="message"> Output message </param>
        public DifferentOrdersOfMatricesException(string message) : base(message) { }
    }
}
