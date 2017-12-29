using System;
using System.Collections.Generic;
using System.Linq;
using LASI.Utilities;

namespace LASI.Core.Analysis.Binding
{
    /// <summary>
    /// Attempts to establish bindings between verbals and their objects at the Phrase level.
    /// </summary>
    internal sealed partial class ObjectBinder : IIntraSentenceBinder
    {
        /// <summary>
        /// Initializes a new instance of ObjectBinder class.
        /// </summary>
        public ObjectBinder()
        {
            state0 = new State0(this);
            state1 = new State1(this);
            state2 = new State2(this);
            state3 = new State3(this);
            state4 = new State4(this);
            state5 = new State5(this);
        }

        #region Methods

        /// <summary>
        /// Performance IVerbal :=: IVerbalObject binding between the applicable elements within the provided sentence.
        /// </summary>
        /// <param name="sentence">The Sentence to bind within.</param>
        public void Bind(Sentence sentence)
        {
            if (!sentence.Phrases.OfVerbPhrase().Any())
            {
                throw new VerblessPhrasalSequenceException(sentence.Phrases);
            }
            Bind(sentence.Phrases);
        }

        /// <summary>
        /// Performs IVerbal :=: IVerbalObject binding between the applicable elements within the provided sequence of Phrase instances.
        /// </summary>
        /// <param name="contiguousPhrases">The sequence of Phrase instances to bind within.</param>
        public void Bind(IEnumerable<Phrase> contiguousPhrases)
        {
            var phrases = contiguousPhrases.ToList();
            var verbPhraseIndex = phrases.FindIndex(r => r is VerbPhrase);
            bindingTarget = contiguousPhrases.ElementAtOrDefault(verbPhraseIndex) as VerbPhrase;
            if (bindingTarget != null)
            {
                var remainingPhrases = phrases.Skip(verbPhraseIndex + 1);
                if (remainingPhrases.Any())
                {
                    source = new Stack<Phrase>(remainingPhrases);
                    inputStream = new PhraseStackWrapper(source);
                    foreach (var state in States)
                    {
                        state.Stream = inputStream;
                    }
                    try
                    {
                        state0.Transition(inputStream.Get());
                    }
                    catch (InvalidOperationException e)
                    {
                        e.Log();
                    }
                }
            }
        }
        private IEnumerable<State> States
        {
            get
            {
                yield return state0;
                yield return state1;
                yield return state2;
                yield return state3;
                yield return state4;
                yield return state5;
            }
        }
        void BindDirect()
        {
            foreach (var e in entities)
            {
                bindingTarget.BindDirectObject(e);
            }
            entities.Clear();
            conjunctNounPhrases.Clear();
            directBound = true;
        }
        void BindIndirect()
        {
            foreach (var e in entities)
            {
                bindingTarget.BindIndirectObject(e);
            }
            entities.Clear();
            conjunctNounPhrases.Clear();
        }
        void BindBuiltupAdjectivePhrases(NounPhrase phrase)
        {
            foreach (var adjp in this.lastAdjectivals)
            {
                phrase.BindDescriptor(adjp);
            }
            this.lastAdjectivals.Clear();
        }
        #endregion

        #region Fields
        IVerbalObject directObject;
        bool directBound;
        VerbPhrase bindingTarget;
        Stack<Phrase> source;
        PhraseStackWrapper inputStream;
        List<AdjectivePhrase> lastAdjectivals = new List<AdjectivePhrase>();
        List<NounPhrase> conjunctNounPhrases = new List<NounPhrase>();
        Stack<NounPhrase> entities = new Stack<NounPhrase>();
        State0 state0;
        State1 state1;
        State2 state2;
        State3 state3;
        State4 state4;
        State5 state5;
        ConjunctionPhrase lastConjunctive;
        IPrepositional lastPrepositional;
        #endregion


        #region State Classes

