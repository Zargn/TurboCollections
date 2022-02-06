using System;
using NUnit.Framework;

namespace TurboCollections.Test
{
    public class TurboBinarySearchTreeTests
    {
        int[] GetShortUnOrderedBalancedArray()
        {
            return new[] {10, 5, 15, 2, 8, 12, 18};
        }
        
        int[] GetUnOrderedArray()
        {
            return new[] {10, 5, 15, 2, 8, 12, 18, 4, 6, 14, 16, 1};
        }

        int[] GetOrderedArray()
        {
            return new[] {1, 2, 4, 5, 6, 8, 10, 12, 14, 15, 16, 18};
        }
        
        int[] GetReverseOrderedArray()
        {
            var result = GetOrderedArray();
            for (int i = 0; i < result.Length / 2; i++)
            {
                var cache = result[i];
                result[i] = result[result.Length - i - 1];
                result[result.Length - i - 1] = cache;
            }

            foreach (var VARIABLE in result)
            {
                Console.WriteLine($":- {VARIABLE}");
            }
            
            return result;
        }

        TurboBinarySearchTree<int> GetTree()
        {
            TurboBinarySearchTree<int> tree = new();

            var input = GetUnOrderedArray();

            foreach (var VARIABLE in input)
            {
                tree.Insert(VARIABLE);
            }

            return tree;
        }


        [TestCase(1), TestCase(5), TestCase(22)]
        public void InsertIncreasesCount(int itemsToAdd)
        {
            TurboBinarySearchTree<int> tree = new();

            for (int i = 1; i < itemsToAdd + 1; i++)
            {
                tree.Insert(i);
            }

            Assert.AreEqual(itemsToAdd,tree.Count);
        }
        
        [Test]
        public void InsertSortsItemsCorrectly()
        {
            TurboBinarySearchTree<int> tree = new();

            var input = GetShortUnOrderedBalancedArray();

            foreach (var VARIABLE in input)
            {
                tree.Insert(VARIABLE);
            }

            var test = GetOrderedArray();

            for (int i = 0; i < input.Length; i++)
            {
                Console.WriteLine($"Testing index {i}");
                Assert.AreEqual(i, tree.Search(input[i]));
            }
        }

        [Test]
        public void DeleteActuallyRemovesItem()
        {
            var tree = GetTree();

            tree.Insert(42);
            tree.Delete(42);
            Assert.AreEqual(-1, tree.Search(42));
        }

        [Test]
        public void DeleteRemovesItemWithTwoChildrenCorrectly()
        {
            var tree = GetTree();

            tree.Delete(5);
            Assert.AreEqual(-1, tree.Search(5));
            Assert.AreEqual(1, tree.Search(4));
        }

        [Test]
        public void SearchReturnsCorrectIndex()
        {
            var tree = GetTree();
            
            Assert.AreEqual(9, tree.Search(6));
            Assert.AreEqual(0, tree.Search(10));
            Assert.AreEqual(1, tree.Search(5));
        }

        [Test]
        public void GetInOrderReturnsCorrectArray()
        {
            var tree = GetTree();

            var result = tree.GetInOrder();
            var wantedResult = GetOrderedArray();

            foreach (var VARIABLE in result)
            {
                Console.WriteLine(VARIABLE);
            }
            
            for (int i = 0; i < result.Length; i++)
            {
                Console.WriteLine($"Expected: {wantedResult[i]}, Found: {result[i]}");
                Assert.AreEqual(wantedResult[i], result[i]);
            }
        }
        
        [Test]
        public void GetInReverseOrderReturnsCorrectArray()
        {
            var tree = GetTree();

            var result = tree.GetInReverseOrder();
            var wantedResult = GetReverseOrderedArray();

            foreach (var VARIABLE in result)
            {
                Console.WriteLine(VARIABLE);
            }
            
            for (int i = 0; i < result.Length; i++)
            {
                Console.WriteLine($"Expected: {wantedResult[i]}, Found: {result[i]}");
                Assert.AreEqual(wantedResult[i], result[i]);
            }
        }

        [Test]
        public void GetEnumeratorGivesCorrectEnumerator()
        {
            var tree = GetTree();

            var wantedResult = GetOrderedArray();

            int i = 0;
            foreach (var VARIABLE in tree)
            {
                Assert.AreEqual(wantedResult[i], VARIABLE);
                i++;
            }
        }

        [Test]
        public void CloneReturnsCorrectTree()
        {
            var tree = GetTree();
            var tree2 = tree.Clone();

            var wantedResult = GetOrderedArray();

            for (int i = 0; i < wantedResult.Length; i++)
            {
                Assert.AreEqual(tree.Search(wantedResult[i]), tree2.Search(wantedResult[i]));
            }
        }

        [Test]
        public void DeleteTreeRemovesAllItems()
        {
            var tree = GetTree();
            
            tree.DeleteTree();

            var intArray = GetOrderedArray();

            foreach (var i in intArray)
            {
                Assert.AreEqual(-1, tree.Search(i));
            }
        }
        
        [Test]
        public void DeleteTreeResetsCount()
        {
            var tree = GetTree();
            
            tree.DeleteTree();

            Assert.AreEqual(0, tree.Count);
        }
    }
}