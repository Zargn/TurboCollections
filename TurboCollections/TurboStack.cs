using System;
using System.Collections;
using System.Collections.Generic;

namespace TurboCollections
{
    public class TurboStack<T> : IEnumerable<T>
    {
        private T[] items = new T[2];



        // returns the current amount of items contained in the stack.
        public int Count
        {
            get;
            private set;
        }
        
        
        
        // adds one item on top of the stack.
        public void Push(T item)
        {
            if (Count == items.Length)
            {
                ReSize();
            }

            items[Count] = item;
            Count++;
        }


        // returns the item on top of the stack without removing it.
        public T Peek()
        {
            if (Count == 0)
                throw new System.Exception("Stack is empty so no elements are available to read!");
            
            return items[Count - 1];
        }


        // returns the item on top of the stack and removes it at the same time.
        public T Pop()
        {
            if (Count == 0)
                throw new System.Exception("Stack is empty so no elements are available to read!");

            Count--;
            var result = items[Count];
            items[Count] = default(T);
            return result;
        }


        // removes all items from the stack.
        public void Clear()
        {
            items = Array.Empty<T>();
            Count = 0;
        }
        
        

        void ReSize()
        {
            var result = new T[items.Length * 2];
            for (int i = 0; i < Count; i++)
            {
                result[i] = items[i];
            }

            items = result;
        }
    
        
        
        // gets the iterator for this collection. Used by IEnumerable<T>-Interface to support foreach.
        // IEnumerator<T> IEnumerable<T>.GetEnumerator();
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
    }
}