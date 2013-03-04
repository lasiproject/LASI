using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LASI.Algorithm.ObjectBinding
{
    class EntityReferenceBinder
    {
        Stack<GenericSingularNoun> genericSingular = new Stack<GenericSingularNoun>();
        Stack<GenericPluralNoun> genericPlural = new Stack<GenericPluralNoun>();
        Stack<ProperSingularNoun> properSingular = new Stack<ProperSingularNoun>();
        Stack<ProperPluralNoun> properPlural = new Stack<ProperPluralNoun>();
        Stack<PresentParticipleGerund> gerund = new Stack<PresentParticipleGerund>();

        public EntityReferenceBinder(IEnumerable<Word> stream) {
            Stream = new WordStream(stream);
            BeginProcess();
        }
        protected virtual void BeginProcess() {

        }

        protected virtual void ProcessNext(GenericSingularNoun entity) {
            genericSingular.Push(entity);
        }
        protected virtual void ProcessNext(GenericPluralNoun entity) {
            genericPlural.Push(entity);
        }
        protected virtual void ProcessNext(ProperSingularNoun entity) {
            properSingular.Push(entity);
        }
        protected virtual void ProcessNext(ProperPluralNoun entity) {
            properPlural.Push(entity);
        }
        protected virtual void ProcessNext(PresentParticipleGerund entity) {
            gerund.Push(entity);
        }

        private static double ComputeLikelyhood() {
            throw new NotImplementedException();
        }

        protected WordStream Stream {
            get;
            set;
        }




        enum PronounGender
        {
            Male,
            Female,
            Thing,
            Group
        }

        protected readonly string[] malePronounText = new[] { "he", "him", "himself", "hisself", "his" };
        protected readonly string[] femalePronounText = new[] { "she", "her", "herself", "hers" };
    }
}
