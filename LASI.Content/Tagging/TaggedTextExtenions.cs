namespace LASI.Content
{
    static class TaggedTextExtenions
    {
        public static void Deconstruct(this TaggedText? value, out string tag, out string text) => (tag, text) = (value?.Tag, value?.Text);
    }
}