
namespace LASI.Algorithm
{
    public interface IPrepositional : ILexical
    {
        /// <summary>
        /// Gets or sets the IPropositionLinkable construct on the Right side of the IPrepositional.
        /// </summary>
        ILexical ToTheRightOf {
            get;
            set;
        }
        /// <summary>
        /// Gets or sets the IPropositionLinkable construct on the Left side of the IPrepositional.
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
        /// Gets or sets the contextually extrapolated role of the PrepositionalConstruct.
        /// </summary>
        /// <see cref="Role"/>
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
