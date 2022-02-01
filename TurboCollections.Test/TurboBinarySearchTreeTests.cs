﻿using System;
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
        public void SearchMethodReturnsCorrectArray()
        {
            TurboBinarySearchTree<int> tree = new();

            var input = GetUnOrderedArray();

            foreach (var VARIABLE in input)
            {
                tree.Insert(VARIABLE);
            }
            
            
        }
    }
}