using System;
using System.Collections;
using System.Collections.Generic;

namespace TurboCollections
{
    public class TurboQueue<T> : IEnumerable<T>
    {
        /// <summary>
        /// Internal array for storing the queue items.
        /// </summary>
        private T[] items = new T[4];

        
        
        /// <summary>
        /// Returns the current amount of items in the queue.
        /// </summary>
        public int Count { get; private set; }

        
        
        /// <summary>
        /// This holds the current offset to apply so you get the correct data when peeking or Deq-Queueing.
        /// </summary>
        private int startIndexOffset = 0;


        
        /// <summary>
        /// Add <T>Item to the end of the queue.
        /// </summary>
        /// <param name="item">Item to be added.</param>
        public void Enqueue(T item)
        {
            if (Count == items.Length)
                ReSizeToFit(Count + 1);

            if (Count + startIndexOffset == items.Length)
            {
                items[Count + startIndexOffset - items.Length] = item;
            }
            else
            {
                items[Count] = item;
            }

            Count++;
        }


        
        /// <summary>
        /// </summary>
        /// <returns>The the item at the front of the queue.</returns>
        /// <exception cref="Exception">Queue is empty! There is nothing to return!</exception>
        public T Peek()
        {
            if (Count == 0)
                throw new System.Exception("Queue is empty! There is nothing to return!");

            return items[startIndexOffset];
        }

        
        
        /// <summary>
        /// Removes and returns the item at the front of the queue.
        /// </summary>
        /// <returns>The item at the front of the queue.</returns>
        /// <exception cref="Exception">Queue is empty! There is nothing to remove and return!</exception>
        public T Dequeue()
        {
            if (Count == 0)
                throw new System.Exception("Queue is empty! There is nothing to remove and return!");

            T itemToReturn = items[startIndexOffset];
            items[startIndexOffset] = default(T);
            startIndexOffset++;
            Count--;
            if (startIndexOffset == items.Length)
                startIndexOffset = 0;
            return itemToReturn;
        }



        // removes all items from the queue.
        /// <summary>
        /// Clear the queue to a empty state.
        /// </summary>
        public void Clear()
        {
            startIndexOffset = 0;
            Count = 0;
            items = Array.Empty<T>();
        }



        /// <summary>
        /// Resizes the internal array to fit the new target length.
        /// </summary>
        /// <param name="targetLength"></param>
        void ReSizeToFit(int targetLength)
        {
            ShiftArrayToStart();

            // Create a new array of sufficient size.
            int newLenght = items.Length;
            while (newLenght < targetLength)
            {
                newLenght *= 2;
            }

            // Move all items to the new array.
            T[] result = new T[newLenght];
            for (int i = 0; i < Count; i++)
            {
                result[i] = items[i];
            }
            
            items = result;
        }



        /// <summary>
        /// Shifts the internal array back to index 0.
        /// </summary>
        void ShiftArrayToStart()
        {
            T[] result = new T[items.Length];

            // Go through all the items in correct order.
            int resetToZeroOffset = 0;
            for (int i = 0; i < Count; i++)
            {
                // If we reach the end of the array, loop back and continue at index 0.
                if (i + startIndexOffset == items.Length)
                    resetToZeroOffset = items.Length * -1;
                
                result[i] = items[i + startIndexOffset + resetToZeroOffset];
            }

            items = result;
        }


        
        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>An enumerator that can be used to iterate through the collection.</returns>
        public Enumerator GetEnumerator()
        {
            return new Enumerator(items, Count, startIndexOffset);
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
            private readonly int startPositionOffset;
            private int backToStartOfArrayOffset;
            private int index;
            
            public Enumerator(T[] items, int count, int startPositionOffset)
            {
                this.items = items;
                this.count = count;
                this.startPositionOffset = startPositionOffset;
                this.backToStartOfArrayOffset = 0;
                this.index = -1;
            }
            
            
            
            /// <summary>
            /// Advances the enumerator to the next element of the enumeration.
            /// </summary>
            /// <returns>Returns a boolean indicating whether an element is available.</returns>
            public bool MoveNext()
            {
                if (index >= count)
                    return false;
                index++;
                if (index + startPositionOffset + backToStartOfArrayOffset >= items.Length)
                    backToStartOfArrayOffset = items.Length * -1;
                return index < count;
            }

            
            
            /// <summary>
            /// Resets the enumerator back to the beginning of the enumeration.
            /// </summary>
            public void Reset()
            {
                index = -1;
                backToStartOfArrayOffset = 0;
            }

            
            
            /// <summary>
            /// Returns the current element of the enumeration.
            /// </summary>
            public T Current => items[index + startPositionOffset + backToStartOfArrayOffset];

            
            
            object IEnumerator.Current => Current;

            public void Dispose()
            {
                Reset();
            }
        }
    }
}