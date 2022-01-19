using System.ComponentModel.Design;
using System.Diagnostics;
using TurboCollections;

namespace TurboCollectionsSpeedTests
{
    class Program
    {
        public static void Main(string[] args)
        {
            SpeedTests speedTests = new();
            speedTests.Selection();
        }
    }

    class SpeedTests
    {
        private const int testIterations = 100000000;
        
        public void Selection()
        {
            Console.WriteLine("(1) Run list test");
            Console.WriteLine("(2) Run stack test");
            Console.WriteLine("(3) Run queue test");
            while (true)
            {
                string input = Console.ReadLine();
                switch (input)
                {
                    case "1":
                        ListTester();
                        break;
                    case "2":
                        StackTester();
                        break;
                    case "3":
                        QueueTester();
                        break;
                    default:
                        Console.WriteLine("Incorrect input");
                        break;
                }
            }
        }

        void ListTester()
        {
            Stopwatch stopwatch = new();
            stopwatch.Start();
        }

        void StackTester()
        {
            
        }

        void QueueTester()
        {
            Console.WriteLine("Starting queue test");
            Console.WriteLine("Starting queue write test. Writes 100 000 000 entries to a queue.");

            TurboQueue<int> queue = new();

            Stopwatch stopwatch = new();
            stopwatch.Start();

            for (int i = 0; i < testIterations; i++)
            {
                queue.Enqueue(i);
            }
            
            stopwatch.Stop();
            Console.WriteLine($"Write test took: {stopwatch.ElapsedMilliseconds} milliSeconds");
        }
    }
}

