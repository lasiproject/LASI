using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LASI.Algorithm.Binding
{
    public class SubjectBinder
    {
        List<State> stateList = new List<State>();
        /// <summary>
        /// This is the Bind function for the SubjectBinder Class 
        /// </summary>
        /// <param name="s"></param>
        public void Bind(Sentence s) {

            List<VerbPhrase> v1 = new List<VerbPhrase>(s.Phrases.GetVerbPhrases());

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
                    //subject for normal sentence.
                    if ((i.PreviousPhrase is NounPhrase) && 
                        (i.PreviousPhrase.ParentSentence == i.ParentSentence) && 
                        !(i.PreviousPhrase as NounPhrase).wasBound)
                    {
                        
                        (i as VerbPhrase).BindSubject(i.PreviousPhrase as NounPhrase);
                        (i.PreviousPhrase as NounPhrase).wasBound = true;
                    }
                    if ((i.PreviousPhrase.PreviousPhrase is NounPhrase) && 
                        (i.PreviousPhrase.PreviousPhrase.ParentSentence == i.ParentSentence) && 
                        !(i.PreviousPhrase.PreviousPhrase as NounPhrase).wasBound)
                    {
                        (i as VerbPhrase).BindSubject(i.PreviousPhrase.PreviousPhrase as NounPhrase);
                        (i.PreviousPhrase.PreviousPhrase as NounPhrase).wasBound = true;
                    }


                    //if the last verb, you can't find any more subjects
                    if (s.GetPhrasesAfter(i).GetVerbPhrases().Count() == 0)
                        break;
                }

                //handle case of inverted sentence (http://en.wikipedia.org/wiki/Inverted_sentence)
                if ((i is AdverbPhrase) && (i.NextPhrase is VerbPhrase) && (i.NextPhrase.NextPhrase is NounPhrase)
                    && (i.ParentSentence == i.NextPhrase.NextPhrase.ParentSentence)
                    && !(i.NextPhrase.NextPhrase as NounPhrase).wasBound)
                {
                    (i.NextPhrase as VerbPhrase).BindSubject(i.NextPhrase.NextPhrase as NounPhrase);
                    s.isStandard = false;
                    (i.NextPhrase.NextPhrase as NounPhrase).wasBound = true;
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
        }


        public void display() {

            for (int i = 0; i < stateList.Count; i++) {

                Console.Write(stateList[i].StatePhrase);
                Console.Write("\n");
            }

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

