using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LASI.Algorithm.Binding
{
    public class PhraseWiseObjectBinder
    {
        public PhraseWiseObjectBinder(VerbPhrase bindingTarget, IEnumerable<Phrase> remainingPhrases) {
            _bindingTarget = bindingTarget;


            inputstream = new Stack<Phrase>(remainingPhrases);

            St0 = new State0(this);
            St1 = new State1(this);
            St2 = new State2(this);
            St4 = new State4(this);
            St5 = new State5(this);
            St6 = new State6(this);

            ConjunctNounPhrases = new List<NounPhrase>();
            if (inputstream.Count < 1) {
                return;
            }
            St0.ProcessNext(inputstream.PopDynamic());

        }

        protected IEntity directObject = null;
        protected IEntity indirectObject = null;
        protected Stack<NounPhrase> entities = new Stack<NounPhrase>();
        protected bool directObjectFound, indirectObjectFound;
        protected Stack<Phrase> inputstream;
        protected VerbPhrase _bindingTarget;
        protected List<AdjectivePhrase> lastAdjectivals = new List<AdjectivePhrase>();
        protected List<NounPhrase> ConjunctNounPhrases;
        private State0 St0;
        private State1 St1;
        private State2 St2;
        private State4 St4;
        private State5 St5;
        private ConjunctionPhrase lastConjunctive;
        private State6 St6;

        public ConjunctionPhrase LastConjunctive {
            get {
                return lastConjunctive;
            }
            set {
                lastConjunctive = value;
            }
        }
        public void CheckForExitEnd() {
            if (inputstream.Count < 1) {
                _bindingTarget.IndirectObject = indirectObject;
                _bindingTarget.DirectObject = directObject;
            }
        }
        private void BindBuiltupAdjectivePhrases(NounPhrase phrase) {
            foreach (var adjp in this.lastAdjectivals) {
                phrase.BindDescriber(adjp);
            }
            this.lastAdjectivals.Clear();
        }


        abstract class State
        {
            protected State(PhraseWiseObjectBinder machine) {
                _machine = machine;
                stream = machine.inputstream;
            }
            private string stateName;

            protected string StateName {
                get {
                    return stateName;
                }
                set {
                    stateName = value;
                }
            }
            private int stateNumber;
            private PhraseWiseObjectBinder _machine;

            public PhraseWiseObjectBinder Machine {
                get {
                    return _machine;
                }
                set {
                    _machine = value;
                }
            }

            protected int StateNumber {
                get {
                    return stateNumber;
                }
                set {
                    stateNumber = value;
                }
            }


            public virtual void ProcessNext(Phrase phrase) {
                throw new InvalidStateTransitionException(stateNumber, phrase);
            }

            private Stack<Phrase> stream;

            protected Stack<Phrase> Stream {
                get {
                    return stream;
                }
                set {
                    stream = value;
                }
            }
        }
        class State0 : State
        {
            public State0(PhraseWiseObjectBinder machine)
                : base(machine) {
                St1 = machine.St1;
                St2 = machine.St2;
            }
            public virtual void ProcessNext(NounPhrase phrase) {
                Machine.entities.Push(phrase);
                Machine.CheckForExitEnd();
                St2.ProcessNext(Stream.PopDynamic());
            }
            public virtual void ProcessNext(AdjectivePhrase phrase) {
                Machine.lastAdjectivals.Add(phrase);
                St1.ProcessNext(Stream.PopDynamic());
            }
            private State1 St1;
            private State2 St2;
        }


        class State1 : State
        {


            public State1(PhraseWiseObjectBinder machine)
                : base(machine) {
                St2 = Machine.St2;

            }
            public void ProcessNext(NounPhrase phrase) {
                Machine.entities.Push(phrase);
                Machine.BindBuiltupAdjectivePhrases(phrase);
                Machine.CheckForExitEnd();
                St2.ProcessNext(Stream.PopDynamic());
            }
            private State2 St2;
        }
        class State2 : State
        {


            public State2(PhraseWiseObjectBinder machine)
                : base(machine) {
                St4 = Machine.St4;
            }
            public void ProcessNext(ConjunctionPhrase phrase) {
                phrase.OnLeft = Machine.entities.Peek();
                Machine.LastConjunctive = phrase;
                Machine.ConjunctNounPhrases.Add(Machine.entities.Pop());
                Machine.CheckForExitEnd();
                St4.ProcessNext(Stream.PopDynamic());
            }
            public void ProcessNext(AdjectivePhrase phrase) {
                IndirectObjectFound();
            }
            public void ProcessNext(NounPhrase phrase) {
                IndirectObjectFound();
            }
            public void ProcessNext(PrepositionalPhrase phrase) {
                DirectObjectFound();
            }

            private void DirectObjectFound() {
                foreach (var E in Machine.entities) {
                    E.DirectObjectOf = Machine._bindingTarget;
                    Machine._bindingTarget.DirectObject = E;
                }
                Machine.entities.Clear();
                Machine.ConjunctNounPhrases.Clear();
                Machine.directObjectFound = true;
            }

            private void IndirectObjectFound() {
                foreach (var E in Machine.entities) {
                    E.IndirectObjectOf = Machine._bindingTarget;
                    Machine._bindingTarget.IndirectObject = E;
                }
                Machine.entities.Clear();
                Machine.ConjunctNounPhrases.Clear();
                Machine.indirectObjectFound = true;
            }
            private State4 St4;
        }
        class State4 : State
        {


            public State4(PhraseWiseObjectBinder machine)
                : base(machine) {
                St2 = Machine.St2;
                St5 = Machine.St5;
            }
            public void ProcessNext(NounPhrase phrase) {
                Machine.entities.Push(phrase);
                Machine.ConjunctNounPhrases.Add(phrase);
                Machine.lastConjunctive.OnRight = phrase;
                Machine.CheckForExitEnd();
                St2.ProcessNext(Stream.PopDynamic());
            }
            public void ProcessNext(AdjectivePhrase phrase) {
                Machine.lastAdjectivals.Add(phrase);
                if (Stream.Count < 1)
                    return;
                St5.ProcessNext(Stream.PopDynamic());
            }
            private State2 St2;
            private State5 St5;
        }
        class State5 : State
        {
            public State5(PhraseWiseObjectBinder machine)
                : base(machine) {
                St2 = Machine.St2;
                St5 = Machine.St5;
                St6 = machine.St6;
            }

            public void ProcessNext(NounPhrase phrase) {
                Machine.ConjunctNounPhrases.Add(phrase);
                Machine.entities.Push(phrase);
                Machine.BindBuiltupAdjectivePhrases(phrase);
                Machine.CheckForExitEnd();
                St2.ProcessNext(Stream.PopDynamic());
            }
            public void ProcessNext(ConjunctionPhrase phrase) {
                phrase.OnLeft = Machine.lastAdjectivals.Last();
                Machine.LastConjunctive = phrase;
                Machine.CheckForExitEnd();
                St6.ProcessNext(Stream.PopDynamic());
            }
            private State2 St2;
            private State5 St5;
            private State6 St6;


        }
        class State6 : State
        {


            public State6(PhraseWiseObjectBinder machine)
                : base(machine) {
                St5 = Machine.St5;
                St2 = machine.St2;
            }
            public void ProcessNext(AdjectivePhrase phrase) {
                Machine.lastAdjectivals.Add(phrase);
                Machine.lastConjunctive.OnRight = phrase;
                Machine.CheckForExitEnd();
                St5.ProcessNext(Stream.PopDynamic());
            }
            public void ProcessNext(NounPhrase phrase) {
                Machine.entities.Push(phrase);
                Machine.ConjunctNounPhrases.Add(phrase);
                Machine.lastConjunctive.OnRight = phrase;
                Machine.BindBuiltupAdjectivePhrases(phrase);
                Machine.CheckForExitEnd();
                St2.ProcessNext(Stream.PopDynamic());
            }


            private State2 St2;
            private State5 St5;

        }


    }

    static class DynamicStackExtensions
    {
        internal static dynamic PopDynamic(this Stack<Phrase> stack) {
            return stack.Pop();
        }
    }

}
