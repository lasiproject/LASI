namespace LASI.Core.Binding.Experimental
{
    using System.Linq;

    static class PreBinder
    {
        internal static void BindPairedDelimiters(Paragraph paragraph)
        {
            ProcessQuotePairs<QuotationMark<SingleQuote>, SingleQuote>(paragraph);
            ProcessQuotePairs<QuotationMark<DoubleQuote>, DoubleQuote>(paragraph);
        }

        private static void ProcessQuotePairs<TQuote, TM>(Paragraph paragraph)
            where TQuote : QuotationMark<TM>
            where TM : QuotationMark<TM>
        {
            var singles = paragraph.Words.OfType<TM>().ToList();
            if (singles.Count > 1)
            {
                var pairs = from i in Enumerable.Range(0, singles.Count)
                            where i % 2 == 0 && i < singles.Count - 1
                            select new
                            {
                                QuotationStart = singles[i],
                                QuotationEnd = singles[i + 1]
                            };
                foreach (var pair in pairs)
                {
                    pair.QuotationStart.PairedWith.PairWith(pair.QuotationEnd);
                }
            }
        }
    }
}




