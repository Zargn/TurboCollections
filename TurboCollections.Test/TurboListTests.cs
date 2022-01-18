using System.Collections;
using NUnit.Framework;
using TurboCollections;

namespace TurboCollections.Test
{
    public class TurboListTests
    {
        // [SetUp]
        // public void Setup()
        // {
        // }

        [Test]
        public void NewListIsEmpty()
        {
            var list = new TurboList<int>();
            Assert.Zero(list.Count);
        }

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
            int[] input = new[] {3, 4, 5};
            list.Add(3);
            list.Add(4);
            list.Add(5);
            list.AddRange(input);
            Assert.AreEqual(3, list.Get(3));
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
            list.Add(3);
            list.Add(4);
            list.Add(5);
            var resultNumber = 0;
            foreach (var VARIABLE in list)
            {
                resultNumber = VARIABLE;
            }
        
            Assert.AreEqual(5, resultNumber);
        }

        // [Test]
        // public void
        // {
        //     var list = new TurboList<int>();
        // }
    }
}