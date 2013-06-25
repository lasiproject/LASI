using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LASI.Utilities;

namespace LASI.Algorithm.Thesauri
{
    /// <summary>
    /// Provides an indexed lookup between the values of the Noun enum and their corresponding string representation in WordNet data.noun files.
    /// </summary>
    internal class NounPointerSymbolMap
    {
        /// <summary>
        /// Gets the Noun value corresponding to the Key string. 
        /// The Value UNDEFINED is returned if no value is mapped to the key.
        /// </summary>
        /// <param name="key">The Key string for which to retrieve a Noun value.</param>
        /// <returns>The Noun value corresponding to the Key string.
        /// The Value UNDEFINED is returned if no value is mapped to the key.</returns>
        public NounPointerSymbol this[string key] {
            get {
                NounPointerSymbol result;
                data.TryGetValue(key, out result);
                return result;
            }
        }
        public override string ToString() {
            return data.Format(pair => string.Format("\"{0}\" -> {1}", pair.Key, pair.Value));
        }
        private static readonly IReadOnlyDictionary<string, NounPointerSymbol> data = new Dictionary<string, NounPointerSymbol>{ 
            { "!", NounPointerSymbol.Antonym },
            { "@", NounPointerSymbol.HypERnym },
            { "@i", NounPointerSymbol.InstanceHypERnym },
            { "~", NounPointerSymbol.HypOnym },
            { "~i", NounPointerSymbol.InstanceHypOnym },
            { "#m", NounPointerSymbol.MemberHolonym },
            { "#s", NounPointerSymbol.SubstanceHolonym },
            { "#p", NounPointerSymbol.PartHolonym },
            { "%m", NounPointerSymbol.MemberMeronym },
            { "%s", NounPointerSymbol.SubstanceMeronym },
            { "%p", NounPointerSymbol.PartMeronym },
            { "=", NounPointerSymbol.Attribute },
            { "+", NounPointerSymbol.DerivationallyRelatedForm },
            { ";c", NounPointerSymbol.DomainOfSynset_TOPIC },
            { "-c", NounPointerSymbol.MemberOfThisDomain_TOPIC },
            { ";r", NounPointerSymbol.DomainOfSynset_REGION },
            { "-r", NounPointerSymbol.MemberOfThisDomain_REGION },
            { ";u", NounPointerSymbol.DomainOfSynset_USAGE },
            { "-u", NounPointerSymbol.MemberOfThisDomain_USAGE }
        };
    }
    /// <summary>
    /// Provides an indexed lookup between the values of the VerbPointerSymbol enum and their corresponding string representation in WordNet data.verb files.
    /// </summary>
    internal class VerbPointerSymbolMap
    {
        /// <summary>
        /// Gets the VerbPointerSymbol value corresponding to the Key string. 
        /// The Value UNDEFINED is returned if no value is mapped key.
        /// </summary>
        /// <param name="key">The Key string for which to retrieve a Noun value.</param>
        /// <returns>The VerbPointerSymbol value corresponding to the Key string.
        /// The Value UNDEFINED is returned if no value is mapped to the key.</returns>
        public VerbPointerSymbol this[string key] {
            get {
                VerbPointerSymbol result;
                data.TryGetValue(key, out result);
                return result;
            }
        }
        private static readonly IReadOnlyDictionary<string, VerbPointerSymbol> data = new Dictionary<string, VerbPointerSymbol> {
            { "!", VerbPointerSymbol. Antonym }, 
            { "@", VerbPointerSymbol.Hypernym },
            { "~", VerbPointerSymbol.Hyponym },
            { "*", VerbPointerSymbol.Entailment },
            { ">", VerbPointerSymbol.Cause },
            { "^", VerbPointerSymbol. AlsoSee },
            { "$", VerbPointerSymbol.Verb_Group },
            { "+", VerbPointerSymbol.DerivationallyRelatedForm },
            { ";c", VerbPointerSymbol.DomainOfSynset_TOPIC },
            { ";r", VerbPointerSymbol.DomainOfSynset_REGION },
            { ";u", VerbPointerSymbol.DomainOfSynset_USAGE}
        };
    }
    /// <summary>
    /// Provides an indexed lookup between the values of the AdjectivePointerSymbol enum and their corresponding string representation in WordNet data.adj files.
    /// </summary>
    internal class AdjectivePointerSymbolMap
    {
        /// <summary>
        /// Gets the VerbPointerSymbol value corresponding to the Key string. 
        /// The Value UNDEFINED is returned if no value is mapped key.
        /// </summary>
        /// <param name="key">The Key string for which to retrieve a Adjective value.</param>
        /// <returns>The VerbPointerSymbol value corresponding to the Key string.
        /// The Value UNDEFINED is returned if no value is mapped to the key.</returns>
        public AdjectivePointerSymbol this[string key] {
            get {
                AdjectivePointerSymbol result;
                data.TryGetValue(key, out result);
                return result;
            }
        }
        private static readonly IReadOnlyDictionary<string, AdjectivePointerSymbol> data = new Dictionary<string, AdjectivePointerSymbol> {
            { "!", AdjectivePointerSymbol. Antonym }, 
            { "&", AdjectivePointerSymbol.SimilarTo},
            { "<", AdjectivePointerSymbol.ParticipleOfVerb},
            { @"\", AdjectivePointerSymbol.Pertainym_pertains_to_noun},
            { "=", AdjectivePointerSymbol.Attribute},
            { "^", AdjectivePointerSymbol.AlsoSee },
            { ";c", AdjectivePointerSymbol.DomainOfSynset_TOPIC },
            { ";r", AdjectivePointerSymbol.DomainOfSynset_REGION },
            { ";u", AdjectivePointerSymbol.DomainOfSynset_USAGE}
        };
    }

