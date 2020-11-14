using NUnit.Framework;
using SadPumpkin.HashTable.HashGenerators;

namespace SadPumpkin.HashTable.Tests.HashGenerators
{
    [TestFixture]
    public class PrimeHashGeneratorTests
    {
        [Test]
        public void can_create_new()
        {
            var newGenerator = new PrimeHashGenerator<int>();

            Assert.IsNotNull(newGenerator);
        }

        [Test]
        public void returns_prime_for_null()
        {
            var newGenerator = new PrimeHashGenerator<object>(17);

            int result = newGenerator.GetHashCode(null);

            Assert.AreEqual(17, result);
        }

        [Test]
        public void returns_hash_of_int()
        {
            var newGenerator = new PrimeHashGenerator<int>(17);

            int testValue = 1234567;

            int result = newGenerator.GetHashCode(testValue);
            int expectedResult = 17 - (testValue % 17);

            Assert.AreEqual(expectedResult, result);
        }

        [Test]
        public void returns_hash_of_object()
        {
            var newGenerator = new PrimeHashGenerator<string>();

            string testValue = "1234567";

            int result = newGenerator.GetHashCode(testValue);
            int expectedResult = 17 - (testValue.GetHashCode() % 17);

            Assert.AreEqual(expectedResult, result);
        }

        [Test]
        public void returns_different_hashes_for_different_primes()
        {
            var newGenerator1 = new PrimeHashGenerator<string>(13);
            var newGenerator2 = new PrimeHashGenerator<string>(17);

            string testValue = "1234567";

            int result1 = newGenerator1.GetHashCode(testValue);
            int result2 = newGenerator2.GetHashCode(testValue);

            Assert.AreNotEqual(result1, result2);
        }
    }
}