using NUnit.Framework;

namespace TurboCollections.Test
{
    public class TurboHashSetTests
    {
        TurboHashSet<int> GetHashSet()
        {
            TurboHashSet<int> hashSet = new();

            int[] numbers = GetNumberArray();

            foreach (var VARIABLE in numbers)
            {
                hashSet.Insert(VARIABLE);
            }

            return hashSet;
        }

        int[] GetNumberArray()
        {
            return new[] {5, 15, 255, 123, 5356521, 42, 512, 17, 97};
        }


        [TestCase(1), TestCase(5), TestCase(12), TestCase(32)]
        public void InsertCorrectlyIncreasesCount(int itemsToAdd)
        {
            TurboHashSet<int> hashSet = new TurboHashSet<int>();

            for (int i = 1; i < itemsToAdd + 1; i++)
            {
                hashSet.Insert(i*i);
            }

            Assert.AreEqual(itemsToAdd, hashSet.Count);
        }

        [Test]
        public void InsertReturnsTrueOnSuccessful()
        {
            TurboHashSet<int> hashSet = new();

            Assert.AreEqual(true, hashSet.Insert(42));
            Assert.AreEqual(1, hashSet.Count);
        }

        [Test]
        public void ExistsReturnsTrueWhenItemPresent()
        {
            var hashSet = GetHashSet();

            var numbers = GetNumberArray();

            for (int i = 0; i < numbers.Length; i++)
            {
                Assert.AreEqual(true, hashSet.Exists(numbers[i]));
            }
        }

        [Test]
        public void ExistsReturnsFalseWhenItemNotPresent()
        {
            var hashSet = GetHashSet();
            var numbers = GetNumberArray();

            for (int i = 0; i < numbers.Length; i++)
            {
                Assert.AreEqual(false, hashSet.Exists(numbers[i] * 2));
            }
        }
    }
}