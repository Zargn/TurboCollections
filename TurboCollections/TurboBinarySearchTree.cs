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
             */
            
            
            // TODO instructions: 
            
            // If the Node has no children: Just remove it.

            // If the Node has one child: Replace the node with the child node.
            
            // Else: Search either for the Maximum of the left sub-tree (go left and then always right until you find a
            // leaf) or the Minimum of the right sub-tree (go right and then always left) and replace the node with the
            // leaf you just found.
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