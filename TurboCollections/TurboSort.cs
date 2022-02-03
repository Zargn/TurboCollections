namespace TurboCollections.Sorting
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



        /// <summary>
        /// Holds a value and that values index for use in quicksort pivots.
        /// </summary>
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
        
        

        /// <summary>
        /// Quicksort algorithm.
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public static TurboList<int> QuickSort(TurboList<int> list)
        {
            if (list.Count <= 1)
            {
                return list;
            }


            // TODO: Optimise this further. Only do the index calculations once for example.
            pivotElement[] pivots =
            {
                new (list.Get(0), 0),
                new (list.Get(list.Count / 2), list.Count / 2),
                new (list.Get(list.Count - 1), list.Count - 1)
            };
            
            // Half sort the pivots.
            for (int i = 0; i < 2; i++)
                if (pivots[i].value > pivots[i + 1].value)
                    (pivots[i], pivots[i + 1]) = (pivots[i + 1], pivots[i]);

            // Select the median pivot.
            var selectedPivotElement = pivots[0].value > pivots[1].value ? pivots[0] : pivots[1];
            var pivotPoint = selectedPivotElement.value;
            list.RemoveAt(selectedPivotElement.index);

            // Split the array at the pivot.
            TurboList<int> low = new();
            TurboList<int> high = new();
            for (int i = 0; i < list.Count; i++)
            {
                var item = list.Get(i);
                if (item < pivotPoint)
                {
                    low.Add(item);
                }
                else
                {
                    high.Add(item);
                }
            }
            
            low = QuickSort(low);
            high = QuickSort(high);

            list.Clear();
            list.AddRange(low);
            list.Add(pivotPoint);
            list.AddRange(high);
            
            return list;
        }

        // 3. Bonus: Add a SelectionSort(TurboList<int>)-Method that sorts the List using SelectionSort.
    }
}