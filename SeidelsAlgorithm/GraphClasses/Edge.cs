using System;
using System.Collections.Generic;
using System.Text;

namespace Seidel_s_Algorithm
{
    public class Edge
    {
        // First node
        public Node FirstNode { get; private set; }

        // Second node
        public Node SecondNode { get; private set; }

        // Constan weight of the edge
        public const byte Weight = 1;

        /// <summary>
        /// Creates new instance of Edge class.
        /// Represents edge between two nodes
        /// </summary>
        /// <param name="firstNode"> Firse node </param>
        /// <param name="secondNode"> Second node </param>

        public Edge(Node firstNode, Node secondNode)
        {
            FirstNode = firstNode;
            SecondNode = secondNode;
        }
    }
}
