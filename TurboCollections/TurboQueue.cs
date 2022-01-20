using System.Collections;

namespace TurboCollections
{
    public class TurboQueue<T> : IEnumerable<T>
    {
        private T[] items = new T[4];

        public int Count { get; private set; }

        /// <summary>
        /// This holds the current offset to apply so you get the correct data when peeking or Dequeueing.
        /// </summary>
        private int startIndexOffset = 0;



        // adds one item to the back of the queue.
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



        // returns the item in the front of the queue without removing it.
        public T Peek()
        {
            if (Count == 0)
                throw new System.Exception("Queue is empty! There is nothing to return!");

            return items[startIndexOffset];
        }



        // returns the item in the front of the queue and removes it at the same time.
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
        public void Clear()
        {
            startIndexOffset = 0;
            Count = 0;
            items = Array.Empty<T>();
        }



        /// <summary>
        /// Resizes the internal array to fit the new target lenght.
        /// </summary>
        /// <param name="targetLenght"></param>
        void ReSizeToFit(int targetLenght)
        {
            ShiftArrayToStart();

            int newLenght = items.Length;
            while (newLenght < targetLenght)
            {
                newLenght *= 2;
            }

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

            int resetToZeroOffset = 0;
            for (int i = 0; i < Count; i++)
            {
                if (i + startIndexOffset == items.Length)
                    resetToZeroOffset = items.Length * -1;
                result[i] = items[i + startIndexOffset + resetToZeroOffset];
            }

            items = result;
        }




        // --------------- optional ---------------
        // gets the iterator for this collection. Used by IEnumerable<T>-Interface to support foreach.

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
            
            public bool MoveNext()
            {
                if (index >= count)
                    return false;
                index++;
                if (index + startPositionOffset + backToStartOfArrayOffset >= items.Length)
                    backToStartOfArrayOffset = items.Length * -1;
                return index < count;
            }

            public void Reset()
            {
                index = -1;
                backToStartOfArrayOffset = 0;
            }

            public T Current => items[index + startPositionOffset + backToStartOfArrayOffset];

            object IEnumerator.Current => Current;

            public void Dispose()
            {
                Reset();
            }
        }
    }
}