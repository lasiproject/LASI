namespace LASI.WebApp.Models
{
    using static System.Char;
    /// <summary>
    /// Validates the contents of a text field ensuring that it contains characters
    /// only characters which are valid in a nominal context.
    /// </summary>
    public sealed class NameFieldAttribute : System.ComponentModel.DataAnnotations.ValidationAttribute
    {
        /// <summary>
        /// Determines if the specified value is valid.
        /// </summary>
        /// <param name="value">The value to validate.</param>
        /// <returns><c>true</c> if the specified value is valid; otherwise, false.</returns>
        public override bool IsValid(object value)
        {
            var text = value as string ?? string.Empty;
            for (var i = 0; i < text.Length; ++i)
            {
                if (IsInvalidCharater(text[i]))
                {
                    ErrorMessage = $"Field may not contain the character {text[i]}";
                    return false;
                }
            }
            return true;
        }
        private static bool IsInvalidCharater(char c) => IsPunctuation(c) || IsWhiteSpace(c) || IsControl(c) || IsSymbol(c);
    }
}