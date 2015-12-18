// ReSharper disable ObjectCreationAsStatement

using System.Linq;
using Affecto.Authentication.Passwords.Specifications;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Affecto.Authentication.Passwords.Tests.Specifications
{
    [TestClass]
    public class SpecialCharacterSpecificationTests
    {
        private SpecialCharacterSpecification sut;

        [TestInitialize]
        public void Setup()
        {
            sut = new SpecialCharacterSpecification();
        }

        [TestMethod]
        public void OnlySpecialCharacters()
        {
            bool result = sut.IsSatisfiedBy("%&#");
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void NumberAndOtherCharacters()
        {
            bool result = sut.IsSatisfiedBy("ABa#&_32bcC55x");
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void WhitespaceString()
        {
            bool result = sut.IsSatisfiedBy("  ");
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

        public void AssertDissatisfaction(bool result)
        {
            Assert.IsFalse(result);
            Assert.AreEqual(1, sut.ReasonsForDissatisfaction.Count());
            Assert.AreEqual(SpecialCharacterSpecification.PasswordDoesNotContainSpecialCharacters, sut.ReasonsForDissatisfaction.Single());
        }
    }
}