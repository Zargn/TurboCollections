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
                    EnsureSize(items.Length);
                
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
        /// Ensure that the size of the internal array can fit all items.
        /// </summary>
        /// <param name="sizeIncrease">total size increase</param>
        private void EnsureSize(int sizeIncrease)
        {
            Console.WriteLine("Ensure size!");
            
            var targetSize = items.Length + sizeIncrease;
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