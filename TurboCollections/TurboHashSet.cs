namespace TurboCollections
{
    // TODO:
    /*
     * Look into prime numbers as length of internal array. How would I calculate the next one?
     */

    public class TurboHashSet<T>
    {
        private struct container
        {
            public T item;
            
            public int nextIndex = -2;

            public bool hasItem;

            public container(T item)
            {
                this.item = item;
                nextIndex = -1;
                hasItem = true;
            }
        }


        private const double TableToTotalLenghtRatio = 0.86;
        private int cellarStartIndex;
        
        
        private container[] items = new container[48];
        
        public int Count { get; private set; }

        
        // Automatically set the cellarStartIndex on instantiation.
        public TurboHashSet()
        {
            cellarStartIndex = (int) (items.Length * TableToTotalLenghtRatio + 1);
        }
        


        /// <summary>
        /// Insert the item into the hashset.
        /// </summary>
        /// <param name="item">item to insert</param>
        /// <returns>False if item was present already, and true if it was added.</returns>
        public bool Insert(T item)
        {
            /* PseudoCode
             * Get index from item.
             *
             * Repeat three times:
             * {
             * Check if index holds a item.
             * if it does:
             *      Check if index holds same item.
             *      if it does:
             *          return false.
             *      if not:
             *          Check if item at index has a nextIndex value.
             *          if it does:
             *              Check if the container at nextIndex holds a item.
             * if not && we do not have a remembered index:
             *      remember index for later.
             * }    
             */

            var hashIndex = GetIndexFromHash(item);
            int oldIndex = 0;

            // Check if the index in the table is free.
            if (!items[hashIndex].hasItem)
            {
                items[hashIndex] = new container(item);
                Count++;
                return true;
            }
                
            // If it was occupied then scan the chain.
            while (items[hashIndex].hasItem)
            {
                if (Equals(items[hashIndex].item, item))
                {
                    return false;
                }
            
                if (items[hashIndex].nextIndex != -1)
                {
                    hashIndex = items[hashIndex].nextIndex;
                }
                else
                {
                    oldIndex = hashIndex;
                    break;
                }
            }

            // Select location for the item.
            for (int i = cellarStartIndex; i < items.Length; i++)
            {
                if (!items[i].hasItem)
                {
                    items[i] = new container(item);
                    items[oldIndex].nextIndex = i;
                    Count++;
                    return true;
                }
            }
            
            // If we reach this part then that means the item wasn't found, but it also didn't fit in the cellar.
            Resize();
            return Insert(item);

            /* PseudoCode OLD
             * 1. Pick the index by doing item.hashcode %[Modulo] hashTableSize[items.length]
             * 2. Check if that index has the default value && we haven't "remembered" any index in the iterations before.
             * 3. if it has: Then remember that index.
             * 4. if not: Check if that item is the same as the adding one.
             * 5.     if it is: return true.
             * 6.     if not: Attempt from step two with the next index in the array a maximum of two times.
             * 7. increase the index by one (If using the standard collision resolution method)
             * 7. Repeat step 2 to 7, 3 times.
             * 8. if no index was remembered then resize the array and insert the item again.
             * 9. if a index was remembered then add the item to that index.
             */
        }



        /// <summary>
        /// Search the array for the item.
        /// </summary>
        /// <param name="item">item to search for</param>
        /// <returns>bool representing whether it was found or not</returns>
        public bool Exists(T item)
        {
            var hashIndex = GetIndexFromHash(item);
            
            while (items[hashIndex].hasItem)
            {
                if (Equals(items[hashIndex].item, item))
                {
                    return true;
                }
            
                if (items[hashIndex].nextIndex != -1)
                {
                    hashIndex = items[hashIndex].nextIndex;
                }
                else
                {
                    break;
                }
            }

            return false;


            /* PseudoCode
             * 1. Get hashcode from item
             * 2. Get index from hashcode
             * 3. check if the index has the item searched for.
             * 4.       if it is: Return true.
             * 5. increase index using CollisionResolution(index)
             * 6. repeat step 3 to 5 up to a maximum of 3 times.
             * 7. If this step is reached, then the item wasn't found.
             *    therefore return false.
             */

            /* PseudoCode 2
             * 1. Pick the index by doing item.hashcode %[Modulo] hashTableSize[items.length]
             * 2. Check if that index holds a item.
             * 3.   if it does: Check if that item is the one we are looking for.
             * 4.       if it is: Return true.
             * 5.       if not: Attempt from step 2 with the next index in the array, for a maximum of two times.
             * 6.           if more than two tries pass: Return false.
             */
        }

        
        
        /// <summary>
        /// Remove the item from the hashset, if it can be found.
        /// </summary>
        /// <param name="item">item to remove</param>
        /// <returns>bool representing whether it was found and removed or not</returns>
        public bool Remove(T item)
        {
            var hashIndex = GetIndexFromHash(item);

            for (int i = 0; i < 3; i++)
            {
                if (Equals(items[hashIndex].item, item))
                {
                    items[hashIndex] = new container();
                    Count--;
                    return true;
                }

                hashIndex = CollisionResolution(hashIndex);
            }

            return false;

            /* PseudoCode
             * 1. Get hashcode of object.
             * 2. Get index of hash.
             * 3. Check if index holds item
             * 4. if it does:
             * 5.       remove the item.
             * 6.       decrease count.
             * 7.       return true.
             * 8. if it does not:
             * 9.       set index using CollisionResolution(index)
             * 10.       repeat from step 3 up to a maximum of 3 times.
             * 11. if this step is reached then the item was not found.
             * 12. return false.
             */
        }

        
        
        /// <summary>
        /// Resize the internal array to fit more items.
        /// </summary>
        private void Resize()
        {
            var oldItemArray = items;
            items = new container[items.Length * 2];

            Count = 0;
            
            foreach (var item in oldItemArray)
            {
                if (item.hasItem)
                    Insert(item.item);
            }
            
            cellarStartIndex = (int) (items.Length * TableToTotalLenghtRatio + 1);

            /* PseudoCode
             * Create local variable copy of items.
             * resize items[] by making it a new array with double the size.
             * go through the local items array
             * if the item at the slot is not default:
             *      insert() that item.
             */
        }

        
        
        /// <summary>
        /// Checks whether the item can fit by using a collision resolution strategy. If it doesn't, then the internal
        /// array will be resized and then another attempt to insert the item will be made by making a recursive call to
        /// insert at the end of the method.
        /// </summary>
        /// <returns>bool representing if the collision was handled or not</returns>
        private int CollisionResolution(int index)
        {
            index++;
            if (index < cellarStartIndex || index > items.Length)
                index = cellarStartIndex;

            return index;
        }



        /// <summary>
        /// Extract a index using a hash function.
        /// </summary>
        /// <param name="item">item to extract hash from</param>
        /// <returns>hashcode in int form</returns>
        private int GetIndexFromHash(T item)
        {
            if (Equals(item, null))
                return 0;
            
            var itemHash = item.GetHashCode();

            itemHash %= (int) (items.Length * TableToTotalLenghtRatio);
            return itemHash;
        }
    }
}