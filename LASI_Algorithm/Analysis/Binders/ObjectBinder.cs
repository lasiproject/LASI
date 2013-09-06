using LASI.Algorithm.DocumentConstructs;
using LASI.Utilities;
using LASI.Algorithm.Patternization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LASI.Utilities.FunctionExtensions;
namespace LASI.Algorithm.Binding
{
    /// <summary>
    /// Attempts to establish bindings between verbals and their objects at the Phrase level.
    /// </summary>
    public class ObjectBinder
    {
        /// <summary>
        /// Initializes a new instance of ObjectBinder class.
        /// </summary>
        public ObjectBinder() {
            St0 = new State0(this);
            St1 = new State1(this);
            St2 = new State2(this);
            St4 = new State4(this);
            St5 = new State5(this);
            St6 = new State6(this);
        }

        #region Methods

        /// <summary>
        /// Performance IVerbal :=: IVerbalObject binding between the applicable elements within the provuded sentence.
        /// </summary>
        /// <param name="sentence">The Sentence to bind within.</param>
        public void Bind(Sentence sentence) {
            if (!sentence.Phrases.GetVerbPhrases().Any()) {
                throw new VerblessPhrasalSequenceException();
            }
            Bind(sentence.Phrases);
        }

        /// <summary>
        /// Performs IVerbal :=: IVerbalObject binding between the applicable elements within the provuded sequence of Phrase instances.
        /// </summary>
        /// <param name="contiguousPhrases">The sequence of Phrase instances to bind within.</param>
        public void Bind(IEnumerable<Phrase> contiguousPhrases) {
            var phrases = contiguousPhrases.ToList();
            var verbPhraseIndex = phrases.FindIndex(r => r is VerbPhrase);
            bindingTarget = contiguousPhrases.ElementAtOrDefault(verbPhraseIndex) as VerbPhrase;
            if (bindingTarget != null) {
                var remainingPhrases = phrases.Skip(verbPhraseIndex + 1);
                if (remainingPhrases.Any()) {
                    source = new Stack<Phrase>(remainingPhrases);
                    inputstream = new PhraseStackWrapper(source, this);
                    new State[] { St0, St1, St2, St4, St5, St6 }.ToList().ForEach(state => state.Stream = inputstream);
                    try {
                        St0.Transition(inputstream.Pop() as dynamic);
                    }
                    catch (InvalidOperationException) {
                    }
                }
            }
        }
        private void AssociateDirect() {
            foreach (var e in entities) {
                bindingTarget.BindDirectObject(e);
            }
            entities.Clear();
            ConjunctNounPhrases.Clear();
            directFound = true;
        }
        private void AssociateIndirect() {
            foreach (var e in entities) {
                bindingTarget.BindIndirectObject(e);
            }
            entities.Clear();
            ConjunctNounPhrases.Clear();
        }
        private void BindBuiltupAdjectivePhrases(NounPhrase phrase) {
            foreach (var adjp in this.lastAdjectivals) {
                phrase.BindDescriptor(adjp);
            }
            this.lastAdjectivals.Clear();
        }
        #endregion

        #region Fields
        private IVerbalObject directObject;
        private bool directFound;
        private VerbPhrase bindingTarget;
        private Stack<Phrase> source;
        private PhraseStackWrapper inputstream;
        private List<AdjectivePhrase> lastAdjectivals = new List<AdjectivePhrase>();
        private List<NounPhrase> ConjunctNounPhrases = new List<NounPhrase>();
        private List<AdjectivePhrase> ConjunctAdjectivePhrases = new List<AdjectivePhrase>();
        private Stack<NounPhrase> entities = new Stack<NounPhrase>();
        private State0 St0;
        private State1 St1;
        private State2 St2;
        private State4 St4;
        private State5 St5;
        private State6 St6;
        private ConjunctionPhrase lastConjunctive;
        private PrepositionalPhrase lastPrepositional;
        #endregion

        #region Helper Classes
        class PhraseStackWrapper
        {
            protected internal PhraseStackWrapper(Stack<Phrase> source, ObjectBinder machine) { Machine = machine; stream = new Stack<Phrase>(source); }
            public ObjectBinder Machine { get; private set; }
            public Phrase Pop() { return stream.Pop(); }
            public bool Any { get { return stream.Any(); } }
            public bool None { get { return !Any; } }
            public int Count { get { return stream.Count; } }
            public List<Phrase> ToList() { return stream.ToList(); }
            private Stack<Phrase> stream;
        }
        #endregion

