using System;
using System.Collections.Generic;
using System.Linq;
using LASI.Utilities;

namespace LASI.Core.Analysis.Binding
{
    /// <summary>
    /// Attempts to establish bindings between verbals and their objects at the Phrase level.
    /// </summary>
    public class ObjectBinder : IIntraSentenceBinder
    {
        #region Fields
        private IVerbalObject directObject;
        private bool directFound;
        private VerbPhrase bindingTarget;
        private Stack<Phrase> source;
        private PhraseStackWrapper inputstream;
        private List<AdjectivePhrase> lastAdjectivals = new List<AdjectivePhrase>();
        private List<NounPhrase> ConjunctNounPhrases = new List<NounPhrase>();
        private Stack<NounPhrase> entities = new Stack<NounPhrase>();
        private S0 s0;
        private S1 s1;
        private S2 s2;
        private S3 s3;
        private S4 s4;
        private S5 s5;
        private ConjunctionPhrase lastConjunctive;
        private IPrepositional lastPrepositional;
        #endregion

        /// <summary>
        /// Initializes a new instance of ObjectBinder class.
        /// </summary>
        public ObjectBinder()
        {
            s0 = new S0(this);
            s1 = new S1(this);
            s2 = new S2(this);
            s3 = new S3(this);
            s4 = new S4(this);
            s5 = new S5(this);
        }

        #region Methods

