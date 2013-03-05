using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LASI.Algorithm;
using LASI.FileSystem;
using LASI.Utilities;

namespace Dustin_Experimentation
{
    class SubjectBinder
    {
        List<State> stateList = new List<State>();
        
        public void bind()
        {
            string str = TaggerUtil.TagString("Running quickly through the field, Dustin and Aluan were coding for CS 411.");
            Console.WriteLine(str);
            Document doc = TaggerUtil.TaggedToDoc(str);
            State s1 = new State();

            s1.S = StateType.Initial;
            
            stateList.Add(s1);
            foreach (var i in doc.Phrases)
            {
                if (i is AdjectivePhrase)
                {
                    State s2 = new State();
                    s2.StatePhrase = i;
                    stateList.Add(s2);
                    Console.WriteLine(i);
                }
                if (i is NounPhrase)
                {
                    State s3 = new State();
                    s3.StatePhrase = i;
                    stateList.Add(s3);
                    Console.WriteLine(i);
                }
                if (i is VerbPhrase && i.Words.Count(n => n is PresentParticipleGerund) > 0)
                {
                    State s4 = new State();
                    s4.StatePhrase = i;
                    stateList.Add(s4);
                    Console.WriteLine(i);
                }
                if (i is ConjunctionPhrase)
                {
                    State s5 = new State();
                    s5.StatePhrase = i;
                    stateList.Add(s5);
                    Console.WriteLine(i);
                }
                if (i is VerbPhrase && i.Words.Count(n => n is PresentParticipleGerund) == 0)
                {
                    State s6 = new State();
                    s6.StatePhrase = i;
                    stateList.Add(s6);
                    Console.WriteLine(i);
                }
                if (i is AdverbPhrase)
                {
                    State s7 = new State();
                    s7.StatePhrase = i;
                    stateList.Add(s7);
                    Console.WriteLine(i);
                }
                if (i is PrepositionalPhrase)
                {
                    State s8 = new State();
                    s8.StatePhrase = i;
                    stateList.Add(s8);
                    Console.WriteLine(i);
                }
                if (i is ParticlePhrase)
                {
                    State s9 = new State();
                    stateList.Add(s9);
                    s9.StatePhrase = i;
                    Console.WriteLine(i);
                }
                if (i is InterjectionPhrase)
                {
                    State s10 = new State();
                    s10.StatePhrase = i;
                    stateList.Add(s10);
                    Console.WriteLine(i);
                }
                
            }
        }
        public void display()
        {

            for (int i = 0; i < stateList.Count; i++)
            {
                
                Console.Write(stateList[i].StatePhrase);
                Console.Write("\n");
            }
        }
        class State
        {
            public State()
            {
                S = StateType.Default;
            }
            public StateType S{get; set;}
            public int count{get; private set;}
            public Phrase StatePhrase
            {
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

