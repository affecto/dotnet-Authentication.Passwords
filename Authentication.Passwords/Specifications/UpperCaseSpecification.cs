using Affecto.Patterns.Specification;

namespace Affecto.Authentication.Passwords.Specifications
{
    internal class UpperCaseSpecification : RegexSpecification
    {
        public const string PasswordDoesNotContainUpperCaseLetters = "PasswordDoesNotContainUpperCaseLetters";

        private const string Pattern = "[A-Z����]";

        public UpperCaseSpecification()
            : base(Pattern, PasswordDoesNotContainUpperCaseLetters)
        {
        }
    }
}