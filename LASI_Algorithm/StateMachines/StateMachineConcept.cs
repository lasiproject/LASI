using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LASI.Algorithm.StateMachines
{
    /// <summary>
    /// Binds a collection of words together in linear order
    /// </summary>
    class StateMachineConcept
    {
        Stack<NounPhrase> nounPhraseStack = new Stack<NounPhrase>();
        Stack<VerbPhrase> verbPhraseStack = new Stack<VerbPhrase>();
        Stack<TransitiveVerbPhrase> transitiveVerbPhraseStack = new Stack<TransitiveVerbPhrase>();
        Stack<AdjectivePhrase> adjectivePhraseStack = new Stack<AdjectivePhrase>();
        Stack<AdverbPhrase> adverbPhraseStack = new Stack<AdverbPhrase>();
        private State0 state0;
        private State1 state1;
        private InputPhraseStream inputFeed;
        public StateMachineConcept() {
            state0 = new State0(this);
            state1 = new State1(this);
        }



        internal class State0
        {
            public State0(StateMachineConcept owner) {
                machine = owner;
            }
            private readonly StateMachineConcept machine;
            public void ProcessNext(Phrase InvalidInput) {
                throw new InvalidStateTransitionException("State0", InvalidInput);
            }
            public void ProcessNext(NounPhrase nounPhrase) {
                machine.nounPhraseStack.Push(nounPhrase);
                machine.state1.ProcessNext(machine.inputFeed.Next);
            }
            public void ProcessNext(VerbPhrase verbPhrasePhrase) {
                throw new NotImplementedException();
            }
            public void ProcessNext(TransitiveVerbPhrase transitiveVerbPhrase) {
                throw new NotImplementedException();
            }
            public void ProcessNext(PronounPhrase pronounPhrase) {
                throw new NotImplementedException();
            }
            public void ProcessNext(AdjectivePhrase adjectivePhrase) {
                throw new NotImplementedException();
            }
            public void ProcessNext(AdverbPhrase adverbPhrase) {
                throw new NotImplementedException();
            }


        }
        public void Start(InputPhraseStream phrases) {
            inputFeed = phrases;
            state0.ProcessNext(phrases.Next);

        }

    }


    internal class State1
    {
        private StateMachineConcept machine;

        public State1(StateMachineConcept owner) {
            machine = owner;
        }
        public void ProcessNext(AdjectivePhrase adjectivePhrase) {
            throw new NotImplementedException();
        }
    }


    internal class InputPhraseStream
    {
        public InputPhraseStream(IEnumerable<Phrase> phrases) {
            phraseStack = new Stack<Phrase>(phrases);
        }
        public InputPhraseStream(Sentence phrases) {
            phraseStack = new Stack<Phrase>(phrases.Phrases);
        }
        public InputPhraseStream(Clause phrases) {
            phraseStack = new Stack<Phrase>(phrases.Phrases);

        }
        public dynamic Next {
            get {
                return phraseStack.Pop();
            }
        }
        Stack<Phrase> phraseStack;
    }

}
