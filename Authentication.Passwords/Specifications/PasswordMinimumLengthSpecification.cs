using System;
using Affecto.Patterns.Specification;

namespace Affecto.Authentication.Passwords.Specifications
{
    public class PasswordMinimumLengthSpecification : Specification<Password>
    {
        public const string PasswordTooShort = "PasswordTooShort";

        private readonly int minimumLength;

        public PasswordMinimumLengthSpecification(int minimumLength)
        {
            if (minimumLength < 1)
            {
                throw new ArgumentException("Password minimum length must be greater than zero.");
            }
            this.minimumLength = minimumLength;
        }

        protected override bool IsSatisfied(Password password)
        {
            if (password == null)
            {
                throw new ArgumentNullException("password");
            }

            if (password.Value.Length < minimumLength)
            {
                AddReasonForDissatisfaction(PasswordTooShort);
                return false;
            }

            return true;
        }
    }
}