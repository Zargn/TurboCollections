using System;

namespace TurboCollections
{
    public class TurboBinarySearchTree<T> where T : IComparable<T>
    {
        private T[] items = new T[4];

        public int Count { get; private set; }

        

        /// <summary>
        /// Returns index of items if it can be found in the tree.
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public int Search(T item)
        {
            if (items[0].Equals(item))
                return 0;

            int searchIndex = 0;
            while (true)
            {
                if (item.CompareTo(items[searchIndex]) < 0)
                {
                    searchIndex = searchIndex * 2 + 1;
                }
                else
                {
                    searchIndex = searchIndex * 2 + 2;
                }

                if (searchIndex >= items.Length)
                    return -1;

                if (items[searchIndex].Equals(item))
                    return searchIndex;
            }
        }



        /// <summary>
        /// Inserts a item into the tree. Will automatically place it at the correct location.
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public int Insert(T item)
        {
            // I don't like repeating this here but it is what it is with the time I have.

            if (item.Equals(default(T)))
                throw new System.Exception("Default value of type is not supported by this Tree.");
            
            if (Count == 0)
            {
                Count++;
                items[0] = item;
                return 0;
            }

            int searchIndex = 0;
            while (true)
            {
                if (item.CompareTo(items[searchIndex]) < 0)
                {
                    searchIndex = searchIndex * 2 + 1;
                }
                else
                {
                    searchIndex = searchIndex * 2 + 2;
                }
                
                if (searchIndex >= items.Length)
                    EnsureSize(items.Length + 1);
                
                if (items[searchIndex].Equals(default(T)))
                {
                    break;
                }
            }

            items[searchIndex] = item;
            Count++;
            
            return searchIndex;
        }



        public bool Delete(T item)
        {
            var itemIndex = Search(item);

            if (itemIndex == -1)
                return false;

            if (itemIndex * 2 + 1 > items.Length)
            {
                items[itemIndex] = default(T);
                return true;
            }

            // Since the array size is doubled, adding one layer to the tree every resize, we only need to check if itemIndex is larger than items.length once.
            int children = 0;
            int childIndex = 0;
            if (!items[itemIndex * 2 + 1].Equals(default(T)))
            {
                childIndex = itemIndex * 2 + 1;
                children++;
            }
            if (!items[itemIndex * 2 + 2].Equals(default(T)))
            {
                childIndex = itemIndex * 2 + 2;
                children++;
            }

            if (children == 0)
            {
                // If it doesn't have any children then we simply remove it.
                items[itemIndex] = default(T);
                return true;
            }

            if (children == 1)
            {
                var scanIndex = childIndex;
                // Replace the deleted object with the child tree. Divide each index by 2?
                ReInsertItem(childIndex);
                return true;
            }

            if (children == 2) // todo cleanup
            {
                var currentIndex = itemIndex * 2 + 1;
                var nextIndex = currentIndex;
                while (currentIndex * 2 + 2 < items.Length)
                {
                    nextIndex = nextIndex * 2 + 2;

                    if (items[nextIndex].Equals(default(T)))
                        break;

                    currentIndex = nextIndex;
                }

                items[itemIndex] = items[currentIndex];
                items[currentIndex] = default(T);
                return true;
            }
            
            
            #region todo
            // Remove item:
            /*
             * if (itemindex * 2 + 1 > items.length)
             *      Then node has no children. Remove it.
             *
             * Since we double the size of the array every resize we don't need to worry that itemindex * 2 + 2 is
             * outside the limits of the array.
             * int children;
             * if (items[itemindex * 2 + 1] != default)
             *      children ++
             * if (items[itemindex * 2 + 2] != default)
             *      children ++
             *
             * if (children == 0)
             *      Node has no children. Remove it.
             *
             * if (children = 1)
             *      replace node with child.
             *
             * if (children = 2)
             *      go through the left child tree looking for the leaf furthest to the right.
             *      index = itemIndex * 2 + 1;
             *      while (true)
             *          index = index * 2 + 2;
             *          if (index is outside array)
             *              move item at index to deleted items index at itemIndex;
             *              return;
             *          
             */
            #endregion
            // TODO instructions: 

            // If the Node has no children: Just remove it.

            // If the Node has one child: Replace the node with the child node.

            // Else: Search either for the Maximum of the left sub-tree (go left and then always right until you find a
            // leaf) or the Minimum of the right sub-tree (go right and then always left) and replace the node with the
            // leaf you just found.

            return false;
        }



        void ReInsertItem(int index)
        {
            var targetIndexLeft = index * 2 + 1;
            
            if (targetIndexLeft >= items.Length)
            {
                return;
            }

            // Reinsert the item in the tree.
            var cache = items[index];
            items[index] = default(T);
            Insert(cache);
            
            var targetIndexRight = index * 2 + 2;
            
            if (!items[targetIndexLeft].Equals(default(T)))
            {
                ReInsertItem(targetIndexLeft);
            }

            if (!items[targetIndexRight].Equals(default(T)))
            {
                ReInsertItem(targetIndexRight);
            }
        }



        /// <summary>
        /// Beginning of prototype of moving items manually instead of using insert. Might offer better performance if done well.
        /// </summary>
        /// <param name="index"></param>
        void MoveSubTreeUp(int index)
        {
            var targetIndexLeft = index * 2 + 1;
            
            if (targetIndexLeft >= items.Length)
            {
                return;
            }
            
            var targetIndexRight = index * 2 + 2;
            
            if (!items[targetIndexLeft].Equals(default(T)))
            {
                items[index] = items[targetIndexLeft];
                items[targetIndexLeft] = default(T);
                MoveSubTreeUp(targetIndexLeft);
            }
            if (!items[targetIndexRight].Equals(default(T)))
            {
                items[index + 1] = items[targetIndexLeft];
                items[targetIndexLeft] = default(T);
                MoveSubTreeUp(targetIndexLeft);
            }
        }
        



        /// <summary>
        /// Ensure that the size of the internal array can fit all items.
        /// </summary>
        /// <param name="sizeIncrease">new size</param>
        private void EnsureSize(int targetSize)
        {
            var currentSize = items.Length;
            
            if (currentSize > targetSize)
                return;


            while (currentSize < targetSize)
            {
                currentSize *= 2;
            }

            Console.WriteLine("New size: " + currentSize);
            
            T[] result = new T[currentSize];
            for (int i = 0; i < items.Length; i++)
            {
                result[i] = items[i];
            }

            items = result;
        }



        // TODO:
        /*
            - Insert, Search, Delete,
            - GetEnumerator: returns all items in order, from min to max
            - GetInOrder: same as GetEnumerator
            - GetInReverseOrder: returns all items in reverse order, from max to min
         */
    }
}