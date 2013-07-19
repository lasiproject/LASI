using LASI.Algorithm.DocumentConstructs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LASI.Algorithm.Binding
{
    /// <summary>
    /// Establishes bindings between verbals and their subjects at the Phrase level.
    /// </summary>
    public class SubjectBinder
    {
        List<State> stateList = new List<State>();
        /// <summary>
        /// This is the Bind function for the SubjectBinder Class 
        /// </summary>
        /// <param name="s">The sentence to bind within.</param>
        public void Bind(Sentence s) {

            //This variable was not being used.
            //List<VerbPhrase> v1 = new List<VerbPhrase>(s.Phrases.GetVerbPhrases());

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
                if (i is VerbPhrase && i.Words.Any(n => n is PresentParticipleGerund)) {
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

                //Aluan says just an experiment
                //if (i is VerbPhrase && i.Words.Count(n => n is PresentParticipleGerund) == 0) {
                if (i is VerbPhrase && i.Words.Any(w => w is Verb && !(w is PresentParticipleGerund))) {
                    State s6 = new State();
                    s6.StatePhrase = i;
                    s6.S = StateType.Final;
                    stateList.Add(s6);
                    //subject for normal sentence.
                    if ((i.PreviousPhrase is NounPhrase) &&
                        (i.PreviousPhrase.Sentence == i.Sentence) &&
                         (i.PreviousPhrase as NounPhrase).SubjectOf == null) {

                        (i as VerbPhrase).BindSubject(i.PreviousPhrase as NounPhrase); //(i.PreviousPhrase as NounPhrase).WasSubjectBound = true;

                    }
                    if (i.PreviousPhrase != null && ((i.PreviousPhrase.PreviousPhrase is NounPhrase) &&
                        (i.PreviousPhrase.PreviousPhrase.Sentence == i.Sentence) &&
                         (i.PreviousPhrase.PreviousPhrase as NounPhrase).SubjectOf == null)) {

                        (i as VerbPhrase).BindSubject(i.PreviousPhrase.PreviousPhrase as NounPhrase);//(i.PreviousPhrase.PreviousPhrase as NounPhrase).WasSubjectBound = true;

                    }


                    //if the last word, you can't find any more subjects
                    if (s.GetPhrasesAfter(i).GetVerbPhrases().Count() == 0)
                        break;
                }

                //handle case of inverted sentence (http://en.wikipedia.org/wiki/Inverted_sentence)
                if ((i is AdverbPhrase) && (i.NextPhrase is VerbPhrase) && (i.NextPhrase.NextPhrase is NounPhrase)
                    && (i.Sentence == i.NextPhrase.NextPhrase.Sentence)
                    //Aluan says
                    //I don't think the WasBound property is needed because we can check if the SubjectOf property of the NounPhrase is null. 
                    //If it is, that indicates that the NounPhrase was bound as the subject of an IVerbal, 
                    //which is what I think you are checking with this line: && !(i.NextPhrase.NextPhrase as NounPhrase).WasBound)
                    && (i.NextPhrase.NextPhrase as NounPhrase).SubjectOf == null) {
                    (i.NextPhrase as VerbPhrase).BindSubject(i.NextPhrase.NextPhrase as NounPhrase);
                    //Aluan says
                    //I changed the name of this property from IsStandard to IsInverted because it is a rarer case.
                    //Hence, the next line sets the property to true instead of fasle.
                    s.IsInverted = true;
                    //Aluan says
                    //I don't think this line is needed because of the change in the if condition mentioned above.
                    //(i.NextPhrase.NextPhrase as NounPhrase).WasSubjectBound = true;

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

        /// <summary>
        /// Display the current state of the SubjectBinder for debugging purposes.
        /// </summary>
        public void display() {

            for (int i = 0; i < stateList.Count; i++) {

                Console.Write(stateList[i].StatePhrase);
                Console.Write("\n");
            }

        }
        internal class State
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
        internal enum StateType
        {
            Initial,
            Default,
            Fail,
            Final
        }
    }
}

