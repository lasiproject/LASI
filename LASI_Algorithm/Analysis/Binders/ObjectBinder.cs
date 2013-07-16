using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LASI.Utilities;
using LASI.Utilities.TypedSwitch;
using LASI.Algorithm.DocumentConstructs;

namespace LASI.Algorithm.Binding
{
    /// <summary>
    /// Attempts to establish bindings between verbals and their objects at the Phrase level.
    /// </summary>
    public class ObjectBinder
    {
        /// <summary>
        /// Initializes a new instances of ObjectBinder class.
        /// </summary>
        public ObjectBinder() {
            St0 = new State0(this);
            St1 = new State1(this);
            St2 = new State2(this);
            St3 = new State3(this);
            St4 = new State4(this);
            St5 = new State5(this);
            St6 = new State6(this);
        }
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
        /// Performance IVerbal :=: IVerbalObject binding between the applicable elements within the provuded sequence of Phrase instances.
        /// </summary>
        /// <param name="contiguousPhrases">The sequence of Phrase instances to bind within.</param>
        public void Bind(IEnumerable<Phrase> contiguousPhrases) {
            var phrases = contiguousPhrases.ToList();
            var verbPhraseIndex = phrases.FindIndex(r => r is VerbPhrase);
            bindingTarget = contiguousPhrases.ElementAtOrDefault(verbPhraseIndex) as VerbPhrase;
            if (bindingTarget == null)
                return;
            var remainingPhrases = phrases.Skip(verbPhraseIndex + 1).Reverse();
            if (remainingPhrases.Any()) {
                foreach (var phrase in remainingPhrases) {
                    inputstream.Push(phrase);
                }
                try {
                    St0.Transition(inputstream.Pop() as dynamic);
                }
                catch (InvalidOperationException) {
                }
            }
        }





        private void TestBind(IEnumerable<Phrase> sequence) {
            var target = sequence.First(p => p is VerbPhrase) as VerbPhrase;
            var remaining = sequence.GetPhrasesAfter(bindingTarget);
            if (!remaining.Any())
                return;
            TypedBind(remaining, target);
        }

        private void TypedBind(IEnumerable<Phrase> remaining, VerbPhrase target) {
            remaining.First().Switch()
                .Case<AdverbPhrase>(ap => {
                    target.ModifyWith(ap);
                    TypedBind(remaining.Skip(1), ap, target);
                })
                .Case<NounPhrase>(np => TypedBind(remaining.Skip(1), np, target));

        }

        private void TypedBind(IEnumerable<Phrase> remaining, AdverbPhrase ap, VerbPhrase target) {
            remaining.First().Switch()
                .Case<AdjectivePhrase>(p => TypedBind(remaining, p, target));

        }

        private void TypedBind(IEnumerable<Phrase> remaining, AdjectivePhrase p, VerbPhrase target) {
            remaining.First().Switch()
                .Case<NounPhrase>(np => {
                    np.BindDescriptor(p);
                })
                .Case<AdverbPhrase>(ap => {
                    p.ModifyWith(ap);
                })
                .Case<ConjunctionPhrase>(cp => {

                });
        }



        private void TypedBind(IEnumerable<Phrase> remaining, NounPhrase np, VerbPhrase target) {
            remaining.First().Switch()
                .Case<PrepositionalPhrase>(pp => {
                    target.BindIndirectObject(np);
                    TypedBind(remaining, pp, target);
                })
                .Case<ConjunctionPhrase>(cp => {
                    TypedBind(remaining, cp, target);
                });


        }

        private void TypedBind(IEnumerable<Phrase> remaining, ConjunctionPhrase cp, VerbPhrase target) {
            remaining.First().Switch()
                .Case<NounPhrase>(np => cp.JoinedRight = np);
        }

        private void TypedBind(IEnumerable<Phrase> remaining, PrepositionalPhrase pp, VerbPhrase target) {
            remaining.First().Switch()
               .Case<NounPhrase>(np => {
                   target.BindDirectObject(np);
                   TypedBind(remaining, np, target);
               });
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
            IndirectFound = true;
        }
        private void BindBuiltupAdjectivePhrases(NounPhrase phrase) {
            foreach (var adjp in this.lastAdjectivals) {
                phrase.BindDescriptor(adjp);
            }
            this.lastAdjectivals.Clear();
        }


        private Phrase LastPhrase {
            get;
            set;
        }
        #region Fields
        private Stack<Phrase> inputstream = new Stack<Phrase>();
        private VerbPhrase bindingTarget;
        private IVerbalObject directObject;

        private IVerbalObject DirectObject {
            get { return directObject; }
            set { directObject = value; }
        }
        private IVerbalObject indirectObject;

