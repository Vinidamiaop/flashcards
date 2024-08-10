using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace flashcards.domain.Utils
{
    public partial class Slugfy
    {
        public static string GenerateSlug(string text)
        {
            if (string.IsNullOrEmpty(text)) return string.Empty;

            text = RemoveDiacritics(text);

            text = text.ToLowerInvariant();

            text = ReplaceSpecialChars().Replace(text, string.Empty);

            text = ReplaceWhiteSpace().Replace(text, "-").Trim('-');

            return text;
        }

        private static string RemoveDiacritics(string text)
        {
            var normalizedString = text.Normalize(System.Text.NormalizationForm.FormD);
            var stringBuilder = new StringBuilder();

            foreach (var caractere in normalizedString)
            {
                var unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(caractere);
                if (unicodeCategory != UnicodeCategory.NonSpacingMark)
                {
                    stringBuilder.Append(caractere);
                }
            }

            return stringBuilder.ToString().Normalize(NormalizationForm.FormC);
        }

        [GeneratedRegex(@"[^a-z0-9\s-]")]
        private static partial Regex ReplaceSpecialChars();
        [GeneratedRegex(@"\s+")]
        private static partial Regex ReplaceWhiteSpace();
    }
}