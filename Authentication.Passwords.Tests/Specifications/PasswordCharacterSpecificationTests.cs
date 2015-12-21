// ReSharper disable ObjectCreationAsStatement

using System;
using System.Linq;
using Affecto.Authentication.Passwords.Specifications;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Affecto.Authentication.Passwords.Tests.Specifications
{
    [TestClass]
    public class PasswordCharacterSpecificationTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void MinimumCharacterClassAppearanceCannotBeZeroWhenInitialized()
        {
            new PasswordCharacterSpecification(0);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void MinimumCharacterClassAppearanceCannotBeNegativeWhenInitialized()
        {
            new PasswordCharacterSpecification(-25);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void PasswordCannotBeNull()
        {
            var sut = new PasswordCharacterSpecification(1);
            sut.IsSatisfiedBy(null);
        }

        [TestMethod]
        public void PasswordContainsCharactersFromAllClasses()
        {
            var sut = new PasswordCharacterSpecification(4);
            bool result = sut.IsSatisfiedBy(new Password("abcABC123&%#"));

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void PasswordContainsCharactersFromLowerCaseUpperCaseAndNumberClasses()
        {
            var sut = new PasswordCharacterSpecification(3);
            bool result = sut.IsSatisfiedBy(new Password("abcABC123"));

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void PasswordContainsCharactersFromLowerCaseUpperCaseAndSpecialClasses()
        {
            var sut = new PasswordCharacterSpecification(3);
            bool result = sut.IsSatisfiedBy(new Password("abcABC#%&"));

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void PasswordContainsCharactersFromLowerCaseNumberAndSpecialClasses()
        {
            var sut = new PasswordCharacterSpecification(3);
            bool result = sut.IsSatisfiedBy(new Password("abc123#%&"));

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void PasswordContainsCharactersFromUpperCaseNumberAndSpecialClasses()
        {
            var sut = new PasswordCharacterSpecification(3);
            bool result = sut.IsSatisfiedBy(new Password("ABC123#%&"));

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void PasswordContainsOnlyUpperCaseAndLowerCaseClasses()
        {
            var sut = new PasswordCharacterSpecification(3);
            bool result = sut.IsSatisfiedBy(new Password("ABCabc"));

            Assert.IsFalse(result);
            Assert.AreEqual(1, sut.ReasonsForDissatisfaction.Count());
            Assert.AreEqual(PasswordCharacterSpecification.PasswordDoesNotContainEnoughCharacterClasses, sut.ReasonsForDissatisfaction.Single());
        }

        [TestMethod]
        public void PasswordContainsOnlyUpperCaseAndNumberClasses()
        {
            var sut = new PasswordCharacterSpecification(3);
            bool result = sut.IsSatisfiedBy(new Password("ABC123"));

            Assert.IsFalse(result);
            Assert.AreEqual(1, sut.ReasonsForDissatisfaction.Count());
            Assert.AreEqual(PasswordCharacterSpecification.PasswordDoesNotContainEnoughCharacterClasses, sut.ReasonsForDissatisfaction.Single());
        }

        [TestMethod]
        public void PasswordContainsOnlyUpperCaseAndSpecialCharacterClasses()
        {
            var sut = new PasswordCharacterSpecification(3);
            bool result = sut.IsSatisfiedBy(new Password("ABC#%&"));

            Assert.IsFalse(result);
            Assert.AreEqual(1, sut.ReasonsForDissatisfaction.Count());
            Assert.AreEqual(PasswordCharacterSpecification.PasswordDoesNotContainEnoughCharacterClasses, sut.ReasonsForDissatisfaction.Single());
        }

        [TestMethod]
        public void PasswordContainsOnlyLowerCaseAndNumberClasses()
        {
            var sut = new PasswordCharacterSpecification(3);
            bool result = sut.IsSatisfiedBy(new Password("abc123"));

            Assert.IsFalse(result);
            Assert.AreEqual(1, sut.ReasonsForDissatisfaction.Count());
            Assert.AreEqual(PasswordCharacterSpecification.PasswordDoesNotContainEnoughCharacterClasses, sut.ReasonsForDissatisfaction.Single());
        }

        [TestMethod]
        public void PasswordContainsOnlyLowerCaseAndSpecialCharacterClasses()
        {
            var sut = new PasswordCharacterSpecification(3);
            bool result = sut.IsSatisfiedBy(new Password("abc#%&"));

            Assert.IsFalse(result);
            Assert.AreEqual(1, sut.ReasonsForDissatisfaction.Count());
            Assert.AreEqual(PasswordCharacterSpecification.PasswordDoesNotContainEnoughCharacterClasses, sut.ReasonsForDissatisfaction.Single());
        }

        [TestMethod]
        public void PasswordContainsOnlyNumberAndSpecialCharacterClasses()
        {
            var sut = new PasswordCharacterSpecification(3);
            bool result = sut.IsSatisfiedBy(new Password("123#%&"));

            Assert.IsFalse(result);
            Assert.AreEqual(1, sut.ReasonsForDissatisfaction.Count());
            Assert.AreEqual(PasswordCharacterSpecification.PasswordDoesNotContainEnoughCharacterClasses, sut.ReasonsForDissatisfaction.Single());
        }
    }
}