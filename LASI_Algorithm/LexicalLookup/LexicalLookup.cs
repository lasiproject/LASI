using LASI.Algorithm.LexicalLookup.Lookups;
using LASI.Utilities;
using System;
using System.Text;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.IO;

namespace LASI.Algorithm.LexicalLookup
{
    #region Type Aliases
    // Although the Interface name IDescriprtor was chosen because of its deseriable generality, e.g. it is implemented by SubordinateClause,
    // In the context of the LexicalLookup class it refers solely to Adjectival constructs such as Adjectives or Adjective Phrases.
    using IAdjectival = LASI.Algorithm.IDescriptor;
    // Although the Interface name IEntity was chosen because of its deseriable generality, 
    // e.g. it is implemented by AggregateEntities such as IEntityGroup, and weak entities like RelativePronouns
    // In the context of the LexicalLookup class it refers solely to INounal (read noun like )constructs such as Nouns Noun Phrases.
    using INounal = LASI.Algorithm.IEntity;
    // Because of this, an alias is defined. You can make use of this to aid readability in many situations. 
    // Works analogously to typedef in C++ in that it lets you define a type alias to provide contextual semantics without the overhead of new types.
    #endregion

    /// <summary>
    /// Provides Comprehensive static facilities for Synoynm Identification, Word and Phrase Comparison, Gender Stratification, and Named Entity Recognition.
    /// </summary>
    public static class LexicalLookup
    {
        #region Public Methods

        #region Synonym Lookup Methods

        /// <summary>
        /// Returns the synonyms for the provided Noun.
        /// </summary>
        /// <param name="noun">The Noun to lookup.</param>
        /// <returns>The synonyms for the provided Noun.</returns>
        public static IEnumerable<string> Lookup(Noun noun) {
            return InternalLookup(noun);
        }
        /// <summary>
        /// Returns the synonyms for the provided Verb.
        /// </summary>
        /// <param name="verb">The Verb to lookup.</param>
        /// <returns>The synonyms for the provided Verb.</returns>
        public static IEnumerable<string> Lookup(Verb verb) {
            return InternalLookup(verb);
        }
        /// <summary>
        /// Returns the synonyms for the provided Adjective.
        /// </summary>
        /// <param name="adjective">The Adjective to lookup.</param>
        /// <returns>The synonyms for the provided Adjective.</returns>
        public static IEnumerable<string> Lookup(Adjective adjective) {
            return InternalLookup(adjective);
        }
        /// <summary>
        /// Returns the synonyms for the provided Adverb.
        /// </summary>
        /// <param name="adverb">The Adverb to lookup.</param>
        /// <returns>The synonyms for the provided Adverb.</returns>
        public static IEnumerable<string> Lookup(Adverb adverb) {
            return InternalLookup(adverb);
        }
        /// <summary>
        /// Returns the synonyms for the provided noun text.
        /// </summary>
        /// <param name="nounText">The text corresponding to a noun.</param>
        /// <returns>The synonyms for the provided noun text.</returns>
        public static IEnumerable<string> LookupNoun(string nounText) {
            switch (nounLoadingState) {
                case LoadingState.Finished:
                    return cachedNounData.GetOrAdd(nounText, key => nounLookup[key]);
                case LoadingState.NotStarted:
                    NounThesaurusLoadTask.Wait();
                    return cachedNounData.GetOrAdd(nounText, key => nounLookup[key]);
                case LoadingState.InProgress:
                    throw new NounDataNotLoadedException();
                default:
                    return Enumerable.Empty<string>();
            }
        }
        /// <summary>
        /// Returns the synonyms for the provided verb text.
        /// </summary>
        /// <param name="verbText">The text corresponding to a noun.</param>
        /// <returns>The synonyms for provided the verb text.</returns>
        public static IEnumerable<string> LookupVerb(string verbText) {
            switch (verbLoadingState) {
                case LoadingState.Finished:
                    return cachedVerbData.GetOrAdd(verbText, key => verbLookup[key]);
                case LoadingState.NotStarted:
                    VerbThesaurusLoadTask.Wait();
                    return cachedVerbData.GetOrAdd(verbText, key => verbLookup[key]);
                case LoadingState.InProgress:
                    throw new VerbDataNotLoadedException();
                default:
                    return Enumerable.Empty<string>();
            }
        }
        /// <summary>
        /// Returns the synonyms for the provided adjective text.
        /// </summary>
        /// <param name="adjectiveText">The text corresponding to an adjective.</param>
        /// <returns>The synonyms for provided the adjective text.</returns>
        public static IEnumerable<string> LookupAdjective(string adjectiveText) {
            switch (adjectiveLoadingState) {
                case LoadingState.Finished:
                    return cachedAdjectiveData.GetOrAdd(adjectiveText, key => adjectiveLookup[key]);
                case LoadingState.NotStarted:
                    AdjectiveThesaurusLoadTask.Wait();
                    return cachedAdjectiveData.GetOrAdd(adjectiveText, key => adjectiveLookup[key]);
                case LoadingState.InProgress:
                    throw new AdjectiveDataNotLoadedException();
                default:
                    return Enumerable.Empty<string>();
            }
        }
        /// <summary>
        /// Returns the synonyms for the provided adverb text.
        /// </summary>
        /// <param name="adverbText">The text corresponding to an adverb.</param>
        /// <returns>The synonyms for provided the adverb text.</returns>
        public static IEnumerable<string> LookupAdverb(string adverbText) {
            switch (adverbLoadingState) {
                case LoadingState.Finished:
                    return cachedAdverbData.GetOrAdd(adverbText, key => adverbLookup[key]);
                case LoadingState.NotStarted:
                    AdverbThesaurusLoadTask.Wait();
                    return cachedAdverbData.GetOrAdd(adverbText, key => adverbLookup[key]);
                case LoadingState.InProgress:
                    throw new AdverbDataNotLoadedException();
                default:
                    return Enumerable.Empty<string>();
            }
        }
        /// <summary>
        /// Determines if two Noun instances are synonymous.
        /// </summary>
        /// <param name="first">The first Noun.</param>
        /// <param name="second">The second Noun</param>
        /// <returns>True if the Noun instances are synonymous, false otherwise.</returns>
        /// <remarks>There are two calling conventions for this method. See the following examples:
        /// <code>if ( LexicalLookup.IsSimilarTo(vp1, vp2) ) { ... }</code>
        /// <code>if ( vp1.IsSimilarTo(vp2) ) { ... }</code>
        /// Please prefer the second convention.
        /// </remarks>
        public static bool IsSynonymFor(this Noun first, Noun second) {
            return InternalLookup(first).Contains(second.Text);
        }
        /// <summary>
        /// Determines if two Verb instances are synonymous.
        /// </summary>
        /// <param name="first">The first Verb.</param>
        /// <param name="second">The second Verb</param>
        /// <returns>True if the Verb instances are synonymous, false otherwise.</returns>
        /// <remarks>There are two calling conventions for this method. See the following examples:
        /// <code>if ( LexicalLookup.IsSimilarTo(vp1, vp2) ) { ... }</code>
        /// <code>if ( vp1.IsSimilarTo(vp2) ) { ... }</code>
        /// Please prefer the second convention.
        /// </remarks>
        public static bool IsSynonymFor(this Verb first, Verb second) {
            return InternalLookup(first).Contains(second.Text);
        }
        /// <summary>
        /// Determines if two Adjective instances are synonymous.
        /// </summary>
        /// <param name="word">The first Adjective.</param>
        /// <param name="other">The second Adjective</param>
        /// <returns>True if the Adjective instances are synonymous, false otherwise.</returns>
        /// <remarks>There are two calling conventions for this method. See the following examples:
        /// <code>if ( LexicalLookup.IsSimilarTo(vp1, vp2) ) { ... }</code>
        /// <code>if ( vp1.IsSimilarTo(vp2) ) { ... }</code>
        /// Please prefer the second convention.
        /// </remarks>
        public static bool IsSynonymFor(this Adjective word, Adjective other) {
            return InternalLookup(word).Contains(other.Text);
        }
        /// <summary>
        /// Determines if two Adverb instances are synonymous.
        /// </summary>
        /// <param name="word">The first Adverb.</param>
        /// <param name="other">The second Adverb</param>
        /// <returns>True if the Adverb instances are synonymous, false otherwise.</returns>
        public static bool IsSynonymFor(this Adverb word, Adverb other) {
            return InternalLookup(word).Contains(other.Text);
        }

