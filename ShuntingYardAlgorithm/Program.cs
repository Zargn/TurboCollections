using TurboCollections;

namespace ShuntingYardAlgorithm
{
    class Program
    {
        private static void Main()
        {
            while (true)
            {
                ShuntingYardAlgorithm shuntingYardAlgorithm = new();

                TurboQueue<Token> TokenQueue = new TurboQueue<Token>();

                while (TokenQueue.Count == 0)
                {
                    Console.WriteLine("Please input a math formula.");
                    Console.WriteLine("Allowed characters are 0-9, +, -, /, *, (, )");

                    TokenQueue = shuntingYardAlgorithm.ConvertToReversePolish(Console.ReadLine());
                    if (TokenQueue.Count == 0)
                        Console.WriteLine("UnPermitted characters found! Please try again.");
                }

                decimal result = ReversePolishCalculator.CalculateFromQueue(TokenQueue);
                Console.WriteLine($"Result of expression is: decimal:{result}");


                Console.WriteLine("Write quit to stop the program, or press enter to start again.");
                if (Console.ReadLine() is "quit" or "Quit")
                    break;
                Console.Clear();
            }
        }
    }
}