        #region State Classes

        class State0 : State
        {
            public State0(ObjectBinder machine) : base(machine, "s0") { }

            public override void Transition(Phrase phrase) {
                try {
                    InternalBind(phrase);
                }
                catch (InvalidOperationException) {
                    PerformExceptionFallback();
                }
            }

            private void InternalBind(Phrase phrase) {
                phrase.Match()
                    .Case<PrepositionalPhrase>(phr => {
                        Machine.lastPrepositional = phr;

                        if (Machine.inputstream.Count > 1)
                            Machine.St0.Transition(Stream.Pop() as dynamic);
                    })
                    .Case<VerbPhrase>(phr => new ObjectBinder().Bind(Stream.ToList().Prepend(phr)))
                    .Case<AdverbPhrase>(phr => {
                        Machine.bindingTarget.ModifyWith(phr);
                        if (Stream.None)
                            return;
                        Machine.St0.Transition(Stream.Pop() as dynamic);
                    })
                    .Case<ConjunctionPhrase>(phr => {
                        if (Machine.lastPrepositional != null) {
                            phr.PrepositionOnLeft = Machine.lastPrepositional;
                            Machine.lastPrepositional.ToTheRightOf = phr;

                        }
                        if (Stream.None) {
                            if (!Machine.directFound)
                                Machine.AssociateDirect();
                            else
                                Machine.AssociateIndirect();

                            return;
                        }
                        Machine.St2.Transition(Stream.Pop() as dynamic);
                    })
                    .Case<NounPhrase>(phr => {
                        if (Machine.lastPrepositional != null) {
                            phr.PrepositionOnLeft = Machine.lastPrepositional;
                            Machine.lastPrepositional.ToTheRightOf = phr;
                            Machine.bindingTarget.AttachObjectViaPreposition(phr.PrepositionOnLeft);
                        }
                        Machine.entities.Push(phr);
                        if (Stream.None) {
                            if (!Machine.directFound)
                                Machine.AssociateDirect();
                            else
                                Machine.AssociateIndirect();

                            return;
                        }

                        Machine.St2.Transition(Stream.Pop() as dynamic);
                    })
                    .Case<AdjectivePhrase>(phr => {
                        Machine.lastAdjectivals.Add(phr);
                        if (Machine.inputstream.Any)
                            Machine.St1.Transition(Stream.Pop() as dynamic);
                    })
                    .Case<SubordinateClauseBeginPhrase>(WhenSbar)
                    .Case<SymbolPhrase>(WhenSbar)
                    .Default(() => base.Transition(phrase));
            }

            private void WhenSbar(Phrase phrase) {
                var subordinateClauseConstituents = new List<Phrase> {
                    phrase};
                for (var r = Stream.Count > 0 ? Stream.Pop() : null; !(r.Words.First() is Punctuation) && Stream.Count > 0; r = Stream.Pop()) {
                    subordinateClauseConstituents.Add(r);
                }
                var subClause = new SubordinateClause(subordinateClauseConstituents);
                Machine.bindingTarget.ModifyWith(subClause);
                new ObjectBinder().Bind(subordinateClauseConstituents);
            }
        }

        class State1 : State
        {
            public State1(ObjectBinder machine) : base(machine, "s1") { }
            public override void Transition(Phrase phrase) {
                try {
                    InternalBind(phrase);
                }
                catch (InvalidOperationException) {
                    PerformExceptionFallback();
                }
            }

            private void InternalBind(Phrase phrase) {
                phrase.Match()
                .Case<VerbPhrase>(phr => {
                    phr.AdjectivalModifier = Machine.lastAdjectivals.Last();
                    Machine.lastAdjectivals.Clear();
                    Machine.St1.Transition(Stream.Pop());
                })
                .Case<NounPhrase>(phr => {
                    Machine.entities.Push(phr);
                    Machine.BindBuiltupAdjectivePhrases(phr);
                    if (Stream.None) {
                        if (!Machine.directFound)
                            Machine.AssociateDirect();
                        else
                            Machine.AssociateIndirect();
                        return;
                    }
                    Machine.St2.Transition(Stream.Pop() as dynamic);
                })
                .Case<PrepositionalPhrase>(phr => {
                    Machine.lastPrepositional = phr;
                    Machine.St0.Transition(Stream.Pop() as dynamic);
                })
                .Case<ConjunctionPhrase>(phr => Machine.lastConjunctive = phr)
                .Default(() => base.Transition(phrase));
            }

        }