        #endregion

        #region Similarity Comparion Methods

        /// <summary>
        /// Determines if two IEntity instances are similar.
        /// </summary>
        /// <param name="first">The first IEntity</param>
        /// <param name="second">The second IEntity</param>
        /// <returns>True if the given IEntity instances are similar, false otherwise.</returns>
        /// <remarks>There are two calling conventions for this method. See the following examples
        /// <code>if ( LexicalLookup.IsSimilarTo(e1, e2) ) { ... }</code>
        /// <code>if ( e1.IsSimilarTo(e2) ) { ... }</code>
        /// Please prefer the second convention.
        /// </remarks>
        public static SimResult IsSimilarTo(this INounal first, INounal second) {
            //If both have the same text, ignoring case, we will assume similarity. 
            if (first.Text.ToUpper() == second.Text.ToUpper()) {
                return new SimResult(true);
            }

            var n1 = first as Noun;
            var n2 = second as Noun;
            //If both are Nouns...
            if (n1 != null && n2 != null) {
                return new SimResult(n1.IsSynonymFor(n2));
            }

            var np1 = first as NounPhrase;
            var np2 = second as NounPhrase;
            //If both are NounPhrases...
            if (np1 != null && np2 != null) {
                return np1.IsSimilarTo(np2);
            }

            var np = first as NounPhrase ?? second as NounPhrase;
            var n = first as Noun ?? second as Noun;
            //If one of them is a NounPhrase and the other is a Noun
            if (n != null && np != null) {
                return n.IsSimilarTo(np);
            }
            return new SimResult(false);
        }
        /// <summary>
        /// Determines if the provided Noun is similar to the provided NounPhrase.
        /// </summary>
        /// <param name="first">The Noun.</param>
        /// <param name="second">The NounPhrase.</param>
        /// <returns>True if the provided Noun is similar to the provided NounPhrase, false otherwise.</returns>
        /// <remarks>There are two calling conventions for this method. See the following examples:
        /// <code>if ( LexicalLookup.IsSimilarTo(n1, np2) ) { ... }</code>
        /// <code>if ( n1.IsSimilarTo(np2) ) { ... }</code>
        /// Please prefer the second convention.
        /// </remarks>
        public static SimResult IsSimilarTo(this Noun first, NounPhrase second) {
            var phraseNouns = second.Words.GetNouns();
            return new SimResult(phraseNouns.Count() == 1 && phraseNouns.First().IsSynonymFor(first));
        }
        /// <summary>
        /// Determines if the provided NounPhrase is similar to the provided Noun.
        /// </summary>
        /// <param name="first">The NounPhrase.</param>
        /// <param name="second">The Noun.</param>
        /// <returns>True if the provided NounPhrase is similar to the provided Noun, false otherwise.</returns>
        /// <remarks>There are two calling conventions for this method. See the following examples:
        /// <code>if ( LexicalLookup.IsSimilarTo(np1, n2) ) { ... }</code>
        /// <code>if ( np1.IsSimilarTo(n2) ) { ... }</code>
        /// Please prefer the second convention.
        /// </remarks>
        public static SimResult IsSimilarTo(this NounPhrase first, Noun second) {
            return new SimResult(second.IsSimilarTo(first));
        }
        /// <summary>
        /// Determines if the two provided Noun instances are similar.
        /// </summary>
        /// <param name="first">The first Noun.</param>
        /// <param name="second">The second Noun.</param>
        /// <returns>True if the first Noun is similar to the second, false otherwise.</returns>
        /// <remarks>There are two calling conventions for this method. See the following examples:
        /// <code>if ( LexicalLookup.IsSimilarTo(n1, n2) ) { ... }</code>
        /// <code>if ( n1.IsSimilarTo(n2) ) { ... }</code>
        /// Please prefer the second convention.
        /// </remarks>
        public static SimResult IsSimilarTo(this Noun first, Noun second) {
            return new SimResult(first.IsSynonymFor(second));
        }
        /// <summary>
        /// Determines if the two provided Verb instances are similar.
        /// </summary>
        /// <param name="first">The first Verb.</param>
        /// <param name="second">The second Verb.</param>
        /// <returns>True if the first Verb is similar to the second, false otherwise.</returns>
        /// <remarks>There are two calling conventions for this method. See the following examples:
        /// <code>if ( LexicalLookup.IsSimilarTo(v1, v2) ) { ... }</code>
        /// <code>if ( v1.IsSimilarTo(v2) ) { ... }</code>
        /// Please prefer the second convention.
        /// </remarks>
        public static SimResult IsSimilarTo(this Verb first, Verb second) {
            return new SimResult(first.IsSynonymFor(second));
        }
        /// <summary>
        /// Determines if the provided VerbPhrase is similar to the provided Verb.
        /// </summary>
        /// <param name="first">The VerbPhrase.</param>
        /// <param name="second">The Verb.</param>
        /// <returns>True if the provided VerbPhrase is similar to the provided Verb, false otherwise.</returns>
        /// <remarks>There are two calling conventions for this method. See the following examples:
        /// <code>if ( LexicalLookup.IsSimilarTo(vp1, v2) ) { ... }</code>
        /// <code>if ( vp1.IsSimilarTo(v2) ) { ... }</code>
        /// Please prefer the second convention.
        /// </remarks>
        public static SimResult IsSimilarTo(this VerbPhrase first, Verb second) {
            return new SimResult(second.IsSimilarTo(first));
        }
        /// <summary>
        /// Determines if the provided Verb is similar to the provided VerbPhrase.
        /// </summary>
        /// <param name="first">The Verb.</param>
        /// <param name="second">The VerbPhrase.</param>
        /// <returns>True if the provided Verb is similar to the provided VerbPhrase, false otherwise.</returns>
        /// <remarks>There are two calling conventions for this method. See the following examples:
        /// <code>if ( LexicalLookup.IsSimilarTo(v1, vp2) ) { ... }</code>
        /// <code>if ( v1.IsSimilarTo(vp2) ) { ... }</code>
        /// Please prefer the second convention.
        /// </remarks>
        public static SimResult IsSimilarTo(this Verb first, VerbPhrase second) {
            return new SimResult(second.Words.TakeWhile(w => !(w is ToLinker)).GetVerbs().Any(v => v.IsSynonymFor(first)));//This is kind of rough.
        }
        /// <summary>
        /// Determines if two IVerbal instances are similar.
        /// </summary>
        /// <param name="first">The first IVerbal</param>
        /// <param name="second">The second IVerbal</param>
        /// <returns>True if the given IVerbal instances are similar, false otherwise.</returns>
        /// <remarks>There are two calling conventions for this method. See the following examples
        /// <code>if ( LexicalLookup.IsSimilarTo(v1, v2) ) { ... }</code>
        /// <code>if ( v1.IsSimilarTo(v2) ) { ... }</code> 
        /// Please prefer the second convention.
        /// </remarks>
        public static SimResult IsSimilarTo(this IVerbal first, IVerbal second) {

            //Compare literal text.
            if (first.Text.ToUpper() == second.Text.ToUpper()) {
                return new SimResult(true);
            }

            //If both are of type Verb check if syonymous
            var v1 = first as Verb;
            var v2 = second as Verb;
            if (v1 != null && v2 != null) {
                return new SimResult(v1.IsSynonymFor(v2));
            }

            //If both are of type VerbPhrase check for similarity
            var vp1 = first as VerbPhrase;
            var vp2 = second as VerbPhrase;
            if (vp1 != null && vp2 != null) {
                return vp1.IsSimilarTo(vp2);
            }

            //If one is of type Verb and the other is of Type VerbPhrase, test for similarirty.
            var vp = first as VerbPhrase ?? second as VerbPhrase;
            var v = first as Verb ?? second as Verb;
            if (v != null && vp != null) {
                return v.IsSimilarTo(vp);
            }

            return new SimResult(false);

        }
        /// <summary>
        /// Determines if two NounPhrases are similar.
        /// </summary>
        /// <param name="first">The first NounPhrase</param>
        /// <param name="second">The second NounPhrase</param>
        /// <returns>True if the given NounPhrases are similar, false otherwise.</returns>
        /// <remarks>There are two calling conventions for this method. See the following examples:
        /// <code>if ( LexicalLookup.IsSimilarTo(np1, np2) ) { ... }</code>
        /// <code>if ( np1.IsSimilarTo(np2) ) { ... }</code>
        /// Please prefer the second convention.
        /// </remarks>
        public static SimResult IsSimilarTo(this NounPhrase first, NounPhrase second) {
            var ratio = GetSimilarityRatio(first, second);
            return new SimResult(ratio > SIMILARITY_THRESHOLD, ratio);
        }
        /// <summary>
        /// Determines if two VerbPhrases are similar.
        /// </summary>
        /// <param name="first">The first VerbPhrase</param>
        /// <param name="second">The second VerbPhrase</param>
        /// <returns>True if the given VerbPhrases are similar, false otherwise.</returns>
        /// <remarks>There are two calling conventions for this method. See the following examples:
        /// <code>if ( LexicalLookup.IsSimilarTo(vp1, vp2) ) { ... }</code>
        /// <code>if ( vp1.IsSimilarTo(vp2) ) { ... }</code>
        /// Please prefer the second convention.
        /// </remarks>
        public static SimResult IsSimilarTo(this VerbPhrase first, VerbPhrase second) {

            //Look into refining this
            List<Verb> leftHandVerbs = first.Words.GetVerbs().ToList();
            List<Verb> rightHandVerbs = second.Words.GetVerbs().ToList();

            bool result = leftHandVerbs.Count == rightHandVerbs.Count;

            if (result) {
                try {
                    for (var i = 0; i < leftHandVerbs.Count; ++i) {
                        result &= leftHandVerbs[i].IsSynonymFor(rightHandVerbs[i]);
                    }
                } catch (NullReferenceException) {
                    return new SimResult(false);
                }
            }

            return new SimResult(result);
        }
        /// <summary>
        /// Determines if the two provided Adjective instances are similar.
        /// </summary>
        /// <param name="first">The first Adjective.</param>
        /// <param name="second">The second Adjective.</param>
        /// <returns>True if the first Adjective is similar to the second, false otherwise.</returns>
        /// <remarks>There are two calling conventions for this method. See the following examples:
        /// <code>if ( LexicalLookup.IsSimilarTo(a1, a2) ) { ... }</code>
        /// <code>if ( a1.IsSimilarTo(a2) ) { ... }</code>
        /// Please prefer the second convention.
        /// </remarks>
        public static SimResult IsSimilarTo(this Adjective first, Adjective second) {
            return new SimResult(first.IsSynonymFor(second));
        }
        /// <summary>
        /// Determines if the provided AdjectivePhrase is similar to the provided Adjective.
        /// </summary>
        /// <param name="first">The AdjectivePhrase.</param>
        /// <param name="second">The Adjective.</param>
        /// <returns>True if the provided AdjectivePhrase is similar to the provided Adjective, false otherwise.</returns>
        /// <remarks>There are two calling conventions for this method. See the following examples:
        /// <code>if ( LexicalLookup.IsSimilarTo(ap1, a2) ) { ... }</code>
        /// <code>if ( ap1.IsSimilarTo(a2) ) { ... }</code>
        /// Please prefer the second convention.
        /// </remarks>
        public static SimResult IsSimilarTo(this AdjectivePhrase first, Adjective second) {
            return new SimResult(second.IsSimilarTo(first));
        }
        /// <summary>
        /// Determines if the provided Adjective is similar to the provided AdjectivePhrase.
        /// </summary>
        /// <param name="first">The Adjective.</param>
        /// <param name="second">The AdjectivePhrase.</param>
        /// <returns>True if the provided Adjective is similar to the provided AdjectivePhrase, false otherwise.</returns>
        /// <remarks>There are two calling conventions for this method. See the following examples:
        /// <code>if ( LexicalLookup.IsSimilarTo(a1, ap2) ) { ... }</code>
        /// <code>if ( a1.IsSimilarTo(ap2) ) { ... }</code>
        /// Please prefer the second convention.
        /// </remarks>
        public static SimResult IsSimilarTo(this Adjective first, AdjectivePhrase second) {
            return new SimResult(second.Words.GetAdjectives().Any(adj => adj.IsSynonymFor(first)));
        }
        /// <summary>
        /// Determines if two IDescriptor instances are similar.
        /// </summary>
        /// <param name="first">The first IDescriptor</param>
        /// <param name="second">The second IDescriptor</param>
        /// <returns>True if the given IDescriptor instances are similar, false otherwise.</returns>
        /// <remarks>There are two calling conventions for this method. See the following examples
        /// <code>if ( LexicalLookup.IsSimilarTo(d1, d2) ) { ... }</code>
        /// <code>if ( d1.IsSimilarTo(d2) ) { ... }</code> 
        /// Please prefer the second convention.
        /// </remarks>
        public static SimResult IsSimilarTo(this IAdjectival first, IAdjectival second) {

            //Compare literal text.
            if (first.Text.ToUpper() == second.Text.ToUpper()) {
                return new SimResult(true);
            }

            //If both are of type Adjective check if syonymous
            var a1 = first as Adjective;
            var a2 = second as Adjective;
            if (a1 != null && a2 != null) {
                return new SimResult(a1.IsSynonymFor(a2));
            }

            //If both are of type AdjectivePhrase check for similarity
            var ap1 = first as AdjectivePhrase;
            var ap2 = second as AdjectivePhrase;
            if (ap1 != null && ap2 != null) {
                return ap1.IsSimilarTo(ap2);
            }

            //If one is of type Verb and the other is of Type VerbPhrase, test for similarirty.
            var ap = first as AdjectivePhrase ?? second as AdjectivePhrase;
            var a = first as Adjective ?? second as Adjective;
            if (a != null && ap != null) {  // operator ?? means that if either first OR second is a Phrase
                return a.IsSimilarTo(ap);   // var "ap" will hold a reference to it, the same applies to Word and "var a"
            }                               // the to blocks of type checks above ensure that they are not both Words or Phrases

            return new SimResult(false);

        }
        /// <summary>
        /// Determines if two AdjectivePhrase are similar.
        /// </summary>
        /// <param name="first">The first AdjectivePhrase</param>
        /// <param name="second">The second AdjectivePhrase</param>
        /// <returns>True if the given AdjectivePhrase are similar, false otherwise.</returns>
        /// <remarks>There are two calling conventions for this method. See the following examples:
        /// <code>if ( LexicalLookup.IsSimilarTo(ap1, ap2) ) { ... }</code>
        /// <code>if ( ap1.IsSimilarTo(ap2) ) { ... }</code>
        /// Please prefer the second convention.
        /// </remarks>
        public static SimResult IsSimilarTo(this AdjectivePhrase first, AdjectivePhrase second) {

            //Look into refining this
            List<Adjective> leftHandAdjectives = first.Words.GetAdjectives().ToList();
            List<Adjective> rightHandAdjectives = second.Words.GetAdjectives().ToList();

            bool result = leftHandAdjectives.Count == rightHandAdjectives.Count;

            if (result) {
                try {
                    for (var i = 0; i < leftHandAdjectives.Count; ++i) {
                        result &= leftHandAdjectives[i].IsSynonymFor(rightHandAdjectives[i]);
                    }
                } catch (NullReferenceException) {
                    return new SimResult(false);
                }
            }
            return new SimResult(result);
        }
        /// <summary>
        /// Determines if the two provided Adverb instances are similar.
        /// </summary>
        /// <param name="first">The first Adverb.</param>
        /// <param name="second">The second Adverb.</param>
        /// <returns>True if the first Adverb is similar to the second, false otherwise.</returns>
        /// <remarks>There are two calling conventions for this method. See the following examples:
        /// <code>if ( LexicalLookup.IsSimilarTo(a1, a2) ) { ... }</code>
        /// <code>if ( a1.IsSimilarTo(a2) ) { ... }</code>
        /// Please prefer the second convention.
        /// </remarks>
        public static SimResult IsSimilarTo(this Adverb first, Adverb second) {
            return new SimResult(first.IsSynonymFor(second));
        }
        /// <summary>
        /// Determines if the provided AdverbPhrase is similar to the provided Adverb.
        /// </summary>
        /// <param name="first">The AdverbPhrase.</param>
        /// <param name="second">The Adjective.</param>
        /// <returns>True if the provided AdverbPhrase is similar to the provided Adverb, false otherwise.</returns>
        /// <remarks>There are two calling conventions for this method. See the following examples:
        /// <code>if ( LexicalLookup.IsSimilarTo(ap1, a2) ) { ... }</code>
        /// <code>if ( ap1.IsSimilarTo(a2) ) { ... }</code>
        /// Please prefer the second convention.
        /// </remarks>
        public static SimResult IsSimilarTo(this AdverbPhrase first, Adverb second) {
            return new SimResult(second.IsSimilarTo(first));
        }
        /// <summary>
        /// Determines if the provided Adverb is similar to the provided AdverbPhrase.
        /// </summary>
        /// <param name="first">The Adverb.</param>
        /// <param name="second">The AdverbPhrase.</param>
        /// <returns>True if the provided Adverb is similar to the provided AdverbPhrase, false otherwise.</returns>
        /// <remarks>There are two calling conventions for this method. See the following examples:
        /// <code>if ( LexicalLookup.IsSimilarTo(a1, ap2) ) { ... }</code>
        /// <code>if ( a1.IsSimilarTo(ap2) ) { ... }</code>
        /// Please prefer the second convention.
        /// </remarks>
        public static SimResult IsSimilarTo(this Adverb first, AdverbPhrase second) {
            return new SimResult(second.Words.GetAdverbs().Any(adj => adj.IsSynonymFor(first)));
            // Must refine this to check for negators and modals which will potentially invert the meaning.
        }
        /// <summary>
        /// Determines if two IAdverbial instances are similar.
        /// </summary>
        /// <param name="first">The first IAdverbial</param>
        /// <param name="second">The second IAdverbial</param>
        /// <returns>True if the given IAdverbial instances are similar, false otherwise.</returns>
        /// <remarks>There are two calling conventions for this method. See the following examples
        /// <code>if ( LexicalLookup.IsSimilarTo(d1, d2) ) { ... }</code>
        /// <code>if ( a1.IsSimilarTo(a2) ) { ... }</code> 
        /// Please prefer the second convention.
        /// </remarks>
        public static SimResult IsSimilarTo(this IAdverbial first, IAdverbial second) {

            //Compare literal text.
            if (first.Text.ToUpper() == second.Text.ToUpper()) {
                return new SimResult(true);
            }

            //If both are of type Adverb check if syonymous
            var a1 = first as Adverb;
            var a2 = second as Adverb;
            if (a1 != null && a2 != null) {
                return new SimResult(a1.IsSynonymFor(a2));
            }

            //If both are of type AdverbPhrase check for similarity
            var ap1 = first as AdverbPhrase;
            var ap2 = second as AdverbPhrase;
            if (ap1 != null && ap2 != null) {
                return ap1.IsSimilarTo(ap2);
            }

            //If one is of type Adverb and the other is of Type AdverbPhrase, test for similarirty.
            var ap = first as AdverbPhrase ?? second as AdverbPhrase;
            var a = first as Adverb ?? second as Adverb;
            if (a != null && ap != null) {  // operator ?? means that if either first OR second is a Phrase
                return a.IsSimilarTo(ap);   // var "ap" will hold a reference to it, the same applies to Word and "var a"
            }                               // the to blocks of type checks above ensure that they are not both Words or Phrases

            return new SimResult(false);

        }
        /// <summary>
        /// Determines if two AdverbPhrases are similar.
        /// </summary>
        /// <param name="first">The first AdverbPhrase</param>
        /// <param name="second">The second AdverbPhrase</param>
        /// <returns>True if the given AdverbPhrases are similar, false otherwise.</returns>
        /// <remarks>There are two calling conventions for this method. See the following examples:
        /// <code>if ( LexicalLookup.IsSimilarTo(ap1, ap2) ) { ... }</code>
        /// <code>if ( ap1.IsSimilarTo(ap2) ) { ... }</code>
        /// Please prefer the second convention.
        /// </remarks>
        public static SimResult IsSimilarTo(this AdverbPhrase first, AdverbPhrase second) {

            //Look into refining this
            List<Adverb> leftHandAdjectives = first.Words.GetAdverbs().ToList();
            List<Adverb> rightHandAdjectives = second.Words.GetAdverbs().ToList();

            bool result = leftHandAdjectives.Count == rightHandAdjectives.Count;

            if (result) {
                try {
                    for (var i = 0; i < leftHandAdjectives.Count; ++i) {
                        result &= leftHandAdjectives[i].IsSynonymFor(rightHandAdjectives[i]);
                    }
                } catch (NullReferenceException) {
                    return new SimResult(false);
                }
            }
            return new SimResult(result);
        }
        /// <summary>
        /// Returns a double value indicating the degree of similarity between two NounPhrases.
        /// </summary>
        /// <param name="first">The first NounPhrase</param>
        /// <param name="second">The second NounPhrase</param>
        /// <returns>A double value indicating the degree of similarity between two NounPhrases.</returns>
        private static double GetSimilarityRatio(NounPhrase first, NounPhrase second) {
            NounPhrase outer = null;
            NounPhrase inner = null;
            double similarCount = 0.0d;

            if (first.Words.Count() >= second.Words.Count()) {
                outer = first;
                inner = second;
            } else {
                outer = second;
                inner = first;
            }

            if ((outer.Words.GetNouns().Count() != 0) && (inner.Words.GetNouns().Count() != 0)) {
                foreach (var o in outer.Words.GetNouns()) {
                    foreach (var i in inner.Words.GetNouns()) {
                        if (i.IsSimilarTo(o))
                            similarCount += 0.7;
                    }
                    var scaleFactor = inner.Words.GetNouns().Count() * outer.Words.GetNouns().Count();
                    return (similarCount / scaleFactor == 0 ? 1 : scaleFactor);
                }

            }
            return 0d;
        }

