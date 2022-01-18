using System.Runtime.Loader;
using NUnit.Framework;

namespace TurboCollections.Test
{
    public class TurboStackTests
    {
        [Test]
        public void CountReturnsZeroIfEmpty()
        {
            TurboStack<int> stack = new TurboStack<int>();
            Assert.AreEqual(0, stack.Count);
        }
        
        [Test, TestCase(1), TestCase(5)]
        public void CountReturnsCorrectValueBasedOnAddedItems(int AmountToAdd)
        {
            TurboStack<int> stack = new TurboStack<int>();
            for (int i = 0; i < AmountToAdd; i++)
            {
                stack.Push(i);
            }
            Assert.AreEqual(AmountToAdd, stack.Count);
        }
        
        [Test]
        public void PeekReturnsCorrectItem()
        {
            TurboStack<int> stack = new TurboStack<int>();
            stack.Push(5);
            stack.Push(2);
            stack.Push(8);
            Assert.AreEqual(8, stack.Peek());
        }
        
        [Test]
        public void PopReturnsCorrectItem()
        {
            TurboStack<int> stack = new TurboStack<int>();
            stack.Push(5);
            stack.Push(2);
            stack.Push(8);
            Assert.AreEqual(8, stack.Pop());
        }
        
        [Test]
        public void PopCorrectlyDeletesOneItem()
        {
            TurboStack<int> stack = new TurboStack<int>();
            stack.Push(5);
            stack.Push(2);
            stack.Push(8);
            stack.Pop();
            Assert.AreEqual(2, stack.Peek());
        }
        
        [Test]
        public void ClearCompletelyEmptiesStack()
        {
            TurboStack<int> stack = new TurboStack<int>();
            stack.Push(5);
            stack.Push(2);
            stack.Push(8);
            stack.Clear();
            Assert.AreEqual(0, stack.Count);
        }
        
        [Test]
        public void GetEnumeratorReturnsCorrectEnumerator()
        {
            TurboStack<int> stack = new TurboStack<int>();
            stack.Push(5);
            stack.Push(2);
            stack.Push(8);
            var finalNumber = 0;
            foreach (var VARIABLE in stack)
            {
                finalNumber = VARIABLE;
            }
            Assert.AreEqual(8, stack.Peek());
        }
        
        // [Test]
        // public void
        // {
        //     TurboStack<int> stack = new TurboStack<int>();
        // }
    }
}