namespace LASI.Algorithm
{
    public interface IAction : ILexical, ISubjectTaker, IAdverbialModifiable, IModalityModifiable
    {
        void AttachObjectViaPreposition(IPrepositional prep);

        void BindSubject(IEntity subject);

        System.Collections.Generic.IEnumerable<IEntity> BoundSubjects {
            get;
        }

        ILexical ObjectViaPreposition {
            get;
        }
        Clause GivenExposition {
            get;
            set;
        }
    }
}