        #endregion

        #region Full Name Lookup Methods

        /// <summary>
        /// Returns a NameGender value indiciating the likely gender of the ProperNoun.
        /// </summary>
        /// <param name="name">The ProperNoun whose gender to lookup.</param>
        /// <returns>A NameGender value indiciating the likely gender of the ProperNoun.</returns>
        public static NameGender GetNameGender(this ProperNoun name) {
            return name.IsFemaleName() ? NameGender.Female : name.IsMaleName() ? NameGender.Male : name.IsLastName() ? NameGender.Unknown : NameGender.UNDEFINED;
        }
        /// <summary>
        /// Returns a NameGender value indiciating the likely prevailing gender within the NounPhrase.
        /// </summary>
        /// <param name="name">The NounPhrase whose prevailing gender to lookup.</param>
        /// <returns>A NameGender value indiciating the likely prevailing gender of the NounPhrase.</returns>
        public static NameGender GetNameGender(this NounPhrase name) {
            return name.IsFemaleFullName() ? NameGender.Female : name.IsMaleFullName() ? NameGender.Male : name.IsFullName() ? NameGender.Unknown : NameGender.UNDEFINED;
        }

        /// <summary>
        /// Determines if the provided NounPhrase is a known Full Name.
        /// </summary>
        /// <param name="name">The NounPhrase to check.</param>
        /// <returns>True if the provided NounPhrase is a known Full Name, false otherwise.</returns>
        public static bool IsFullName(this NounPhrase name) {
            return CheckFullNameGender(name, IsFirstName);
        }
        /// <summary>
        /// Determines if the provided NounPhrase is a known Full Female Name.
        /// </summary>
        /// <param name="name">The NounPhrase to check.</param>
        /// <returns>True if the provided NounPhrase is a known Full Female Name, false otherwise.</returns>
        public static bool IsFemaleFullName(this NounPhrase name) {
            return CheckFullNameGender(name, IsFemaleName);
        }
        /// <summary>
        /// Determines if the provided NounPhrase is a known Full Male Name.
        /// </summary>
        /// <param name="name">The NounPhrase to check.</param>
        /// <returns>True if the provided NounPhrase is a known Full Male Name, false otherwise.</returns>
        public static bool IsMaleFullName(this NounPhrase name) {
            return CheckFullNameGender(name, properNoun => properNoun.IsMaleName());
        }


