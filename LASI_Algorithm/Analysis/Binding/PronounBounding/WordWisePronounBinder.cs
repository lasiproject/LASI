using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LASI.Algorithm.DocumentConstructs;

namespace LASI.Algorithm.Binding
{
    public class WordWisePronounBinder
    {
        #region Constructors

        public WordWisePronounBinder() {
        }

        #endregion

        #region Methods

        public void Bind(Document document) {
            Bind(document.Words);
        }
        public void Bind(Paragraph paragraph) {
            Bind(paragraph.Words);
        }
        public void Bind(Sentence sentence) {
            Bind(sentence.Words);
        }
        public void Bind(IEnumerable<Word> sequentialWords) {
            SequentialWords = sequentialWords;
        }

        #endregion

        #region Static Methods

        /// <summary>
        /// Determins the  PronounGenerder enum value representing the gender of the given pronoun.
        /// </summary>
        /// <param name="pronoun">The pronoun whose gender to is to be checked</param>
        /// <returns>A PronounGenerder enum value representing the gender of the given pronoun.</returns>
        private static PronounGender DeterminePronounGender(Pronoun pronoun) {
            var compareText = pronoun.Text.ToLower();
            return
                malePronounText.Contains(compareText) ?
                PronounGender.Male :
                femalePronounText.Contains(compareText) ?
                PronounGender.Female :
                neurtalPronounText.Contains(compareText) ?
                PronounGender.Thing :
                firstPersonSingularPronounText.Contains(compareText) ?
                PronounGender.Ambiguous :
                firstPersonPluralPronounText.Contains(compareText) ?
                PronounGender.Ambiguous :
                pluralPronounsText.Contains(compareText) ?
                PronounGender.Ambiguous :
                PronounGender.Undefined;

        }

        #endregion

        #region Properties

        public IEnumerable<Word> SequentialWords {
            get;
            protected set;
        }

        #endregion

        #region Fields

        private Stack<ProperSingularNoun> properSingularNouns = new Stack<ProperSingularNoun>();
        private Stack<ProperPluralNoun> properPluralNouns = new Stack<ProperPluralNoun>();
        private Stack<GenericSingularNoun> genericSingularNouns = new Stack<GenericSingularNoun>();
        private Stack<GenericPluralNoun> genericPluralNouns = new Stack<GenericPluralNoun>();
        private Stack<PersonalPronoun> personalPronouns = new Stack<PersonalPronoun>();

        #endregion

        #region Static Fields

        //Common personal Pronouns by gender and plurality
        static readonly string[] malePronounText = new[] { "he", "him", "himself", "hisself", "his" };
        static readonly string[] femalePronounText = new[] { "she", "her", "herself", "hers" };
        static readonly string[] neurtalPronounText = new[] { "it", "itself", "its" };
        static readonly string[] firstPersonSingularPronounText = new[] { "i", "me", "myself", "mine" };
        static readonly string[] firstPersonPluralPronounText = new[] { "we", "us", "ourselves", "ours" };
        static readonly string[] secondsPersonSingularPronounText = new[] { "you", "yourself", "yours" };
        static readonly string[] pluralPronounsText = new[] { "them", "they", "themselves", "theirs" };

        #endregion
    }



    internal enum PronounGender
    {
        Male,
        Female,
        Thing,
        Ambiguous,
        Undefined,
    }
}
