using System;
using System.Collections.Generic;
using System.Linq;

namespace TurboCollections
{
    public class TurboList<T>
    {
        private T[] items;
        public TurboList()
        {
            items = Array.Empty<T>();
        }



        // returns the current amount of items contained in the list.
        public int Count => items.Length;

        
        
        // adds one item to the end of the list.
        public void Add(T item)
        {
            items = ReSize(items, Count + 1);
            items[^1] = item;
        }

        
        
        // gets the item at the specified index. If the index is outside the correct range, an exception is thrown.
        public T Get(int index)
        {
            if (index > Count || index < 0)
                throw new System.Exception("Requested index was out of range of the list!");
            
            return items[index];
        }

        
        
        // replaces the item at the specified index. If the index is outside the correct range, an exception is thrown.
        public void Set(int index, T value)
        {
            if (index > Count || index < 0)
                throw new System.Exception("Requested index was out of range of the list!");

            items[index] = value;
        }
        
        
        
        // removes all items from the list.
        public void Clear()
        {
            items = Array.Empty<T>();
        }

        
        
        // removes one item from the list. If the 4th item is removed, then the 5th item becomes the 4th, the 6th becomes the 5th and so on.
        public void RemoveAt(int index)
        {
            if (index > Count || index < 0)
                throw new System.Exception("Requested index was out of range of the list!");

            var result = new T[Count - 1];
            var currentIndex = 0;

            for (int i = 0; i < Count; i++)
            {
                if (i != index)
                {
                    result[currentIndex] = items[i];
                    currentIndex++;
                }
            }


            items = result;
        }

        
        
        // returns true, if the given item can be found in the list, else false.
        public bool Contains(T searchItem)
        {
            return IndexOf(searchItem) != -1;
        }
        
        
        
        // returns the index of the given item if it is in the list, else -1.
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
        
        
        
        // removes the specified item from the list, if it can be found.
        public bool Remove(T item)
        {
            if (IndexOf(item) != -1)
            {
                RemoveAt(IndexOf(item));
                return true;
            }

            return false;
        }
        
        
        
        // adds multiple items to this list at once.
        public void AddRange(IEnumerable<T> itemsToAdd)
        {
            int currentIndex = Count;
            items = ReSize(items, Count + itemsToAdd.Count());
            foreach (var item in itemsToAdd)
            {
                items[currentIndex] = item;
                currentIndex++;
            }
        }
        
        
        
        // // gets the iterator for this collection. Used by IEnumerator to support foreach.
        // IEnumerator<T>.GetEnumerator();
        public IEnumerator<T> GetEnumerator()
        {
            IEnumerable<T> enumerable = items;
            return enumerable.GetEnumerator();
        }



        private T[] ReSize(T[] array, int lenght)
        {
            var result = new T[lenght];
            for (int i = 0; i < Math.Min(Count, lenght); i++)
            {
                result[i] = array[i];
            }

            return result;

            
            // Marc's stuff.
            T[] newArray = new T[Count + 1];
            for (int i = 0; i < Count; i++)
            {
                newArray[i] = items[i];
            }

            items = newArray;
        }
    }
}