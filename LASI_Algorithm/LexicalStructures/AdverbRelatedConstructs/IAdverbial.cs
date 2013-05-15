﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LASI.Algorithm.FundamentalSyntacticInterfaces
{
    /// <summary>
    /// Defines the role reqirements for Adverbial elements, generally Adverbs or or AdvebPhrases, which may modify the meaning of IAdverbialModifiable elememts such as Verbs, VerbPhrases, Adjectives, and AdjectivePhrases.
    /// Along with the rhs interfaces in the Syntactic Interfaces Library, the IAdverbial interface provides for generalization and abstraction over Word and Phrase types.
    /// </summary>
    public interface IAdverbial : ILexical
    {
        IVerbal Modified {
            get;
            set;
        }
    }
}
