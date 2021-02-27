using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Seidel_s_Algorithm
{
    class Node
    {
        public string Name { get; private set; }

        private List<Edge> _edges;
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

        public Node(string name)
        {
            Name = name;
            Edges = new List<Edge>();
        }

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

        public override bool Equals(object node)
        {
            if(!(node is Node))
            {
                return false;
            }
            return Name.Equals((node as Node).Name);
        }

        // These warnings...
        public override int GetHashCode()
        {
            return HashCode.Combine(Name, _edges, Edges);
        }
    }

    class BadEdgeException : Exception
    {
        public BadEdgeException(string message) : base(message)
        { }
    }
}