        private static bool CheckFullNameGender(NounPhrase name, Func<ProperNoun, bool> firstNameCondition) {
            var pns = name.Words.GetProperNouns();
            var pcnt = pns.Count();
            return pcnt > 1 &&
            pns.FirstOrDefault(firstNameCondition) != null &&
            pns.LastOrDefault(IsLastName) != null;
        }

        #endregion

        #region First Name Lookup Methods
        /// <summary>
        /// Determines wether the provided ProperNoun is a FirstName.
        /// </summary>
        /// <param name="proper">The ProperNoun to check.</param>
        /// <returns>True if the provided ProperNoun is a FirstName, false otherwise.</returns>
        public static bool IsFirstName(this ProperNoun proper) {
            return proper.Text.IsFirstName();
        }
        /// <summary>
        /// Determines if provided text is in the set of Female or Male first names.
        /// </summary>
        /// <param name="text">The text to check.</param>
        /// <returns>True if the provided text is in the set of Female or Male first names, false otherwise.</returns>
        public static bool IsFirstName(this string text) {
            return femaleNames.Count > maleNames.Count ?
                maleNames.Contains(text) || femaleNames.Contains(text) :
                femaleNames.Contains(text) || maleNames.Contains(text);
        }
        /// <summary>
        /// Determines wether the ProperNoun's text corresponds to a last name in the english language.
        /// Lookups are performed in a case insensitive manner and currently do not respect plurality.
        /// </summary>
        /// <param name="proper">The ProperNoun to check.</param>
        /// <returns>True if the ProperNoun's text corresponds to a last name in the english language, false otherwise.</returns>
        public static bool IsLastName(this ProperNoun proper) {
            return proper.Text.IsLastName();
        }
        /// <summary>
        /// Determines wether the ProperNoun's text corresponds to a female first name in the english language.
        /// Lookups are performed in a case insensitive manner and currently do not respect plurality.
        /// </summary>
        /// <param name="proper">The ProperNoun to test.</param>
        /// <returns>True if the ProperNoun's text corresponds to a female first name in the english language, false otherwise.</returns>
        public static bool IsFemaleName(this ProperNoun proper) {
            return proper.Text.IsFemaleName();
        }
        /// <summary>
        /// Returns a value indicating wether the ProperNoun's text corresponds to a male first name in the english language. 
        /// Lookups are performed in a case insensitive manner and currently do not respect plurality.
        /// </summary>
        /// <param name="proper">The ProperNoun to test.</param>
        /// <returns>True if the ProperNoun's text corresponds to a male first name in the english language, false otherwise.</returns>
        public static bool IsMaleName(this ProperNoun proper) {
            return proper.Text.IsMaleName();
        }
        /// <summary>
        /// Returns a value indicating wether the provided string corresponds to a common lastname in the english language. 
        /// Lookups are performed in a case insensitive manner and currently do not respect plurality.
        /// </summary>
        /// <param name="text">The Name to lookup</param>
        /// <returns>True if the provided string corresponds to a common lastname in the english language, false otherwise.</returns>
        public static bool IsLastName(this string text) {
            return lastNames.Contains(text);
        }
        /// <summary>
        /// Returns a value indicating wether the provided string corresponds to a common female name in the english language. 
        /// Lookups are performed in a case insensitive manner and currently do not respect plurality.
        /// </summary>
        /// <param name="text">The Name to lookup</param>
        /// <returns>True if the provided string corresponds to a common female name in the english language, false otherwise.</returns>
        public static bool IsFemaleName(this string text) {
            return femaleNames.Contains(text);
        }
        /// <summary>
        /// Returns a value indicating wether the provided string corresponds to a common male name in the english language. 
        /// Lookups are performed in a case insensitive manner and currently do not respect plurality.
        /// </summary>
        /// <param name="text">The Name to lookup</param>
        /// <returns>True if the provided string corresponds to a common male name in the english language, false otherwise.</returns>
        public static bool IsMaleName(this string text) {
            return maleNames.Contains(text);
        }

