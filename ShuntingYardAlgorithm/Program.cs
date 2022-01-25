using System;
using TurboCollections;

namespace ShuntingYardAlgorithm
{
    class Program
    {
        private static void Main()
        {
            while (true)
            {
                TurboQueue<Token> tokenQueue = new TurboQueue<Token>();

                while (tokenQueue.Count == 0)
                {
                    Console.WriteLine("Please input a math formula.");
                    Console.WriteLine("Allowed characters are 0-9, +, -, /, *, (, )");

                    tokenQueue = ShuntingYardAlgorithm.ConvertToReversePolish(Console.ReadLine());
                    if (tokenQueue.Count == 0)
                        Console.WriteLine("UnPermitted characters found! Please try again.");
                }

                decimal result = ReversePolishCalculator.CalculateFromQueue(tokenQueue);
                Console.WriteLine($"Result of expression is: decimal:{result}");


                Console.WriteLine("Write quit to stop the program, or press enter to start again.");
                if (Console.ReadLine() is "quit" or "Quit")
                    break;
                Console.Clear();
            }
        }
    }
}

