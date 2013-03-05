using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LASI.Algorithm;
using LASI.FileSystem;

namespace Dustin_Experimentation
{
    class SubjectBinder
    {
        List<State> stateList = new List<State>();
        Document doc = TaggerUtil.UntaggedToDoc("Running quickly through the field, Dustin and Aluan were coding.");
        public void bind()
        {
            State s1 = new State();
            State s2 = new State();
            State s3 = new State();
            State s4 = new State();
            State s5 = new State();
            State s6 = new State();
            

            s1.S = StateType.Initial;
            s6.S = StateType.Final;
            
            stateList.Add(s1);
            foreach (var i in doc.Phrases)
            {
                if (i is AdjectivePhrase)
                {
                    stateList.Add(s2);
                    s2.StatePhrase = i;
                }
                if (i is NounPhrase)
                {
                    stateList.Add(s3);
                    s3.StatePhrase = i;
                }
                if (i is VerbPhrase && i.Words.Count(n => n is PresentParticipleGerund) > 0)
                {
                    stateList.Add(s4);
                    s4.StatePhrase = i;
                }
                if (i is ConjunctionPhrase)
                {
                    stateList.Add(s5);
                    s5.StatePhrase = i;
                }
                if (i is VerbPhrase && i.Words.Count(n => n is PresentParticipleGerund) == 0)
                {
                    stateList.Add(s6);
                    s6.StatePhrase = i;
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

