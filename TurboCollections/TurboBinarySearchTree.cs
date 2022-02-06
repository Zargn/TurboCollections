using System;
using System.Collections;
using System.Collections.Generic;

namespace TurboCollections
{
    public class TurboBinarySearchTree<T> : IEnumerable<T> where T : IComparable<T>
    {
        // internal item storage array.
        private T[] items = new T[StartArraySize];
        
        
        private const int StartArraySize = 1;
        
        
        
        /// <summary>
        /// Returns current amount of items in the tree
        /// </summary>
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



        /// <summary>
        /// Delete a item from the tree.
        /// </summary>
        /// <param name="item">item to delete</param>
        /// <returns>whether the item was found and removed or not</returns>
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

                var possibleChildIndex = currentIndex * 2 + 1;
                if (possibleChildIndex < items.Length)
                {
                    if (!items[possibleChildIndex].Equals(default(T)))
                    {
                        ReInsertItem(possibleChildIndex);
                    }
                }

                return true;
            }
            
            
            #region Instructions for delete
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
            
            // TODO instructions: 

            // If the Node has no children: Just remove it.

            // If the Node has one child: Replace the node with the child node.

            // Else: Search either for the Maximum of the left sub-tree (go left and then always right until you find a
            // leaf) or the Minimum of the right sub-tree (go right and then always left) and replace the node with the
            // leaf you just found.
            #endregion
            
            return false;
        }


        
        /// <summary>
        /// Inserts the item and calls itself for any possible children of the target item.
        /// </summary>
        /// <param name="index">index of item to reinsert in tree</param>
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
                currentSize = currentSize * 2 + 1;
            }

            Console.WriteLine("New size: " + currentSize);
            
            T[] result = new T[currentSize];
            for (int i = 0; i < items.Length; i++)
            {
                result[i] = items[i];
            }

            items = result;
        }



        /// <summary>
        /// Lazy but effective method. Didn't have time to do the more complex version the teacher wanted but this works.
        /// </summary>
        public void DeleteTree()
        {
            items = new T[StartArraySize];
            Count = 0;
        }



        /// <summary>
        /// Return a array of sorted items in reverse from the tree.
        /// </summary>
        /// <returns>reverse sorted array from tree</returns>
        public T[] GetInReverseOrder()
        {
            T[] result = new T[Count];
            int resultIndex = result.Length - 1;

            foreach (var i in GetItemsInOrder(0))
            {
                result[resultIndex] = i;
                resultIndex--;
            }

            return result;
        }

        
        
        /// <summary>
        /// Returns a array of sorted items from the tree.
        /// </summary>
        /// <returns>sorted array from the tree</returns>
        public T[] GetInOrder()
        {
            T[] result = new T[Count];
            int resultIndex = 0;

            foreach (var i in GetItemsInOrder(0))
            {
                result[resultIndex] = i;
                resultIndex++;
            }

            return result;
        }

        
        
        /// <summary>
        /// Recursive method that returns each element in the tree starting at index sorted from smallest to largest.
        /// </summary>
        /// <param name="index">start index</param>
        /// <returns>IEnumerable with items in correct order</returns>
        private IEnumerable<T> GetItemsInOrder(int index)
        {
            var leftChildIndex = index * 2 + 1;
            if (leftChildIndex >= items.Length)
            {
                Console.WriteLine("Break loop at index: " + index);
                yield return items[index];
            }
            else
            {
                var rightChildIndex = index * 2 + 2;
                if (!items[leftChildIndex].Equals(default(T)))
                {
                    foreach (var item in GetItemsInOrder(leftChildIndex))
                    {
                        Console.WriteLine($"Returning {item}");
                        yield return item;
                    }
                }

                Console.WriteLine($"Returning: {items[index]}");
                yield return items[index];

                if (!items[rightChildIndex].Equals(default(T)))
                {
                    foreach (var item in GetItemsInOrder(rightChildIndex))
                    {
                        Console.WriteLine($"Returning {item}");
                        yield return item;
                    }
                }
            }
        }



        public TurboBinarySearchTree<T> Clone()
        {
            TurboBinarySearchTree<T> result = new();

            for (int i = 0; i < items.Length; i++)
            {
                if (!Equals(items[i], default(T)))
                    result.Insert(items[i]);
            }

            return result;
        }


        // private Enumerator GetEnumerator()
        // {
        //     return new Enumerator(items, Count);
        // }

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            return GetItemsInOrder(0).GetEnumerator();
            // return GetEnumerator();
        }

        // public Enumerator GetEnumerator()
        // {
        //     
        // }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetItemsInOrder(0).GetEnumerator();
            // return GetEnumerator();
        }


        // private struct Enumerator : IEnumerator<T>
        // {
        //     private readonly T[] items;
        //     private readonly int count;
        //     private int index;
        //     private int iterationNumber;
        //
        //     public Enumerator(T[] items, int count)
        //     {
        //         this.items = items;
        //         this.count = count;
        //         this.index = 0;
        //         this.iterationNumber = 0;
        //     }
        //
        //
        //     private int GetNextIndex(int index)
        //     {
        //         int result;
        //         
        //         /*
        //          * While result < items.length
        //          *      Go down and left once.
        //          *      if items[result] == default
        //          *          return result; 
        //          */
        //     }
        //     
        //     
        //     public bool MoveNext()
        //     {
        //         if (iterationNumber >= count)
        //             return false;
        //         
        //         // how to get the next item?
        //         
        //         /*
        //          * Get the first item.
        //          * index = 
        //          *
        //          *
        //          *
        //          *
        //          *
        //          *
        //          *
        //          * 
        //          */
        //         return false;
        //     }
        //
        //     public void Reset()
        //     {
        //         throw new NotImplementedException();
        //     }
        //
        //     public T Current => items[index];
        //
        //     object IEnumerator.Current => Current;
        //
        //     public void Dispose()
        //     {
        //         throw new NotImplementedException();
        //     }
        // }



        #region Prototypes
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
        #endregion
    }
}