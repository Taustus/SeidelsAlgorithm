using System;
using System.Collections.Generic;

namespace Seidel_s_Algorithm
{
    class Program
    {
        static void Main(string[] args)
        {
            Graph graph = new Graph(20);
            graph.WriteToConsoleWithColors();
            Matrix A = new Matrix(graph);



            Console.Read();
        }
    }
}
