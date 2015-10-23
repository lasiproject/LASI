using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace LASI.Core
{
    static class SubordinateClauseIdentifier
    {
        public static void Identify(params Sentence[] sentences)
        {
            foreach (var s in sentences)
            {
                var remainder = RemainderofSentenceIncludingSubordinator(s.Words);
            }
        }
        private static IEnumerable<Word> RemainderofSentenceIncludingSubordinator(IEnumerable<Word> words) =>
            words.SkipWhile(w => isRelativePronounOrSubordinatingConjunction(w));
        /// <summary>
        /// This is the most common beginning of a subordinating clause. It begins with either a subordinating conjunction or a relative (wh) pronoun. 
        /// Example, "Dennis, who was a huge dick, ate at Wendy's and harassed the management.
        /// "who was a huge dick," is the subordinate clause. 
        /// </summary>
        /// <param name="word">Word</param>
        /// <returns> <c>true</c> or false</returns>
        private static bool isRelativePronounOrSubordinatingConjunction(Word word)
        {
            var prep = word as Preposition;
            if (prep != null)
                return !(word is RelativePronoun) || !(prep.Role == PrepositionRole.SubordinatingConjunction);
            return false;
        }


    }
}