        sealed class State0 : State
        {
            public State0(ObjectBinder machine) : base(machine, nameof(state0)) { }

            public override void Transition(Phrase phrase)
            {
                try
                {
                    InternalBind(phrase);
                }
                catch (InvalidOperationException)
                {
                    PerformExceptionFallback();
                }
            }

            protected override void InternalBind(Phrase phrase)
            {
                phrase.Match()
                    .Case((PrepositionalPhrase p) =>
                    {
                        Machine.lastPrepositional = p;
                        if (Machine.inputStream.Count > 1)
                        {
                            Machine.state0.Transition(Stream.Get());
                        }
                    })
                    .Case((VerbPhrase v) => new ObjectBinder().Bind(Stream.ToList().Prepend(v)))
                    .Case((AdverbPhrase phr) =>
                    {
                        Machine.bindingTarget.ModifyWith(phr);
                        if (Stream.Any)
                        {
                            Machine.state0.Transition(Stream.Get());
                        }
                    })
                    .Case((ConjunctionPhrase c) =>
                    {
                        if (Machine.lastPrepositional != null)
                        {
                            c.BindLeftPrepositional(Machine.lastPrepositional);
                            Machine.lastPrepositional.ToTheRightOf = c;
                        }
                        BindIfExhaustedOrContinueVia(ToState2);
                    })
                    .Case((NounPhrase n) =>
                    {
                        if (Machine.lastPrepositional != null)
                        {
                            n.BindLeftPrepositional(Machine.lastPrepositional);
                            Machine.lastPrepositional.ToTheRightOf = n;
                            Machine.bindingTarget.AttachObjectViaPreposition(n.LeftPrepositional);
                        }
                        Machine.entities.Push(n);
                        BindIfExhaustedOrContinueVia(ToState2);
                    })
                    .Case((AdjectivePhrase a) =>
                    {
                        Machine.lastAdjectivals.Add(a);
                        if (Machine.inputStream.Any)
                        {
                            Machine.state1.Transition(Stream.Get());
                        }
                    })
                    .Case((SubordinateClauseBeginPhrase a) => WhenSbar(a))
                    .Case((SymbolPhrase a) => WhenSbar(a))
                    .Default(() => base.Transition(phrase));
            }

            void WhenSbar(Phrase phrase)
            {
                var subordinateClauseConstituents = new List<Phrase> { phrase };
                for (var r = Stream.Count > 0 ? Stream.Get() : null; r != null && !(r.Words.First() is Punctuator) && Stream.Count > 0; r = Stream.Get())
                {
                    subordinateClauseConstituents.Add(r);
                }
                var subordinateClause = new SubordinateClause(subordinateClauseConstituents);
                Machine.bindingTarget.ModifyWith(subordinateClause);
                new ObjectBinder().Bind(subordinateClauseConstituents);
            }
        }

        sealed class State1 : State
        {
            public State1(ObjectBinder machine) : base(machine, nameof(state1)) { }
            public override void Transition(Phrase phrase)
            {
                try { InternalBind(phrase); } catch (InvalidOperationException) { PerformExceptionFallback(); }
            }

            protected override void InternalBind(Phrase phrase)
            {
                phrase.Match()
                    .Case((VerbPhrase v) =>
                    {
                        v.PostpositiveDescriptor = Machine.lastAdjectivals.Last();
                        Machine.lastAdjectivals.Clear();
                        Machine.state1.Transition(Stream.Get());
                    })
                    .Case((NounPhrase n) =>
                    {
                        Machine.entities.Push(n);
                        Machine.BindBuiltupAdjectivePhrases(n);
                        BindIfExhaustedOrContinueVia(ToState2);
                    })
                    .Case((PrepositionalPhrase p) =>
                    {
                        Machine.lastPrepositional = p;
                        Machine.state0.Transition(Stream.Get());
                    })
                    .Case((ConjunctionPhrase c) =>
                    {
                        Machine.lastConjunctive = c;
                    })
                    .Default(() => base.Transition(phrase));
            }
        }
        sealed class State2 : State
        {
            public State2(ObjectBinder machine) : base(machine, nameof(state2)) { }
            public override void Transition(Phrase phrase)
            {
                try
                {
                    InternalBind(phrase);
                }
                catch (InvalidOperationException)
                {
                    PerformExceptionFallback();
                }
            }

