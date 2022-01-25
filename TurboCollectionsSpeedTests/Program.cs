using System;
using System.Collections.Generic;
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
                        StandardListTester();
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
            TurboList<int> list = new();
            Stopwatch stopwatch = new();

            Console.WriteLine("Starting Add test");
            
            stopwatch.Start();
            for (int i = 0; i < testIterations; i++)
            {
                list.Add(i);
            }
            stopwatch.Stop();
            Console.WriteLine("Add test completed");
            long AddTestTime = stopwatch.ElapsedMilliseconds;
            stopwatch.Reset();
            
            
            Console.WriteLine("Starting RemoveAt test");
            
            stopwatch.Start();
            for (int i = 0; i < testIterations; i++)
            {
                list.RemoveAt(testIterations - 1 - i);
            }
            stopwatch.Stop();
            Console.WriteLine("RemoveAt test completed");
            long RemoveAtTestTime = stopwatch.ElapsedMilliseconds;
            stopwatch.Reset();
            
            
            Console.WriteLine("Starting AddRemove test");
            
            stopwatch.Start();
            for (int i = 0; i < testIterations; i++)
            {
                list.Add(i);
                list.RemoveAt(0);
            }
            stopwatch.Stop();
            Console.WriteLine("AddRemove test completed");
            long AddRemoveAtTestTime = stopwatch.ElapsedMilliseconds;
            stopwatch.Reset();

            Console.WriteLine($"Add test took: {AddTestTime} milliseconds");
            Console.WriteLine($"RemoveAt test took: {RemoveAtTestTime} milliseconds");
            Console.WriteLine($"AddRemoveAt test took: {AddRemoveAtTestTime} milliseconds");
        }
        
        void StandardListTester()
        {
            Console.WriteLine();
            Console.WriteLine("Starting standard list test");
            
            List<int> list = new();
            Stopwatch stopwatch = new();

            Console.WriteLine("Starting Add test");
            
            stopwatch.Start();
            for (int i = 0; i < testIterations; i++)
            {
                list.Add(i);
            }
            stopwatch.Stop();
            Console.WriteLine("Add test completed");
            long addTestTime = stopwatch.ElapsedMilliseconds;
            stopwatch.Reset();
            
            
            Console.WriteLine("Starting RemoveAt test");
            
            stopwatch.Start();
            for (int i = 0; i < testIterations; i++)
            {
                list.RemoveAt(testIterations - 1 - i);
            }
            stopwatch.Stop();
            Console.WriteLine("RemoveAt test completed");
            long removeAtTestTime = stopwatch.ElapsedMilliseconds;
            stopwatch.Reset();
            
            
            Console.WriteLine("Starting AddRemove test");
            
            stopwatch.Start();
            for (int i = 0; i < testIterations; i++)
            {
                list.Add(i);
                list.RemoveAt(0);
            }
            stopwatch.Stop();
            Console.WriteLine("AddRemove test completed");
            long addRemoveAtTestTime = stopwatch.ElapsedMilliseconds;
            stopwatch.Reset();

            Console.WriteLine($"Add test took: {addTestTime} milliseconds");
            Console.WriteLine($"RemoveAt test took: {removeAtTestTime} milliseconds");
            Console.WriteLine($"AddRemoveAt test took: {addRemoveAtTestTime} milliseconds");
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

