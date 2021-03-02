using System;
using System.Collections.Generic;
using System.Text;

namespace Seidel_s_Algorithm
{
    public static class Algorithm
    {
        public static SquaredMatrix RunSeidelsAlgo(SquaredMatrix A)
        {
            if (AllOnes(A))
            {
                return A;
            }

            SquaredMatrix Z = A * A;

            SquaredMatrix B = CreateMatrixWithOnes(A, Z);

            SquaredMatrix T = RunSeidelsAlgo(B);

            SquaredMatrix X = T * A;

            int[] degree = CalculateDegree(A);

            SquaredMatrix D = CalculateDMatrix(T, X, degree);

            return D;
        }

        private static SquaredMatrix CalculateDMatrix(SquaredMatrix T, SquaredMatrix X, int[] degree)
        {
            int[,] d = new int[T.Order, T.Order];
            for (int i = 0; i < T.Order; i++)
            {
                for (int l = 0; l < T.Order; l++)
                {
                    d[i, l] = 2 * T[i, l];

                    if (X[i, l] < T[i, l] * degree[l])
                    {
                        d[i, l] -= 1;
                    }
                }
            }
            return new SquaredMatrix(d);
        }

        private static int[] CalculateDegree(SquaredMatrix A)
        {
            int[] degree = new int[A.Order];

            for (int i = 0; i < A.Order; i++)
            {
                int sum = 0;
                for (int l = 0; l < A.Order; l++)
                {
                    sum += A[i, l];
                }
                degree[i] = sum;
            }

            return degree;
        }

        private static SquaredMatrix CreateMatrixWithOnes(SquaredMatrix A, SquaredMatrix Z)
        {
            int[,] b = new int[A.Order, A.Order];

            for (int i = 0; i < A.Order; i++)
            {
                for (int l = 0; l < A.Order; l++)
                {
                    if (i != l && (A[i, l] == 1 || Z[i, l] > 0))
                    {
                        b[i, l] = 1;
                    }
                    else
                    {
                        b[i, l] = 0;
                    }
                }
            }
            return new SquaredMatrix(b);
        }

        private static bool AllOnes(SquaredMatrix A)
        {
            for (int i = 0; i < A.Order; i++)
            {
                for (int l = 0; l < A.Order; l++)
                {
                    if (i != l && A[i, l] != 1)
                    {
                        // If we found any zero in matrix except main diagonal - return false
                        return false;
                    }
                }
            }
            // If we didn't return on loop - return true
            return true;
        }

    }


}
