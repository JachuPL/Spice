namespace Spice.WebAPI.Tests.Common
{
    internal static class StringGenerator
    {
        public static string Generate(int charCount)
        {
            string result = string.Empty;
            for (int i = 0; i < charCount; i++)
            {
                result += "a";
            }

            return result;
        }
    }
}