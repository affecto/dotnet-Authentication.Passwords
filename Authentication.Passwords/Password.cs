using System;
using BrockAllen.IdentityReboot;
using Microsoft.AspNet.Identity;

namespace Affecto.Authentication.Passwords
{
    public class Password
    {
        private readonly IPasswordHasher passwordHasher;

        public string Value { get; private set; }

        public Password(string password)
            : this(new AdaptivePasswordHasher(), password)
        {
        }

        internal Password(IPasswordHasher passwordHasher, string password)
        {
            if (passwordHasher == null)
            {
                throw new ArgumentNullException("passwordHasher");
            }
            if (password == null)
            {
                throw new ArgumentNullException("password");
            }

            this.passwordHasher = passwordHasher;
            Value = password;
        }

        public string Hash()
        {
            return passwordHasher.HashPassword(Value);
        }

        public PasswordMatch MatchTo(string hashedPassword)
        {
            PasswordVerificationResult result = passwordHasher.VerifyHashedPassword(hashedPassword, Value);

            switch (result)
            {
                case PasswordVerificationResult.Failed:
                    return PasswordMatch.Failed;
                case PasswordVerificationResult.Success:
                    return PasswordMatch.Success;
                case PasswordVerificationResult.SuccessRehashNeeded:
                    return PasswordMatch.SuccessRehashNeeded;
                default:
                    throw new NotImplementedException("Unknown password match result.");
            }
        }
    }
}