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



        public static TurboList<int> QuickSort(TurboList<int> list)
        {
            return list;
        }





        // 2. Add a QuickSort(TurboList<int>)-Method that sorts the List using Quicksort.
        
        // 3. Bonus: Add a SelectionSort(TurboList<int>)-Method that sorts the List using SelectionSort.
    }
}