
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LASI.Algorithm
{
    class GerundVerbPhrase : VerbPhrase, IEntity
    {
        public GerundVerbPhrase(IEnumerable<Word> composedWords)
            : base(composedWords) {
        }



        public void BindPronoun(IPronoun pro) {
            if (!boundPronouns.Contains(pro)) {
                boundPronouns.Add(pro);
                pro.BindAsReferringTo(this);
            }
        }
        public void BindDescriptor(IDescriptor adj) {
            if (!describers.Contains(adj)) {
                describers.Add(adj);
                adj.Describes = this;
            }
        }
        public void AddPossession(IEntity possession) {
            if (!possessed.Contains(possession)) {
                possessed.Add(possession);
                possession.Possesser = this;
            }
        }

        public IEntity Possesser { get; set; }
        public IVerbal DirectObjectOf { get; set; }
        public IVerbal IndirectObjectOf { get; set; }
        public IVerbal SubjectOf { get; set; }

        public EntityKind EntityKind { get { return EntityKind.Activitiy; } }
        public IEnumerable<IDescriptor> Descriptors { get { return describers; } }
        public IEnumerable<IEntity> Possessed { get { return possessed; } }
        public IEnumerable<IPronoun> BoundPronouns { get { return boundPronouns; } }
        #region Fields

        private ICollection<IDescriptor> describers = new List<IDescriptor>();
        private ICollection<IEntity> possessed = new List<IEntity>();
        private ICollection<IPronoun> boundPronouns = new List<IPronoun>();

        #endregion






    }
}
