using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LASI.Algorithm;
using LASI.Utilities;
using LASI.Utilities.TypedSwitch;
using LASI.Algorithm.DocumentConstructs;

namespace Aluan_Experimentation
{
    class Class1
    {
        public void WeightWordsInSentence(Sentence sentence) {
            foreach (var word in sentence.Words) {
                new Switch(word)
                .Case<Verb>(v => {
                    if (v.Subjects.Count() > 2)
                        v.Weight++;
                })
                .Case<Noun>(n => {
                    n.Weight = 15;
                })
                .Default<Word>(w => {
                });
            }
        }
    }
}
