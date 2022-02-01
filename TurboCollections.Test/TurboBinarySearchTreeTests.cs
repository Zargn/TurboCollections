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
        public void SearchMethodReturnsCorrectArray()
        {
            TurboBinarySearchTree<int> tree = new();

            var input = GetUnOrderedArray();

            foreach (var VARIABLE in input)
            {
                tree.Insert(VARIABLE);
            }
            
            // TODO: Need a getenumerator or similar to finish this test.
        }
    }
}