            protected override void InternalBind(Phrase phrase)
            {
                phrase.Match()
                    .Case((ConjunctionPhrase c) =>
                    {
                        c.JoinedLeft = Machine.entities.Peek();
                        Machine.lastConjunctive = c;
                        Machine.conjunctNounPhrases.Add(Machine.entities.Peek());
                        BindIfExhaustedOrContinueVia(ToState3);
                    })
                    .Case((AdjectivePhrase a) =>
                    {
                        Machine.BindIndirect();
                        Machine.state3.Transition(Stream.Get());
                    })
                    .Case((AdverbPhrase a) =>
                    {
                        Machine.bindingTarget.ModifyWith(a);
                        foreach (var e in Machine.entities)
                        {
                            if (!Machine.directBound)
                            {
                                Machine.bindingTarget.BindDirectObject(e);
                            }
                            else
                            {
                                Machine.bindingTarget.BindIndirectObject(e);
                            }
                        }
                        if (Stream.Any)
                        {
                            Machine.state0.Transition(Stream.Get());
                        }
                    })
                    .Case((VerbPhrase v) =>
                    {
                        var infinitive = new InfinitivePhrase(
                            phrase.Words.Concat(
                            phrase.Sentence.GetPhrasesAfter(phrase).TakeWhile(w => !(w is IConjunctive || w is IPrepositional)).OfWord()));
                        Machine.directObject = infinitive;
                    })
                    .Case((IPrepositional p) =>
                    {
                        foreach (var e in Machine.entities) { Machine.bindingTarget.BindDirectObject(e); }
                        Machine.lastPrepositional = p;
                        Machine.entities.Last().BindRightPrepositional(Machine.lastPrepositional);
                        p.ToTheLeftOf = Machine.entities.Last();
                        Machine.entities.Clear();
                        Machine.directBound = true;
                        Machine.conjunctNounPhrases.Clear();
                        Machine.state0.Transition(Stream.Get());
                    })
                    .Case((NounPhrase n) =>
                    {
                        foreach (var e in Machine.entities)
                        {
                            Machine.bindingTarget.BindIndirectObject(e);
                        }
                        Machine.entities.Clear();
                        Machine.conjunctNounPhrases.Clear();
                        Machine.entities.Push(n);
                        if (Stream.None)
                        {
                            Machine.BindDirect();
                        }
                        else
                        {
                            ToState0();
                        }
                    })
                   .Case((SymbolPhrase s) => WhenSbar())
                   .Case((SubordinateClauseBeginPhrase s) => WhenSbar())
                   .Default(() => base.Transition(phrase));
            }

            void WhenSbar()
            {
                while (Stream.Count > 1)
                {
                    var endOfSbar = Stream.Get();
                    if (endOfSbar is SymbolPhrase || endOfSbar is SubordinateClauseBeginPhrase)
                    {
                        break;
                    }
                }
                this.Transition(Stream.Get());
                PerformExceptionFallback();
            }
        }

        sealed class State3 : State
        {
            public override void Transition(Phrase phrase)
            {
                try
                {
                    InternalBind(phrase);
                }
                catch (InvalidOperationException)
                {
                    PerformExceptionFallback();
                }
            }

