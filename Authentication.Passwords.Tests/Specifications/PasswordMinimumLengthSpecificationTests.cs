// ReSharper disable ObjectCreationAsStatement

using System;
using System.Linq;
using Affecto.Authentication.Passwords.Specifications;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Affecto.Authentication.Passwords.Tests.Specifications
{
    [TestClass]
    public class PasswordMinimumLengthSpecificationTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void MinimumLengthCannotBeZeroWhenInitialized()
        {
            new PasswordMinimumLengthSpecification(0);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void MinimumLengthCannotBeNegativeWhenInitialized()
        {
            new PasswordMinimumLengthSpecification(-25);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void PasswordCannotBeNull()
        {
            var sut = new PasswordMinimumLengthSpecification(1);
            sut.IsSatisfiedBy(null);
        }

        [TestMethod]
        public void PasswordLengthIsEqualToMinimumLength()
        {
            var sut = new PasswordMinimumLengthSpecification(4);
            bool result = sut.IsSatisfiedBy(new Password("four"));

            Assert.IsTrue(result);
            Assert.AreEqual(0, sut.ReasonsForDissatisfaction.Count());
        }

        [TestMethod]
        public void PasswordLengthIsGreaterThanMinimumLength()
        {
            var sut = new PasswordMinimumLengthSpecification(4);
            bool result = sut.IsSatisfiedBy(new Password("fourty"));

            Assert.IsTrue(result);
            Assert.AreEqual(0, sut.ReasonsForDissatisfaction.Count());
        }

        [TestMethod]
        public void PasswordLengthIsLessThanMinimumLength()
        {
            var sut = new PasswordMinimumLengthSpecification(4);
            bool result = sut.IsSatisfiedBy(new Password("two"));

            Assert.IsFalse(result);
            Assert.AreEqual(1, sut.ReasonsForDissatisfaction.Count());
            Assert.AreEqual(PasswordMinimumLengthSpecification.PasswordTooShort, sut.ReasonsForDissatisfaction.Single());
        }
    }
}