        #endregion

        #endregion

        #region Private Methods

        #region Internal Syonym Lookup Methods

        private static ISet<string> InternalLookup(Noun noun) {
            return cachedNounData.GetOrAdd(noun.Text, key => nounLookup[key]);
        }
        private static ISet<string> InternalLookup(Verb verb) {
            return cachedVerbData.GetOrAdd(verb.Text, key => verbLookup[key]);
        }
        private static ISet<string> InternalLookup(Adverb adverb) {
            return cachedAdverbData.GetOrAdd(adverb.Text, key => adverbLookup[key]);
        }
        private static ISet<string> InternalLookup(Adjective adjective) {
            return cachedAdjectiveData.GetOrAdd(adjective.Text, key => adjectiveLookup[key]);
        }

        private static async Task LoadNameDataAsync() {
            await Task.Factory.ContinueWhenAll(
                new[] {  
                    Task.Run(async () => lastNames = await ReadSplitLinesAsync(lastNamesFilePath)),
                    Task.Run(async () => femaleNames = await ReadSplitLinesAsync(femaleNamesFilePath)),
                    Task.Run(async () => maleNames = await ReadSplitLinesAsync(maleNamesFilePath)) 
                },
                results => {
                    genderAmbiguousFirstNames = new HashSet<string>(maleNames.Intersect(femaleNames).Concat(femaleNames.Intersect(maleNames)), StringComparer.OrdinalIgnoreCase);

                    var stratified = from m in maleNames.Select((s, i) => new { Rank = (double)i / maleNames.Count, Name = s })
                                     join f in femaleNames.Select((s, i) => new { Rank = (double)i / femaleNames.Count, Name = s })
                                     on m.Name equals f.Name
                                     group f.Name by f.Rank / m.Rank > 1 ? 'M' : m.Rank / f.Rank > 1 ? 'F' : 'U';

                    maleNames.ExceptWith(from s in stratified where s.Key == 'F' from n in s select n);
                    femaleNames.ExceptWith(from s in stratified where s.Key == 'M' from n in s select n);
                }
            );
        }

