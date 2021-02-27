using System;
using System.Collections.Generic;

namespace Seidel_s_Algorithm
{
    class Program
    {
        static void Main(string[] args)
        {
            do
            {
                bool flag = false;
                int nodesCount = 0;
                do
                {
                    Console.WriteLine("Enter number of nodes to generate graph:");
                    flag = int.TryParse(Console.ReadLine(), out int n);
                    nodesCount = n;

                } while (!flag);

                Console.WriteLine();

                Graph graph = new Graph(nodesCount);

                graph.WriteToConsoleWithColors();

                Console.WriteLine("\nRunning Seidel's Algo...\n");

                SquaredMatrix result = Algorithm.RunSeidelsAlgo(graph.AdjacencyMatrix);

                Console.WriteLine($"Result:\n\n");

                result.WriteToConsoleWithColors(graph);

                Console.WriteLine("\nPress ESC to exit...\n");

            } while (Console.ReadKey().Key != ConsoleKey.Escape);
        }
    }
}
