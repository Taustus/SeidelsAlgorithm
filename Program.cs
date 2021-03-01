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
                // To makes zsure that user will enter number
                bool flag = false;

                // Future nodes count
                int nodesCount = 0;
                do
                {
                    Console.WriteLine("Enter number of nodes to generate graph:");
                    flag = int.TryParse(Console.ReadLine(), out int n);
                    nodesCount = n;

                } while (!flag);

                Console.WriteLine();

                // Create new Graph instance
                Graph graph = new Graph(nodesCount);

                // Write it's info to console
                graph.WriteToConsoleWithColors();

                Console.WriteLine("\nRunning Seidel's Algo...\n");

                // Run Seidel's algorithm on current graph
                SquaredMatrix result = Algorithm.RunSeidelsAlgo(graph.AdjacencyMatrix);

                Console.WriteLine($"Result:\n");

                result.WriteToConsoleWithColors(graph);

                // Try again?
                Console.WriteLine("\nPress any key to repeat...\n\nPress ESC to exit...\n");

            } while (Console.ReadKey().Key != ConsoleKey.Escape);
        }
    }
}