        private IVerbalObject IndirectObject {
            get { return indirectObject; }
            set { indirectObject = value; }
        }
        private bool directFound;
        private bool indirectFound;

        private bool IndirectFound {
            get { return indirectFound; }
            set { indirectFound = value; }
        }

        private List<AdjectivePhrase> lastAdjectivals = new List<AdjectivePhrase>();
        private List<NounPhrase> ConjunctNounPhrases = new List<NounPhrase>();
        private List<AdjectivePhrase> ConjunctAdjectivePhrases = new List<AdjectivePhrase>();
        private Stack<NounPhrase> entities = new Stack<NounPhrase>();
        private State0 St0;
        private State1 St1;
        private State2 St2;
        private State3 St3;
        private State4 St4;
        private State5 St5;
        private State6 St6;
        private ConjunctionPhrase lastConjunctive;
        private PrepositionalPhrase lastPrepositional;


        #endregion




        abstract class State
        {
            protected State(ObjectBinder machine) {
                _machine = machine;
                _stream = machine.inputstream;
            }

            public virtual void Transition(Phrase phrase) {
                throw new InvalidStateTransitionException(StateName, phrase);
            }

            protected void Universal(Phrase Phrase) {
                Machine.LastPhrase = Phrase;
            }

            protected string StateName {
                get {
                    return stateName;
                }
                set {
                    stateName = value;
                }
            }


            public ObjectBinder Machine {
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
            private ObjectBinder _machine;


        }



        class State0 : State
        {
            public State0(ObjectBinder machine)
                : base(machine) {
                StateName = "s0";
            }
            public void Transition(PrepositionalPhrase phrase) {
                Machine.lastPrepositional = phrase;
                try {
                    if (Machine.inputstream.Count > 1)
                        Machine.St0.Transition(Stream.Pop() as dynamic);
                }
                catch (InvalidOperationException) {
                    Machine.AssociateDirect();
                    Machine.AssociateIndirect();
                }
            }
            public void Transition(VerbPhrase phrase) {
                new ObjectBinder().Bind(new[] { phrase }.Concat(Stream.ToList()));
            }
            public void Transition(SubordinateClauseBeginPhrase phrase) {
                var subordinateClauseConstituents = new List<Phrase> {
                    phrase};
                for (var r = Stream.Count > 0 ? Stream.Pop() : null; !(r.Words.First() is Punctuation) && Stream.Count > 0; r = Stream.Pop()) {
                    subordinateClauseConstituents.Add(r);
                }
                var subClause = new ClauseTypes.SubordinateClause(subordinateClauseConstituents);
                Machine.bindingTarget.ModifyWith(subClause);
                new ObjectBinder().Bind(subordinateClauseConstituents);
            }

            public void Transition(AdverbPhrase phrase) {
                Machine.bindingTarget.ModifyWith(phrase);
                Universal(phrase);
                if (!Stream.Any())
                    return;
                try {
                    Machine.St0.Transition(Stream.Pop() as dynamic);
                }
                catch (InvalidOperationException) {
                    Machine.AssociateDirect();
                    Machine.AssociateIndirect();
                }

            }


            public void Transition(ConjunctionPhrase phrase) {
                if (Machine.lastPrepositional != null) {
                    phrase.PrepositionOnLeft = Machine.lastPrepositional;
                    Machine.lastPrepositional.ToTheRightOf = phrase;

                }
                //Machine(parent);
                if (!Stream.Any()) {
                    if (!Machine.directFound)
                        Machine.AssociateDirect();
                    else
                        Machine.AssociateIndirect();

                    return;
                }
                Universal(phrase);

                try {
                    Machine.St2.Transition(Stream.Pop() as dynamic);
                }
                catch (InvalidOperationException) {
                    Machine.AssociateDirect();
                    Machine.AssociateIndirect();
                }

            }


            public void Transition(NounPhrase phrase) {
                if (Machine.lastPrepositional != null) {
                    phrase.PrepositionOnLeft = Machine.lastPrepositional;
                    Machine.lastPrepositional.ToTheRightOf = phrase;
                    Machine.bindingTarget.AttachObjectViaPreposition(phrase.PrepositionOnLeft);
                }
                Machine.entities.Push(phrase);
                if (!Stream.Any()) {
                    if (!Machine.directFound)
                        Machine.AssociateDirect();
                    else
                        Machine.AssociateIndirect();

                    return;
                }
                Universal(phrase);

                try {
                    Machine.St2.Transition(Stream.Pop() as dynamic);
                }
                catch (InvalidOperationException) {
                    Machine.AssociateDirect();
                    Machine.AssociateIndirect();
                }
            }
            public void Transition(AdjectivePhrase phrase) {
                Machine.lastAdjectivals.Add(phrase);
                Universal(phrase);
                if (Machine.inputstream.Count > 0) {
                    try {
                        Machine.St1.Transition(Stream.Pop() as dynamic);
                    }
                    catch (InvalidOperationException) {
                        Machine.AssociateDirect();
                        Machine.AssociateIndirect();
                    }
                }
            }
        }


