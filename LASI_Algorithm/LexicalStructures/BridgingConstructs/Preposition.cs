using LASI.Algorithm.SyntacticInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Configuration;
namespace LASI.Algorithm
{
    /// <summary>
    /// Represents a preposition such as "below", "atop", "into", "through", "by", "via", or "for".
    /// Example: The duplicitous blue bird, via its trecherous machinations, betrayed the ardent, hard-working dog.
    /// </summary>
    public class Preposition : Word, IPrepositional
    {

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the Preposition class.
        /// </summary>
        /// <param name="text">The literal text content of the Preposition.</param>
        public Preposition(string text)
            : base(text) {
            PrepositionalRole = Algorithm.PrepositionalRole.Undetermined;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the PrepositionLinkable construct on the right-hand-side of the Preposition.
        /// </summary>
        public virtual IPrepositionLinkable OnRightSide {
            get;
            set;
        }
        /// <summary>
        /// Gets or sets the PrepositionLinkable construct on the left-hand-side of the Preposition.
        /// </summary>
        public virtual IPrepositionLinkable OnLeftSide {
            get;
            set;
        }
        /// <summary>
        /// Gets the object of the IPrepositional construct.
        /// </summary>
        public ILexical PrepositionalObject {
            get;
            protected set;
        }
        #endregion
        /// <summary>
        /// Binds an ILexical construct as the object of the Preposition. 
        /// Lexical constructs include word, Phrase, and Clause Types.
        /// </summary>
        /// <param name="prepositionalObject">The ILexical construct as the object of the Preposition.</param>
        public void BindObjectOfPreposition(ILexical prepositionalObject) {
            PrepositionalObject = prepositionalObject;
        }
        #region Methods

        #endregion`

        #region Fields




        #endregion

        /// <summary>
        /// Gets or sets the contextually extrapolated role of the Preposition.
        /// </summary>
        /// <see cref="PrepositionalRole"/>
        public PrepositionalRole PrepositionalRole {
            get;
            set;
        }


        //static Preposition() {
        //    using (var reader = new System.IO.StreamReader(ConfigurationManager.AppSettings["PrepositionDataFilePath"]))
        //        for (var l = reader.ReadLine(); !reader.EndOfStream; l = reader.ReadLine())
        //            knownLexicalMembers.Add(new string(new string(l.TakeWhile(c => c != '/').ToArray()).Trim().TakeWhile(c => c != ' ').ToArray()));
        //    knownLexicalMembers = knownLexicalMembers.Distinct().ToList();
        //}

        private static List<string> knownLexicalMembers = new List<string>();
        protected static IReadOnlyList<string> KnownLexicalMembers {
            get {
                return knownLexicalMembers;
            }
        }
    }
}
