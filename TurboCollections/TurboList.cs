using System;
using System.Collections;

namespace TurboCollections
{
    public class TurboList<T> : IEnumerable<T>
    {
        private T[] items = new T[4];

        public int Count
        {
            get;
            private set;
        }



        // returns the current amount of items contained in the list.
        public int CountOLD => items.Length;

        
        
        // adds one item to the end of the list.
        public void Add(T item)
        {
            ReSizeToTarget(Count + 1);
            items[Count] = item;
            Count++;
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
            items = new T[4];
            Count = 0;
        }

        
        
        // removes one item from the list. If the 4th item is removed, then the 5th item becomes the 4th, the 6th becomes the 5th and so on.
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
            ReSizeToTarget(Count + itemsToAdd.Count());

            foreach (var item in itemsToAdd)
            {
                items[Count] = item;
                Count++;
            }
        }
        
        
        
        // // gets the iterator for this collection. Used by IEnumerator to support foreach.
        public IEnumerator<T> GetEnumerator()
        {
            T[] result = new T[Count];
            for (int i = 0; i < Count; i++)
            {
                result[i] = items[i];
            }

            IEnumerable<T> enumerable = result;
            return enumerable.GetEnumerator();
        }


        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
        
        

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
            for (int i = 0; i < Count; i++)
            {
                result[i] = items[i];
            }

            items = result;
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

            public bool MoveNext()
            {
                if (index >= count)
                    return false;
                return ++index < count;
            }

            public void Reset()
            {
                index = -1;
            }

            public T Current => items[index];

            object IEnumerator.Current => Current;

            public void Dispose()
            {
                Reset();
            }
        }
    }
}