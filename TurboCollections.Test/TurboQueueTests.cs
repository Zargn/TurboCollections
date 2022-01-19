using System;
using NUnit.Framework;

namespace TurboCollections.Test
{
    public class TurboQueueTests
    {
        [Test]
        public void CountReturnsZeroFromEmptyQueue()
        {
            TurboQueue<int> queue = new TurboQueue<int>();
            Assert.Zero(queue.Count);
        }
        
        [TestCase(1), TestCase(5)]
        public void AddingToQueueIncreasesCount(int elementsToAdd)
        {
            TurboQueue<int> queue = new TurboQueue<int>();
            for (int i = 0; i < elementsToAdd; i++)
            {
                queue.Enqueue(i);
            }
            Assert.AreEqual(elementsToAdd, queue.Count);
        }
        
        [Test]
        public void PeekReturnsCorrectValue()
        {
            TurboQueue<int> queue = new TurboQueue<int>();
            queue.Enqueue(1);
            queue.Enqueue(2);
            queue.Enqueue(3);
            Assert.AreEqual(1, queue.Peek());
        }
        
        [Test]
        public void DequeueReturnsCorrectItem()
        {
            TurboQueue<int> queue = new TurboQueue<int>();
            queue.Enqueue(1);
            queue.Enqueue(2);
            queue.Enqueue(3);
            Assert.AreEqual(1, queue.Dequeue());
            queue.Dequeue();
            Assert.AreEqual(3,queue.Dequeue());
        }
        
        [Test]
        public void ClearCorrectlyEmptiesQueue()
        {
            TurboQueue<int> queue = new TurboQueue<int>();
            queue.Enqueue(1);
            queue.Enqueue(2);
            queue.Enqueue(3);
            queue.Clear();
            Assert.AreEqual(0, queue.Count);
        }
        
        [Test]
        public void EnqueueWorksCorrectlyAfterUnqueueing()
        {
            TurboQueue<int> queue = new TurboQueue<int>();
            queue.Enqueue(1);
            queue.Enqueue(2);
            queue.Enqueue(3);
            queue.Enqueue(4);
            queue.Dequeue();
            queue.Enqueue(5);
            queue.Dequeue();
            queue.Dequeue();
            queue.Dequeue();
            queue.Enqueue(6);
            queue.Enqueue(7);
            queue.Dequeue();
            Assert.AreEqual(6, queue.Peek());
        }
        
        // [Test]
        // public void 
        // {
        //     TurboQueue<int> queue = new TurboQueue<int>();
        // }
    }
}