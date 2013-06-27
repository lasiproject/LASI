
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
    /// Represents a preposition such as "below", "atop", "into", "through", "by","via", or "for".
    /// Example: The duplicitous blue bird, via its trecherous machinations, betrayed the ardent, hard-working dog.
    /// </summary>
    public class Preposition : Word, IPrepositional
    {

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the Preposition class.
        /// </summary>
        /// <param name="text">The key text content of the Preposition.</param>
        public Preposition(string text)
            : base(text) {
            Role = GetPrepositionalRole(Text);
        }

        private static PrepositionRole GetPrepositionalRole(string text) {
            return KnownSubordinatingWordStrings.Contains(text) ?
                PrepositionRole.SubordinatingConjunction : PrepositionRole.Undetermined;
        }
        public override string ToString() {
            return base.ToString() + (Word.VerboseOutput ? " " + Role : string.Empty);
        }
        #endregion


        #region Methods
        /// <summary>
        /// Binds an ILexical construct as the object of the Preposition. 
        /// Lexical constructs include word, Phrase, and Clause Types.
        /// </summary>
        /// <param name="prepositionalObject">The ILexical construct as the object of the Preposition.</param>
        public void BindObject(ILexical prepositionalObject) {
            BoundObject = prepositionalObject;
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
        public ILexical BoundObject {
            get;
            protected set;
        }
        /// <summary>
        /// Gets or sets the contextually extrapolated role of the Preposition.
        /// </summary>
        /// <see cref="Role"/>
        public PrepositionRole Role {
            get;
            set;
        }
        #endregion

        #region Fields




        #endregion



        static Preposition() {
            using (var reader = new System.IO.StreamReader(ConfigurationManager.AppSettings["PrepositionDataFilePath"]))
                for (var l = reader.ReadLine(); !reader.EndOfStream; l = reader.ReadLine())
                    knownSubordinatingWordStrings.Add(new string(new string(l.TakeWhile(c => c != '/').ToArray()).Trim().TakeWhile(c => c != ' ').ToArray()));
            knownSubordinatingWordStrings = knownSubordinatingWordStrings.Distinct().ToList();
        }

        private static List<string> knownSubordinatingWordStrings = new List<string>();
        protected static IReadOnlyList<string> KnownSubordinatingWordStrings {
            get {
                return knownSubordinatingWordStrings;
            }
        }
    }
}