        /// <summary>
        /// Performance IVerbal :=: IVerbalObject binding between the applicable elements within the provided sentence.
        /// </summary>
        /// <param name="sentence">The Sentence to bind within.</param>
        public void Bind(Sentence sentence)
        {
            if (sentence.Phrases.OfVerbPhrase().None())
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
                    inputstream = new PhraseStackWrapper(source, this);
                    foreach (var state in States)
                    {
                        state.Stream = inputstream;
                    }
                    try
                    {
                        s0.Transition(inputstream.Get());
                    }
                    catch (InvalidOperationException e)
                    {
                        e.Log();
                    }
                }
            }
        }
        IEnumerable<State> States
        {
            get
            {
                yield return s0;
                yield return s1;
                yield return s2;
                yield return s3;
                yield return s4;
                yield return s5;
            }
        }
        private void AssociateDirect()
        {
            foreach (var e in entities)
            {
                bindingTarget.BindDirectObject(e);
            }
            entities.Clear();
            ConjunctNounPhrases.Clear();
            directFound = true;
        }
        private void AssociateIndirect()
        {
            foreach (var e in entities)
            {
                bindingTarget.BindIndirectObject(e);
            }
            entities.Clear();
            ConjunctNounPhrases.Clear();
        }
        private void BindBuiltupAdjectivePhrases(NounPhrase phrase)
        {
            foreach (var adjp in this.lastAdjectivals)
            {
                phrase.BindDescriptor(adjp);
            }
            this.lastAdjectivals.Clear();
        }
        #endregion



        #region State Classes

        class S0 : State
        {
            public S0(ObjectBinder machine) : base(machine, "s0") { }

            public override void Transition(Phrase phrase)
            {
                try { InternalBind(phrase); } catch (InvalidOperationException) { PerformExceptionFallback(); }
            }

            private void InternalBind(Phrase phrase)
            {
                phrase.Match()
                    .Case((PrepositionalPhrase p) =>
                    {
                        M.lastPrepositional = p;
                        if (M.inputstream.Count > 1) { M.s0.Transition(Stream.Get()); }
                    })
                    .Case((VerbPhrase v) => new ObjectBinder().Bind(Stream.ToList().Prepend(v)))
                    .Case((AdverbPhrase phr) =>
                    {
                        M.bindingTarget.ModifyWith(phr);
                        if (Stream.Any)
                            M.s0.Transition(Stream.Get());
                    })
                    .Case((ConjunctionPhrase c) =>
                    {
                        if (M.lastPrepositional != null)
                        {
                            c.PrepositionOnLeft = M.lastPrepositional;
                            M.lastPrepositional.ToTheRightOf = c;
                        }
                        if (Stream.None)
                        {
                            if (!M.directFound) { M.AssociateDirect(); } else { M.AssociateIndirect(); }
                            return;
                        }
                        M.s2.Transition(Stream.Get());
                    })
                    .Case((NounPhrase n) =>
                    {
                        if (M.lastPrepositional != null)
                        {
                            n.PrepositionOnLeft = M.lastPrepositional;
                            M.lastPrepositional.ToTheRightOf = n;
                            M.bindingTarget.AttachObjectViaPreposition(n.PrepositionOnLeft);
                        }
                        M.entities.Push(n);
                        if (Stream.None)
                        {
                            if (!M.directFound) { M.AssociateDirect(); } else { M.AssociateIndirect(); }
                            return;
                        }
                        M.s2.Transition(Stream.Get());
                    })
                    .Case((AdjectivePhrase a) =>
                    {
                        M.lastAdjectivals.Add(a);
                        if (M.inputstream.Any)
                            M.s1.Transition(Stream.Get());
                    })
                    .Case<SubordinateClauseBeginPhrase>(a => WhenSbar(a))
                    .Case<SymbolPhrase>(a => WhenSbar(a))
                    .Default(() => base.Transition(phrase));
            }

            private void WhenSbar(Phrase phrase)
            {
                var subordinateClauseConstituents = new List<Phrase> {
                    phrase};
                for (var r = Stream.Count > 0 ? Stream.Get() : null; r != null && !(r.Words.First() is Punctuator) && Stream.Count > 0; r = Stream.Get())
                {
                    subordinateClauseConstituents.Add(r);
                }
                var subClause = new SubordinateClause(subordinateClauseConstituents);
                M.bindingTarget.ModifyWith(subClause);
                new ObjectBinder().Bind(subordinateClauseConstituents);
            }
        }

        class S1 : State
        {
            public S1(ObjectBinder machine) : base(machine, "s1") { }
            public override void Transition(Phrase phrase)
            {
                try { InternalBind(phrase); } catch (InvalidOperationException) { PerformExceptionFallback(); }
            }

            private void InternalBind(Phrase phrase)
            {
                phrase.Match()
                    .Case((VerbPhrase v) =>
                    {
                        v.PostpositiveDescriptor = M.lastAdjectivals.Last();
                        M.lastAdjectivals.Clear();
                        M.s1.Transition(Stream.Get());
                    })
                    .Case((NounPhrase n) =>
                    {
                        M.entities.Push(n);
                        M.BindBuiltupAdjectivePhrases(n);
                        if (Stream.None)
                        {
                            if (!M.directFound) { M.AssociateDirect(); } else { M.AssociateIndirect(); }
                            return;
                        }
                        M.s2.Transition(Stream.Get());
                    })
                    .Case((PrepositionalPhrase p) =>
                    {
                        M.lastPrepositional = p;
                        M.s0.Transition(Stream.Get());
                    })
                    .Case((Action<ConjunctionPhrase>)(c => M.lastConjunctive = c))
                .Default(() => base.Transition(phrase));
            }

        }
        class S2 : State
        {
            public S2(ObjectBinder machine) : base(machine, "s2") { }
            public override void Transition(Phrase phrase)
            {
                try { InternalBind(phrase); } catch (InvalidOperationException) { PerformExceptionFallback(); }
            }

            private void InternalBind(Phrase phrase)
            {
                phrase.Match()
                    .Case((ConjunctionPhrase c) =>
                    {
                        c.JoinedLeft = M.entities.Peek();
                        M.lastConjunctive = c;
                        M.ConjunctNounPhrases.Add(M.entities.Peek());
                        if (Stream.None)
                        {
                            if (!M.directFound) { M.AssociateDirect(); } else { M.AssociateIndirect(); }
                            return;
                        }
                        M.s3.Transition(Stream.Get());
                    })
                    .Case((AdjectivePhrase a) =>
                    {
                        M.AssociateIndirect();
                        M.s3.Transition(Stream.Get());
                    })
                    .Case((AdverbPhrase a) =>
                    {
                        M.bindingTarget.ModifyWith(a);
                        foreach (var e in M.entities)
                        {
                            if (!M.directFound)
                            {
                                M.bindingTarget.BindDirectObject(e);
                            }
                            else
                            {
                                M.bindingTarget.BindIndirectObject(e);
                            }
                        }
                        if (Stream.Any) { M.s0.Transition(Stream.Get()); }
                    })
                    .Case((VerbPhrase v) =>
                    {
                        InfinitivePhrase infinitive = new InfinitivePhrase(
                            phrase.Words.Concat(
                            phrase.Sentence.GetPhrasesAfter(phrase).TakeWhile(w => !(w is IConjunctive || w is IPrepositional)).OfWord()));
                        M.directObject = infinitive;
                    })
                    .Case((IPrepositional p) =>
                    {
                        foreach (var e in M.entities) { M.bindingTarget.BindDirectObject(e); }
                        M.lastPrepositional = p;
                        M.entities.Last().PrepositionOnRight = M.lastPrepositional;
                        p.ToTheLeftOf = M.entities.Last();
                        M.entities.Clear();
                        M.directFound = true;
                        M.ConjunctNounPhrases.Clear();
                        M.s0.Transition(Stream.Get());
                    })
                    .Case((NounPhrase n) =>
                    {
                        foreach (var e in M.entities) { M.bindingTarget.BindIndirectObject(e); }
                        M.entities.Clear();
                        M.ConjunctNounPhrases.Clear();
                        M.entities.Push(n);
                        if (Stream.None)
                        {
                            M.AssociateDirect();
                            return;
                        }
                        M.s0.Transition(Stream.Get());
                    })
                   .Case((SymbolPhrase s) => WhenSbar())
                   .Case((SubordinateClauseBeginPhrase s) => WhenSbar())
                   .Default(() => base.Transition(phrase));
            }

            private void WhenSbar()
            {
                while (Stream.Count > 1)
                {
                    var endOfSbar = Stream.Get();
                    if (endOfSbar is SymbolPhrase || endOfSbar is SubordinateClauseBeginPhrase) { break; }
                }
                this.Transition(Stream.Get());
                PerformExceptionFallback();
            }

        }

        class S3 : State
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

            private void InternalBind(Phrase phrase)
            {
                phrase.Match()
                .Case<AdjectivePhrase>(phr =>
                {
                    M.lastAdjectivals.Add(phr);
                    if (Stream.None)
                    {
                        if (!M.directFound) { M.AssociateDirect(); } else { M.AssociateIndirect(); }
                        return;
                    }
                    M.s4.Transition(Stream.Get());
                })
                .Case<NounPhrase>(n =>
                {
                    M.entities.Push(n);
                    M.ConjunctNounPhrases.Add(n);
                    if (M.lastConjunctive != null)
                    {
                        M.lastConjunctive.JoinedRight = n;
                    }
                    if (Stream.None)
                    {
                        if (!M.directFound) { M.AssociateDirect(); } else { M.AssociateIndirect(); }
                        return;
                    }
                    M.s2.Transition(Stream.Get());
                })
               .Default(() => base.Transition(phrase));
            }
            public S3(ObjectBinder machine) : base(machine, "s3") { }
        }

        class S4 : State
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
            private void InternalBind(Phrase phrase)
            {
                phrase.Match()
                .Case<NounPhrase>(n =>
                {
                    M.ConjunctNounPhrases.Add(n);
                    M.entities.Push(n);
                    M.BindBuiltupAdjectivePhrases(n);
                    if (Stream.None)
                    {
                        if (!M.directFound) { M.AssociateDirect(); } else { M.AssociateIndirect(); }
                        return;
                    }
                    M.s2.Transition(Stream.Get());
                })
                .Case<ConjunctionPhrase>(c =>
                {
                    c.JoinedLeft = M.lastAdjectivals.Last();
                    M.lastConjunctive = c;
                    if (Stream.None)
                    {
                        if (!M.directFound) { M.AssociateDirect(); } else { M.AssociateIndirect(); }
                        return;
                    }

                    M.s5.Transition(Stream.Get());
                })
               .Default(() => base.Transition(phrase));
            }
            public S4(ObjectBinder machine) : base(machine, "s4") { }
        }

        class S5 : State
        {

            public override void Transition(Phrase phrase)
            {
                try { InternalBind(phrase); }
                catch (InvalidOperationException)
                {
                    PerformExceptionFallback();
                }
            }

            private void InternalBind(Phrase phrase)
            {
                phrase.Match()
                .Case<AdjectivePhrase>(a =>
                {
                    M.lastAdjectivals.Add(a);
                    M.lastConjunctive.JoinedRight = phrase;
                    if (Stream.None)
                    {
                        if (!M.directFound) { M.AssociateDirect(); } else { M.AssociateIndirect(); }
                        return;
                    }
                    M.s4.Transition(Stream.Get());
                })
                .Case<NounPhrase>(n =>
                {
                    M.entities.Push(n);
                    M.ConjunctNounPhrases.Add(n);
                    M.lastConjunctive.JoinedRight = n;
                    M.BindBuiltupAdjectivePhrases(n);
                    if (Stream.None)
                    {
                        if (!M.directFound) { M.AssociateDirect(); } else { M.AssociateIndirect(); }
                        return;
                    }
                    M.s2.Transition(Stream.Get());
                })
                .Default(() => base.Transition(phrase));
            }

            public S5(ObjectBinder machine) : base(machine, "s5") { }
        }
        abstract class State
        {
            protected State(ObjectBinder machine, string stateName) { M = machine; Stream = machine.inputstream; StateName = stateName; }
            public virtual void Transition(Phrase phrase) { throw new InvalidStateTransitionException(StateName, phrase); }
            public virtual void PerformExceptionFallback()
            {
                M.AssociateDirect();
                M.AssociateIndirect();
            }
            protected string StateName { get; private set; }
            public ObjectBinder M { get; private set; }
            public PhraseStackWrapper Stream { get; set; }
        }
        #endregion

        #region Helper Classes
        private class PhraseStackWrapper
        {
            public PhraseStackWrapper(Stack<Phrase> source, ObjectBinder machine) { Machine = machine; stream = new Stack<Phrase>(source); }
            public ObjectBinder Machine { get; private set; }
            public Phrase Get() { return stream.Pop(); }
            public bool Any { get { return stream.Any(); } }
            public bool None { get { return !Any; } }
            public int Count { get { return stream.Count; } }
            public List<Phrase> ToList() { return stream.ToList(); }
            private Stack<Phrase> stream;
        }
        #endregion

    }
}
