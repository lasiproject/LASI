using System.Text;

namespace AspSixApp.Tests
{
    static class StringExtensions
    {
        public static byte[] ToByteArray(this string value) {
            return Encoding.Default.GetBytes(value);
        }

    }
}