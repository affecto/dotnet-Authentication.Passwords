// ReSharper disable ObjectCreationAsStatement

using System;
using BrockAllen.IdentityReboot;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Affecto.Authentication.Passwords.Tests
{
    [TestClass]
    public class PasswordTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void PasswordCannotBeNullWhenInitialized()
        {
            new Password(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void PasswordCannotBeNullWhenInitializedWithInternalConstructor()
        {
            new Password(new AdaptivePasswordHasher(), null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void PasswordHasherCannotBeNullWhenInitializedWithInternalConstructor()
        {
            new Password(null, "foo");
        }

        [TestMethod]
        public void PasswordIsHashed()
        {
            const string password = "VerySecret";
            var sut = new Password(password);

            string hashedPassword = sut.Hash();

            Assert.IsNotNull(hashedPassword);
        }

        [TestMethod]
        public void PasswordIsMatching()
        {
            const string password = "VerySecret";
            var expectedPassword = new Password(password);
            string expectedHash = expectedPassword.Hash();

            var sut = new Password(password);
            PasswordMatch match = sut.MatchTo(expectedHash);

            Assert.AreEqual(PasswordMatch.Success, match);
        }

        [TestMethod]
        public void PasswordIsMatchingButRehashNeeded()
        {
            const string password = "VerySecret";
            var expectedPassword = new Password(new AdaptivePasswordHasher(2), password);
            string expectedHash = expectedPassword.Hash();

            var sut = new Password(password);
            PasswordMatch match = sut.MatchTo(expectedHash);

            Assert.AreEqual(PasswordMatch.SuccessRehashNeeded, match);
        }

        [TestMethod]
        public void PasswordIsNotMatching()
        {
            const string password = "VerySecret";
            var expectedPassword = new Password(password);
            string expectedHash = expectedPassword.Hash();

            var sut = new Password("AnotherPassword");
            PasswordMatch match = sut.MatchTo(expectedHash);

            Assert.AreEqual(PasswordMatch.Failed, match);
        }
    }
}