        class State2 : State
        {
            public State2(ObjectBinder machine) : base(machine, "s2") { }
            public void Transition(ConjunctionPhrase phrase) {
                phrase.JoinedLeft = Machine.entities.Peek();
                Machine.lastConjunctive = phrase;
                Machine.ConjunctNounPhrases.Add(Machine.entities.Peek());
                if (Stream.None) {
                    if (!Machine.directFound)
                        Machine.AssociateDirect();
                    else
                        Machine.AssociateIndirect();

                    return;
                }

                try {
                    Machine.St4.Transition(Stream.Pop() as dynamic);
                }
                catch (InvalidOperationException) {
                    PerformExceptionFallback();
                }

            }
            public void Transition(AdjectivePhrase phrase) {
                Machine.AssociateIndirect();

                try {
                    Machine.St4.Transition(Stream.Pop() as dynamic);
                }
                catch (InvalidOperationException) {
                    PerformExceptionFallback();
                }

            }
            public void Transition(VerbPhrase phrase) {
                InfinitivePhrase infinitive = new InfinitivePhrase(
                    phrase.Words.Concat(
                   phrase.Sentence.GetPhrasesAfter(phrase)
                    .TakeWhile(w => !(w is IConjunctive || w is IPrepositional)).GetWords()));
                Machine.directObject = infinitive;

            }
            public void Transition(PrepositionalPhrase phrase) {
                foreach (var e in Machine.entities)
                    Machine.bindingTarget.BindDirectObject(e);
                try {
                    Machine.lastPrepositional = phrase;

                    Machine.entities.Last().PrepositionOnRight = Machine.lastPrepositional;
                }
                catch (InvalidOperationException) {
                }
                phrase.ToTheLeftOf = Machine.entities.Last();
                Machine.entities.Clear();
                Machine.directFound = true;
                Machine.ConjunctNounPhrases.Clear();
                try {
                    Machine.St0.Transition(Stream.Pop() as dynamic);
                }
                catch (InvalidOperationException) {
                    PerformExceptionFallback();
                }
            }
            public void Transition(NounPhrase phrase) {
                foreach (var e in Machine.entities)
                    Machine.bindingTarget.BindIndirectObject(e);
                Machine.entities.Clear();
                Machine.ConjunctNounPhrases.Clear();

                Machine.entities.Push(phrase);

                if (Stream.None) {

                    Machine.AssociateDirect();
                    return;
                }

                try {
                    Machine.St0.Transition(Stream.Pop() as dynamic);
                }
                catch (InvalidOperationException) {
                    PerformExceptionFallback();
                }
            }
            public void Transition(SymbolPhrase phrase) {
                WhenSbar(phrase);
            }
            public void Transition(SubordinateClauseBeginPhrase phrase) {
                WhenSbar(phrase);
            }

            private void WhenSbar(Phrase phrase) {
                while (Stream.Count > 1) {
                    var endOfSbar = Stream.Pop();
                    if (endOfSbar is SymbolPhrase || endOfSbar is SubordinateClauseBeginPhrase) { break; }
                }
                this.Transition(Stream.Pop() as dynamic);
                PerformExceptionFallback();
            }
            public void Transition(AdverbPhrase phrase) {
                Machine.bindingTarget.ModifyWith(phrase);
                foreach (var e in Machine.entities) {
                    if (!Machine.directFound)
                        Machine.bindingTarget.BindDirectObject(e);
                    else
                        Machine.bindingTarget.BindIndirectObject(e);
                }
                if (Stream.Any) {
                    try {
                        Machine.St0.Transition(Stream.Pop() as dynamic);
                    }
                    catch (InvalidOperationException) {
                        PerformExceptionFallback();
                    }
                } else { return; }

            }
        }


        class State4 : State
        {
            public override void Transition(Phrase phrase) {
                try {
                    InternalBind(phrase);
                }
                catch (InvalidOperationException) {
                    PerformExceptionFallback();
                }
            }

