using Affecto.Patterns.Specification;

namespace Affecto.Authentication.Passwords.Specifications
{
    internal class NumberSpecification : RegexSpecification
    {
        public const string PasswordDoesNotContainNumbers = "PasswordDoesNotContainNumbers";

        private const string Pattern = @"\d";

        public NumberSpecification()
            : base(Pattern, PasswordDoesNotContainNumbers)
        {
        }
    }
}