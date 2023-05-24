namespace ProductsPricing.Core.Extensions
{
    public static class StringExtensions
    {
        public static string LimitStringIfExceedsMaxLength(this string str, int maxLength)
        {
            if (string.IsNullOrWhiteSpace(str))
            {
                throw new ArgumentNullException(nameof(str));
            }

            if (maxLength > str.Length)
            {
                throw new ArgumentException(nameof(maxLength));
            }

            return str.Length > maxLength ? str[..maxLength] + "..." : str;
        }
    }
}