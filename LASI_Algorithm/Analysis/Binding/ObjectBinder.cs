using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LASI.Algorithm.Binding
{
    public class ObjectBinder
    {

        public void Bind(Sentence sentence) {

            var phrases = sentence.Phrases.ToList();
            bindingTarget = phrases.GetVerbPhrases().ToList().First();
            var verbPhraseIndex = phrases.IndexOf(bindingTarget);

            var remainingPhrases = phrases.Skip(verbPhraseIndex + 1).Reverse();
            if (remainingPhrases.Count() > 0) {
                inputstream.PushAll(remainingPhrases);
                St0 = new State0(this);
                St1 = new State1(this);
                St2 = new State2(this);
                St3 = new State3(this);
                St4 = new State4(this);
                St5 = new State5(this);
                St6 = new State6(this);
                St0.ProcessNext(inputstream.PopDynamic());

            }
        }
        private void AssociateDirect() {
            foreach (var e in entities) {
                bindingTarget.BindDirectObject(e);
            }
            entities.Clear();
            ConjunctNounPhrases.Clear();

        }

        private void AssociateIndirect() {
            foreach (var e in entities) {
                bindingTarget.BindIndirectObject(e);
            }
            entities.Clear();
            ConjunctNounPhrases.Clear();

        }
        private Stack<Phrase> inputstream = new Stack<Phrase>();

        private Phrase lastPhrase;
        private VerbPhrase bindingTarget;
        private ConjunctionPhrase lastConjunctive;
        private PrepositionalPhrase lastPrepositional;

        private bool directFound;


        private List<AdjectivePhrase> lastAdjectivals = new List<AdjectivePhrase>();
        private List<NounPhrase> ConjunctNounPhrases = new List<NounPhrase>();
        private Stack<NounPhrase> entities = new Stack<NounPhrase>();
        private State0 St0;
        private State1 St1;
        private State2 St2;
        private State3 St3;
        private State4 St4;
        private State5 St5;
        private State6 St6;





        private void BindBuiltupAdjectivePhrases(NounPhrase phrase) {
            foreach (var adjp in lastAdjectivals) {
                phrase.BindDescriber(adjp);
            }
            lastAdjectivals.Clear();
        }


        abstract class State
        {
            protected State(ObjectBinder machine, string stateName) {
                Machine = machine;
                Stream = machine.inputstream;
                StateName = stateName;
            }

            public virtual void ProcessNext(Phrase phrase) {
                throw new InvalidStateTransitionException(StateName, phrase);
            }

            protected void Universal(Phrase Phrase) {
                Machine.lastPhrase = Phrase;
            }

            protected dynamic GetNext() {
                return GetNext();
            }

            protected string StateName {
                get;
                set;
            }


            protected ObjectBinder Machine {
                get;
                set;
            }


            protected Stack<Phrase> Stream {
                get;
                set;
            }


        }
        class State0 : State
        {
            public State0(ObjectBinder machine)
                : base(machine, "State 0") {

            }
            public void ProcessNext(PrepositionalPhrase phrase) {
                Machine.lastPrepositional = phrase;
                Machine.St0.ProcessNext(GetNext());
            }
            public virtual void ProcessNext(VerbPhrase phrase) {
                new ObjectBinder().Bind(new Sentence(new[] { phrase }.Concat(Stream.ToList())));
            }

            public virtual void ProcessNext(AdverbPhrase phrase) {
                Machine.bindingTarget.ModifyWith(phrase);
                Universal(phrase);
                if (Stream.Count < 1)
                    return;
                Machine.St0.ProcessNext(GetNext());

            }
            public virtual void ProcessNext(NounPhrase phrase) {
                if (Machine.lastPrepositional != null) {
                    phrase.PrepositionOnLeft = Machine.lastPrepositional;
                    Machine.lastPrepositional.OnRightSide = phrase;
                    Machine.bindingTarget.AttachObjectViaPreposition(phrase.PrepositionOnLeft);
                }
                Machine.entities.Push(phrase);
                if (Stream.Count < 1) {
                    if (!Machine.directFound)
                        Machine.AssociateDirect();
                    else
                        Machine.AssociateIndirect();

                    return;
                }
                Universal(phrase);

                Machine.St2.ProcessNext(GetNext());
            }
            public virtual void ProcessNext(AdjectivePhrase phrase) {
                Machine.lastAdjectivals.Add(phrase);
                Universal(phrase);
                if (Stream.Count > 0) {
                    Machine.St1.ProcessNext(GetNext());
                }
            }
        }


        class State1 : State
        {
            public State1(ObjectBinder machine)
                : base(machine, "State 1") {
            }
            public void ProcessNext(NounPhrase phrase) {
                Machine.entities.Push(phrase);
                Machine.BindBuiltupAdjectivePhrases(phrase);
                if (Stream.Count < 1) {
                    if (!Machine.directFound)
                        Machine.AssociateDirect();
                    else
                        Machine.AssociateIndirect();

                    return;
                }
                Universal(phrase);

                Machine.St2.ProcessNext(GetNext());
            }
            public void ProcessNext(ConjunctionPhrase phrase) {
                Machine.lastConjunctive = phrase;
                Universal(phrase);
            }
            public void ProcessNext(PrepositionalPhrase phrase) {
                Machine.lastPrepositional = phrase;

                Universal(phrase);
                Machine.St0.ProcessNext(GetNext());
            }
        }
        class State3 : State
        {
            public State3(ObjectBinder machine)
                : base(machine, "State 3") {

            }

        }
        class State2 : State
        {


            public State2(ObjectBinder machine)
                : base(machine, "State 2") {

            }
            public void ProcessNext(ConjunctionPhrase phrase) {
                phrase.OnLeft = Machine.entities.Peek();
                Machine.lastConjunctive = phrase;
                Machine.ConjunctNounPhrases.Add(Machine.entities.Peek());
                if (Stream.Count < 1) {
                    if (!Machine.directFound)
                        Machine.AssociateDirect();
                    else
                        Machine.AssociateIndirect();

                    return;
                }
                Universal(phrase);

                Machine.St4.ProcessNext(GetNext());
            }
            public void ProcessNext(AdjectivePhrase phrase) {
                Machine.AssociateIndirect();
                Universal(phrase);

                Machine.St4.ProcessNext(GetNext());
            }

            public void ProcessNext(PrepositionalPhrase phrase) {
                foreach (var e in Machine.entities)
                    Machine.bindingTarget.BindDirectObject(e);
                Machine.lastPrepositional = phrase;

                Machine.entities.Last().PrepositionOnRight = Machine.lastPrepositional;
                phrase.OnLeftSide = Machine.entities.Last();
                Machine.entities.Clear();
                Machine.directFound = true;
                Machine.ConjunctNounPhrases.Clear();
                Universal(phrase);

                Machine.St0.ProcessNext(GetNext());
            }
            public void ProcessNext(NounPhrase phrase) {
                foreach (var e in Machine.entities)
                    Machine.bindingTarget.BindIndirectObject(e);
                Machine.entities.Clear();

                Machine.ConjunctNounPhrases.Clear();

                Machine.entities.Push(phrase);

                if (Stream.Count < 1) {

                    Machine.AssociateDirect();
                    return;
                }
                Universal(phrase);

                Machine.St0.ProcessNext(GetNext());
            }
            public void ProcessNext(SubordinateClauseBeginPhrase phrase) {
                Machine.AssociateDirect();
                Machine.AssociateIndirect();
                Universal(phrase);

            }
            public virtual void ProcessNext(AdverbPhrase phrase) {
                Machine.bindingTarget.ModifyWith(phrase);
                Universal(phrase);
                foreach (var e in Machine.entities) if (!Machine.directFound)
                        Machine.bindingTarget.BindDirectObject(e);
                    else
                        Machine.bindingTarget.BindIndirectObject(e);
                if (Stream.Count < 1)
                    return;
                Machine.St0.ProcessNext(GetNext());

            }

        }
        class State4 : State
        {


            public State4(ObjectBinder machine)
                : base(machine, "State 4") {
            }
            public void ProcessNext(NounPhrase phrase) {
                Machine.entities.Push(phrase);
                Machine.ConjunctNounPhrases.Add(phrase);
                Machine.lastConjunctive.OnRight = phrase;
                if (Stream.Count < 1) {
                    if (!Machine.directFound)
                        Machine.AssociateDirect();
                    else
                        Machine.AssociateIndirect();

                    return;
                }
                Universal(phrase);

                Machine.St2.ProcessNext(GetNext());
            }
            public void ProcessNext(AdjectivePhrase phrase) {
                Machine.lastAdjectivals.Add(phrase);
                if (Stream.Count < 1) {
                    if (!Machine.directFound)
                        Machine.AssociateDirect();
                    else
                        Machine.AssociateIndirect();

                    return;
                }
                Universal(phrase);

                Machine.St5.ProcessNext(GetNext());
            }
        }
        class State5 : State
        {
            public State5(ObjectBinder machine)
                : base(machine, "State 5") {
            }

            public void ProcessNext(NounPhrase phrase) {
                Machine.ConjunctNounPhrases.Add(phrase);
                Machine.entities.Push(phrase);
                Machine.BindBuiltupAdjectivePhrases(phrase);
                if (Stream.Count < 1) {
                    if (!Machine.directFound)
                        Machine.AssociateDirect();
                    else
                        Machine.AssociateIndirect();

                    return;
                }
                Universal(phrase);

                Machine.St2.ProcessNext(GetNext());
            }
            public void ProcessNext(ConjunctionPhrase phrase) {
                phrase.OnLeft = Machine.lastAdjectivals.Last();
                Machine.lastConjunctive = phrase;
                if (Stream.Count < 1) {
                    if (!Machine.directFound)
                        Machine.AssociateDirect();
                    else
                        Machine.AssociateIndirect();

                    return;
                }
                Universal(phrase);

                Machine.St6.ProcessNext(GetNext());
            }

        }
        class State6 : State
        {


            public State6(ObjectBinder machine)
                : base(machine, "State 6") {
            }
            public void ProcessNext(AdjectivePhrase phrase) {
                Machine.lastAdjectivals.Add(phrase);
                Machine.lastConjunctive.OnRight = phrase;
                if (Stream.Count < 1) {
                    if (!Machine.directFound)
                        Machine.AssociateDirect();
                    else
                        Machine.AssociateIndirect();

                    return;
                }
                Universal(phrase);

                Machine.St5.ProcessNext(GetNext());
            }
            public void ProcessNext(NounPhrase phrase) {
                Machine.entities.Push(phrase);
                Machine.ConjunctNounPhrases.Add(phrase);
                Machine.lastConjunctive.OnRight = phrase;
                Machine.BindBuiltupAdjectivePhrases(phrase);
                if (Stream.Count < 1) {
                    if (!Machine.directFound)
                        Machine.AssociateDirect();
                    else
                        Machine.AssociateIndirect();

                    return;
                }
                Universal(phrase);

                Machine.St2.ProcessNext(GetNext());
            }


        }


    }

    static class DynamicStackExtensions
    {
        /// <summary>
        /// An extension method which pops the next item from the stack, but returns it as a an object of Type dynamic.
        /// This allows the overloaded methods present in each state to be correctly selected based on the run time type of the entity.
        /// </summary>
        /// <param name="stack">The Stack<LASI.Algorithm.Phrase> instances from which to pop.</param>
        /// <returns>The Phrase at the top of the stack typed as dynamic.</returns>
        /// <exception cref="System.InvalidOperationException">Thrown if the Stack is empty.</exception>
        internal static dynamic PopDynamic(this Stack<Phrase> stack) {
            return stack.Pop();
        }
        internal static void PushAll(this Stack<Phrase> stack, IEnumerable<Phrase> items) {
            foreach (var i in items)
                stack.Push(i);
        }
    }

}
