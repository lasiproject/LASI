﻿using LASI.Algorithm.DocumentStructures;
using LASI.Utilities;
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

            //Handle case of verbless sentence. Needs to be included for the sake of security of the code. 
            if (s.Phrases.OfVerbPhrase().None())
            {
                throw new VerblessPhrasalSequenceException();
            }

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
                    if ((hasSubjectPronoun(i.PreviousPhrase) || (hasSubjectPronoun(i.PreviousPhrase.PreviousPhrase))) || ((i.PreviousPhrase != null) && (i.PreviousPhrase.PreviousPhrase is NounPhrase) &&
                        (i.PreviousPhrase.PreviousPhrase.Sentence == i.Sentence) &&
                         (i.PreviousPhrase.PreviousPhrase as NounPhrase).SubjectOf == null)) {

                        (i as VerbPhrase).BindSubject(i.PreviousPhrase.PreviousPhrase as NounPhrase);//(i.PreviousPhrase.PreviousPhrase as NounPhrase).WasSubjectBound = true;

                    }


                    //if the last word, you can't find any more subjects
                    if (s.GetPhrasesAfter(i).OfVerbPhrase().None())
                        break;
                }

                //handle case of inverted sentence (http://en.wikipedia.org/wiki/Inverted_sentence)
                if ((i is AdverbPhrase) && (i.NextPhrase is VerbPhrase) && (i.NextPhrase.NextPhrase is NounPhrase)
                    && (i.Sentence == i.NextPhrase.NextPhrase.Sentence)
                    && (i.NextPhrase.NextPhrase as NounPhrase).SubjectOf == null) {
                    (i.NextPhrase as VerbPhrase).BindSubject(i.NextPhrase.NextPhrase as NounPhrase);
                    s.IsInverted = true;

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

                Output.Write(stateList[i].StatePhrase);
                Output.WriteLine();
            }

        }

        /// <summary>
        /// Takes in a phrase and evaluates to see if anything in that phrase is a pronoun that could only be in the subject of a sentence. Will cut down on the number of compares for certian phrases. 
        /// </summary>
        /// <param name="p">Any phrase</param>
        /// <returns>Returns true of false if a phrase has a pronoune in it that can only be in the subject of a sentence</returns>
        private bool hasSubjectPronoun(Phrase p)
        {
            bool val = false;
            foreach (var w in p.Words)
            {
                if ((w is Pronoun) && (w.Text == "he") || (w.Text == "they") || (w.Text == "she"))
                {
                    val = true;
                }
            }
            return val;
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

