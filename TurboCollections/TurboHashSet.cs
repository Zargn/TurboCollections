﻿namespace TurboCollections
{
    public class TurboHashSet<T>
    {
        // TODO list:
        // Look into and decide on a collision resolution strategy.
        
        // Implement a Bool Insert(T item) method.
        // Implement a Bool Exists(T item) method.
        // Implement a Bool Remove(T item) method.
        
        // Implement a internal Resize() method.
        
        
        
        
        
        private T[] items;
        
        public int Count { get; private set; }
        
        public TurboHashSet(int startSize = 48)
        {
            items = new T[startSize];
        }



        /// <summary>
        /// Insert the item into the hashset.
        /// </summary>
        /// <param name="item">item to insert</param>
        /// <returns>bool representing if the item was already inside</returns>
        public bool Insert(T item)
        {
            return false;
        }

        
        
        /// <summary>
        /// Search the array for the item.
        /// </summary>
        /// <param name="item">item to search for</param>
        /// <returns>bool representing whether it was found or not</returns>
        public bool Exists(T item)
        {
            return false;
        }

        
        
        /// <summary>
        /// Remove the item from the hashset, if it can be found.
        /// </summary>
        /// <param name="item">item to remove</param>
        /// <returns>bool representing whether it was found and removed or not</returns>
        public bool Remove(T item)
        {
            return false;
        }

        
        
        /// <summary>
        /// Resize the internal array to fit more items.
        /// </summary>
        private void Resize()
        {
            
        }

        
        
        /// <summary>
        /// Checks whether the item can fit by using a collision resolution strategy. If it doesn't, then the internal
        /// array will be resized and then another attempt to insert the item will be made by making a recursive call to
        /// insert at the end of the method.
        /// </summary>
        /// <returns>bool representing if the collision was handled or not</returns>
        private bool CollisionHandler()
        {
            return false;
        }
    }
}