        class State1 : State
        {
            public State1(ObjectBinder machine)
                : base(machine) {
                StateName = "s1";
            }

            public void Transition(VerbPhrase phrase) {
                phrase.AdjectivalModifier = Machine.lastAdjectivals.Last();
                Machine.lastAdjectivals.Clear();
                try {
                    Machine.St1.Transition(Stream.Pop() as dynamic);
                }
                catch (InvalidOperationException) {
                    Machine.AssociateDirect();
                    Machine.AssociateIndirect();
                }
            }
            public void Transition(NounPhrase phrase) {
                Machine.entities.Push(phrase);
                Machine.BindBuiltupAdjectivePhrases(phrase);
                if (!Stream.Any()) {
                    if (!Machine.directFound)
                        Machine.AssociateDirect();
                    else
                        Machine.AssociateIndirect();

                    return;
                }
                Universal(phrase);

                try {
                    Machine.St2.Transition(Stream.Pop() as dynamic);
                }
                catch (InvalidOperationException) {
                    Machine.AssociateDirect();
                    Machine.AssociateIndirect();
                }
            }
            public void Transition(ConjunctionPhrase phrase) {
                Machine.lastConjunctive = phrase;
                Universal(phrase);
            }
            public void Transition(PrepositionalPhrase phrase) {
                Machine.lastPrepositional = phrase;

                Universal(phrase);
                try {
                    Machine.St0.Transition(Stream.Pop() as dynamic);
                }
                catch (InvalidOperationException) {
                    Machine.AssociateDirect();
                    Machine.AssociateIndirect();
                }
            }
        }
        class State3 : State
        {
            public State3(ObjectBinder machine)
                : base(machine) {
                StateName = "s3";
            }

        }
        class State2 : State
        {


            public State2(ObjectBinder machine)
                : base(machine) {
                StateName = "s2";

            }
            public void Transition(ConjunctionPhrase phrase) {
                phrase.JoinedLeft = Machine.entities.Peek();
                Machine.lastConjunctive = phrase;
                Machine.ConjunctNounPhrases.Add(Machine.entities.Peek());
                if (!Stream.Any()) {
                    if (!Machine.directFound)
                        Machine.AssociateDirect();
                    else
                        Machine.AssociateIndirect();

                    return;
                }
                Universal(phrase);


                try {
                    Machine.St4.Transition(Stream.Pop() as dynamic);
                }
                catch (InvalidOperationException) {
                    Machine.AssociateDirect();
                    Machine.AssociateIndirect();
                }

            }
            public void Transition(AdjectivePhrase phrase) {
                Machine.AssociateIndirect();
                Universal(phrase);

                try {
                    Machine.St4.Transition(Stream.Pop() as dynamic);
                }
                catch (InvalidOperationException) {
                    Machine.AssociateDirect();
                    Machine.AssociateIndirect();
                }

            }
            public void Transition(VerbPhrase phrase) {
                InfinitivePhrase infinitive = new InfinitivePhrase(
                    phrase.Words.Concat(
                   phrase.Sentence.GetPhrasesAfter(phrase)
                    .TakeWhile(w => !(w is IConjunctive || w is IPrepositional)).GetWords()));
                Machine.DirectObject = infinitive;

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
                Universal(phrase);

                try {
                    Machine.St0.Transition(Stream.Pop() as dynamic);
                }
                catch (InvalidOperationException) {
                    Machine.AssociateDirect();
                    Machine.AssociateIndirect();
                }
            }
            public void Transition(NounPhrase phrase) {
                foreach (var e in Machine.entities)
                    Machine.bindingTarget.BindIndirectObject(e);
                Machine.entities.Clear();
                Machine.IndirectFound = true;
                Machine.ConjunctNounPhrases.Clear();

                Machine.entities.Push(phrase);

                if (!Stream.Any()) {

                    Machine.AssociateDirect();
                    return;
                }
                Universal(phrase);

                try {
                    Machine.St0.Transition(Stream.Pop() as dynamic);
                }
                catch (InvalidOperationException) {
                    Machine.AssociateDirect();
                    Machine.AssociateIndirect();
                }
            }
            public void Transition(SubordinateClauseBeginPhrase phrase) {
                Machine.AssociateDirect();
                Machine.AssociateIndirect();
                Universal(phrase);

            }
            public virtual void Transition(AdverbPhrase phrase) {
                Machine.bindingTarget.ModifyWith(phrase);
                Universal(phrase);
                foreach (var e in Machine.entities) if (!Machine.directFound)
                        Machine.bindingTarget.BindDirectObject(e);
                    else
                        Machine.bindingTarget.BindIndirectObject(e);
                if (!Stream.Any())
                    return;
                try {
                    Machine.St0.Transition(Stream.Pop() as dynamic);
                }
                catch (InvalidOperationException) {
                    Machine.AssociateDirect();
                    Machine.AssociateIndirect();
                }

            }

        }
        class State4 : State
        {


