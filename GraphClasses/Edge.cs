using System;
using System.Collections.Generic;
using System.Text;

namespace Seidel_s_Algorithm
{
    class Edge
    {
        public Node FirstNode { get; private set; }
        public Node SecondNode { get; private set; }

        public const byte Weight = 1;

        public Edge(Node firstNode, Node secondNode)
        {
            FirstNode = firstNode;
            SecondNode = secondNode;
        }
    }
}
