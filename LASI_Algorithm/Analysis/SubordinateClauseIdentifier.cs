using LASI.Algorithm.DocumentConstructs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace LASI.Algorithm.Analysis
{
    static class SubordinateClauseIdentifier
    {

        public static void Identify(params Sentence[] sentences)
        {
            foreach (Sentence s in sentences) 
            {
                var remainder = RemainderofSentenceIncludingSubordinator(s.Words);
                
                

            }   
        }

        
        private static IEnumerable<Word> RemainderofSentenceIncludingSubordinator(IEnumerable<Word> words)
        {
            var remainder = words.SkipWhile(w =>
            {
                return isRelativePronounorSubordinatingConjunction(w);

            });
            return remainder;

        }


        /// <summary>
        /// This is the most common beginning of a subordinating clause. It begins with either a subordinating conjuntion or a relative (wh) pronoun. 
        /// Example, "Dennis, who was a huge dick, ate at Wendy's and harasseed the management.
        /// "who was a huge dick," is your subordinate clause. 
        /// </summary>
        /// <param name="s">Word</param>
        /// <returns> true or false</returns>
        private static bool isRelativePronounorSubordinatingConjunction(Word w)
        {
            var prep = w as Preposition;
            if (prep != null)
                return !(w is RelativePronoun) || !(prep.PrepositionalRole == PrepositionalRole.SubordinatingConjunction);
            return false;
        }


    }
}
