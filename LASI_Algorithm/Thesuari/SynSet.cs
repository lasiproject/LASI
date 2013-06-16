using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LASI.Algorithm.Thesuari;

namespace LASI.Algorithm.Thesauri
{
    interface IVariantLookup<TKey, out TElement, out TEnumerable> : IEnumerable<IGrouping<TKey, TElement>>, System.Collections.IEnumerable
        where TEnumerable : IEnumerable<TElement>, System.Collections.IEnumerable
        where TKey : struct
        where TElement : struct
    {
        TEnumerable this[TKey key] {
            get;
        }
        int Count {
            get;
        }
        bool Contains(TKey key);

    }
    public enum NounPointerSymbol
    {
        UNKNOWN = 0,
        Antonym,//!  
        Hypernym,//@  
        InstanceHypernym,//@i 
        Hyponym,//~ 
        InstanceHyponym,//~i
        Memberholonym,//#m 
        Substanceholonym,//#subject 
        Partholonym,//#p 
        Membermeronym,//%m 
        Substancemeronym,//%subject 
        Partmeronym,//%p 
        Attribute,//=  
        Derivationallyrelatedform,//+  
        Domainofsynset_TOPIC,//;c 
        Memberofthisdomain_TOPIC,//-c 
        Domainofsynset_REGION,//;r 
        Memberofthisdomain_REGION,//-r 
        Domainofsynset_USAGE,//;u 
        Memberofthisdomain_USAGE,//-u  
    }
    public enum VerbPointerSymbol
    {
        UNKNOWN = 0,
        Antonym,//!
        Hypernym,//@
        Hyponym,//~ 
        Entailment,//*
        Cause,//>
        Also_see,//^
        Verb_Group,//$    
        Derivationallyrelatedform,//+    
        Domainofsynset_TOPIC,//;c  
        Domainofsynset_REGION,//;r
        Domainofsynset_USAGE,//;u 
    }
    internal class NounPointerSymbolMap
    {
        public NounPointerSymbol this[string key] {
            get {
                NounPointerSymbol result;
                data.TryGetValue(key, out result);
                return result;
            }
        }
        private static readonly IReadOnlyDictionary<string, NounPointerSymbol> data = new Dictionary<string, NounPointerSymbol>{ 
            { "!", NounPointerSymbol.Antonym },
            { "@", NounPointerSymbol.Hypernym },
            { "@i", NounPointerSymbol.InstanceHypernym },
            { "~", NounPointerSymbol.Hyponym },
            { "~i", NounPointerSymbol.InstanceHyponym },
            { "#m", NounPointerSymbol.Memberholonym },
            { "#s", NounPointerSymbol.Substanceholonym },
            { "#p", NounPointerSymbol.Partholonym },
            { "%m", NounPointerSymbol.Membermeronym },
            { "%s", NounPointerSymbol.Substancemeronym },
            { "%p", NounPointerSymbol.Partmeronym },
            { "=", NounPointerSymbol.Attribute },
            { "+", NounPointerSymbol.Derivationallyrelatedform },
            { ";c", NounPointerSymbol.Domainofsynset_TOPIC },
            { "-c", NounPointerSymbol.Memberofthisdomain_TOPIC },
            { ";r", NounPointerSymbol.Domainofsynset_REGION },
            { "-r", NounPointerSymbol.Memberofthisdomain_REGION },
            { ";u", NounPointerSymbol.Domainofsynset_USAGE },
            { "-u", NounPointerSymbol.Memberofthisdomain_USAGE }
        };
    }
    internal class VerbPointerSymbolMap
    {
        public VerbPointerSymbol this[string key] {
            get {
                VerbPointerSymbol result;
                data.TryGetValue(key, out result);
                return result;
            }
        }
        private static readonly IReadOnlyDictionary<string, VerbPointerSymbol> data = new Dictionary<string, VerbPointerSymbol> {
            { "!", VerbPointerSymbol. Antonym },// !
            { "@", VerbPointerSymbol.Hypernym },// @
            { "~", VerbPointerSymbol.Hyponym },// ~ 
            { "*", VerbPointerSymbol.Entailment },// *
            { ">", VerbPointerSymbol.Cause },// >
            { "^", VerbPointerSymbol. Also_see },// ^
            { "$", VerbPointerSymbol.Verb_Group },// $ 
            { "+", VerbPointerSymbol.Derivationallyrelatedform },// +
            { ";c", VerbPointerSymbol.Domainofsynset_TOPIC },// ;c 
            { ";r", VerbPointerSymbol.Domainofsynset_REGION },// ;r
            { ";u", VerbPointerSymbol.Domainofsynset_USAGE}// ;u 
        };
    }

