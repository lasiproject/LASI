using System.Collections.Generic;

namespace LASI.Algorithm
{
    public interface IPronounBindable
    {
        void BindPronoun(Pronoun pro);

        IEnumerable<Pronoun> BoundPronouns {
            get;
        }
    }
}
