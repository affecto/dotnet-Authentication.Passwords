using System;
using Affecto.Patterns.Specification;

namespace Affecto.Authentication.Passwords.Specifications
{
    public class PasswordCharacterSpecification : Specification<Password>
    {
        public const string PasswordDoesNotContainEnoughCharacterClasses = "PasswordDoesNotContainEnoughCharacterClasses";

        private static readonly LowerCaseSpecification LowerCaseSpecification = new LowerCaseSpecification();
        private static readonly UpperCaseSpecification UpperCaseSpecification = new UpperCaseSpecification();
        private static readonly NumberSpecification NumberSpecification = new NumberSpecification();
        private static readonly SpecialCharacterSpecification SpecialCharacterSpecification = new SpecialCharacterSpecification();

        private readonly int minimumCharacterClassAppearance;

        public PasswordCharacterSpecification(int minimumCharacterClassAppearance)
        {
            if (minimumCharacterClassAppearance < 1)
            {
                throw new ArgumentException("Minimum character class appearance must be greater than zero.");
            }
            this.minimumCharacterClassAppearance = minimumCharacterClassAppearance;
        }

        protected override bool IsSatisfied(Password password)
        {
            if (password == null)
            {
                throw new ArgumentNullException("password");
            }

            string passwordValue = password.Value;
            int matchCount = 0;

            if (LowerCaseSpecification.IsSatisfiedBy(passwordValue))
            {
                matchCount++;
            }
            if (UpperCaseSpecification.IsSatisfiedBy(passwordValue))
            {
                matchCount++;
            }
            if (NumberSpecification.IsSatisfiedBy(passwordValue))
            {
                matchCount++;
            }
            if (SpecialCharacterSpecification.IsSatisfiedBy(passwordValue))
            {
                matchCount++;
            }

            if (matchCount < minimumCharacterClassAppearance)
            {
                AddReasonForDissatisfaction(PasswordDoesNotContainEnoughCharacterClasses);
                return false;
            }

            return true;
        }
    }
}