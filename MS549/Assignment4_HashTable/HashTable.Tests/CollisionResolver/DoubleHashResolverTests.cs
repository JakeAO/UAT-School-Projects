using NUnit.Framework;
using SadPumpkin.HashTable.CollisionResolver;
using SadPumpkin.HashTable.HashGenerators;

namespace SadPumpkin.HashTable.Tests.CollisionResolver
{
    [TestFixture]
    public class DoubleHashResolverTests
    {
        [Test]
        public void can_create_new()
        {
            StandardHashGenerator<int> hashGenerator = new StandardHashGenerator<int>();
            DoubleHashResolver newResolver = new DoubleHashResolver(hashGenerator);

            Assert.IsNotNull(newResolver);
        }

        [Test]
        public void returns_original_if_no_misses()
        {
            StandardHashGenerator<int> hashGenerator = new StandardHashGenerator<int>();
            DoubleHashResolver newResolver = new DoubleHashResolver(hashGenerator);

            int originalHash = 123456;
            int newHash = newResolver.ResolveHash(originalHash, 0);

            Assert.AreEqual(originalHash, newHash);
        }

        [TestCase(100, 0, ExpectedResult = 100)]
        [TestCase(100, 1, ExpectedResult = 200)]
        [TestCase(100, 2, ExpectedResult = 400)]
        [TestCase(100, 3, ExpectedResult = 800)]
        [TestCase(100, 4, ExpectedResult = 1600)]
        [TestCase(100, 5, ExpectedResult = 3200)]
        [TestCase(100, 6, ExpectedResult = 6400)]
        [TestCase(100, 7, ExpectedResult = 12800)]
        [TestCase(100, 8, ExpectedResult = 25600)]
        public int new_hash_scales_quadratically_with_misses(int originalHash, int misses)
        {
            StandardHashGenerator<int> hashGenerator = new StandardHashGenerator<int>();
            DoubleHashResolver newResolver = new DoubleHashResolver(hashGenerator);

            int newHash = newResolver.ResolveHash(originalHash, misses);

            return newHash;
        }
    }
}