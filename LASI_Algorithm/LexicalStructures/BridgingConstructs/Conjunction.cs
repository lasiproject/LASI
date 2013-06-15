using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace LASI.Algorithm
{
    /// <summary>
    /// Represents a conjunction adverb which links two clauses together.
    /// </summary>
    public class Conjunction : Word, IConjunctive
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the Conjunction class.
        /// </summary>
        /// <param name="text">the key text content of the adverb.</param>
        public Conjunction(string text)
            : base(text) {












            //char c1 = text.First(
            //    (char c) => {
            //        return char.IsPunctuation(c);
            //    });


            //char c2 = text.First((char c) => char.IsPunctuation(c));

            //char c3 = text.First(c => char.IsPunctuation(c));

            //IEnumerable<char> allPunctuators1 = text.Where(
            //    (char c) => {
            //        return char.IsPunctuation(c);
            //    });

            //IEnumerable<char> allPunctuators2 = text.Where((char c) => char.IsPunctuation(c));


            //IEnumerable<char> allPunctuators3 = text.Where(c => char.IsPunctuation(c));


            //IEnumerable<char> betweenFirstTwoPunctuators =
            //    text.SkipWhile(c => char.IsPunctuation(c)).Skip(1).TakeWhile(c => char.IsPunctuation(c));
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the clause on the second hand side of conjunction.
        /// </summary>
        public virtual ILexical OnRight {
            get;
            set;
        } /// <summary>
        /// Gets or sets the clause on the first hand side of conjunction.
        /// </summary>
        public virtual ILexical OnLeft {
            get;
            set;
        }

        #endregion

    }
}
