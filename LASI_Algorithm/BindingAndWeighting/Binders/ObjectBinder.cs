using LASI.Core.DocumentStructures;
using LASI.Utilities;
using LASI.Core.Patternization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LASI.Core.Binding
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
            St4 = new State3(this);
            St5 = new State4(this);
            St6 = new State5(this);
        }

        #region Methods

        /// <summary>
        /// Performance IVerbal :=: IVerbalObject binding between the applicable elements within the provuded sentence.
        /// </summary>
        /// <param name="sentence">The Sentence to bind within.</param>
        public void Bind(Sentence sentence) {
            if (sentence.Phrases.OfVerbPhrase().None()) {
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
                        St0.Transition(inputstream.Get());
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
        private State3 St4;
        private State4 St5;
        private State5 St6;
        private ConjunctionPhrase lastConjunctive;
        private PrepositionalPhrase lastPrepositional;
        #endregion

        #region Helper Classes
        class PhraseStackWrapper
        {
            protected internal PhraseStackWrapper(Stack<Phrase> source, ObjectBinder machine) { Machine = machine; stream = new Stack<Phrase>(source); }
            public ObjectBinder Machine { get; private set; }
            public Phrase Get() { return stream.Pop(); }
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
                try { InternalBind(phrase); }
                catch (InvalidOperationException) { PerformExceptionFallback(); }
            }

            private void InternalBind(Phrase phrase) {
                phrase.Match()
                    .Case<PrepositionalPhrase>(phr => {
                        Machine.lastPrepositional = phr;

                        if (Machine.inputstream.Count > 1)
                            Machine.St0.Transition(Stream.Get());
                    })
                    .Case<VerbPhrase>(phr => new ObjectBinder().Bind(Stream.ToList().Prepend(phr)))
                    .Case<AdverbPhrase>(phr => {
                        Machine.bindingTarget.ModifyWith(phr);
                        if (Stream.None)
                            return;
                        Machine.St0.Transition(Stream.Get());
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
                        Machine.St2.Transition(Stream.Get());
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

                        Machine.St2.Transition(Stream.Get());
                    })
                    .Case<AdjectivePhrase>(phr => {
                        Machine.lastAdjectivals.Add(phr);
                        if (Machine.inputstream.Any)
                            Machine.St1.Transition(Stream.Get());
                    })
                    .Case<SubordinateClauseBeginPhrase>(WhenSbar)
                    .Case<SymbolPhrase>(WhenSbar)
                    .Default(() => base.Transition(phrase));
            }

            private void WhenSbar(Phrase phrase) {
                var subordinateClauseConstituents = new List<Phrase> {
                    phrase};
                for (var r = Stream.Count > 0 ? Stream.Get() : null; r != null && !(r.Words.First() is Punctuator) && Stream.Count > 0; r = Stream.Get()) {
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
                try { InternalBind(phrase); }
                catch (InvalidOperationException) { PerformExceptionFallback(); }
            }

            private void InternalBind(Phrase phrase) {
                phrase.Match()
                .Case<VerbPhrase>(phr => {
                    phr.AdjectivalModifier = Machine.lastAdjectivals.Last();
                    Machine.lastAdjectivals.Clear();
                    Machine.St1.Transition(Stream.Get());
                })
                .Case<NounPhrase>(phr => {
                    Machine.entities.Push(phr);
                    Machine.BindBuiltupAdjectivePhrases(phr);
                    if (Stream.None) {
                        if (!Machine.directFound) { Machine.AssociateDirect(); } else { Machine.AssociateIndirect(); }
                        return;
                    }
                    Machine.St2.Transition(Stream.Get());
                })
                .Case<PrepositionalPhrase>(phr => {
                    Machine.lastPrepositional = phr;
                    Machine.St0.Transition(Stream.Get());
                })
                .Case<ConjunctionPhrase>(phr => Machine.lastConjunctive = phr)
                .Default(() => base.Transition(phrase));
            }

        }

        class State2 : State
        {
            public State2(ObjectBinder machine) : base(machine, "s2") { }

            public override void Transition(Phrase phrase) {
                try { InternalBind(phrase); }
                catch (InvalidOperationException) { PerformExceptionFallback(); }
            }

            private void InternalBind(Phrase phrase) {
                phrase.Match()
                    .Case<ConjunctionPhrase>(phr => {
                        phr.JoinedLeft = Machine.entities.Peek();
                        Machine.lastConjunctive = phr;
                        Machine.ConjunctNounPhrases.Add(Machine.entities.Peek());
                        if (Stream.None) {
                            if (!Machine.directFound)
                                Machine.AssociateDirect();
                            else
                                Machine.AssociateIndirect();
                            return;
                        }
                        Machine.St4.Transition(Stream.Get());
                    })
                    .Case<AdjectivePhrase>(phr => {
                        Machine.AssociateIndirect();
                        Machine.St4.Transition(Stream.Get());
                    })
                    .Case<AdverbPhrase>(phr => {
                        Machine.bindingTarget.ModifyWith(phr);
                        foreach (var e in Machine.entities) {
                            if (!Machine.directFound)
                                Machine.bindingTarget.BindDirectObject(e);
                            else
                                Machine.bindingTarget.BindIndirectObject(e);
                        }
                        if (Stream.Any) { Machine.St0.Transition(Stream.Get()); }
                    })
                    .Case<VerbPhrase>(phr => {
                        InfinitivePhrase infinitive = new InfinitivePhrase(
                            phrase.Words.Concat(
                            phrase.Sentence.GetPhrasesAfter(phrase).TakeWhile(w => !(w is IConjunctive || w is IPrepositional)).OfWord()));
                        Machine.directObject = infinitive;
                    })
                    .Case<PrepositionalPhrase>(phr => {
                        foreach (var e in Machine.entities) { Machine.bindingTarget.BindDirectObject(e); }
                        Machine.lastPrepositional = phr;
                        Machine.entities.Last().PrepositionOnRight = Machine.lastPrepositional;
                        phr.ToTheLeftOf = Machine.entities.Last();
                        Machine.entities.Clear();
                        Machine.directFound = true;
                        Machine.ConjunctNounPhrases.Clear();
                        Machine.St0.Transition(Stream.Get());
                    })
                    .Case<NounPhrase>(phr => {
                        foreach (var e in Machine.entities) { Machine.bindingTarget.BindIndirectObject(e); }
                        Machine.entities.Clear();
                        Machine.ConjunctNounPhrases.Clear();
                        Machine.entities.Push(phr);
                        if (Stream.None) {
                            Machine.AssociateDirect();
                            return;
                        }
                        Machine.St0.Transition(Stream.Get());
                    })
                    .Case<SymbolPhrase>(phr => {
                        WhenSbar(phr);
                    })
                    .Case<SubordinateClauseBeginPhrase>(phr => {
                        WhenSbar(phr);
                    })
                    .Default(() => base.Transition(phrase));
            }

            [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "phrase")]
            private void WhenSbar(Phrase phrase) {
                while (Stream.Count > 1) {
                    var endOfSbar = Stream.Get();
                    if (endOfSbar is SymbolPhrase || endOfSbar is SubordinateClauseBeginPhrase) { break; }
                }
                this.Transition(Stream.Get());
                PerformExceptionFallback();
            }

        }

        class State3 : State
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

                    Machine.St5.Transition(Stream.Get());

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
                    Machine.St2.Transition(Stream.Get());
                })
               .Default(() => base.Transition(phrase));
            }
            public State3(ObjectBinder machine) : base(machine, "s3") { }
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
                    Machine.St2.Transition(Stream.Get());
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

                    Machine.St6.Transition(Stream.Get());
                })
               .Default(() => base.Transition(phrase));
            }
            public State4(ObjectBinder machine) : base(machine, "s4") { }
        }

        class State5 : State
        {

            public override void Transition(Phrase phrase) {
                try { InternalBind(phrase); }
                catch (InvalidOperationException) {
                    PerformExceptionFallback();
                }
            }

            private void InternalBind(Phrase phrase) {
                phrase.Match()
                .Case<AdjectivePhrase>(phr => {
                    Machine.lastAdjectivals.Add(phr);
                    Machine.lastConjunctive.JoinedRight = phrase;
                    if (Stream.None) {
                        if (!Machine.directFound)
                            Machine.AssociateDirect();
                        else
                            Machine.AssociateIndirect();
                        return;
                    }
                    Machine.St5.Transition(Stream.Get());
                })
                .Case<NounPhrase>(phr => {
                    Machine.entities.Push(phr);
                    Machine.ConjunctNounPhrases.Add(phr);
                    Machine.lastConjunctive.JoinedRight = phr;
                    Machine.BindBuiltupAdjectivePhrases(phr);
                    if (Stream.None) {
                        if (!Machine.directFound)
                            Machine.AssociateDirect();
                        else
                            Machine.AssociateIndirect();
                        return;
                    }
                    Machine.St2.Transition(Stream.Get());
                })
                .Default(() => base.Transition(phrase));
            }

            public State5(ObjectBinder machine) : base(machine, "s5") { }
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
