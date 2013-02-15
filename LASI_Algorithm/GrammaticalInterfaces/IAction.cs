namespace LASI.Algorithm
{
    public interface IAction : ILexical, ISubjectTaker, IAdverbialModifiable, IModalityModifiable
    {
        IEntity BoundSubject {
            get;
            set;
        }
    }
}
