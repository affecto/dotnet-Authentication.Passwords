// ReSharper disable ObjectCreationAsStatement

using System.Linq;
using Affecto.Authentication.Passwords.Specifications;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Affecto.Authentication.Passwords.Tests.Specifications
{
    [TestClass]
    public class NumberSpecificationTests
    {
        private NumberSpecification sut;

        [TestInitialize]
        public void Setup()
        {
            sut = new NumberSpecification();
        }

        [TestMethod]
        public void OnlyNumberCharacters()
        {
            bool result = sut.IsSatisfiedBy("123");
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void NumberAndOtherCharacters()
        {
            bool result = sut.IsSatisfiedBy("ABa32bcC55x");
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void NullString()
        {
            bool result = sut.IsSatisfiedBy(null);
            AssertDissatisfaction(result);
        }

        [TestMethod]
        public void EmptyString()
        {
            bool result = sut.IsSatisfiedBy(string.Empty);
            AssertDissatisfaction(result);
        }

        [TestMethod]
        public void WhitespaceString()
        {
            bool result = sut.IsSatisfiedBy("  ");
            AssertDissatisfaction(result);
        }

        public void AssertDissatisfaction(bool result)
        {
            Assert.IsFalse(result);
            Assert.AreEqual(1, sut.ReasonsForDissatisfaction.Count());
            Assert.AreEqual(NumberSpecification.PasswordDoesNotContainNumbers, sut.ReasonsForDissatisfaction.Single());
        }
    }
}