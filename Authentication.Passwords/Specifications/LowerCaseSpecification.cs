using Affecto.Patterns.Specification;

namespace Affecto.Authentication.Passwords.Specifications
{
    internal class LowerCaseSpecification : RegexSpecification
    {
        public const string PasswordDoesNotContainLowerCaseLetters = "PasswordDoesNotContainLowerCaseLetters";

        private const string Pattern = "[a-zוהצü]";

        public LowerCaseSpecification()
            : base(Pattern, PasswordDoesNotContainLowerCaseLetters)
        {
        }
    }
}