    public class VerbSetPointerRelationTable : IVariantLookup<VerbPointerSymbol, int, ISet<int>>
    {
        public VerbSetPointerRelationTable(IEnumerable<KeyValuePair<VerbPointerSymbol, int>> relationData) {
            data = new Dictionary<VerbPointerSymbol, HashSet<int>>();
        }

        private IDictionary<VerbPointerSymbol, HashSet<int>> data;
        private IEnumerable<IGrouping<VerbPointerSymbol, int>> groupData;
        public ISet<int> this[VerbPointerSymbol key] {
            get {
                return data[key];
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
    public class NounSetPointerRelationTable : IVariantLookup<NounPointerSymbol, int, ISet<int>>
    {
        public NounSetPointerRelationTable(IEnumerable<KeyValuePair<NounPointerSymbol, int>> relationData) {
            data = new Dictionary<NounPointerSymbol, HashSet<int>>();
        }

        private IDictionary<NounPointerSymbol, HashSet<int>> data;
        private IEnumerable<IGrouping<NounPointerSymbol, int>> groupData;



        public ISet<int> this[NounPointerSymbol key] {
            get {
                return data[key];
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

    public class NounSynSet
    {

        public WordNetNounCategory LexName {
            get;
            private set;
        }
        public NounSynSet(int ID, IEnumerable<string> words, IEnumerable<int> pointers, WordNetNounCategory lexCategory) {
            this.ID = ID;
            Words = new HashSet<string>(words);
            ReferencedIndexes = new HashSet<int>(pointers);
            LexName = lexCategory;

        }
        public NounSynSet(int ID, IEnumerable<string> words, IEnumerable<KeyValuePair<NounPointerSymbol, int>> pointerRelations, WordNetNounCategory lexCategory) {
            this.ID = ID;
            Words = new HashSet<string>(words);
            RelatedOnPointerSymbol = new NounSetPointerRelationTable(pointerRelations);
            ReferencedIndexes = new HashSet<int>(from pair in pointerRelations
                                                 select pair.Value);
            LexName = lexCategory;
        }

        public IEnumerable<int> this[NounPointerSymbol pointerSymbol] {
            get {
                return RelatedOnPointerSymbol[pointerSymbol];
            }
        }

        public NounSetPointerRelationTable RelatedOnPointerSymbol {
            get;
            set;
        }


        public HashSet<string> Words {
            get;
            private set;

        }

        public HashSet<int> ReferencedIndexes {
            get;
            private set;
        }

        public int ID {
            get;
            private set;
        }

    }
    public class VerbSynSet
    {
        public WordNetVerbCategory LexName {
            get;
            private set;
        }

        public VerbSynSet(int ID, IEnumerable<string> words, IEnumerable<KeyValuePair<VerbPointerSymbol, int>> pointerRelations, WordNetVerbCategory lexCategory) {
            this.ID = ID;
            Words = new HashSet<string>(words);
            RelatedOnPointerSymbol = new VerbSetPointerRelationTable(pointerRelations);
            ReferencedIndexes = new HashSet<int>(from pair in pointerRelations
                                                 select pair.Value);
            LexName = lexCategory;
        }

        public IEnumerable<int> this[VerbPointerSymbol pointerSymbol] {
            get {
                return RelatedOnPointerSymbol[pointerSymbol];
            }
        }

        public VerbSetPointerRelationTable RelatedOnPointerSymbol {
            get;
            set;
        }


        public HashSet<string> Words {
            get;
            private set;

        }

        public HashSet<int> ReferencedIndexes {
            get;
            private set;
        }

        public int ID {
            get;
            private set;
        }

    }
}