using LASI.Algorithm.LexicalInformationProviders.Lookups;
using LASI.Utilities;
using System;
using System.Text;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.IO;

namespace LASI.Algorithm.LexicalInformationProviders
{
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
        /// Returns the synonyms for the provided Word.
        /// </summary>
        /// <param name="word">The Word to lookup.</param>
        /// <returns>The synonyms for the provided Word.</returns>
        /// <remarks>Type runtime type checking is enforced such that if the provided Word is not an instance of Noun, Verb, Adjective, or Adverb or one of their subtypes, a  NoSynonymLookupForTypeException is thrown.</remarks>
        /// <exception cref="NoSynonymLookupForTypeException"></exception>
        public static IEnumerable<string> Lookup(Word word) {
            return InternalLookup(word as dynamic);
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
        /// <summary>
        /// Determines if two Word instances are synonymous. Type checking is enforced.
        /// </summary>
        /// <param name="word">The first Word.</param>
        /// <param name="other">The second Word</param>
        /// <returns>True if the Word instances are synonymous, false otherwise.</returns>
        /// <remarks>There are two calling conventions for this method. See the following examples:
        /// <code>if ( LexicalLookup.IsSimilarTo(vp1, vp2) ) { ... }</code>
        /// <code>if ( vp1.IsSimilarTo(vp2) ) { ... }</code>
        /// Please prefer the second convention.
        /// </remarks>
        public static bool IsSynonymFor(this Word word, Word other) {

            return (word is Noun && other is Noun ||
                word is Verb && other is Verb ||
                word is Adverb && other is Adverb ||
                word is Adjective && other is Adjective
                ) && Lookup(other).Contains(word.Text);
        }
        #endregion

        #region Similarity Comparion Methods
        /// <summary>
        /// Determines if two IEntity instances are similar.
        /// </summary>
        /// <param name="first">The first IEntity</param>
        /// <param name="second">The Second IEntity</param>
        /// <returns>True if the given IEntity instances are similar, false otherwise.</returns>
        /// <remarks>There are two calling conventions for this method. See the following examples
        /// <code>if ( LexicalLookup.IsSimilarTo(e1, e2) ) { ... }</code>
        /// <code>if ( e1.IsSimilarTo(e2) ) { ... }</code>
        /// Please prefer the second convention.
        /// </remarks>
        public static bool IsSimilarTo(this IEntity first, IEntity second) {
            if (first.Text.ToUpper() == second.Text.ToUpper()) {
                return true;
            }

            var n1 = first as Noun;
            var n2 = second as Noun;
            if (n1 != null && n2 != null) {
                return n1.IsSynonymFor(n2);
            }

            var np1 = first as NounPhrase;
            var np2 = second as NounPhrase;
            if (np1 != null && np2 != null) {
                return np1.IsSimilarTo(np2);
            }
            //need to add functionality to compare a Noun with a NounPhrase.


            var np = first as NounPhrase ?? second as NounPhrase;
            var n = first as Noun ?? second as Noun;
            if (n != null && np != null) {
                return n.IsSimilarTo(np);
            }
            return false;
        }
        /// <summary>
        /// Determines if the provided Noun is similar to the provided NounPhrase.
        /// </summary>
        /// <param name="first">The Noun.</param>
        /// <param name="second">The NounPhrase.</param>
        /// <returns>True if the provided Noun is similar to the provided NounPhrase, false otherwise.</returns>
        /// <remarks>There are two calling conventions for this method. See the following examples:
        /// <code>if ( LexicalLookup.IsSimilarTo(vp1, vp2) ) { ... }</code>
        /// <code>if ( vp1.IsSimilarTo(vp2) ) { ... }</code>
        /// Please prefer the second convention.
        /// </remarks>
        public static bool IsSimilarTo(this Noun first, NounPhrase second) {
            var nouns = second.Words.GetNouns();
            return nouns.Count() == 1 && nouns.First().IsSynonymFor(first);
        }
        /// <summary>
        /// Determines if the provided NounPhrase is similar to the provided Noun.
        /// </summary>
        /// <param name="first">The NounPhrase.</param>
        /// <param name="second">The Noun.</param>
        /// <returns>True if the provided NounPhrase is similar to the provided Noun, false otherwise.</returns>
        /// <remarks>There are two calling conventions for this method. See the following examples:
        /// <code>if ( LexicalLookup.IsSimilarTo(vp1, vp2) ) { ... }</code>
        /// <code>if ( vp1.IsSimilarTo(vp2) ) { ... }</code>
        /// Please prefer the second convention.
        /// </remarks>
        public static bool IsSimilarTo(this NounPhrase first, Noun second) {
            var nouns = first.Words.GetNouns();
            return nouns.Count() == 1 && nouns.First().IsSynonymFor(second);
        }
        /// <summary>
        /// Determines if the provided VerbPhrase is similar to the provided Verb.
        /// </summary>
        /// <param name="first">The VerbPhrase.</param>
        /// <param name="second">The Verb.</param>
        /// <returns>True if the provided VerbPhrase is similar to the provided Verb, false otherwise.</returns>
        /// <remarks>There are two calling conventions for this method. See the following examples:
        /// <code>if ( LexicalLookup.IsSimilarTo(vp1, vp2) ) { ... }</code>
        /// <code>if ( vp1.IsSimilarTo(vp2) ) { ... }</code>
        /// Please prefer the second convention.
        /// </remarks>
        public static bool IsSimilarTo(this VerbPhrase first, Verb second) {
            return second.IsSimilarTo(first);
        }
        /// <summary>
        /// Determines if the provided Verb is similar to the provided VerbPhrase.
        /// </summary>
        /// <param name="first">The Verb.</param>
        /// <param name="second">The VerbPhrase.</param>
        /// <returns>True if the provided Verb is similar to the provided VerbPhrase, false otherwise.</returns>
        /// <remarks>There are two calling conventions for this method. See the following examples:
        /// <code>if ( LexicalLookup.IsSimilarTo(vp1, vp2) ) { ... }</code>
        /// <code>if ( vp1.IsSimilarTo(vp2) ) { ... }</code>
        /// Please prefer the second convention.
        /// </remarks>
        public static bool IsSimilarTo(this Verb first, VerbPhrase second) {
            var verbs = second.Words.GetVerbs();
            return verbs.Count() == 1 && verbs.First().IsSynonymFor(first);
        }
        /// <summary>
        /// Determines if two IVerbal instances are similar.
        /// </summary>
        /// <param name="first">The first IVerbal</param>
        /// <param name="second">The Second IVerbal</param>
        /// <returns>True if the given IVerbal instances are similar, false otherwise.</returns>
        /// <remarks>There are two calling conventions for this method. See the following examples
        /// <code>if ( LexicalLookup.IsSimilarTo(v1, v2) ) { ... }</code>
        /// <code>if ( v1.IsSimilarTo(v2) ) { ... }</code> 
        /// Please prefer the second convention.
        /// </remarks>
        public static bool IsSimilarTo(this IVerbal first, IVerbal second) {

            //Compare literal text.
            if (first.Text.ToUpper() == second.Text.ToUpper()) {
                return true;
            }

            //If both are of type Verb check if syonymous
            var v1 = first as Verb;
            var v2 = second as Verb;
            if (v1 != null && v2 != null) {
                return v1.IsSynonymFor(v2);
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

            return false;

        }
        /// <summary>
        /// Determines if two NounPhrases are similar.
        /// </summary>
        /// <param name="first">The first NounPhrase</param>
        /// <param name="second">The Second NounPhrase</param>
        /// <returns>True if the given NounPhrases are similar, false otherwise.</returns>
        /// <remarks>There are two calling conventions for this method. See the following examples:
        /// <code>if ( LexicalLookup.IsSimilarTo(vp1, vp2) ) { ... }</code>
        /// <code>if ( vp1.IsSimilarTo(vp2) ) { ... }</code>
        /// Please prefer the second convention.
        /// </remarks>
        public static bool IsSimilarTo(this NounPhrase first, NounPhrase second) {

            return GetSimilarityRatio(first, second) > SIMILARITY_THRESHOLD;
        }
        /// <summary>
        /// Determines if two VerbPhrases are similar.
        /// </summary>
        /// <param name="first">The first VerbPhrase</param>
        /// <param name="second">The Second VerbPhrase</param>
        /// <returns>True if the given VerbPhrases are similar, false otherwise.</returns>
        /// <remarks>There are two calling conventions for this method. See the following examples:
        /// <code>if ( LexicalLookup.IsSimilarTo(vp1, vp2) ) { ... }</code>
        /// <code>if ( vp1.IsSimilarTo(vp2) ) { ... }</code>
        /// Please prefer the second convention.
        /// </remarks>
        public static bool IsSimilarTo(this VerbPhrase first, VerbPhrase second) {

            //Look into refining this
            List<Verb> leftHandVerbs = first.Words.GetVerbs().ToList();
            List<Verb> rightHandVerbs = second.Words.GetVerbs().ToList();

            bool result = leftHandVerbs.Count == rightHandVerbs.Count;

            if (result) {
                try {
                    for (var i = 0; i < leftHandVerbs.Count; ++i) {
                        result &= leftHandVerbs[i].IsSynonymFor(rightHandVerbs[i]);
                    }
                }
                catch (NullReferenceException) {
                    return false;
                }
            }

            return result;
        }
        /// <summary>
        /// Returns a double value indicating the degree of similarity between two NounPhrases.
        /// </summary>
        /// <param name="first">The first NounPhrase</param>
        /// <param name="second">The second NounPhrase</param>
        /// <returns>A double value indicating the degree of similarity between two NounPhrases.</returns>
        public static double GetSimilarityRatio(NounPhrase first, NounPhrase second) {
            NounPhrase outer = null;
            NounPhrase inner = null;
            double similarCount = 0.0d;

            if (first.Words.Count() >= second.Words.Count()) {
                outer = first;
                inner = second;
            }
            else {
                outer = second;
                inner = first;
            }

            if ((outer.Words.GetNouns().Count() != 0) && (inner.Words.GetNouns().Count() != 0)) {
                foreach (var o in outer.Words.GetNouns()) {
                    foreach (var i in inner.Words.GetNouns()) {
                        if (i.IsSimilarTo(o) || i.IsAliasFor(o))
                            similarCount += 0.7;
                    }
                    var scaleFactor = inner.Words.GetNouns().Count() * outer.Words.GetNouns().Count();
                    return (similarCount / scaleFactor == 0 ? 1 : scaleFactor);
                }

            }
            return 0d;
        }
        /// <summary>
        /// Returns a double value indicating the degree of similarity between two NounPhrases.
        /// </summary>
        /// <param name="first">The first NounPhrase</param>
        /// <param name="second">The second NounPhrase</param>
        /// <returns>A double value indicating the degree of similarity between two NounPhrases.</returns>
        public static double SimilarityRatioWith(this NounPhrase first, NounPhrase second) {
            return GetSimilarityRatio(first, second);
        }
        #endregion

        #region Full Name Lookup Methods

        /// <summary>
        /// Determines if the provided NounPhrase is a known Full Name.
        /// </summary>
        /// <param name="name">The NounPhrase to check.</param>
        /// <returns>True if the provided NounPhrase is a known Full Name, false otherwise.</returns>
        public static bool IsFullName(this NounPhrase name) {
            return InternalFullNameChecking(name, proper => proper.IsFirstName());
        }
        /// <summary>
        /// Determines if the provided NounPhrase is a known Full Female Name.
        /// </summary>
        /// <param name="name">The NounPhrase to check.</param>
        /// <returns>True if the provided NounPhrase is a known Full Female Name, false otherwise.</returns>
        public static bool IsFullFemaleName(this NounPhrase name) {
            return InternalFullNameChecking(name, proper => proper.IsFemaleName());
        }
        /// <summary>
        /// Determines if the provided NounPhrase is a known Full Male Name.
        /// </summary>
        /// <param name="name">The NounPhrase to check.</param>
        /// <returns>True if the provided NounPhrase is a known Full Male Name, false otherwise.</returns>
        public static bool IsFullMaleName(this NounPhrase name) {
            return InternalFullNameChecking(name, properNoun => properNoun.IsMaleName());
        }


        private static bool InternalFullNameChecking(NounPhrase name, Func<ProperNoun, bool> fnPredicate) {
            var pns = name.Words.GetProperNouns();
            var pcnt = pns.Count();
            return pcnt > 1 &&
            pns.FirstOrDefault(proper => proper != null && fnPredicate(proper)) != null &&
            pns.Skip(1).Take(pcnt - 1).All(s => s != null && s.IsFirstName() || s.IsLastName()) &&
            pns.Last().IsLastName();
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
            return proper.Text.IsLastName();
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
        private static ISet<string> InternalLookup(Word word) {
            throw new NoSynonymLookupForTypeException(word);
        }
        private static async Task LoadNameDataAsync() {
            await Task.Factory.ContinueWhenAll(
                new Task[] {  
                    Task.Run(async () => lastNames = await GetNameDataAsync(lastNamesFilePath)),
                    Task.Run(async () => femaleNames = await GetNameDataAsync(femaleNamesFilePath)),
                    Task.Run(async () => maleNames =await GetNameDataAsync(maleNamesFilePath) ) 
                },
                results => {
                    genderAmbiguousFirstNames = maleNames.Intersect(femaleNames).Concat(femaleNames.Intersect(maleNames)).ToSet(StringComparer.OrdinalIgnoreCase);

                    var i1 = maleNames.Select((s, i) => new { Rank = (double)i / maleNames.Count, Name = s });
                    var i2 = femaleNames.Select((s, i) => new { Rank = (double)i / femaleNames.Count, Name = s });

                    var sect = from m in i1
                               join f in i2 on m.Name equals f.Name
                               select new { MorF = f.Rank / m.Rank > 1 ? 'M' : m.Rank / f.Rank > 1 ? 'F' : 'U', Name = f.Name };

                    var stratified = from s in sect group s by s.MorF
                                         into g
                                         select new { Key = g.Key, Count = g.Count(), Names = g.ToArray() };

                    maleNames.ExceptWith(from s in stratified where s.Key == 'F' from n in s.Names select n.Name);
                    femaleNames.ExceptWith(from s in stratified where s.Key == 'M' from n in s.Names select n.Name);
                }
            );
        }

        private static async Task<HashSet<string>> GetNameDataAsync(string fileName) {
            using (var reader = new StreamReader(fileName)) {
                return new HashSet<string>((
                    await reader.ReadToEndAsync()).Split(new[] { '\n' },
                    StringSplitOptions.RemoveEmptyEntries),
                    StringComparer.OrdinalIgnoreCase
                );
            }
        }
        /// <summary>
        /// Returns a sequence of Tasks containing all of the yet unstarted LexicalLookup loading operations.
        /// Await each Task to start its corresponding loading operation.
        /// </summary>
        /// <returns>a sequence of Tasks containing all of the yet unstarted LexicalLookup loading operations.</returns>
        public static IEnumerable<Task<string>> UnstartedLoadingTasks {
            get {

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
                return Tasks.ToArray();
            }
        }

        #endregion

        #endregion

        #region Public Properties
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
    }
}


