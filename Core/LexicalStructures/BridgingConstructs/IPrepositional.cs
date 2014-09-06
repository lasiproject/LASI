namespace LASI.Core
{
    /// <summary>
    /// <para> Defines the role requirements for Prepositional elements, generally Prepositions or Prepositional Phrases, which serve a wide array of linguistic functions. </para>
    /// <para> Along with the other interfaces in the Syntactic Interfaces Library, the IPrepositional interface provides for generalization and abstraction over word and Phrase types. </para>
    /// </summary>
    public interface IPrepositional : ILexical
    {
        /// <summary>
        /// Gets or sets the ILexical construct on the Right side of the IPrepositional.
        /// </summary>
        ILexical ToTheRightOf {
            get;
            set;
        }
        /// <summary>
        /// Gets or sets the ILexical construct on the Left side of the IPrepositional.
        /// </summary>
        ILexical ToTheLeftOf {
            get;
            set;
        }
        /// <summary>
        /// Gets the object of the IPrepositional construct.
        /// </summary>
        ILexical BoundObject {
            get;
        }
        /// <summary>
        /// Gets the contextually extrapolated role of the IPrepositional Construct.
        /// </summary>
        /// <see cref="PrepositionRole"/>
        PrepositionRole Role {
            get;
        }
        /// <summary>
        /// Binds an ILexical construct as the object of the IPrepositional. 
        /// Lexical constructs include word, Phrase, and Clause Types.
        /// </summary>
        /// <param name="prepositionalObject">The ILexical construct as the object of the IPrepositional.</param>
        void BindObject(ILexical prepositionalObject);
    }
}