        private static async Task<HashSet<string>> ReadSplitLinesAsync(string fileName) {
            using (var reader = new StreamReader(fileName)) {
                return new HashSet<string>((
                    await reader.ReadToEndAsync()).Split(new[] { '\n' },
                    StringSplitOptions.RemoveEmptyEntries),
                    StringComparer.OrdinalIgnoreCase
                );
            }
        }

        #endregion

        #endregion

        #region Public Properties

        /// <summary>
        /// Returns a sequence of Tasks containing all of the yet unstarted LexicalLookup loading operations.
        /// Await each Task to start its corresponding loading operation.
        /// </summary>
        /// <returns>a sequence of Tasks containing all of the yet unstarted LexicalLookup loading operations.</returns>
        public static IEnumerable<Task<string>> GetUnstartedLoadingTasks() {
            var Tasks = new List<Task<string>>();
            if (nounLoadingState == LoadingState.NotStarted)
                Tasks.Add(NounThesaurusLoadTask);
            if (verbLoadingState == LoadingState.NotStarted)
                Tasks.Add(VerbThesaurusLoadTask);
            if (adjectiveLoadingState == LoadingState.NotStarted)
                Tasks.Add(AdjectiveThesaurusLoadTask);
            if (adverbLoadingState == LoadingState.NotStarted)
                Tasks.Add(AdverbThesaurusLoadTask);
            Tasks.Add(Task.Run(async () => {
                await LoadNameDataAsync();
                return "Loaded Name Data";
            }));
            return Tasks;
        }


