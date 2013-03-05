using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LASI.Algorithm
{
    public class SubjectBinder
    {
        List<State> stateList = new List<State>();

        public void bind(Sentence s) {
            //State s1 = new State();
            VerbPhrase v1 = null;
            //s1.S = StateType.Initial;

            //stateList.Add(s1);
            foreach (var i in s.Phrases) {
                if (i is AdjectivePhrase) {
                    State s2 = new State();
                    s2.StatePhrase = i;
                    stateList.Add(s2);
                }
                if (i is NounPhrase) {
                    State s3 = new State();
                    s3.StatePhrase = i;
                    stateList.Add(s3);
                }
                if (i is VerbPhrase && i.Words.Count(n => n is PresentParticipleGerund) > 0) {
                    State s4 = new State();
                    s4.StatePhrase = i;
                    stateList.Add(s4);
                    v1 = s4.StatePhrase as VerbPhrase;
                    break;
                }
                if (i is ConjunctionPhrase) {
                    State s5 = new State();
                    s5.StatePhrase = i;
                    stateList.Add(s5);
                }
                if (i is VerbPhrase && i.Words.Count(n => n is PresentParticipleGerund) == 0) {
                    State s6 = new State();
                    s6.StatePhrase = i;
                    s6.S = StateType.Final;
                    stateList.Add(s6);
                    v1 = s6.StatePhrase as VerbPhrase;
                    break;                    
                }
                if (i is AdverbPhrase) {
                    State s7 = new State();
                    s7.StatePhrase = i;
                    stateList.Add(s7);
                }
                if (i is PrepositionalPhrase) {
                    State s8 = new State();
                    s8.StatePhrase = i;
                    stateList.Add(s8);
                }
                if (i is ParticlePhrase) {
                    State s9 = new State();
                    stateList.Add(s9);
                    s9.StatePhrase = i;
                }
                if (i is InterjectionPhrase) {
                    State s10 = new State();
                    s10.StatePhrase = i;
                    stateList.Add(s10);
                }

            }
            foreach (var i in stateList)
            {
                if(i.StatePhrase is NounPhrase)
                    v1.BindSubject(i.StatePhrase as NounPhrase);
            }
        }
        public void display() {

            for (int i = 0; i < stateList.Count; i++) {

                Console.Write(stateList[i].StatePhrase);
                Console.Write("\n");
            }
            //Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~");
        }
        class State
        {
            public State() {
                S = StateType.Default;
            }
            public StateType S {
                get;
                set;
            }
            public int count {
                get;
                private set;
            }
            public Phrase StatePhrase {
                get;
                set;
            }

        }
        enum StateType
        {
            Initial,
            Default,
            Fail,
            Final
        }
    }
}

