using LASI.Algorithm.DocumentConstructs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace LASI.Algorithm
{
    static class SubordinateClauseIdentifier
    {
        public static void Identify(params Sentence[] sentences) {
            foreach (var s in sentences) {
                var remainder = RemainderofSentenceIncludingSubordinator(s.Words);
            }
        }
        private static IEnumerable<Word> RemainderofSentenceIncludingSubordinator(IEnumerable<Word> words) {
            var remainder = words.SkipWhile(w => {
                return isRelativePronounorSubordinatingConjunction(w);

            });
            return remainder;

        }
        /// <summary>
        /// This is the most common beginning of a subordinating clause. It begins with either a subordinating conjuntion or a relative (wh) pronoun. 
        /// Example, "Dennis, who was a huge dick, ate at Wendy's and harasseed the management.
        /// "who was a huge dick," is the subordinate clause. 
        /// </summary>
        /// <param name="word">Word</param>
        /// <returns> true or false</returns>
        private static bool isRelativePronounorSubordinatingConjunction(Word word) {
            var prep = word as Preposition;
            if (prep != null)
                return !(word is RelativePronoun) || !(prep.Role == PrepositionRole.SubordinatingConjunction);
            return false;
        }


    }
}
