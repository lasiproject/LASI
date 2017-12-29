namespace LASI.Core
{
    /// <summary>
    /// Represents the word "TO", a dynamic prepositional construct which can link Words, Phrases and Clauses together.
    /// </summary>
    public class ToLinker : Word, IPrepositional
    {
        /// <summary>
        /// Initializes a new instance of the ToLinker class.
        /// </summary>
        public ToLinker(string text) : base(text) { }

        /// <summary>
        /// Binds an ILexical construct as the object of the ToLinker. Lexical constructs include word, Phrase, and Clause Types.
        /// </summary>
        /// <param name="prepositionalObject">The ILexical construct as the object of the ToLinker.</param>
        public void BindObject(ILexical prepositionalObject)
        {
            BoundObject = prepositionalObject;
        }

        /// <summary>
        /// The object of the IPrepositional construct.
        /// </summary>
        public ILexical BoundObject { get; private set; }

        /// <summary>
        /// Gets or sets the contextually extrapolated <see cref="Core.PrepositionRole"/> of the ToLinker.
        /// </summary>
        public PrepositionRole PrepositionRole { get; set; }

        /// <summary>
        /// Gets or sets the IprepositionLinkable construct on the right-hand-side of the ToLinker.
        /// </summary>
        public ILexical ToTheRightOf
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the IprepositionLinkable construct on the left-hand-side of the ToLinker.
        /// </summary>
        public ILexical ToTheLeftOf
        {
            get;
            set;
        }
    }
}
