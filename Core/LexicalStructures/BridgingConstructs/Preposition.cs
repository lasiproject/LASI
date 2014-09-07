using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace LASI.Core
{
    /// <summary>
    /// <para> Represents a preposition such as "below", "atop", "into", "through", "by","via", or "for". </para> 
    /// <para> Example: The duplicitous blue bird, via its treacherous machinations, betrayed the ardent, hard-working dog. </para> 
    /// </summary>
    public class Preposition : Word, IPrepositional
    {

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the Preposition class.
        /// </summary>
        /// <param name="text">The text content of the Preposition.</param>
        public Preposition(string text)
            : base(text) {
            Role = knownSubordinators.Contains(text) ?
                PrepositionRole.SubordinatingConjunction : PrepositionRole.Undetermined;
        }

        /// <summary>
        /// Returns a string representation of the Preposition.
        /// </summary>
        /// <returns>A string representation of the Preposition.</returns>
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
        public virtual ILexical ToTheRightOf { get; set; }
        /// <summary>
        /// Gets or sets the PrepositionLinkable construct on the left-hand-side of the Preposition.
        /// </summary>
        public virtual ILexical ToTheLeftOf { get; set; }
        /// <summary>
        /// Gets the object of the IPrepositional construct.
        /// </summary>
        public ILexical BoundObject { get; private set; }
        /// <summary>
        /// Gets or sets the contextually extrapolated role of the Preposition.
        /// </summary>
        /// <see cref="Role"/>
        public PrepositionRole Role { get; set; }


        #endregion

        /// <summary>
        /// Static constructor which loads preposition identification and categorization information.
        /// </summary>
        static Preposition() {
            using (var reader = new System.IO.StreamReader(PrepositionaInfoFilePath)) {
                knownSubordinators = (
                        from line in reader.ReadToEnd().SplitRemoveEmpty('\r', '\n')
                        let len = line.IndexOf('/')
                        select line.Substring(0, len > 0 ? len : line.Length)
                    ).ToHashSet(StringComparer.OrdinalIgnoreCase);
            }
        }

        private static readonly string PrepositionaInfoFilePath = ConfigurationManager.AppSettings["SubordinatingPrepositionalsInfoFile"];
        private static readonly ISet<string> knownSubordinators;

    }
}