        #region NameData Accessors

        /// <summary>
        /// Gets a sequence of all known Last Names.
        /// </summary>
        public static IEnumerable<string> LastNames {
            get {
                return lastNames;
            }
        }
        /// <summary>
        /// Gets a sequence of all known Female Names.
        /// </summary>
        public static IEnumerable<string> FemaleNames {
            get {
                return femaleNames;
            }
        }
        /// <summary>
        /// Gets a sequence of all known Male Names.
        /// </summary>
        public static IEnumerable<string> MaleNames {
            get {
                return maleNames;
            }
        }
        /// <summary>
        /// Gets a sequence of all known Names which are just as likely to be Female or Male.
        /// </summary>
        public static IEnumerable<string> GenderAmbiguousFirstNames {
            get {
                return genderAmbiguousFirstNames;
            }
        }

        #endregion

        #endregion

        #region Private Properties

        #region Task Aquisition Accessors
        private static Task<string> AdjectiveThesaurusLoadTask {
            get {
                return Task.Run(async () => {
                    adjectiveLoadingState = LoadingState.InProgress;
                    await adjectiveLookup.LoadAsync();
                    adjectiveLoadingState = LoadingState.Finished;
                    return "Adjective Thesaurus Loaded";
                });
            }
        }
        private static Task<string> AdverbThesaurusLoadTask {
            get {
                return Task.Run(async () => {
                    adverbLoadingState = LoadingState.InProgress;
                    await adverbLookup.LoadAsync();
                    adverbLoadingState = LoadingState.Finished;
                    return "Adverb Thesaurus Loaded";
                });
            }
        }
        private static Task<string> VerbThesaurusLoadTask {
            get {
                return Task.Run(async () => {
                    verbLoadingState = LoadingState.InProgress;
                    await verbLookup.LoadAsync();
                    verbLoadingState = LoadingState.Finished;
                    return "Verb Thesaurus Loaded";
                });
            }
        }
        private static Task<string> NounThesaurusLoadTask {
            get {
                return Task.Run(async () => {
                    nounLoadingState = LoadingState.InProgress;
                    await nounLookup.LoadAsync();
                    nounLoadingState = LoadingState.Finished;
                    return "Noun Thesaurus Loaded";
                });

            }
        }
        #endregion

        #endregion

        #region Private Fields
        // WordNet Data File Paths
        private static readonly string nounWNFilePath = ConfigurationManager.AppSettings["ThesaurusFileDirectory"] + "data.noun";
        private static readonly string verbWNFilePath = ConfigurationManager.AppSettings["ThesaurusFileDirectory"] + "data.verb";
        private static readonly string adverbWNFilePath = ConfigurationManager.AppSettings["ThesaurusFileDirectory"] + "data.adv";
        private static readonly string adjectiveWNFilePath = ConfigurationManager.AppSettings["ThesaurusFileDirectory"] + "data.adj";
        // Internal Thesauri
        private static NounLookup nounLookup = new NounLookup(nounWNFilePath);
        private static VerbLookup verbLookup = new VerbLookup(verbWNFilePath);
        private static AdjectiveLookup adjectiveLookup = new AdjectiveLookup(adjectiveWNFilePath);
        private static AdverbLookup adverbLookup = new AdverbLookup(adverbWNFilePath);
        // Name Data File Paths
        private static readonly string lastNamesFilePath = ConfigurationManager.AppSettings["NameDataDirectory"] + "last.txt";
        private static readonly string femaleNamesFilePath = ConfigurationManager.AppSettings["NameDataDirectory"] + "femalefirst.txt";
        private static readonly string maleNamesFilePath = ConfigurationManager.AppSettings["NameDataDirectory"] + "malefirst.txt";
        // Name Data Sets
        private static ISet<string> lastNames;
        private static ISet<string> maleNames;
        private static ISet<string> femaleNames;
        private static ISet<string> genderAmbiguousFirstNames;
        // Synonym Lookup Caches
        private static ConcurrentDictionary<string, ISet<string>> cachedNounData = new ConcurrentDictionary<string, ISet<string>>(Concurrency.CurrentMax, 4096);
        private static ConcurrentDictionary<string, ISet<string>> cachedVerbData = new ConcurrentDictionary<string, ISet<string>>(Concurrency.CurrentMax, 4096);
        private static ConcurrentDictionary<string, ISet<string>> cachedAdjectiveData = new ConcurrentDictionary<string, ISet<string>>(Concurrency.CurrentMax, 4096);
        private static ConcurrentDictionary<string, ISet<string>> cachedAdverbData = new ConcurrentDictionary<string, ISet<string>>(Concurrency.CurrentMax, 4096);
        //Loading states for specific data items
        private static LoadingState nounLoadingState = LoadingState.NotStarted;
        private static LoadingState verbLoadingState = LoadingState.NotStarted;
        private static LoadingState adjectiveLoadingState = LoadingState.NotStarted;
        private static LoadingState adverbLoadingState = LoadingState.NotStarted;
        // Similarity threshold for Phrase comparisons.
        private const double SIMILARITY_THRESHOLD = 0.6;
        #endregion