            protected override void InternalBind(Phrase phrase)
            {
                phrase.Match()
                .Case((AdjectivePhrase a) =>
                {
                    Machine.lastAdjectivals.Add(a);
                    BindIfExhaustedOrContinueVia(ToState4);
                })
                .Case((NounPhrase n) =>
                {
                    Machine.entities.Push(n);
                    Machine.conjunctNounPhrases.Add(n);
                    if (Machine.lastConjunctive != null)
                    {
                        Machine.lastConjunctive.JoinedRight = n;
                    }
                    BindIfExhaustedOrContinueVia(ToState2);
                })
               .Default(() => base.Transition(phrase));
            }
            public State3(ObjectBinder machine) : base(machine, nameof(state3)) { }
        }

        sealed class State4 : State
        {
            public override void Transition(Phrase phrase)
            {
                try
                {
                    InternalBind(phrase);
                }
                catch (InvalidOperationException)
                {
                    PerformExceptionFallback();
                }
            }
            protected override void InternalBind(Phrase phrase)
            {
                phrase.Match()
                .Case((NounPhrase n) =>
                {
                    Machine.conjunctNounPhrases.Add(n);
                    Machine.entities.Push(n);
                    Machine.BindBuiltupAdjectivePhrases(n);
                    BindIfExhaustedOrContinueVia(ToState2);
                })
                .Case((ConjunctionPhrase c) =>
                {
                    c.JoinedLeft = Machine.lastAdjectivals.Last();
                    Machine.lastConjunctive = c;
                    BindIfExhaustedOrContinueVia(ToState5);
                })
               .Default(() => base.Transition(phrase));
            }
            public State4(ObjectBinder machine) : base(machine, nameof(state4)) { }
        }

        sealed class State5 : State
        {
            public override void Transition(Phrase phrase)
            {
                try
                {
                    InternalBind(phrase);
                }
                catch (InvalidOperationException)
                {
                    PerformExceptionFallback();
                }
            }

            protected override void InternalBind(Phrase phrase)
            {
                phrase.Match()
                .Case((AdjectivePhrase a) =>
                {
                    Machine.lastAdjectivals.Add(a);
                    Machine.lastConjunctive.JoinedRight = phrase;
                    BindIfExhaustedOrContinueVia(ToState4);
                })
                .Case((NounPhrase n) =>
                {
                    Machine.entities.Push(n);
                    Machine.conjunctNounPhrases.Add(n);
                    Machine.lastConjunctive.JoinedRight = n;
                    Machine.BindBuiltupAdjectivePhrases(n);
                    BindIfExhaustedOrContinueVia(ToState2);
                })
                .Default(() => base.Transition(phrase));
            }


            public State5(ObjectBinder machine) : base(machine, nameof(state5)) { }
        }
        abstract class State
        {
            protected void ToState0() => Machine.state0.Transition(Stream.Get());
            protected void ToState1() => Machine.state1.Transition(Stream.Get());
            protected void ToState2() => Machine.state2.Transition(Stream.Get());
            protected void ToState3() => Machine.state3.Transition(Stream.Get());
            protected void ToState4() => Machine.state4.Transition(Stream.Get());
            protected void ToState5() => Machine.state5.Transition(Stream.Get());

            protected abstract void InternalBind(Phrase phrase);

            protected State(ObjectBinder machine, string name)
            {
                Machine = machine;
                Stream = machine.inputStream; Name = name;
            }
            public virtual void Transition(Phrase phrase)
            {
                throw new InvalidStateTransitionException(Name, phrase);
            }
            public virtual void PerformExceptionFallback()
            {
                Machine.BindDirect();
                Machine.BindIndirect();
            }
            protected void BindIfUnbound()
            {
                if (!Machine.directBound)
                {
                    Machine.BindDirect();
                }
                else
                {
                    Machine.BindIndirect();
                }
            }
            protected void BindIfExhaustedOrContinueVia(Action continuation)
            {
                if (Stream.None)
                {
                    BindIfUnbound();
                }
                else
                {
                    continuation();
                }
            }

            protected string Name { get; }
            protected ObjectBinder Machine { get; }
            public PhraseStackWrapper Stream { get; set; }
        }
        #endregion State Classes
    }
}
