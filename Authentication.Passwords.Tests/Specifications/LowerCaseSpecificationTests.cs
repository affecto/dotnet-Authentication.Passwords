// ReSharper disable ObjectCreationAsStatement

using System.Linq;
using Affecto.Authentication.Passwords.Specifications;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Affecto.Authentication.Passwords.Tests.Specifications
{
    [TestClass]
    public class LowerCaseSpecificationTests
    {
        private LowerCaseSpecification sut;

        [TestInitialize]
        public void Setup()
        {
            sut = new LowerCaseSpecification();
        }

        [TestMethod]
        public void OnlyLowerCaseCharacters()
        {
            bool result = sut.IsSatisfiedBy("abc");
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void LowerCaseAndOtherCharacters()
        {
            bool result = sut.IsSatisfiedBy("ABabcCx");
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
            Assert.AreEqual(LowerCaseSpecification.PasswordDoesNotContainLowerCaseLetters, sut.ReasonsForDissatisfaction.Single());
        }
    }
}