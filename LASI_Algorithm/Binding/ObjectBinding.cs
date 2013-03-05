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
            St0.ProcessNext(inputstream.PopDynamic());
        }

        private void DirectObjectFound() {
            foreach (var E in entities) {
                E.DirectObjectOf = _bindingTarget;
                _bindingTarget.DirectObject = E;
            }
            entities.Clear();
            ConjunctNounPhrases.Clear();

        }

        private void IndirectObjectFound() {
            foreach (var E in entities) {
                E.IndirectObjectOf = _bindingTarget;
                _bindingTarget.IndirectObject = E;
            }
            entities.Clear();
            ConjunctNounPhrases.Clear();

        }
        protected Stack<Phrase> inputstream;
        protected VerbPhrase _bindingTarget;
        protected IEntity directObject;
        protected IEntity indirectObject;


        protected List<AdjectivePhrase> lastAdjectivals = new List<AdjectivePhrase>();
        protected List<NounPhrase> ConjunctNounPhrases = new List<NounPhrase>();
        protected Stack<NounPhrase> entities = new Stack<NounPhrase>();
        private State0 St0;
        private State1 St1;
        private State2 St2;
        private State4 St4;
        private State5 St5;
        private State6 St6;
        private ConjunctionPhrase lastConjunctive;


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
                _stream = machine.inputstream;
            }

            public virtual void ProcessNext(Phrase phrase) {
                throw new InvalidStateTransitionException(StateName, phrase);
            }

            protected string StateName {
                get {
                    return stateName;
                }
                set {
                    stateName = value;
                }
            }


            public PhraseWiseObjectBinder Machine {
                get {
                    return _machine;
                }
                set {
                    _machine = value;
                }
            }


            protected Stack<Phrase> Stream {
                get {
                    return _stream;
                }
                set {
                    _stream = value;
                }
            }



            private string stateName;
            private Stack<Phrase> _stream;
            private PhraseWiseObjectBinder _machine;


        }
        class State0 : State
        {
            public State0(PhraseWiseObjectBinder machine)
                : base(machine) {
                StateName = "s0";

            }

            public virtual void ProcessNext(NounPhrase phrase) {
                Machine.entities.Push(phrase);
                if (Machine.inputstream.Count < 1) {
                    if (Machine.directObject == null)
                        Machine.DirectObjectFound();
                    else
                        Machine.IndirectObjectFound();

                    return;
                }
                Machine.St2.ProcessNext(Stream.PopDynamic());
            }
            public virtual void ProcessNext(AdjectivePhrase phrase) {
                Machine.lastAdjectivals.Add(phrase);
                Machine.St1.ProcessNext(Stream.PopDynamic());
            }
        }


        class State1 : State
        {


            public State1(PhraseWiseObjectBinder machine)
                : base(machine) {
                StateName = "s1";
            }
            public void ProcessNext(NounPhrase phrase) {
                Machine.entities.Push(phrase);
                Machine.BindBuiltupAdjectivePhrases(phrase);
                if (Machine.inputstream.Count < 1) {
                    if (Machine.directObject == null)
                        Machine.DirectObjectFound();
                    else
                        Machine.IndirectObjectFound();

                    return;
                }
                Machine.St2.ProcessNext(Stream.PopDynamic());
            }
        }
        class State2 : State
        {


            public State2(PhraseWiseObjectBinder machine)
                : base(machine) {
                StateName = "s2";

            }
            public void ProcessNext(ConjunctionPhrase phrase) {
                phrase.OnLeft = Machine.entities.Peek();
                Machine.lastConjunctive = phrase;
                Machine.ConjunctNounPhrases.Add(Machine.entities.Peek());
                if (Machine.inputstream.Count < 1) {
                    if (Machine.directObject == null)
                        Machine.DirectObjectFound();
                    else
                        Machine.IndirectObjectFound();

                    return;
                }
                Machine.St4.ProcessNext(Stream.PopDynamic());
            }
            public void ProcessNext(AdjectivePhrase phrase) {
                Machine.IndirectObjectFound();
                Machine.St4.ProcessNext(Stream.PopDynamic());
            }

            public void ProcessNext(PrepositionalPhrase phrase) {
                Machine.DirectObjectFound();
                Machine.St0.ProcessNext(Stream.PopDynamic());
            }


        }
        class State4 : State
        {


            public State4(PhraseWiseObjectBinder machine)
                : base(machine) {

                StateName = "s4";
            }
            public void ProcessNext(NounPhrase phrase) {
                Machine.entities.Push(phrase);
                Machine.ConjunctNounPhrases.Add(phrase);
                Machine.lastConjunctive.OnRight = phrase;
                if (Machine.inputstream.Count < 1) {
                    if (Machine.directObject == null)
                        Machine.DirectObjectFound();
                    else
                        Machine.IndirectObjectFound();

                    return;
                }
                Machine.St2.ProcessNext(Stream.PopDynamic());
            }
            public void ProcessNext(AdjectivePhrase phrase) {
                Machine.lastAdjectivals.Add(phrase);
                if (Machine.inputstream.Count < 1) {
                    if (Machine.directObject == null)
                        Machine.DirectObjectFound();
                    else
                        Machine.IndirectObjectFound();

                    return;
                }
                Machine.St5.ProcessNext(Stream.PopDynamic());
            }
        }
        class State5 : State
        {
            public State5(PhraseWiseObjectBinder machine)
                : base(machine) {
                StateName = "s5";
            }

            public void ProcessNext(NounPhrase phrase) {
                Machine.ConjunctNounPhrases.Add(phrase);
                Machine.entities.Push(phrase);
                Machine.BindBuiltupAdjectivePhrases(phrase);
                if (Machine.inputstream.Count < 1) {
                    if (Machine.directObject == null)
                        Machine.DirectObjectFound();
                    else
                        Machine.IndirectObjectFound();

                    return;
                }
                Machine.St2.ProcessNext(Stream.PopDynamic());
            }
            public void ProcessNext(ConjunctionPhrase phrase) {
                phrase.OnLeft = Machine.lastAdjectivals.Last();
                Machine.lastConjunctive = phrase;
                if (Machine.inputstream.Count < 1) {
                    if (Machine.directObject == null)
                        Machine.DirectObjectFound();
                    else
                        Machine.IndirectObjectFound();

                    return;
                }
                Machine.St6.ProcessNext(Stream.PopDynamic());
            }

        }
        class State6 : State
        {


            public State6(PhraseWiseObjectBinder machine)
                : base(machine) {
                StateName = "s6";
            }
            public void ProcessNext(AdjectivePhrase phrase) {
                Machine.lastAdjectivals.Add(phrase);
                Machine.lastConjunctive.OnRight = phrase;
                if (Machine.inputstream.Count < 1) {
                    if (Machine.directObject == null)
                        Machine.DirectObjectFound();
                    else
                        Machine.IndirectObjectFound();

                    return;
                }
                Machine.St5.ProcessNext(Stream.PopDynamic());
            }
            public void ProcessNext(NounPhrase phrase) {
                Machine.entities.Push(phrase);
                Machine.ConjunctNounPhrases.Add(phrase);
                Machine.lastConjunctive.OnRight = phrase;
                Machine.BindBuiltupAdjectivePhrases(phrase);
                if (Machine.inputstream.Count < 1) {
                    if (Machine.directObject == null)
                        Machine.DirectObjectFound();
                    else
                        Machine.IndirectObjectFound();

                    return;
                }
                Machine.St2.ProcessNext(Stream.PopDynamic());
            }


        }


    }

    static class DynamicStackExtensions
    {
        internal static dynamic PopDynamic(this Stack<Phrase> stack) {
            return stack.Pop();
        }
    }

}