            public State4(ObjectBinder machine)
                : base(machine) {

                StateName = "s4";
            }
            public void Transition(NounPhrase phrase) {
                Machine.entities.Push(phrase);
                Machine.ConjunctNounPhrases.Add(phrase);
                if (Machine.lastConjunctive != null) {
                    Machine.lastConjunctive.JoinedRight = phrase;
                }
                if (!Stream.Any()) {
                    if (!Machine.directFound)
                        Machine.AssociateDirect();
                    else
                        Machine.AssociateIndirect();

                    return;
                }
                Universal(phrase);

                try {
                    Machine.St2.Transition(Stream.Pop() as dynamic);
                }
                catch (InvalidOperationException) {
                    Machine.AssociateDirect();
                    Machine.AssociateIndirect();
                }
            }
            public void Transition(AdjectivePhrase phrase) {
                Machine.lastAdjectivals.Add(phrase);
                if (!Stream.Any()) {
                    if (!Machine.directFound)
                        Machine.AssociateDirect();
                    else
                        Machine.AssociateIndirect();

                    return;
                }
                Universal(phrase);

                try {
                    Machine.St5.Transition(Stream.Pop() as dynamic);
                }
                catch (InvalidOperationException) {
                    Machine.AssociateDirect();
                    Machine.AssociateIndirect();
                }
            }
        }
        class State5 : State
        {
            public State5(ObjectBinder machine)
                : base(machine) {
                StateName = "s5";
            }

            public void Transition(NounPhrase phrase) {
                Machine.ConjunctNounPhrases.Add(phrase);
                Machine.entities.Push(phrase);
                Machine.BindBuiltupAdjectivePhrases(phrase);
                if (!Stream.Any()) {
                    if (!Machine.directFound)
                        Machine.AssociateDirect();
                    else
                        Machine.AssociateIndirect();

                    return;
                }
                Universal(phrase);

                try {
                    Machine.St2.Transition(Stream.Pop() as dynamic);
                }
                catch (InvalidOperationException) {
                    Machine.AssociateDirect();
                    Machine.AssociateIndirect();
                }
            }
            public void Transition(ConjunctionPhrase phrase) {
                phrase.JoinedLeft = Machine.lastAdjectivals.Last();
                Machine.lastConjunctive = phrase;
                if (!Stream.Any()) {
                    if (!Machine.directFound)
                        Machine.AssociateDirect();
                    else
                        Machine.AssociateIndirect();

                    return;
                }
                Universal(phrase);

                try {
                    Machine.St6.Transition(Stream.Pop() as dynamic);
                }
                catch (InvalidOperationException) {
                    Machine.AssociateDirect();
                    Machine.AssociateIndirect();
                }
            }

        }
        class State6 : State
        {


            public State6(ObjectBinder machine)
                : base(machine) {
                StateName = "s6";
            }
            public void Transition(AdjectivePhrase phrase) {
                Machine.lastAdjectivals.Add(phrase);
                Machine.lastConjunctive.JoinedRight = phrase;
                if (!Stream.Any()) {
                    if (!Machine.directFound)
                        Machine.AssociateDirect();
                    else
                        Machine.AssociateIndirect();

                    return;
                }
                Universal(phrase);

                try {
                    Machine.St5.Transition(Stream.Pop() as dynamic);
                }
                catch (InvalidOperationException) {
                    Machine.AssociateDirect();
                    Machine.AssociateIndirect();
                }
            }
            public void Transition(NounPhrase phrase) {
                Machine.entities.Push(phrase);
                Machine.ConjunctNounPhrases.Add(phrase);
                Machine.lastConjunctive.JoinedRight = phrase;
                Machine.BindBuiltupAdjectivePhrases(phrase);
                if (!Stream.Any()) {
                    if (!Machine.directFound)
                        Machine.AssociateDirect();
                    else
                        Machine.AssociateIndirect();

                    return;
                }
                Universal(phrase);

                try {
                    Machine.St2.Transition(Stream.Pop() as dynamic);
                }
                catch (InvalidOperationException) {
                    Machine.AssociateDirect();
                    Machine.AssociateIndirect();
                }
            }
        }
    }



}
