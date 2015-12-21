using Affecto.Patterns.Specification;

namespace Affecto.Authentication.Passwords.Specifications
{
    internal class SpecialCharacterSpecification : RegexSpecification
    {
        public const string PasswordDoesNotContainSpecialCharacters = "PasswordDoesNotContainSpecialCharacters";

        // Pattern from OWASP: https://www.owasp.org/index.php/Password_special_characters
        private const string Pattern = @"[ !""#$%&'()*+,\-\.\/:;<=>?@\[\\\]^_`{|}~]";

        public SpecialCharacterSpecification()
            : base(Pattern, PasswordDoesNotContainSpecialCharacters)
        {
        }
    }
}