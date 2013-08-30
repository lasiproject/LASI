using LASI.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LASI.Algorithm.Lookup.InterSetRelationshipManagement
{
    /// <summary>
    /// Provides an indexed lookup between the values of the Noun enum and their corresponding string representation in WordNet data.noun files.
    /// </summary>
    internal sealed class NounPointerSymbolMap
    {
        /// <summary>
        /// Gets the Noun value corresponding to the Key string. 
        /// The Value UNDEFINED is returned if no value is mapped to the key.
        /// </summary>
        /// <param name="key">The Key string for which to retrieve a Noun value.</param>
        /// <returns>The Noun value corresponding to the Key string.
        /// The Value UNDEFINED is returned if no value is mapped to the key.</returns>
        public NounSetRelationship this[string key] {
            get {
                NounSetRelationship result;
                data.TryGetValue(key, out result);
                return result;
            }
        }
        /// <summary>
        /// Returns a string representation of the NounPointerSymbolMap.
        /// </summary>
        /// <returns>A string representation of the NounPointerSymbolMap.</returns>
        public override string ToString() {
            return data.Format(pair => string.Format("\"{0}\" -> {1}", pair.Key, pair.Value));
        }
        private static readonly IReadOnlyDictionary<string, NounSetRelationship> data = new Dictionary<string, NounSetRelationship>{ 
            { "!", NounSetRelationship.Antonym },
            { "@", NounSetRelationship.HypERnym },
            { "@i", NounSetRelationship.InstanceHypERnym },
            { "~", NounSetRelationship.HypOnym },
            { "~i", NounSetRelationship.InstanceHypOnym },
            { "#m", NounSetRelationship.MemberHolonym },
            { "#s", NounSetRelationship.SubstanceHolonym },
            { "#p", NounSetRelationship.PartHolonym },
            { "%m", NounSetRelationship.MemberMeronym },
            { "%s", NounSetRelationship.SubstanceMeronym },
            { "%p", NounSetRelationship.PartMeronym },
            { "=", NounSetRelationship.Attribute },
            { "+", NounSetRelationship.DerivationallyRelatedForm },
            { ";c", NounSetRelationship.DomainOfSynset_TOPIC },
            { "-c", NounSetRelationship.MemberOfThisDomain_TOPIC },
            { ";r", NounSetRelationship.DomainOfSynset_REGION },
            { "-r", NounSetRelationship.MemberOfThisDomain_REGION },
            { ";u", NounSetRelationship.DomainOfSynset_USAGE },
            { "-u", NounSetRelationship.MemberOfThisDomain_USAGE }
        };
    }
    /// <summary>
    /// Provides an indexed lookup between the values of the VerbPointerSymbol enum and their corresponding string representation in WordNet data.verb files.
    /// </summary>
    internal sealed class VerbPointerSymbolMap
    {
        /// <summary>
        /// Gets the VerbPointerSymbol value corresponding to the Key string. 
        /// The Value UNDEFINED is returned if no value is mapped key.
        /// </summary>
        /// <param name="key">The Key string for which to retrieve a Noun value.</param>
        /// <returns>The VerbPointerSymbol value corresponding to the Key string.
        /// The Value UNDEFINED is returned if no value is mapped to the key.</returns>
        public VerbSetRelationship this[string key] {
            get {
                VerbSetRelationship result;
                data.TryGetValue(key, out result);
                return result;
            }
        }
        private static readonly IReadOnlyDictionary<string, VerbSetRelationship> data = new Dictionary<string, VerbSetRelationship> {
            { "!", VerbSetRelationship. Antonym }, 
            { "@", VerbSetRelationship.Hypernym },
            { "~", VerbSetRelationship.Hyponym },
            { "*", VerbSetRelationship.Entailment },
            { ">", VerbSetRelationship.Cause },
            { "^", VerbSetRelationship. AlsoSee },
            { "$", VerbSetRelationship.Verb_Group },
            { "+", VerbSetRelationship.DerivationallyRelatedForm },
            { ";c", VerbSetRelationship.DomainOfSynset_TOPIC },
            { ";r", VerbSetRelationship.DomainOfSynset_REGION },
            { ";u", VerbSetRelationship.DomainOfSynset_USAGE}
        };
    }
    /// <summary>
    /// Provides an indexed lookup between the values of the AdjectivePointerSymbol enum and their corresponding string representation in WordNet data.adj files.
    /// </summary>
    internal sealed class AdjectivePointerSymbolMap
    {
        /// <summary>
        /// Gets the VerbPointerSymbol value corresponding to the Key string. 
        /// The Value UNDEFINED is returned if no value is mapped key.
        /// </summary>
        /// <param name="key">The Key string for which to retrieve a Adjective value.</param>
        /// <returns>The VerbPointerSymbol value corresponding to the Key string.
        /// The Value UNDEFINED is returned if no value is mapped to the key.</returns>
        public AdjectiveSetRelationship this[string key] {
            get {
                AdjectiveSetRelationship result;
                data.TryGetValue(key, out result);
                return result;
            }
        }
        private static readonly IReadOnlyDictionary<string, AdjectiveSetRelationship> data = new Dictionary<string, AdjectiveSetRelationship> {
            { "!", AdjectiveSetRelationship. Antonym }, 
            { "&", AdjectiveSetRelationship.SimilarTo},
            { "<", AdjectiveSetRelationship.ParticipleOfVerb},
            { @"\", AdjectiveSetRelationship.Pertainym_pertains_to_noun},
            { "=", AdjectiveSetRelationship.Attribute},
            { "^", AdjectiveSetRelationship.AlsoSee },
            { ";c", AdjectiveSetRelationship.DomainOfSynset_TOPIC },
            { ";r", AdjectiveSetRelationship.DomainOfSynset_REGION },
            { ";u", AdjectiveSetRelationship.DomainOfSynset_USAGE}
        };
    }

    /// <summary>
    /// Provides an indexed lookup between the values of the AdjectivePointerSymbol enum and their corresponding string representation in WordNet data.adv files.
    /// </summary>
    internal sealed class AdverbPointerSymbolMap
    {
        /// <summary>
        /// Gets the VerbPointerSymbol value corresponding to the Key string. 
        /// The Value UNDEFINED is returned if no value is mapped key.
        /// </summary>
        /// <param name="key">The Key string for which to retrieve a Noun value.</param>
        /// <returns>The VerbPointerSymbol value corresponding to the Key string.
        /// The Value UNDEFINED is returned if no value is mapped to the key.</returns>
        public AdverbSetRelationship this[string key] {
            get {
                AdverbSetRelationship result;
                data.TryGetValue(key, out result);
                return result;
            }
        }
        private static readonly IReadOnlyDictionary<string, AdverbSetRelationship> data = new Dictionary<string, AdverbSetRelationship> {
            { "!", AdverbSetRelationship. Antonym }, 
            { @"\", AdverbSetRelationship.DerivedFromAdjective},
            { ";c", AdverbSetRelationship.DomainOfSynset_TOPIC },
            { ";r", AdverbSetRelationship.DomainOfSynset_REGION },
            { ";u", AdverbSetRelationship.DomainOfSynset_USAGE}
        };
    }



    internal sealed class NounSetIDSymbolMap : ILookup<NounSetRelationship, int>
    {
        public NounSetIDSymbolMap(IEnumerable<KeyValuePair<NounSetRelationship, int>> relationData) {
            data = (from pair in relationData
                    group pair.Value by pair.Key into g
                    select new { K = g.Key, V = new HashSet<int>(g) })
                    .ToDictionary(pair => pair.K, pair => pair.V);
        }

        private IDictionary<NounSetRelationship, HashSet<int>> data;
        private IEnumerable<IGrouping<NounSetRelationship, int>> groupData;



        public IEnumerable<int> this[NounSetRelationship key] {
            get {
                return data.ContainsKey(key) ? data[key] : Enumerable.Empty<int>();
            }
        }

        public int Count {
            get {
                return data.Count;
            }
        }

        public bool Contains(NounSetRelationship key) {
            return data.ContainsKey(key);
        }

        public IEnumerator<IGrouping<NounSetRelationship, int>> GetEnumerator() {
            return GetGroupDataEnumerator();
        }
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() {
            return GetGroupDataEnumerator();
        }
        private IEnumerator<IGrouping<NounSetRelationship, int>> GetGroupDataEnumerator() {
            groupData = groupData ?? from pair in data
                                     from value in pair.Value
                                     group value by pair.Key;
            return groupData.GetEnumerator();

        }

    }

    internal sealed class VerbSetIDSymbolMap : ILookup<VerbSetRelationship, int>
    {
        public VerbSetIDSymbolMap(IEnumerable<KeyValuePair<VerbSetRelationship, int>> relationData) {
            data = (from pair in relationData
                    group pair.Value by pair.Key into g
                    select new { k = g.Key, v = new HashSet<int>(g) })
                    .ToDictionary(pair => pair.k, pair => pair.v);
        }
        private IDictionary<VerbSetRelationship, HashSet<int>> data;
        private IEnumerable<IGrouping<VerbSetRelationship, int>> groupData;
        public IEnumerable<int> this[VerbSetRelationship key] {
            get {
                return data.ContainsKey(key) ? data[key] : Enumerable.Empty<int>();
            }
        }

        public int Count {
            get {
                return data.Count;
            }
        }

        public bool Contains(VerbSetRelationship key) {
            return data.ContainsKey(key);
        }

        public IEnumerator<IGrouping<VerbSetRelationship, int>> GetEnumerator() {
            return GetGroupDataEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() {
            return GetGroupDataEnumerator();
        }
        private IEnumerator<IGrouping<VerbSetRelationship, int>> GetGroupDataEnumerator() {
            groupData = groupData ?? from pair in data
                                     from value in pair.Value
                                     group value by pair.Key;
            return groupData.GetEnumerator();
        }

    }

    internal sealed class AdjectiveSetIDSymbolMap : ILookup<AdjectiveSetRelationship, int>
    {
        public AdjectiveSetIDSymbolMap(IEnumerable<KeyValuePair<AdjectiveSetRelationship, int>> relationData) {
            data = (from pair in relationData
                    group pair.Value by pair.Key into g
                    select new { K = g.Key, V = new HashSet<int>(g) })
                    .ToDictionary(pair => pair.K, pair => pair.V);
        }
        private IDictionary<AdjectiveSetRelationship, HashSet<int>> data;
        private IEnumerable<IGrouping<AdjectiveSetRelationship, int>> groupData;
        public IEnumerable<int> this[AdjectiveSetRelationship key] {
            get {
                return data.ContainsKey(key) ? data[key] : Enumerable.Empty<int>();
            }
        }

        public int Count {
            get {
                return data.Count;
            }
        }

        public bool Contains(AdjectiveSetRelationship key) {
            return data.ContainsKey(key);
        }

        public IEnumerator<IGrouping<AdjectiveSetRelationship, int>> GetEnumerator() {
            return GetGroupDataEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() {
            return GetGroupDataEnumerator();
        }
        private IEnumerator<IGrouping<AdjectiveSetRelationship, int>> GetGroupDataEnumerator() {
            groupData = groupData ?? from pair in data
                                     from value in pair.Value
                                     group value by pair.Key;
            return groupData.GetEnumerator();
        }

    }
    internal sealed class AdverbSetIDSymbolMap : ILookup<AdverbSetRelationship, int>
    {
        public AdverbSetIDSymbolMap(IEnumerable<KeyValuePair<AdverbSetRelationship, int>> relationData) {
            data = (from pair in relationData
                    group pair.Value by pair.Key into g
                    select new { K = g.Key, V = new HashSet<int>(g) })
                    .ToDictionary(pair => pair.K, pair => pair.V);
        }
        private IDictionary<AdverbSetRelationship, HashSet<int>> data;
        private IEnumerable<IGrouping<AdverbSetRelationship, int>> groupData;
        public IEnumerable<int> this[AdverbSetRelationship key] {
            get {
                return data.ContainsKey(key) ? data[key] : Enumerable.Empty<int>();
            }
        }

        public int Count {
            get {
                return data.Count;
            }
        }

        public bool Contains(AdverbSetRelationship key) {
            return data.ContainsKey(key);
        }

        public IEnumerator<IGrouping<AdverbSetRelationship, int>> GetEnumerator() {
            return GetGroupDataEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() {
            return GetGroupDataEnumerator();
        }
        private IEnumerator<IGrouping<AdverbSetRelationship, int>> GetGroupDataEnumerator() {
            groupData = groupData ?? from pair in data
                                     from value in pair.Value
                                     group value by pair.Key;
            return groupData.GetEnumerator();
        }

    }
}
