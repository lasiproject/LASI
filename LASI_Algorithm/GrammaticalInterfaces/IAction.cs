namespace LASI.Algorithm
{
    public interface IAction : ILexical, ISubjectTaker, IAdverbialModifiable, IModalityModifiable
    {
        void AttachObjectViaPreposition(IPrepositional prep);

        IEntity BoundSubject {
            get;
            set;
        }

        ILexical ObjectViaPreposition {
            get;
        }
    }
}
