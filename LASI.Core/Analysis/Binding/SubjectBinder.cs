using LASI.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LASI.Core.Analysis.Binding
{
    /// <summary>
    /// Establishes bindings between verbals and their subjects at the Phrase level.
    /// </summary>
    public class SubjectBinder : IIntraSentenceBinder
    {
        List<State> stateList = new List<State>();
        /// <summary>
        /// This is the Bind function for the SubjectBinder Class 
        /// </summary>
        /// <param name="sentence">The sentence to bind within.</param>
        public void Bind(Sentence sentence)
        {
            //Handle case of verbless sentence. Needs to be included for the sake of security of the code. 
            if (!sentence.Phrases.OfVerbPhrase().Any())
            {
                throw new VerblessPhrasalSequenceException(sentence.Phrases);
            }

            foreach (var i in sentence.Phrases)
            {
                if (i is AdjectivePhrase)
                {
                    var s2 = new State();
                    s2.StatePhrase = i;
                    stateList.Add(s2);
                }
                if (i is NounPhrase)
                {
                    var s3 = new State();
                    s3.StatePhrase = i;
                    stateList.Add(s3);
                }
                if (i is VerbPhrase && i.Words.Any(n => n is PresentParticiple))
                {
                    var s4 = new State();
                    s4.StatePhrase = i;
                    stateList.Add(s4);
                    break;
                }
                if (i is ConjunctionPhrase)
                {
                    var s5 = new State();
                    s5.StatePhrase = i;
                    stateList.Add(s5);
                }


                if (i is VerbPhrase && i.Words.Any(w => w is Verb && !(w is PresentParticiple)))
                {
                    var s6 = new State();
                    s6.StatePhrase = i;
                    s6.S = StateType.Final;
                    stateList.Add(s6);
                    //subject for normal sentence.
                    if ((i.Previous is NounPhrase np) && (np.Sentence == i.Sentence) && (i.Previous as NounPhrase).SubjectOf is null)
                    {
                        (i as VerbPhrase).BindSubject(i.Previous as NounPhrase); //(i.PreviousPhrase as NounPhrase).WasSubjectBound = true;
                    }
                    if ((i.Previous.HasSubjectPronoun() || (i.Previous.Previous.HasSubjectPronoun())) || ((i.Previous != null) && (i.Previous.Previous is NounPhrase) &&
                        (i.Previous.Previous.Sentence == i.Sentence) &&
                         (i.Previous.Previous as NounPhrase).SubjectOf == null))
                    {
                        (i as VerbPhrase).BindSubject(i.Previous.Previous as NounPhrase);//(i.PreviousPhrase.PreviousPhrase as NounPhrase).WasSubjectBound = true;
                    }
                    //if the last word, you can't find any more subjects
                    if (!sentence.GetPhrasesAfter(i).OfVerbPhrase().Any())
                        break;
                }

                //handle case of inverted sentence (http://en.wikipedia.org/wiki/Inverted_sentence)
                if ((i is AdverbPhrase) && (i.Next is VerbPhrase) && (i.Next.Next is NounPhrase)
                    && (i.Sentence == i.Next.Next.Sentence)
                    && (i.Next.Next as NounPhrase).SubjectOf == null)
                {
                    (i.Next as VerbPhrase).BindSubject(i.Next.Next as NounPhrase);
                    sentence.IsInverted = true;
                }

                if (i is AdverbPhrase)
                {
                    var s7 = new State();
                    s7.StatePhrase = i;
                    stateList.Add(s7);
                }
                if (i is PrepositionalPhrase)
                {
                    var s8 = new State();
                    s8.StatePhrase = i;
                    stateList.Add(s8);
                }
                if (i is ParticlePhrase)
                {
                    var s9 = new State();
                    stateList.Add(s9);
                    s9.StatePhrase = i;
                }
                if (i is InterjectionPhrase)
                {
                    var s10 = new State();
                    s10.StatePhrase = i;
                    stateList.Add(s10);
                }

            }
        }

        /// <summary>
        /// Display the current state of the SubjectBinder for debugging purposes.
        /// </summary>
        public void Display()
        {
            for (var i = 0;
            i < stateList.Count;
            i++)
            {
                Logger.Log(stateList[i].StatePhrase);
            }
        }

        internal class State
        {
            public State()
            {
                S = StateType.Default;
            }
            public StateType S
            {
                get;
                set;
            }
            public Phrase StatePhrase
            {
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
    /// <summary>
    /// Defines extension methods for Phrase elements to aid Subject -> Verbal binding scenarios.
    /// </summary>
    public static class SubjectBinderPhraseExtensions
    {
        /// <summary>
        /// Takes in a phrase and evaluates to see if anything in that phrase is a pronoun that could only be in the subject of a sentence. Will cut down on the number of compares for certain phrases. 
        /// </summary>
        /// <param name="p">Any phrase</param>
        /// <returns>Returns true of false if a phrase has a pronoun in it that can only be in the subject of a sentence</returns>
        public static bool HasSubjectPronoun(this Phrase p) => p.Words
                .OfPronoun()
                .Any(w => subjectPronounStrings.Contains(w.Text));
        private static readonly HashSet<string> subjectPronounStrings = new HashSet<string>(StringComparer.OrdinalIgnoreCase) { "he", "she", "it", "they" };
    }
}


