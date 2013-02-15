namespace LASI.Algorithm
{
    public interface IAction : LASI.Algorithm.ILexical, ISubjectTaker, IAdverbialModifiable, IModalityModifiable
    {
        IEntity BoundSubject {
            get;
            set;
        }
    }
}
