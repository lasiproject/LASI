using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace LASI.Algorithm
{
    public class Clause : ILexical
    {


        /// <summary>
        /// This class is currently experimental and is not a tier in the ParentDocument objects created by the tagged file parsers
        /// Initializes a new instance of the Clause class, by composing the given linear sequence of phrases.
        /// </summary>
        /// <param name="phrases">The linear sequence of Phrases which compose to form the Clause.</param>
        public Clause(IEnumerable<Phrase> phrases) {
            Phrases = phrases;
        }
        /// <summary>
        ///Initializes a new instance of the Clause class, by composing the given linear sequence of words       
        ///As the words are bare in this context, that is not members of a known entity object, they are subsequently implanted in an UndeterminedPhrase instance whose syntactic role should be determined contextually in the future.
        /// </summary>
        /// <param name="words">The linear sequence of Words which compose to form the single UndeterminedPhrase which will comprise the Clause.</param>
        public Clause(IEnumerable<Word> words) {
            Phrases = new List<Phrase>(new[] { new UndeterminedPhrase(words) });
        }
        /// <summary>
        /// Gets the collection of Phrases which comprise the Clause.
        /// </summary>
        public IEnumerable<Phrase> Phrases {
            get;
            protected set;
        }
        /// <summary>
        /// Gets the concatenated text content of all of the Phrases which compose the Clause.
        /// </summary>
        public string Text {
            get {
                return Phrases.Aggregate("", (txt, phrase) => txt += " " + phrase.Text).Trim();
            }
        }

        /// <summary>
        /// Establishes the nested links between the clause, its parent sentence and phrases which comprise it.
        /// </summary>
        /// <param name="sentence"></param>
        internal void EstablishParent(Sentence sentence) {
            ParentSentence = sentence;
            foreach (var r in Phrases)
                r.EstablishParent(this);
        }

        /// <summary>
        /// Gets or set the Document instance to which the Clause belongs.
        /// </summary>
        public Document ParentDocument {
            get {
                return ParentSentence.ParentDocument;
            }
        }
        /// <summary>
        /// Gets or set the Paragraph instance to which the Clause belongs.
        /// </summary>
        public Paragraph ParentParagraph {
            get {
                return ParentSentence.ParentParagraph;
            }
        }
        /// <summary>
        /// Gets the punctuation, if any, which ends the clause.
        /// </summary>
        public Punctuator EndingPunctuation {
            get;
            protected set;
        }


        ///// <summary>
        ///// Gets a string containing the text of the Clause'd  constituents.
        ///// </summary>
        //string ILexical.Text {
        //    get {
        //        return (from r in Phrases
        //                select r).Aggregate("", (sum, r) => sum = sum+" "+r.Text).Trim();
        //    }
        //}

        public override string ToString() {
            return base.ToString() + " \"" + Text + "\"";
        }
        /// <summary>
        /// Gets or sets the numeric weight of the Phrase within the context of its document.
        /// </summary>
        public decimal Weight {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the numeric weight of the Phrase over the context of all extant documents.
        /// </summary>
        public decimal MetaWeight {
            get;
            set;
        }


        public Sentence ParentSentence {
            get;
            protected set;
        }




        public int ID {
            get;
            private set;
        }

        string ILexical.Text {
            get {
                throw new NotImplementedException();
            }
        }

        Type ILexical.Type {
            get {
                return GetType();
            }
        }

        decimal ILexical.Weight {
            get;
            set;
        }

        decimal ILexical.MetaWeight {
            get;
            set;
        }
    }

}