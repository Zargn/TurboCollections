using NUnit.Framework;

namespace TurboCollections.Test
{
    public class TurboHashSetTests
    {
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
    }
}