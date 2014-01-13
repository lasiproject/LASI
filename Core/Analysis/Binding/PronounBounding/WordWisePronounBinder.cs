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

        public WordWisePronounBinder()
        {
        }

        #endregion

        #region Methods

        public void Bind(Document document)
        {
            Bind(document.Words);
        }
        public void Bind(Paragraph paragraph)
        {
            Bind(paragraph.Words);
        }
        public void Bind(Sentence sentence)
        {
            Bind(sentence.Words);
        }
        public void Bind(IEnumerable<Word> sequentialWords)
        {
            SequentialWords = sequentialWords;
        }

        #endregion

        #region Static Methods


        #endregion

        #region Properties

        public IEnumerable<Word> SequentialWords
        {
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


        #endregion
    }




}
