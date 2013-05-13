namespace LASI.Algorithm.FundamentalSyntacticInterfaces
{
    /// <summary>
    /// Defines the role reqirements for Action elements, generally Verbs or VerbPhrases, which can be bound to entity one or more subjects.
    /// Along with the rhs interfaces in the Syntactic Interfaces Library, the ISubjectTaker interface provides for generalization and abstraction over Word and Phrase types.
    /// </summary>
    public interface ISubjectTaker
    {
        void BindSubject(IEntity subject);

        System.Collections.Generic.IEnumerable<IEntity> BoundSubjects {
            get;
        }


    }
}