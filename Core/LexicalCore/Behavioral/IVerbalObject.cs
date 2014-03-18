
namespace LASI.Core
{
    /// <summary>
    /// <para> Defines the role requirements for Action Objects, generally the objects of Verbs or VerbPhrases. </para>
    /// <para> Along with the other interfaces in the Syntactic Interfaces Library, the IActionObject interface provides for generalization and abstraction over word and Phrase types. </para>
    /// </summary>
    public interface IVerbalObject
    {
        /// <summary>
        /// Gets or sets the IVerbal construct the IVerbalSubject is the direct object of.
        /// </summary>
        IVerbal DirectObjectOf {
            get;
            set;
        }
        /// <summary>
        /// Gets or sets the IVerbal construct the IVerbalSubject is the indirect object of.
        /// </summary>
        IVerbal IndirectObjectOf {
            get;
            set;
        }
    }
}
