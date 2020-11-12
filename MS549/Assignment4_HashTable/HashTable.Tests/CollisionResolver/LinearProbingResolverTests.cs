using NUnit.Framework;
using SadPumpkin.HashTable.CollisionResolver;

namespace SadPumpkin.HashTable.Tests.CollisionResolver
{
    [TestFixture]
    public class LinearProbingResolverTests
    {
        [Test]
        public void can_create_new()
        {
            LinearProbingResolver newResolver = new LinearProbingResolver();

            Assert.IsNotNull(newResolver);
        }
        
        [Test]
        public void returns_original_if_no_misses()
        {
            LinearProbingResolver newResolver = new LinearProbingResolver();

            int originalHash = 123456;
            int newHash = newResolver.ResolveHash(originalHash, 0);

            Assert.AreEqual(originalHash, newHash);
        }

        [TestCase(123456, 1, ExpectedResult = 123457)]
        [TestCase(123456, 2, ExpectedResult = 123458)]
        [TestCase(123456, 3, ExpectedResult = 123459)]
        [TestCase(123456, 4, ExpectedResult = 123460)]
        [TestCase(123456, 5, ExpectedResult = 123461)]
        [TestCase(123456, 6, ExpectedResult = 123462)]
        public int new_hash_scales_linearly_with_misses(int originalHash, int misses)
        {
            LinearProbingResolver newResolver = new LinearProbingResolver();

            int newHash = newResolver.ResolveHash(originalHash, misses);

            return newHash;
        }
    }
}