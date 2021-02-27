using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Seidel_s_Algorithm
{
    class Node
    {
        // Name of current node
        public string Name { get; private set; }

        private List<Edge> _edges;

        // Edges of current node
        public List<Edge> Edges
        {
            get
            {
                return _edges;
            }
            private set
            {
                _edges = value;
            }
        }

        /// <summary>
        /// Creates new instance of Node class
        /// </summary>
        /// <param name="name"> Name of node </param>
        public Node(string name)
        {
            Name = name;
            Edges = new List<Edge>();
        }

        /// <summary>
        /// Add edge between this and second nodes
        /// </summary>
        /// <param name="secondNode"> Node to be connected to this node </param>
        /// <returns> This Node instance </returns>
        public Node AddEdge(Node secondNode)
        {
            if (Edges.Exists(edge => edge.SecondNode.Equals(secondNode)) || Equals(secondNode))
            {
                throw new BadEdgeException($"Can't create duplicate of edge between {Name}---{secondNode.Name} !");
            }

            Edges.Add(new Edge(this, secondNode));

            if (!secondNode.Edges.Exists(edge => edge.SecondNode.Equals(this)))
            {
                secondNode.AddEdge(this);
            }

            return this;
        }

        /// <summary>
        /// Remove edge between this and second nodes
        /// </summary>
        /// <param name="secondNode"> Node connected to this node </param>
        /// <returns> This Node instance </returns>
        public Node RemoveEdge(Node secondNode)
        {
            if (!Edges.Exists(edge => edge.SecondNode.Equals(secondNode)))
            {
                throw new BadEdgeException($"Can't remove non-existent edge between {Name}---{secondNode.Name}");
            }

            Edges.Remove(Edges.First(edge => edge.SecondNode.Name.Equals(secondNode.Name)));

            if (secondNode.Edges.Exists(edge => edge.SecondNode.Equals(this)))
            {
                secondNode.RemoveEdge(this);
            }

            return this;
        }

        /// <summary>
        /// Override for Equals(obj)
        /// </summary>
        /// <param name="obj"></param>
        /// <returns> 
        /// Returns true if names of nodes are equal
        /// </returns>
        public override bool Equals(object node)
        {
            if(!(node is Node))
            {
                return false;
            }
            return Name.Equals((node as Node).Name);
        }

        /// <summary>
        /// Override for GetHashCode()
        /// </summary>
        /// <returns> Hash code of current Node instance </returns>
        public override int GetHashCode()
        {
            return HashCode.Combine(Name, _edges, Edges);
        }
    }

    class BadEdgeException : Exception
    {
        /// <summary>
        /// Can't add this type of edge to current node
        /// </summary>
        /// <param name="message"> Output message </param>
        public BadEdgeException(string message) : base(message)
        { }
    }
}
