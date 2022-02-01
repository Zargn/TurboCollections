using System;

namespace TurboCollections
{
    public static class TurboSort
    {
        /// <summary>
        /// Bad example of a BubbleSort algorithm. Takes a list of ints and sorts it to be lowest to highest.
        /// </summary>
        /// <param name="list">list to sort</param>
        /// <returns>sorted list</returns>
        public static TurboList<int> BubbleSort(TurboList<int> list)
        {
            bool swapPerformed = true;
            while (swapPerformed)
            {
                swapPerformed = false;
                for (int i = 0; i < list.Count - 1; i++)
                {
                    if (list.Get(i) > list.Get(i + 1))
                    {
                        var itemCache = list.Get(i);
                        list.Set(i, list.Get(i + 1));
                        list.Set(i + 1, itemCache);
                        swapPerformed = true;
                    }
                }
            }

            return list;
        }

        /*
         
        list Sorter(list)
        if the list count is less or equal to 1
                return the list
        calculate the median of the first, middle, and last element
        create two empty lists. Low and High
        for each element in the list:
                If selected element is less than pivot
                        Move it to the Low list
                else
                        Move it to the High list
        run the Sorter() on both the High and Low list.
        Add the lists back together, Low first, High after
        return the now sorted list
         * 
        */
        private static int nextId = 0;



        struct pivotElement
        {
            public int value;
            public int index;

            public pivotElement(int value, int index)
            {
                this.value = value;
                this.index = index;
            }
        }

        public static TurboList<int> QuickSort(TurboList<int> list)
        {
            
            // TODO:
            // The sorter works only under VERY specific circumstances. Otherwise it breaks into a horrible mess.
            // Start by looking at the main for loop. Something is very wrong here, but where.
            // Check the different pivot point calculations. Why does the two first one cause the error?
            // Look at examples online. What do they do different?




            int id = nextId;
            nextId++;

            Console.WriteLine($"{id} Starting");



            if (list.Count <= 1)
            {
                Console.WriteLine($"{id} List is empty or has one element only. (Returned list)");
                return list;
            }

   
            var pivotPoint = 0;
            
            
            // TODO: Optimise this further. Only do the index calculations once for example.
            pivotElement[] pivots =
            {
                new (list.Get(0), 0),
                new (list.Get(list.Count / 2), list.Count / 2),
                new (list.Get(list.Count - 1), list.Count - 1)
            };


            for (int i = 0; i < 2; i++)
            {
                if (pivots[i].value > pivots[i + 1].value)
                {
                    (pivots[i], pivots[i + 1]) = (pivots[i + 1], pivots[i]);
                }
            }

            var selectedPivotElement = pivots[0].value > pivots[1].value ? pivots[0] : pivots[1];
            pivotPoint = selectedPivotElement.value;
            list.RemoveAt(selectedPivotElement.index);


            Console.WriteLine($"{id} Pivot is: {pivotPoint}");

            // TurboList<int> low = new(), high = new();
            TurboList<int> low = new();
            TurboList<int> high = new();
            for (int i = 0; i < list.Count; i++)
            {
                Console.WriteLine($"{id} Checking {list.Get(i)}");
                // TODO: Optimisation: Only get the element once.
                if (list.Get(i) < pivotPoint)
                {
                    Console.WriteLine($"{id} Added {list.Get(i)} to low");
                    low.Add(list.Get(i));
                }
                else
                {
                    Console.WriteLine($"{id} Added {list.Get(i)} to high");
                    high.Add(list.Get(i));
                }
            }

            // if (low.Count != 0)
            // {
            //     low = QuickSort(low);
            // }
            //
            // if (high.Count != 0)
            // {
            //     high = QuickSort(high);
            // }

            // Console.WriteLine($"{id} Calling new sorter for list low");
            low = QuickSort(low);
            // Console.WriteLine($"{id} sorted low list returned");
            // Console.WriteLine($"{id} calling new sorter for list high");
            high = QuickSort(high);
            // Console.WriteLine($"{id} sorted high list returned");
            
            Console.WriteLine($"{id} Updating result list");

            Console.WriteLine("Will add together:");
            Console.WriteLine("low list:");
            foreach (var VARIABLE in low)
            {
                Console.WriteLine(VARIABLE);
            }

            Console.WriteLine("pivot point:");
            Console.WriteLine(pivotPoint);

            Console.WriteLine("high list:");
            foreach (var VARIABLE in high)
            {
                Console.WriteLine(VARIABLE);
            }

            Console.WriteLine();
            
            
            list.Clear();
            list.AddRange(low);
            // Console.WriteLine("Added " + pivotPoint);
            list.Add(pivotPoint);
            list.AddRange(high);
            
            foreach (var VARIABLE in list)
            {
                Console.WriteLine(VARIABLE);
            }

            Console.WriteLine($"{id} Returning sorted list!");
            return list;
        }





        // 2. Add a QuickSort(TurboList<int>)-Method that sorts the List using Quicksort.
        
        // 3. Bonus: Add a SelectionSort(TurboList<int>)-Method that sorts the List using SelectionSort.
    }
}