    /// <summary>
    /// Provides an indexed lookup between the values of the AdjectivePointerSymbol enum and their corresponding string representation in WordNet data.adv files.
    /// </summary>
    internal class AdverbPointerSymbolMap
    {
        /// <summary>
        /// Gets the VerbPointerSymbol value corresponding to the Key string. 
        /// The Value UNDEFINED is returned if no value is mapped key.
        /// </summary>
        /// <param name="key">The Key string for which to retrieve a Noun value.</param>
        /// <returns>The VerbPointerSymbol value corresponding to the Key string.
        /// The Value UNDEFINED is returned if no value is mapped to the key.</returns>
        public AdverbPointerSymbol this[string key] {
            get {
                AdverbPointerSymbol result;
                data.TryGetValue(key, out result);
                return result;
            }
        }
        private static readonly IReadOnlyDictionary<string, AdverbPointerSymbol> data = new Dictionary<string, AdverbPointerSymbol> {
            { "!", AdverbPointerSymbol. Antonym }, 
            { @"\", AdverbPointerSymbol.DerivedFromAdjective},
            { ";c", AdverbPointerSymbol.DomainOfSynset_TOPIC },
            { ";r", AdverbPointerSymbol.DomainOfSynset_REGION },
            { ";u", AdverbPointerSymbol.DomainOfSynset_USAGE}
        };
    }



    internal class NounSetIDSymbolMap : ILookup<NounPointerSymbol, int>
    {
        public NounSetIDSymbolMap(IEnumerable<KeyValuePair<NounPointerSymbol, int>> relationData) {
            data = (from pair in relationData
                    group pair.Value by pair.Key into g
                    select new KeyValuePair<NounPointerSymbol, HashSet<int>>(g.Key, new HashSet<int>(g))).ToDictionary(pair => pair.Key, pair => pair.Value);
        }

        private IDictionary<NounPointerSymbol, HashSet<int>> data;
        private IEnumerable<IGrouping<NounPointerSymbol, int>> groupData;



        public IEnumerable<int> this[NounPointerSymbol key] {
            get {
                return data.ContainsKey(key) ? data[key] : Enumerable.Empty<int>();
            }
        }

        public int Count {
            get {
                return data.Count;
            }
        }

        public bool Contains(NounPointerSymbol key) {
            return data.ContainsKey(key);
        }

