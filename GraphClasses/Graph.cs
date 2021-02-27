using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Seidel_s_Algorithm
{
    class Graph
    {
        private List<Node> _nodes;
        public List<Node> Nodes
        {
            get
            {
                return _nodes;
            }
            private set
            {
                _nodes = value;
            }
        }
        public Matrix AdjacencyMatrix;
        public int NodesCount { get; private set; }

        public Graph(int nodesCount)
        {
            NodesCount = nodesCount;
            GenerateNodes(nodesCount);
            AdjacencyMatrix = new Matrix(this);
        }

        private void GenerateNodes(int nodesCount)
        {
            Nodes = new List<Node>();
            List<Node> nodesWithEdge = new List<Node>();

            Random rnd = new Random();

            for (int i = 0; i < nodesCount; i++)
            {
                Nodes.Add(new Node((i + 1).ToString()));
            }

            while (nodesWithEdge.Count != nodesCount)
            {
                try
                {
                    Node firstNode = Nodes[rnd.Next(nodesCount)];
                    Node secondNode = Nodes[rnd.Next(nodesCount)];

                    firstNode.AddEdge(secondNode);

                    if (!nodesWithEdge.Contains(firstNode))
                    {
                        nodesWithEdge.Add(firstNode);
                    }
                    if (!nodesWithEdge.Contains(secondNode))
                    {
                        nodesWithEdge.Add(secondNode);
                    }
                }
                catch (BadEdgeException) { }
            }

        }

        #region StringAndConsoleMethods

        /// <summary>
        /// Creates string with info about current instance of graph
        /// </summary>
        /// <returns> Stringified info about graph </returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder($"This graph contains {Nodes.Count} nodes. Nodes are listed below:\n\n");
            foreach (var node in Nodes)
            {
                sb.Append($"{node.Name}\n");
            }
            sb.Append("\nAll edges between all nodes are listed below:\n\n");

            List<Node> shownNodes = new List<Node>();
            foreach (var node in Nodes)
            {
                // We don't want to repeat, because graph is undirected
                foreach (var edge in node.Edges.Where(edge => !shownNodes.Contains(edge.SecondNode)))
                {
                    sb.Append($"{edge.FirstNode.Name} - {edge.SecondNode.Name}\n");
                }
                shownNodes.Add(node);
            }

            // Looks pretty with this thing
            int spacesCount = Nodes.Max(node => node.Name.Length) + 1;
            sb.Append($"\nAdjacency matrix:\n\n{new string(' ', spacesCount + 1)}");
            foreach (var node in Nodes)
            {
                // Top header as node name
                sb.Append($"{node.Name}{new string(' ', spacesCount - (node.Name.Length - 1))}");
            }
            sb.Append("\n");

            int[,] matrix = AdjacencyMatrix.GetMultidimensionalArray();
            for (var i = 0; i < Nodes.Count; i++)
            {
                // Starts with name
                sb.Append($"{Nodes[i].Name}{new string(' ', spacesCount - (Nodes[i].Name.Length - 1))}");
                for (int l = 0; l < matrix.GetLength(1); l++)
                {
                    sb.Append($"{matrix[i, l]}{new string(' ', spacesCount)}");
                }
                sb.Append("\n");
            }

            return sb.ToString();
        }

        /// <summary>
        /// Make adjacency matrix colored in console output
        /// </summary>
        public void WriteToConsoleWithColors()
        {
            string splitBy = "\nAdjacency matrix:\n\n";

            string[] splittedByParts = ToString().Split(splitBy, StringSplitOptions.RemoveEmptyEntries);
            string[] matrixSplitted = splittedByParts[1].Split("\n");

            int spacesCount = Nodes.Max(node => node.Name.Length) + 1;

            int[,] matrix = AdjacencyMatrix.GetMultidimensionalArray();

            Console.WriteLine(splittedByParts[0] + splitBy);
            Console.WriteLine(matrixSplitted[0]);

            for (var i = 0; i < matrix.GetLength(0); i++)
            {
                Console.Write($"{matrix[i, 0]}{new string(' ', spacesCount - (matrix[i, 0].ToString().Length - 1))}");

                for (int l = 0; l < matrix.GetLength(1); l++)
                {
                    ConsoleColor color = matrix[i, l] == 0 ? ConsoleColor.Red : ConsoleColor.Green;
                    Console.ForegroundColor = color;
                    Console.Write($"{matrix[i, l]}{new string(' ', spacesCount)}");
                }
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine();
            }
        }

        #endregion
    }
}
