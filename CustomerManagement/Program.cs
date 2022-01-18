using System;
using System.Net.Http.Headers;
using TurboCollections;

namespace CustomerManagement
{
    class Program
    {
        static void Main(string[] args)
        {
            var turboList = new TurboList<float>();

            CustomerManager customerManager = new CustomerManager();
            customerManager.Run();
        }
    }

    class CustomerManager
    {
        enum Mode
        {
            Add,
            RemoveByName,
            RemoveByIndex,
            Display
        }

        private TurboList<string> Customers = new TurboList<string>();

        public void Run()
        {
            while (true)
            {
                Mode currentMode = SelectOption();

                switch (currentMode)
                {
                    case Mode.Add:
                        AddCustomer();
                        break;
                    case Mode.RemoveByName:
                        RemoveCustomerByName();
                        break;
                    case Mode.RemoveByIndex:
                        RemoveCustomerByIndex();
                        break;
                    case Mode.Display:
                        DisplayAllCustomers();
                        break;
                }
            }
        }

        Mode SelectOption()
        {
            while (true)
            {
                Console.WriteLine("Choose one option:");
                Console.WriteLine("(1) Add a Customer");
                Console.WriteLine("(2) Remove a Customer by name");
                Console.WriteLine("(3) Remove a Customer by index");
                Console.WriteLine("(4) Display all Customers");

                switch (Console.ReadLine())
                {
                    case "1":
                        return Mode.Add;
                    case "2":
                        return Mode.RemoveByName;
                    case "3":
                        return Mode.RemoveByIndex;
                    case "4":
                        return Mode.Display;
                    default:
                        Console.WriteLine("Invalid input. Please try again.");
                        break;
                }
            }
        }

        void AddCustomer()
        {
            Console.Write("Please enter name of customer to add: ");
            string name = Console.ReadLine() ?? "NoName";
            Customers.Add(name);
            Console.WriteLine($"Added Customer {name} at index {Customers.IndexOf(name)}");
        }

        void RemoveCustomerByName()
        {
            Console.WriteLine("Please enter name of customer to remove: ");
            Customers.Remove(Console.ReadLine() ?? "NoName");
        }

        void RemoveCustomerByIndex()
        {
            Console.WriteLine("Please enter index of customer to remove: ");

            int index = 0;
            while (true)
            {
                try
                {
                    index = Convert.ToInt32(Console.ReadLine());
                    break;
                }
                catch
                {
                    Console.WriteLine("Error! Please enter a number!");
                }
            }

            Console.WriteLine($"Removing customer at index {index}");
            Customers.RemoveAt(index);
        }

        void DisplayAllCustomers()
        {
            foreach (var customer in Customers)
            {
                
            }
        }
    }
}