using NUnit.Framework;
using SadPumpkin.HashTable.HashGenerators;

namespace SadPumpkin.HashTable.Tests.HashGenerators
{
    [TestFixture]
    public class StandardHashGeneratorTests
    {
        [Test]
        public void can_create_new()
        {
            var newGenerator = new StandardHashGenerator<int>();

            Assert.IsNotNull(newGenerator);
        }

        [Test]
        public void returns_zero_for_null()
        {
            var newGenerator = new StandardHashGenerator<object>();

            int result = newGenerator.GetHashCode(null);

            Assert.Zero(result);
        }

        [Test]
        public void returns_value_of_int()
        {
            var newGenerator = new StandardHashGenerator<int>();

            int testValue = 1234567;

            int result = newGenerator.GetHashCode(testValue);

            Assert.AreEqual(testValue, result);
        }

        [Test]
        public void returns_hash_code_of_object()
        {
            var newGenerator = new StandardHashGenerator<string>();

            string testValue = "1234567";

            int result = newGenerator.GetHashCode(testValue);

            Assert.AreEqual(testValue.GetHashCode(), result);
        }
    }
}