        public IEnumerator<IGrouping<NounPointerSymbol, int>> GetEnumerator() {
            groupData = groupData ?? from pair in data
                                     from value in pair.Value
                                     group value by pair.Key;
            return groupData.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() {
            throw new NotImplementedException();
        }
    }

    internal class VerbSetIDSymbolMap : ILookup<VerbPointerSymbol, int>
    {
        public VerbSetIDSymbolMap(IEnumerable<KeyValuePair<VerbPointerSymbol, int>> relationData) {
            data = (from pair in relationData
                    group pair.Value by pair.Key into g
                    select new KeyValuePair<VerbPointerSymbol, HashSet<int>>(g.Key, new HashSet<int>(g))).ToDictionary(pair => pair.Key, pair => pair.Value);
        }
        private IDictionary<VerbPointerSymbol, HashSet<int>> data;
        private IEnumerable<IGrouping<VerbPointerSymbol, int>> groupData;
        public IEnumerable<int> this[VerbPointerSymbol key] {
            get {
                return data.ContainsKey(key) ? data[key] : Enumerable.Empty<int>();
            }
        }

        public int Count {
            get {
                return data.Count;
            }
        }

        public bool Contains(VerbPointerSymbol key) {
            return data.ContainsKey(key);
        }

        public IEnumerator<IGrouping<VerbPointerSymbol, int>> GetEnumerator() {
            groupData = groupData ?? from pair in data
                                     from value in pair.Value
                                     group value by pair.Key;
            return groupData.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() {
            throw new NotImplementedException();
        }
    }

    internal class AdjectiveSetIDSymbolMap : ILookup<AdjectivePointerSymbol, int>
    {
        public AdjectiveSetIDSymbolMap(IEnumerable<KeyValuePair<AdjectivePointerSymbol, int>> relationData) {
            data = (from pair in relationData
                    group pair.Value by pair.Key into g
                    select new KeyValuePair<AdjectivePointerSymbol, HashSet<int>>(g.Key, new HashSet<int>(g))).ToDictionary(pair => pair.Key, pair => pair.Value);
        }
        private IDictionary<AdjectivePointerSymbol, HashSet<int>> data;
        private IEnumerable<IGrouping<AdjectivePointerSymbol, int>> groupData;
        public IEnumerable<int> this[AdjectivePointerSymbol key] {
            get {
                return data.ContainsKey(key) ? data[key] : Enumerable.Empty<int>();
            }
        }

        public int Count {
            get {
                return data.Count;
            }
        }

        public bool Contains(AdjectivePointerSymbol key) {
            return data.ContainsKey(key);
        }

        public IEnumerator<IGrouping<AdjectivePointerSymbol, int>> GetEnumerator() {
            groupData = groupData ?? from pair in data
                                     from value in pair.Value
                                     group value by pair.Key;
            return groupData.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() {
            throw new NotImplementedException();
        }
    }
    internal class AdverbSetIDSymbolMap : ILookup<AdverbPointerSymbol, int>
    {
        public AdverbSetIDSymbolMap(IEnumerable<KeyValuePair<AdverbPointerSymbol, int>> relationData) {
            data = (from pair in relationData
                    group pair.Value by pair.Key into g
                    select new KeyValuePair<AdverbPointerSymbol, HashSet<int>>(g.Key, new HashSet<int>(g))).ToDictionary(pair => pair.Key, pair => pair.Value);
        }
        private IDictionary<AdverbPointerSymbol, HashSet<int>> data;
        private IEnumerable<IGrouping<AdverbPointerSymbol, int>> groupData;
        public IEnumerable<int> this[AdverbPointerSymbol key] {
            get {
                return data.ContainsKey(key) ? data[key] : Enumerable.Empty<int>();
            }
        }

        public int Count {
            get {
                return data.Count;
            }
        }

        public bool Contains(AdverbPointerSymbol key) {
            return data.ContainsKey(key);
        }

        public IEnumerator<IGrouping<AdverbPointerSymbol, int>> GetEnumerator() {
            groupData = groupData ?? from pair in data
                                     from value in pair.Value
                                     group value by pair.Key;
            return groupData.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() {
            throw new NotImplementedException();
        }
    }
}
