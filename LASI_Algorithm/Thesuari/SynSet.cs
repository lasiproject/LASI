using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LASI.Algorithm.Thesauri
{
    public enum PointerKind
    {
        UNKNOWN = 0,
        Antonym,//!  
        Hypernym,//@  
        InstanceHypernym,//@i 
        Hyponym,//~ 
        InstanceHyponym,//~i
        Memberholonym,//#m 
        Substanceholonym,//#s 
        Partholonym,//#p 
        Membermeronym,//%m 
        Substancemeronym,//%s 
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
    public class NounRelationshipTranslator
    {
        public PointerKind this[string key] {
            get {
                PointerKind result;
                data.TryGetValue(key, out result);
                return result;
            }
        }
        private static readonly Dictionary<string, PointerKind> data = new Dictionary<string, PointerKind>{ 
            { "!", PointerKind.Antonym },
            { "@", PointerKind.Hypernym },
            { "@i", PointerKind.InstanceHypernym },
            { "~", PointerKind.Hyponym },
            { "~i", PointerKind.InstanceHyponym },
            { "#m", PointerKind.Memberholonym },
            { "#s", PointerKind.Substanceholonym },
            { "#p", PointerKind.Partholonym },
            { "%m", PointerKind.Membermeronym },
            { "%s", PointerKind.Substancemeronym },
            { "%p", PointerKind.Partmeronym },
            { "=", PointerKind.Attribute },
            { "+", PointerKind.Derivationallyrelatedform },
            { ";c", PointerKind.Domainofsynset_TOPIC },
            { "-c", PointerKind.Memberofthisdomain_TOPIC },
            { ";r", PointerKind.Domainofsynset_REGION },
            { "-r", PointerKind.Memberofthisdomain_REGION },
            { ";u", PointerKind.Domainofsynset_USAGE },
            { "-u", PointerKind.Memberofthisdomain_USAGE }
        };
    }

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
    public class PointerRelationMap : IVariantLookup<PointerKind, int, ISet<int>>
    {
        public PointerRelationMap(IEnumerable<KeyValuePair<PointerKind, int>> relationData) {
            data = new Dictionary<PointerKind, HashSet<int>>();
        }

        private IDictionary<PointerKind, HashSet<int>> data;
        private IEnumerable<IGrouping<PointerKind, int>> groupData;



        public ISet<int> this[PointerKind key] {
            get {
                return data[key];
            }
        }

        public int Count {
            get {
                return data.Count;
            }
        }

        public bool Contains(PointerKind key) {
            return data.ContainsKey(key);
        }

        public IEnumerator<IGrouping<PointerKind, int>> GetEnumerator() {
            groupData = groupData ?? from pair in data
                                     from value in pair.Value
                                     group value by pair.Key;
            return groupData.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() {
            return this.GetEnumerator();
        }
    }

    public class SynSet
    {



        //Aluan: I added this field to store some additional information I found in the WordNet files



        //Aluan: I added this Property to access some additional information I found in the WordNet files

        public WordNetNounCategory LexName {
            get;
            private set;
        }

        //Aluan: I added this constructor to include some additional information I found in the WordNet files

        public SynSet(int ID, IEnumerable<string> words, IEnumerable<int> pointers, WordNetNounCategory lexCategory) {
            this.ID = ID;
            Words = new HashSet<string>(words);
            Pointers = new HashSet<int>(pointers);
            LexName = lexCategory;

        }
        public SynSet(int ID, IEnumerable<string> words, IEnumerable<KeyValuePair<PointerKind, int>> pointerRelations, WordNetNounCategory lexCategory) {
            this.ID = ID;
            Words = new HashSet<string>(words);
            ReferencesByRelationship = new PointerRelationMap(pointerRelations);
            Pointers = new HashSet<int>(from pair in pointerRelations
                                        select pair.Value);
            LexName = lexCategory;
        }

        public IEnumerable<int> this[PointerKind pointerType] {
            get {
                return ReferencesByRelationship[pointerType];
            }
        }

        public PointerRelationMap ReferencesByRelationship {
            get;
            private set;
        }


        public HashSet<string> Words {
            get;
            private set;

        }

        public HashSet<int> Pointers {
            get;
            private set;
        }

        public int ID {
            get;
            private set;
        }

    }
}
