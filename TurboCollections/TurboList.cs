using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace TurboCollections
{
    public class TurboList<T> : IEnumerable<T>
    {
        private T[] items = new T[4];

        
        
        /// <summary>
        /// Returns the current amount of elements in this list.
        /// </summary>
        public int Count
        {
            get;
            private set;
        }



        /// <summary>
        /// Add one element at the end of the list.
        /// </summary>
        /// <param name="item">element to add</param>
        public void Add(T item)
        {
            Count++;
            ReSizeToTarget(Count);
            items[Count - 1] = item;
        }
        
        

        /// <summary>
        /// Get the element at the provided index.
        /// </summary>
        /// <param name="index">Index of element to return</param>
        /// <returns></returns>
        /// <exception cref="Exception">Throws a exception if the index provided is outside the bounds of the list</exception>
        public T Get(int index)
        {
            if (index > Count || index < 0)
                throw new System.Exception("Requested index was out of range of the list!");
            
            return items[index];
        }

        
        
        /// <summary>
        /// Replace the element at selected index with another.
        /// </summary>
        /// <param name="index">Target index</param>
        /// <param name="value">Element to set at the index</param>
        /// <exception cref="Exception">Throws a exception if the index provided is outside the bounds of the list</exception>
        public void Set(int index, T value)
        {
            if (index > Count || index < 0)
                throw new System.Exception("Requested index was out of range of the list!");

            items[index] = value;
        }
        
        
        
        /// <summary>
        /// Removes all elements from the list.
        /// </summary>
        public void Clear()
        {
            items = new T[4];
            Count = 0;
        }

        
        
        /// <summary>
        /// Removes the element at provided index.
        /// </summary>
        /// <param name="index">Selected index.</param>
        /// <exception cref="Exception">Throws a exception if the index provided is outside the bounds of the list</exception>
        public void RemoveAt(int index)
        {
            if (index > Count || index < 0)
                throw new System.Exception("Requested index was out of range of the list!");

            for (int i = index; i < Count - 1; i++)
            {
                items[i] = items[i + 1];
            }

            Count--;
        }

        

        /// <summary>
        /// Checks if the element provided exists inside the list.
        /// </summary>
        /// <param name="searchItem">Element to look for</param>
        /// <returns>Boolean based on if the element could be found or not</returns>
        public bool Contains(T searchItem)
        {
            return IndexOf(searchItem) != -1;
        }
        
        

        /// <summary>
        /// Checks if the element provided exists in the list, and returns it's index if found.
        /// </summary>
        /// <param name="searchItem">Element to look for</param>
        /// <returns>int based on the index of the element found, or -1 if it was not found</returns>
        public int IndexOf(T searchItem)
        {
            for (int i = 0; i < Count; i++)
            {
                if (items[i].Equals(searchItem))
                {
                    return i;
                }
            }

            return -1;
        }
        
        

        /// <summary>
        /// Removes the provided element from the list if it can be found.
        /// </summary>
        /// <param name="item">Element to look for</param>
        /// <returns>Boolean representing if it was successful</returns>
        public bool Remove(T item)
        {
            if (IndexOf(item) != -1)
            {
                RemoveAt(IndexOf(item));
                return true;
            }

            return false;
        }
        
        
        
        /// <summary>
        /// Adds multiple elements to the list.
        /// </summary>
        /// <param name="itemsToAdd">Array of elements to add</param>
        public void AddRange(IEnumerable<T> itemsToAdd)
        {
            ReSizeToTarget(Count + itemsToAdd.Count());

            foreach (var item in itemsToAdd)
            {
                items[Count] = item;
                Count++;
            }
        }



        /// <summary>
        /// Resizes the array to fit within the bounds specified in the arguments.
        /// </summary>
        /// <param name="targetLenght">The needed length for the internal array</param>
        private void ReSizeToTarget(int targetLenght)
        {
            if (targetLenght <= items.Length)
                return;

            int sizeToAdd = 0;
            while (targetLenght > items.Length + sizeToAdd)
            {
                sizeToAdd = (items.Length + sizeToAdd) * 2;
            }

            var result = new T[items.Length + sizeToAdd];
            for (int i = 0; i < Count - 1; i++)
            {       
                result[i] = items[i];
            }

            items = result;
        }
        
        
        
        public Enumerator GetEnumerator()
        {
            return new Enumerator(items, Count);
        }

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            return GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }



        public struct Enumerator : IEnumerator<T>
        {
            private readonly T[] items;
            private readonly int count;
            private int index;

            public Enumerator(T[] items, int count)
            {
                this.items = items;
                this.count = count;
                this.index = -1;
            }

            
            // Advances the enumerator to the next element of the enumeration and
            // returns a boolean indicating whether an element is available. Upon
            // creation, an enumerator is conceptually positioned before the first
            // element of the enumeration, and the first call to MoveNext
            // brings the first element of the enumeration into view.
            public bool MoveNext()
            {
                if (index >= count)
                    return false;
                return ++index < count;
            }
            
            

            // Returns the current element of the enumeration. The returned value is
            // undefined before the first call to MoveNext and following a
            // call to MoveNext that returned false. Multiple calls to
            // GetCurrent with no intervening calls to MoveNext
            // will return the same object.
            public T Current => items[index];
            
            
            
            // Resets the enumerator to the beginning of the enumeration, starting over.
            // The preferred behavior for Reset is to return the exact same enumeration.
            // This means if you modify the underlying collection then call Reset, your
            // IEnumerator will be invalid, just as it would have been if you had called
            // MoveNext or Current.
            public void Reset()
            {
                index = -1;
            }

            
            
            object IEnumerator.Current => Current;

            public void Dispose()
            {
                Reset();
            }
        }
    }
}