using System.ComponentModel.DataAnnotations;

namespace AspSixApp.Models
{
    internal class NameFieldAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            var text = value as string ?? string.Empty;
            if (text.Length < 1)
            {
                return false;
            }

            var index = text.IndexOfAny(illegalCharacters);
            if (index > -1)
            {
                ErrorMessage = $"Field may not contain the character {text[index]}";
                return false;
            }

            return true;
        }

        private static readonly char[] illegalCharacters = {' ', '\t', '\n', '\r', '!', '?', '.', ';', ':' };
    }
}