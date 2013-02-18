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
        static VerbPhrase TransitionWith(VerbPhrase v) {
            Console.WriteLine("verb mthd called");
            return v;
        }

        static NounPhrase TransitionWith(NounPhrase n) {
            Console.WriteLine("noun mthd called");
            return n;
        }

        static PronounPhrase TransitionWith(PronounPhrase p) {
            Console.WriteLine("pronoun mthd called");
            return p;
        }
        static ParticlePhrase TransitionWith(ParticlePhrase p) {
            Console.WriteLine("particle mthd called");
            return p;
        }
        static AdverbPhrase TransitionWith(AdverbPhrase a) {
            Console.WriteLine("adverb mthd called");
            return a;
        }
        static AdjectivePhrase TransitionWith(AdjectivePhrase a) {
            Console.WriteLine("adjective mthd called");
            return a;
        }
        static PrepositionalPhrase TransitionWith(PrepositionalPhrase a) {
            Console.WriteLine("prepositional mthd called");
            return a;
        }
        static ConjunctionPhrase TransitionWith(ConjunctionPhrase a) {
            Console.WriteLine("conjunction mthd called");
            return a;
        }
        static RoughListPhrase TransitionWith(RoughListPhrase a) {
            Console.WriteLine("list mthd called");
            return a;
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
