using NUnit.Framework;
using SadPumpkin.HashTable.CollisionResolver;

namespace SadPumpkin.HashTable.Tests.CollisionResolver
{
    [TestFixture]
    public class QuadraticProbingResolverTests
    {
        [Test]
        public void can_create_new()
        {
            QuadraticProbingResolver newResolver = new QuadraticProbingResolver();

            Assert.IsNotNull(newResolver);
        }

        [Test]
        public void returns_original_if_no_misses()
        {
            QuadraticProbingResolver newResolver = new QuadraticProbingResolver();

            int originalHash = 123456;
            int newHash = newResolver.ResolveHash(originalHash, 0);

            Assert.AreEqual(originalHash, newHash);
        }

        [TestCase(100, 1, ExpectedResult = 101)]
        [TestCase(100, 2, ExpectedResult = 104)]
        [TestCase(100, 3, ExpectedResult = 109)]
        [TestCase(100, 4, ExpectedResult = 116)]
        [TestCase(100, 5, ExpectedResult = 125)]
        [TestCase(100, 6, ExpectedResult = 136)]
        [TestCase(100, 7, ExpectedResult = 149)]
        [TestCase(100, 8, ExpectedResult = 164)]
        public int new_hash_scales_quadratically_with_misses(int originalHash, int misses)
        {
            QuadraticProbingResolver newResolver = new QuadraticProbingResolver();

            int newHash = newResolver.ResolveHash(originalHash, misses);

            return newHash;
        }
    }
}