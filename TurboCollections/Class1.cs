using System;

namespace TurboCollections
{
    public class TurboList<T>
    {
        private T[] items;

        public int Count => items.Length;

        public void Add(T item)
        {
            items = ReSize(items.Length + 1, items);
            items[^1] = item;
        }

        public T Get(int index)
        {
            if (index > items.Length || index < 0)
                throw new System.Exception("Requested index was out of range of the list!");
            
            return items[index];
        }

        public void Clear(int index)
        {
            items = Array.Empty<T>();
        }

        public void RemoveAt(int index)
        {
            if (index > items.Length || index < 0)
                throw new System.Exception("Requested index was out of range of the list!");

            items[index] = default(T);
        }



        private T[] ReSize(int lenght, T[] array)
        {
            var result = new T[lenght];
            for (int i = 0; i < Math.Min(array.Length, lenght); i++)
            {
                result[i] = array[i];
            }

            return result;
        }

        private T[] ScanAndClean()
        {
            var result = new T[items.Length];
            int currentIndex = 0;
            // for (int i = 0; i < items.Length; i++)
            // {
            //     if (items[i] != )
            //     {
            //         result[currentIndex] = items[i];
            //         currentIndex++;
            //     }
            // }

            return ReSize(currentIndex, result);
        }
    }
}