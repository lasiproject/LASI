using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LASI.Core.Binding.Experimental
{
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
            if (singles.Count < 2) { return; }
            var pairs = from i in Enumerable.Range(0, singles.Count)
                        where i % 2 == 0 && i < singles.Count - 1
                        select new { QStart = singles[i], QEnd = singles[i + 1] };
            foreach (var pair in pairs)
            {
                pair.QStart.PairedWith.PairWith(pair.QEnd);
            }
        }
    }
}




