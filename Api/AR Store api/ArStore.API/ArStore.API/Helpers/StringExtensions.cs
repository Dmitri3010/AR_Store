namespace ArStore.API.Helpers
{
    public static class StringsExtensions
    {
        public static bool IsEmpty(this string str) => string.IsNullOrWhiteSpace(str);

        public static string ToLowerFirstChar(this string input)
        {
            var newString = input;
            if (!string.IsNullOrEmpty(newString) && char.IsUpper(newString[0]))
                newString = char.ToLower(newString[0]) + newString.Substring(1);
            return newString;
        }
    }
}