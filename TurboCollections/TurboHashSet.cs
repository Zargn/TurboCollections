namespace TurboCollections
{
    public class TurboHashSet<T>
    {
        private T[] items = new T[48];
        
        public int Count { get; private set; }
        
        // TODO: This is not needed. Stop doing unneeded optimisations!
        // public TurboHashSet(int startSize = 48)
        // {
        //     items = new T[startSize];
        // }
        
        

        /// <summary>
        /// Insert the item into the hashset.
        /// </summary>
        /// <param name="item">item to insert</param>
        /// <returns>False if item was present already, and true if it was added.</returns>
        public bool Insert(T item)
        {
            if (item.Equals(default(T)))
                throw new System.Exception("Error: Can't add default value object to hashset!");

            var itemHash = GetIndexFromHash(item);
            var targetIndex = -1;

            for (int i = 0; i < 3; i++)
            {
                if (items[itemHash].Equals(default(T)) && targetIndex == -1)
                {
                    targetIndex = itemHash;
                }

                if (items[itemHash].Equals(item))
                {
                    return false;
                }

                itemHash = CollisionResolution(itemHash);
            }

            if (targetIndex == -1)
            {
                Resize();
                return Insert(item);
            }

            Console.WriteLine($"inserted {item} at index: {targetIndex}");
            items[targetIndex] = item;
            Count++;
            return true;

            /* PseudoCode
             * 1. Pick the index by doing item.hashcode %[Modulo] hashTableSize[items.length]
             * 2. Check if that index already has a item.
             * 3. if not: Then add the item in that slot.
             * 4. if it has: Check if that item is the same as the adding one.
             * 5.     if it is: return true.
             * 6.     if not: Attempt from step two with the next index in the array a maximum of two times.
             * 7.        if more than 2 tries is needed: Resize the internal array and then try to add them again.
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

            for (int i = 0; i < 3; i++)
            {
                if (items[hashIndex].Equals(item))
                    return true;
                hashIndex = CollisionResolution(hashIndex);
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
                if (items[hashIndex].Equals(item))
                {
                    items[hashIndex] = default(T);
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
            items = new T[items.Length * 2];

            Count = 0;
            
            foreach (var item in oldItemArray)
            {
                if (!item.Equals(default(T)))
                    Insert(item);
            }

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
            if (index > items.Length) index -= items.Length;
            return index;
        }



        private int GetIndexFromHash(T item)
        {
            var itemHash = item.GetHashCode();

            itemHash %= items.Length;
            return itemHash;
        }
    }
}