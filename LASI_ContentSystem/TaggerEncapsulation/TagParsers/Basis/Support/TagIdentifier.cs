using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LASI.ContentSystem.TaggerEncapsulation.TagParsers.Experiment.Support
{
    static class TagIdentifier
    {
        static TagType GetTagKind(this string tagSting) {



            throw new NotImplementedException();
        }

        private static readonly IReadOnlyDictionary<string, TagType> phraseTagMap =
                        new[] { "VP", "NP", "PP", "ADVP", "ADJP", "PRT", "CONJP", "S", "SINV", "SQ", "SBARQ", "SBAR", "LST", "INTJ", }
                        .Select(s => new { s, t = TagType.Phrase })
                        .Concat(
                        new[] { "CC", ",", ";", ":", "CD", "DT", "EX", "FW", "IN", "JJ", "JJR", "JJS", "LS", "-LRB-", "-RRB-", "''", "MD", "NN", "NNS", "NNP", "NNPS", "PDT",
                        "POS", "PRP", "PRP$", "RB", "RBR", "RBS", "VB", "VBD", "VBG", "VBN", "VBP", "VBZ", "WDT", "WP", "WP$", "WRB", "RP", "SYM", "TO", "UH", }
                        .Select(s => new { s, t = TagType.Word }))
                .ToDictionary(e => e.s, e => e.t);


        enum TagType
        {
            Unknown = 0,
            Word,
            Phrase,
            Clause,

        }
    }
}