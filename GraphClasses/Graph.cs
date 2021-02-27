using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Seidel_s_Algorithm
{
    class Graph
    {
        private List<Node> _nodes;
        // List of nodes
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
        // Adjacency matrix of current instance of graph
        public SquaredMatrix AdjacencyMatrix;
        // Nodes count
        public int NodesCount { get; private set; }


        public Graph(int nodesCount)
        {
            NodesCount = nodesCount;
            GenerateNodes(nodesCount);
            AdjacencyMatrix = new SquaredMatrix(this);
        }

        /// <summary>
        /// Generate nodes and edges betwen them, forming undirected, unweighted and connected graph
        /// Works only with even number of vertices (have no time to fix this issue)
        /// </summary>
        /// <param name="nodesCount"></param>
        private void GenerateNodes(int nodesCount)
        {
            Nodes = new List<Node>();
            List<Node> nodesWithEdge = new List<Node>();

            Random rnd = new Random();

            for (int i = 0; i < nodesCount; i++)
            {
                Nodes.Add(new Node((i + 1).ToString()));
            }

            bool graphIsConnected = false;
            do
            {
                try
                {
                    List<Node> nodesInPath = GetRandomPath();
                    List<Node> preferredNodes = Nodes.Except(nodesInPath).ToList();

                    if (nodesInPath.Count == Nodes.Count)
                    {
                        graphIsConnected = true;
                        continue;
                    }

                    Node firstNode = Nodes[Nodes.IndexOf(preferredNodes[rnd.Next(preferredNodes.Count)])];
                    List<Node> allowedSecond = Nodes.Where(node => !node.Equals(firstNode) && !node.Edges.Select(edge => edge.SecondNode).Contains(node)).ToList();
                    Node secondNode = allowedSecond[rnd.Next(allowedSecond.Count)];

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
            while (nodesWithEdge.Count != nodesCount || !graphIsConnected);
        }

        /// <summary>
        /// Gets random path to check if this path length equal to NodesCount (this makes graph connected)
        /// </summary>
        /// <returns></returns>
        private List<Node> GetRandomPath()
        {
            Node nodeWithEdge = null;
            if (Nodes.Exists(node => node.Edges.Count > 0))
            {
                nodeWithEdge = Nodes.First(node => node.Edges.Count > 0);
            }
            else
            {
                return new List<Node>();
            }
            List<Node> nodesInPath = GetConnectedNodes(nodeWithEdge, null, new List<Node>());
            return nodesInPath;
        }

        /// <summary>
        /// Recursive method, that returns list of connected nodes
        /// </summary>
        /// <param name="startNode"></param>
        /// <param name="previousNode"></param>
        /// <param name="visitedNodes"></param>
        /// <returns></returns>
        List<Node> GetConnectedNodes(Node startNode, Node previousNode, List<Node> visitedNodes)
        {
            if (!visitedNodes.Contains(startNode))
            {
                visitedNodes.Add(startNode);
            }
            foreach (var edge in startNode.Edges)
            {
                if (edge.SecondNode is null || (!visitedNodes.Contains(edge.SecondNode) && !edge.SecondNode.Equals(previousNode)))
                {
                    List<Node> newVisited = GetConnectedNodes(edge.SecondNode, startNode, visitedNodes);
                    visitedNodes = newVisited.Count > visitedNodes.Count ? newVisited : visitedNodes;
                }
            }
            return visitedNodes;
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
            sb.Append($"\nAdjacency matrix:\n{new string(' ', spacesCount + 1)}");
            foreach (var node in Nodes)
            {
                // Top header as node name
                sb.Append($"{node.Name}{new string(' ', spacesCount - (node.Name.Length - 1))}");
            }
            sb.Append("\n");

            for (var i = 0; i < Nodes.Count; i++)
            {
                // Starts with name
                sb.Append($"{Nodes[i].Name}{new string(' ', spacesCount - (Nodes[i].Name.Length - 1))}");
                for (int l = 0; l < AdjacencyMatrix.Order; l++)
                {
                    sb.Append($"{AdjacencyMatrix[i, l]}{new string(' ', spacesCount)}");
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
            string splitBy = "\nAdjacency matrix:\n";

            string[] splittedByParts = ToString().Split(splitBy, StringSplitOptions.RemoveEmptyEntries);
            string[] matrixSplitted = splittedByParts[1].Split("\n");

            int spacesCount = Nodes.Max(node => node.Name.Length) + 1;

            Console.WriteLine(splittedByParts[0] + splitBy);
            Console.WriteLine(matrixSplitted[0]);

            for (var i = 0; i < AdjacencyMatrix.Order; i++)
            {
                Console.Write($"{Nodes[i].Name}{new string(' ', spacesCount - (Nodes[i].Name.Length - 1))}");

                for (int l = 0; l < AdjacencyMatrix.Order; l++)
                {
                    ConsoleColor color = AdjacencyMatrix[i, l] == 0 ? ConsoleColor.Red : ConsoleColor.Green;
                    Console.ForegroundColor = color;
                    Console.Write($"{AdjacencyMatrix[i, l]}{new string(' ', spacesCount)}");
                }
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine();
            }
        }

        #endregion
    }
}
