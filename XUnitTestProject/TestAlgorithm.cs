using Seidel_s_Algorithm;
using System;
using Xunit;

namespace XUnitTestProject
{
    public class TestAlgorithm
    {
        /// <summary>
        /// Check graph with 3 nodes
        /// </summary>
        [Fact]
        public void MatrixWithOrder_3()
        {
            SquaredMatrix adjacencyMatrix = new SquaredMatrix(new int[,] { { 0, 1, 0 },
                                                                           { 1, 0, 1 },
                                                                           { 0, 1, 0 } });

            SquaredMatrix expectedResultMatrix = new SquaredMatrix(new int[,] { { 0, 1, 2 },
                                                                                { 1, 0, 1 },
                                                                                { 2, 1, 0 } });

            SquaredMatrix actualResultMatrix = Algorithm.RunSeidelsAlgo(adjacencyMatrix);


            Assert.Equal(expectedResultMatrix, actualResultMatrix);
        }

        /// <summary>
        /// Check graph with 4 nodes
        /// </summary>
        [Fact]
        public void MatrixWithOrder_4()
        {
            SquaredMatrix adjacencyMatrix = new SquaredMatrix(new int[,] { { 0, 1, 1, 1 },
                                                                           { 1, 0, 0, 1 },
                                                                           { 1, 0, 0, 0 },
                                                                           { 1, 1, 0, 0 } });

            SquaredMatrix expectedResultMatrix = new SquaredMatrix(new int[,] { { 0, 1, 1, 1 },
                                                                                { 1, 0, 2, 1 },
                                                                                { 1, 2, 0, 2 },
                                                                                { 1, 1, 2, 0 } });

            SquaredMatrix actualResultMatrix = Algorithm.RunSeidelsAlgo(adjacencyMatrix);



            Assert.Equal(expectedResultMatrix, actualResultMatrix);
        }

        /// <summary>
        /// Check graph with 5 nodes
        /// </summary>
        [Fact]
        public void MatrixWithOrder_5()
        {
            SquaredMatrix adjacencyMatrix = new SquaredMatrix(new int[,] { { 0, 1, 0, 1, 0 },
                                                                           { 1, 0, 0, 0, 0 },
                                                                           { 0, 0, 0, 0, 1 },
                                                                           { 1, 0, 0, 0, 1 },
                                                                           { 0, 0, 1, 1, 0 } });

            SquaredMatrix expectedResultMatrix = new SquaredMatrix(new int[,] { { 0, 1, 3, 1, 2 },
                                                                                { 1, 0, 4, 2, 3 },
                                                                                { 3, 4, 0, 2, 1 },
                                                                                { 1, 2, 2, 0, 1 },
                                                                                { 2, 3, 1, 1, 0 } });

            SquaredMatrix actualResultMatrix = Algorithm.RunSeidelsAlgo(adjacencyMatrix);

            Assert.Equal(expectedResultMatrix, actualResultMatrix);
        }

        /// <summary>
        /// Check graph with 6 nodes
        /// </summary>
        [Fact]
        public void MatrixWithOrder_6()
        {
            SquaredMatrix adjacencyMatrix = new SquaredMatrix(new int[,] { { 0, 0, 0, 0, 1, 1 },
                                                                           { 0, 0, 0, 1, 0, 1 },
                                                                           { 0, 0, 0, 1, 0, 1 },
                                                                           { 0, 1, 1, 0, 0, 0 },
                                                                           { 1, 0, 0, 0, 0, 0 },
                                                                           { 1, 1, 1, 0, 0, 0 }});

            SquaredMatrix expectedResultMatrix = new SquaredMatrix(new int[,] { { 0, 2, 2, 3, 1, 1 },
                                                                                { 2, 0, 2, 1, 3, 1 },
                                                                                { 2, 2, 0, 1, 3, 1 },
                                                                                { 3, 1, 1, 0, 4, 2 },
                                                                                { 1, 3, 3, 4, 0, 2 },
                                                                                { 1, 1, 1, 2, 2, 0 }});

            SquaredMatrix actualResultMatrix = Algorithm.RunSeidelsAlgo(adjacencyMatrix);

            Assert.Equal(expectedResultMatrix, actualResultMatrix);
        }
    }
}