        #region Enumerations
        private enum LoadingState
        {
            NotStarted,
            InProgress,
            Finished
        }
        #endregion

        #region Utility Types

        /// <summary>
        /// Encapsulates multiple pieces of information gathered during a similarity comparison into a light weight type.
        /// The structure cannot be created from outside of the LexicalLookup class and is used to convey internal results.
        /// No special syntax is or code changes are required to manipulate this type. It will implicitely convert to bool
        /// So all code with forms such as:  if ( a.IsSimilarTo(b) ) { ... } need not and should not be changed. 
        /// However, If the numeric ratio used to determine similarity is needed and applicable, the type will implcitely convert
        /// to a double. This removes the need for public code such as: if ( LexicalLookup.GetSimiliarityRatio(a, b) > 0.7 ) { ... }
        /// Instead one may simple write the same logic as if: if ( a.IsSimilarTo(b) > 0.7 ) { ... }
        /// </summary>
        public struct SimResult : IEquatable<SimResult>, IComparable<SimResult>
        {
            #region Constructors

            /// <summary>
            /// Initializes a new instance of the SimilarityResult structure from the provided values.
            /// </summary>
            /// <param name="similar">Indicates the result the true of false result of an IsSimilarTo test.</param>
            /// <param name="similarityRatio">Represents the similarity ratio between the tested elements, if applicable.</param>
            internal SimResult(bool similar, double similarityRatio)
                : this() {
                booleanResult = similar;
                rationalResult = similarityRatio;
            }
            /// <summary>
            /// intializes a new instance of the SimilarityResult structure from the provided bool value.
            /// </summary>
            /// <param name="similar">Indicates the result the true of false result of an IsSimilarTo test.</param>
            /// <remarks>Use this constructor when the ratio itself is not specified or not provided.
            /// In such cases, the RatioResult property will be automatically set to 1 or 0 based on the truthfullness of the provided similar argument.
            /// </remarks>
            internal SimResult(bool similar) : this(similar, similar ? 1 : 0) { }

            #endregion

            #region Methods
            public bool Equals(SimResult other) {
                return this == other;
            }
            /// <summary>
            /// Compares the current object with another object of the same type.
            /// </summary>
            /// <param name="other">An object to compare with this object.</param>
            /// <returns>
            /// A value that indicates the relative order of the objects being compared.
            /// The return value has the following meanings: Value Meaning Less than zero
            /// This object is less than the other parameter.Zero This object is equal to
            /// other. Greater than zero This object is greater than other.
            /// </returns>
            public int CompareTo(SimResult other) {
                return this.rationalResult.CompareTo(other.rationalResult);
            }
            public override bool Equals(object obj) {
                return obj != null && obj is SimResult && this == (SimResult)obj;
            }
            public override int GetHashCode() {
                return rationalResult.GetHashCode() ^ booleanResult.GetHashCode();
            }
            #endregion

            #region Fields

            private bool booleanResult;
            private double rationalResult;

            #endregion

            #region Operators

            #region Implcit Conversion Operators
            // These allow the type to implcitely convert to the desired result type for the condition. 
            // Thus, refactoring the IsSimilarTo implementations preserves and enhances existing code
            // without the need to rewrite any conditionals or call expensive methods multiple times to get numeric vs boolean results

            /// <summary>
            /// Converts the SimResult into its boolean representation. The resulting boolean has the same value as the conversion target's booleanResult Property.
            /// </summary>
            /// <param name="sr">The SimResult to convert.</param>
            /// <returns>A boolean with the same value as the conversion target's booleanResult Property.</returns>
            public static implicit operator bool(SimResult sr) { return sr.booleanResult; }
            /// <summary>
            /// Converts the SimResult into its double representation. The resulting boolean has the same value as the conversion target's RatioResult Property.
            /// </summary>
            /// <param name="sr">The SimResult to convert.</param>
            /// <returns>A double with the same value as the conversion target's RatioResult Property.</returns>
            public static implicit operator double(SimResult sr) { return sr.rationalResult; }

            #endregion

            #region Comparison Operators
            /// <summary>
            /// Returns a value that indicates whether the SimResult on the left is equal to the SimResult on the right.
            /// Although it seems unlikely that two instances of SimResult will be compared directly for equality. 
            /// The == and != operators or defined to ensure type coersion does not result from the implicit conversions which make the class convenient.
            /// Equality is defined strictly such that both RatioResult properties must match exactly in addition to both booleanResult properties.
            /// Keep this in mind if, for some reason, it is ever necessary to write code such as: if ( a.IsSimilarTo(b) == b.IsSimilarTo(a) ) { ... } 
            /// as the lexical lookup class itself currently makes no guarantees about reflexive equality over phrase-wise comparisons.
            /// </summary>
            /// <param name="left">The SimRult on the left hand side.</param>
            /// <param name="right">The SimRult on the right hand side.</param>
            /// <returns>True if the SimResult on the left is equal to the SimResult on the right.</returns>
            public static bool operator ==(SimResult left, SimResult right) {
                return left.rationalResult == right.rationalResult && left.booleanResult == right.booleanResult;
            }
            /// <summary>
            /// Returns a value that indicates whether the SimResult on the left is not equal to the SimResult on the right.
            /// Although it seems unlikely that two instances of SimResult will be compared directly for equality. 
            /// The == and != operators or defined to ensure type coersion does not result from the implicit conversions which make the class convenient.
            /// Equality is defined strictly such that both RatioResult properties must match exactly in addition to both booleanResult properties.
            /// Keep this in mind if, for some reason, it is ever necessary to write code such as: if ( a.IsSimilarTo(b) == b.IsSimilarTo(a) ) { ... } 
            /// as the lexical lookup class itself currently makes no guarantees about reflexive equality over phrase-wise comparisons.
            /// </summary>
            /// <param name="left">The SimRult on the left hand side.</param>
            /// <param name="right">The SimRult on the right hand side.</param>
            /// <returns>False if the SimResult on the left is equal to the SimResult on the right.</returns>
            public static bool operator !=(SimResult left, SimResult right) {
                return !(left == right);
            }
            #endregion

            #endregion
        }

        #endregion
    }
}