            private void InternalBind(Phrase phrase) {
                phrase.Match()
                .Case<AdjectivePhrase>(phr => {
                    Machine.lastAdjectivals.Add(phr);
                    if (Stream.None) {
                        if (!Machine.directFound)
                            Machine.AssociateDirect();
                        else
                            Machine.AssociateIndirect();
                        return;
                    }

                    Machine.St5.Transition(Stream.Pop() as dynamic);

                })
                .Case<NounPhrase>(phr => {
                    Machine.entities.Push(phr);
                    Machine.ConjunctNounPhrases.Add(phr);
                    if (Machine.lastConjunctive != null) {
                        Machine.lastConjunctive.JoinedRight = phr;
                    }
                    if (Stream.None) {
                        if (!Machine.directFound)
                            Machine.AssociateDirect();
                        else
                            Machine.AssociateIndirect();
                        return;
                    }
                    Machine.St2.Transition(Stream.Pop() as dynamic);
                })
               .Default(() => base.Transition(phrase));
            }
            public State4(ObjectBinder machine) : base(machine, "s4") { }
        }

        class State5 : State
        {
            public override void Transition(Phrase phrase) {
                try {
                    InternalBind(phrase);
                }
                catch (InvalidOperationException) {
                    PerformExceptionFallback();
                }
            }
            private void InternalBind(Phrase phrase) {
                phrase.Match()
                .Case<NounPhrase>(phr => {
                    Machine.ConjunctNounPhrases.Add(phr);
                    Machine.entities.Push(phr);
                    Machine.BindBuiltupAdjectivePhrases(phr);
                    if (Stream.None) {
                        if (!Machine.directFound)
                            Machine.AssociateDirect();
                        else
                            Machine.AssociateIndirect();

                        return;
                    }
                    Machine.St2.Transition(Stream.Pop() as dynamic);
                })
                .Case<ConjunctionPhrase>(phr => {
                    phr.JoinedLeft = Machine.lastAdjectivals.Last();
                    Machine.lastConjunctive = phr;
                    if (Stream.None) {
                        if (!Machine.directFound)
                            Machine.AssociateDirect();
                        else
                            Machine.AssociateIndirect();

                        return;
                    }

                    Machine.St6.Transition(Stream.Pop() as dynamic);
                })
               .Default(() => base.Transition(phrase));
            }
            public State5(ObjectBinder machine) : base(machine, "s5") { }
        }

        class State6 : State
        {
            public void Transition(AdjectivePhrase phrase) {
                Machine.lastAdjectivals.Add(phrase);
                Machine.lastConjunctive.JoinedRight = phrase;
                if (Stream.None) {
                    if (!Machine.directFound)
                        Machine.AssociateDirect();
                    else
                        Machine.AssociateIndirect();

                    return;
                }

                try {
                    Machine.St5.Transition(Stream.Pop() as dynamic);
                }
                catch (InvalidOperationException) {
                    PerformExceptionFallback();
                }
            }
            public void Transition(NounPhrase phrase) {
                Machine.entities.Push(phrase);
                Machine.ConjunctNounPhrases.Add(phrase);
                Machine.lastConjunctive.JoinedRight = phrase;
                Machine.BindBuiltupAdjectivePhrases(phrase);
                if (Stream.None) {
                    if (!Machine.directFound)
                        Machine.AssociateDirect();
                    else
                        Machine.AssociateIndirect();

                    return;
                }

                try {
                    Machine.St2.Transition(Stream.Pop() as dynamic);
                }
                catch (InvalidOperationException) {
                    PerformExceptionFallback();
                }
            }
            public State6(ObjectBinder machine) : base(machine, "s6") { }
        }


        abstract class State
        {
            protected State(ObjectBinder machine, string stateName) { Machine = machine; Stream = machine.inputstream; StateName = stateName; }
            public virtual void Transition(Phrase phrase) { throw new InvalidStateTransitionException(StateName, phrase); }
            public virtual void PerformExceptionFallback() {
                Machine.AssociateDirect();
                Machine.AssociateIndirect();
            }
            protected string StateName { get; private set; }
            public ObjectBinder Machine { get; private set; }
            public PhraseStackWrapper Stream { get; set; }
        #endregion
        }
    }
}
