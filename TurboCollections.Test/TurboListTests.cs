using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace TurboCollections.Test
{
    public class TurboListTests
    {
        TurboList<T> GetList<T>() {
            return new TurboList<T>();
        }

        int GetListCount<T>(TurboList<T> inputList) {
            return inputList.Count;
        }

        TurboList<T> AddElementsToList<T>(TurboList<T> inputList, T item) {
            inputList.Add(item);
            return inputList;
        }

        TurboList<T> SetElementAtIndexInList<T>(TurboList<T> inputList, int index ,T item) {
            inputList.Set(index, item);
            return inputList;
        }

        TurboList<T> ClearList<T>(TurboList<T> inputList) {
            inputList.Clear();
            return inputList;
        }
        
        TurboList<T> RemoveAtIndexInList<T>(TurboList<T> inputList, int index) {
            inputList.RemoveAt(index);
            return inputList;
        }
        
        bool ListContains<T>(TurboList<T> inputList, T item) {
            return inputList.Contains(item);
        }
        
        int IndexOfInList<T>(TurboList<T> inputList, T item) {
            return inputList.IndexOf(item);
        }
        
        TurboList<T> RemoveItemFromList<T>(TurboList<T> inputList, T item) {
            inputList.Remove(item);
            return inputList;
        }
        
        TurboList<T> AddRangeOfElementsToList<T>(TurboList<T> inputList, IEnumerable<T> items) {
            inputList.AddRange(items);
            return inputList;
        }


        [Test]
        public void NewListIsEmpty()
        {
            var list = GetList<int>();
            Assert.Zero(GetListCount(list));
        }
        
                    
            
        // var list = new TurboList<int>();
        // Assert.Zero(list.Count);
        
        [Test, TestCase(1), TestCase(4), TestCase(42)]
        public void AddingAnElementIncreases(int numberOfElements)
        {
            var list = new TurboList<int>();
            for (int i = 0; i < numberOfElements; i++)
                list.Add(5);
            Assert.AreEqual(numberOfElements,list.Count);
        }
        
        [Test]
        public void AddedElementGoesInCorrectIndex()
        {
            var list = new TurboList<int>();
            list.Add(5);
            list.Add(3);
            Assert.AreEqual(3, list.Get(1));
        }
        
        [Test]
        public void GetCommandReturnsCorrectItem()
        {
            var list = new TurboList<int>();
            list.Add(4);
            Assert.AreEqual(4, list.Get(0));
        }
        
        [Test]
        public void ClearRemovesEverythingFromList()
        {
            var list = new TurboList<int>();
            list.Add(3);
            list.Add(4);
            list.Add(5);
            list.Clear();
            Assert.Zero(list.Count);
        }
        
        [Test]
        public void RemoveAtCorrectlyRemovesItemFromIndex()
        {
            var list = new TurboList<int>();
            list.Add(3);
            list.Add(4);
            list.Add(5);
            list.Add(42);
            list.RemoveAt(1);
            Assert.AreNotEqual(4,list.Get(1));
        }
        
        [Test]
        public void RemoveAtCorrectlyResizesList()
        {
            var list = new TurboList<int>();
            list.Add(3);
            list.Add(4);
            list.Add(5);
            list.Add(5);
            list.RemoveAt(2);
            Assert.AreEqual(3,list.Count);
        }
        
        [Test]
        public void ContainsReturnsTrueIfItemPresent()
        {
            var list = new TurboList<int>();
            list.Add(3);
            list.Add(4);
            list.Add(5);
            Assert.True(list.Contains(4));
        }
        
        [Test]
        public void ContainsReturnsFalseIfItemNotPresent()
        {
            var list = new TurboList<int>();
            list.Add(3);
            list.Add(4);
            list.Add(5);
            Assert.False(list.Contains(42));
        }
        
        [Test]
        public void IndexOfReturnsCorrectIndex()
        {
            var list = new TurboList<int>();
            list.Add(3);
            list.Add(4);
            list.Add(5);
            Assert.AreEqual(2, list.IndexOf(5));
        }
        
        [Test]
        public void IndexOfReturnsNegativeOneIfNothingFound()
        {
            var list = new TurboList<int>();
            list.Add(3);
            list.Add(4);
            list.Add(5);
            Assert.AreEqual(-1, list.IndexOf(42));
        }
        
        [Test]
        public void RemoveFindsAndRemovesItem()
        {
            var list = new TurboList<int>();
            list.Add(3);
            list.Add(4);
            list.Add(5);
            list.Remove(3);
            Assert.AreNotEqual(3, list.Get(0));
        }
        
        [Test]
        public void RemoveDoesntFindItem()
        {
            var list = new TurboList<int>();
            list.Add(3);
            list.Add(4);
            list.Add(5);
            list.Remove(42);
            Assert.IsFalse(list.Remove(42));
        }
        
        [Test]
        public void AddRangeAddsEnoughItems()
        {
            var list = new TurboList<int>();
            int[] input = new[] {3, 4, 5};
            list.Add(3);
            list.Add(4);
            list.Add(5);
            list.AddRange(input);
            Assert.AreEqual(6, list.Count);
        }
        
        [Test]
        public void AddRangeAddsItemsAtCorrectPlaces()
        {
            var list = new TurboList<int>();
            var list2 = new TurboList<int>();
            // int[] input = new[] {3, 4, 5};
            // list.Add(3);
            // list.Add(4);
            // list.Add(5);
            // list.AddRange(input);
            // Assert.AreEqual(3, list.Get(3));
            
            
            for (int i = 0; i < 50; i++)
            {
                list.Add(i);
            }
            for (int i = 50; i < 100; i++)
            {
                list2.Add(i);
            }
            

            TurboList<int> result = new();
            result.AddRange(list);
            result.AddRange(list2);

            for (int i = 0; i < result.Count; i++)
            {
                Assert.AreEqual(i, result.Get(i));
            }
        }
        
        [Test]
        public void SetSetsTheCorrectItemAtIndex()
        {
            var list = new TurboList<int>();
            list.Add(3);
            list.Add(4);
            list.Add(5);
            list.Set(1, 42);
            Assert.AreEqual(42, list.Get(1));
        }
        
        [Test] public void GetEnumeratorWorks()
        {
            var list = new TurboList<int>();
            list.Add(1);
            list.Add(2);
            list.Add(3);
            var resultNumber = 0;
            foreach (var VARIABLE in list)
            {
                resultNumber = VARIABLE;
            }
            
            int i = 1;
            foreach (var item in list)
            {
                Assert.AreEqual(i, item);
                i++;
            }
        
            // Assert.AreEqual(5, resultNumber);
        }

        [Test]
        public void BubbleSortTest()
        {
            var list = new TurboList<int>();
            for (int i = 0; i < 100; i++)
            {
                list.Add(100-i);
            }

            list = TurboSort.BubbleSort(list);
            
            Assert.AreEqual(11, list.Get(10));
            Assert.AreEqual(1, list.Get(0));
            Assert.AreEqual(100, list.Get(99));
        }

        [Test]
        public void QuickSortTest()
        {
            var list = new TurboList<int>();
            for (int i = 0; i < 10; i++)
            {
                list.Add(10 - i);
            }
            
            // list.Add(7);
            // list.Add(2);
            // list.Add(3);


            Console.WriteLine($"Count before sort: {list.Count}");

            list = TurboSort.QuickSort(list);
            
            Console.WriteLine($"Count after sort: {list.Count}");
            Console.WriteLine("----------------");
            
            // foreach (var VARIABLE in list)
            // {
            //     Console.WriteLine(VARIABLE);
            // }

            for (int i = 0; i < list.Count; i++)
            {
                Console.WriteLine(list.Get(i));
            }

            // Assert.AreEqual(11, list.Get(10));
            // Assert.AreEqual(1, list.Get(0));
            // Assert.AreEqual(100, list.Get(99));
            for (int i = 0; i < list.Count; i++)
            {
                Assert.AreEqual(i+1, list.Get(i));
            }
        }
    }
}