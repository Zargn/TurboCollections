namespace TurboCollections
{
    public static class TurboSort
    {
        // 1. Add a BubbleSort(TurboList<int>)-Method that sorts the List using Bubblesort.
        // procedure bubbleSort( list : array of items )
        //
        // loop = list.count;
        //
        // for i = 0 to loop-1 do:
        // swapped = false
		      //
        // for j = 0 to loop-1 do:
        //
        // /* compare the adjacent elements */   
        // if list[j] > list[j+1] then
        //     /* swap them */
        //     swap( list[j], list[j+1] )		 
        // swapped = true
        // end if
        //  
        // end for
        //
        // /*if no number was swapped that means 
        // array is sorted now, break the loop.*/
        //
        // if(not swapped) then
        // break
        // end if
        //
        // end for
        //
        // end procedure return list

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






        // 2. Add a QuickSort(TurboList<int>)-Method that sorts the List using Quicksort.
        
        // 3. Bonus: Add a SelectionSort(TurboList<int>)-Method that sorts the List using SelectionSort.
    }
}