using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LASI.Algorithm.Thesauri;
using LASI.Utilities;

namespace LASI.Algorithm.Thesauri
{

    /// <summary>
    /// Represents a synset parsed from the data.noun file of the WordNet package. Each line in the file represents a grouping known as a synset.
    /// </summary>
    public class NounSynSet
    {


        public NounSynSet(int ID, IEnumerable<string> words, IEnumerable<KeyValuePair<NounPointerSymbol, int>> pointerRelations, WordNetNounCategory lexCategory) {
            this.ID = ID;
            Words = new HashSet<string>(words);
            relatedOnPointerSymbol = new NounSetIDSymbolMap(pointerRelations);
            ReferencedIndexes = new HashSet<int>(from pair in pointerRelations
                                                 select pair.Value);
            LexName = lexCategory;
        }
        /// <summary>
        /// Returns a single string representing the members of the NounSynSet.
        /// </summary>
        /// <returns>A single string representing the members of the NounSynSet.</returns>
        public override string ToString() {
            return "[" + ID + "] " + Words.Aggregate("", (str, code) => {
                return str + "  " + code;
            });
        }
        public override int GetHashCode() {
            return ID;
        }
        public override bool Equals(object obj) {
            return this == obj as NounSynSet;
        }

        public WordNetNounCategory LexName {
            get;
            private set;
        }
        public IEnumerable<int> this[NounPointerSymbol pointerSymbol] {
            get {
                return relatedOnPointerSymbol[pointerSymbol];
            }
        }

        private NounSetIDSymbolMap relatedOnPointerSymbol;


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

        public static bool operator ==(NounSynSet lhs, NounSynSet rhs) {
            if (ReferenceEquals(lhs, null))
                return ReferenceEquals(rhs, null);
            if (ReferenceEquals(rhs, null))
                return ReferenceEquals(lhs, null);
            return lhs.ID == rhs.ID;
        }
        public static bool operator !=(NounSynSet lhs, NounSynSet rhs) {
            return !(lhs == rhs);
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
            RelatedOnPointerSymbol = new VerbSetIDSymbolMap(pointerRelations);
            ReferencedIndexes = new HashSet<int>(from pair in pointerRelations
                                                 select pair.Value);
            LexName = lexCategory;
        }
        /// <summary>
        /// Returns a single string representing the members of the VerbThesaurusSynSet.
        /// </summary>
        /// <returns>A single string representing the members of the VerbThesaurusSynSet.</returns>
        public override string ToString() {
            return "[" + ID + "] " + Words.Aggregate("", (str, code) => {
                return str + "  " + code;
            });
        }
        public override int GetHashCode() {
            return ID;
        }
        public override bool Equals(object obj) {
            return this == obj as VerbSynSet;
        }
        public static bool operator ==(VerbSynSet lhs, VerbSynSet rhs) {
            if (ReferenceEquals(lhs, null))
                return ReferenceEquals(rhs, null);
            if (ReferenceEquals(rhs, null))
                return ReferenceEquals(lhs, null);
            return lhs.ID == rhs.ID;
        }
        public static bool operator !=(VerbSynSet lhs, VerbSynSet rhs) {
            return !(lhs == rhs);
        }
        public IEnumerable<int> this[VerbPointerSymbol pointerSymbol] {
            get {
                return RelatedOnPointerSymbol[pointerSymbol];
            }
        }

        internal VerbSetIDSymbolMap RelatedOnPointerSymbol {
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
    /// <summary>
    /// Represents a synset parsed from the data.noun file of the WordNet package. Each line in the file represents a grouping known as a synset.
    /// </summary>
    public class AdjectiveSynSet
    {


        public AdjectiveSynSet(int ID, IEnumerable<string> words, IEnumerable<KeyValuePair<AdjectivePointerSymbol, int>> pointerRelations, WordNetAdjectiveCategory lexCategory) {
            this.ID = ID;
            Words = new HashSet<string>(words);
            relatedOnPointerSymbol = new AdjectiveSetIDSymbolMap(pointerRelations);
            ReferencedIndexes = new HashSet<int>(from pair in pointerRelations
                                                 select pair.Value);
            LexName = lexCategory;
        }
        /// <summary>
        /// Returns a single string representing the members of the NounSynSet.
        /// </summary>
        /// <returns>A single string representing the members of the NounSynSet.</returns>
        public override string ToString() {
            return "[" + ID + "] " + Words.Aggregate("", (str, code) => {
                return str + "  " + code;
            });
        }
        public override int GetHashCode() {
            return ID;
        }
        public override bool Equals(object obj) {
            return this == obj as AdjectiveSynSet;
        }

        public WordNetAdjectiveCategory LexName {
            get;
            private set;
        }
        public IEnumerable<int> this[AdjectivePointerSymbol pointerSymbol] {
            get {
                return relatedOnPointerSymbol[pointerSymbol];
            }
        }

        private AdjectiveSetIDSymbolMap relatedOnPointerSymbol;


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

        public static bool operator ==(AdjectiveSynSet lhs, AdjectiveSynSet rhs) {
            if (ReferenceEquals(lhs, null))
                return ReferenceEquals(rhs, null);
            if (ReferenceEquals(rhs, null))
                return ReferenceEquals(lhs, null);
            return lhs.ID == rhs.ID;
        }
        public static bool operator !=(AdjectiveSynSet lhs, AdjectiveSynSet rhs) {
            return !(lhs == rhs);
        }


    }
    public class AdverbSynSet
    {


        public AdverbSynSet(int ID, IEnumerable<string> words, IEnumerable<KeyValuePair<AdverbPointerSymbol, int>> pointerRelations, WordNetAdverbCategory lexCategory) {
            this.ID = ID;
            Words = new HashSet<string>(words);
            relatedOnPointerSymbol = new AdverbSetIDSymbolMap(pointerRelations);
            ReferencedIndexes = new HashSet<int>(from pair in pointerRelations
                                                 select pair.Value);
            LexName = lexCategory;
        }
        /// <summary>
        /// Returns a single string representing the members of the NounSynSet.
        /// </summary>
        /// <returns>A single string representing the members of the NounSynSet.</returns>
        public override string ToString() {
            return "[" + ID + "] " + Words.Aggregate("", (str, code) => {
                return str + "  " + code;
            });
        }
        public override int GetHashCode() {
            return ID;
        }
        public override bool Equals(object obj) {
            return this == obj as AdverbSynSet;
        }

        public WordNetAdverbCategory LexName {
            get;
            private set;
        }
        public IEnumerable<int> this[AdverbPointerSymbol pointerSymbol] {
            get {
                return relatedOnPointerSymbol[pointerSymbol];
            }
        }

        private AdverbSetIDSymbolMap relatedOnPointerSymbol;


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

        public static bool operator ==(AdverbSynSet lhs, AdverbSynSet rhs) {
            if (ReferenceEquals(lhs, null))
                return ReferenceEquals(rhs, null);
            if (ReferenceEquals(rhs, null))
                return ReferenceEquals(lhs, null);
            return lhs.ID == rhs.ID;
        }
        public static bool operator !=(AdverbSynSet lhs, AdverbSynSet rhs) {
            return !(lhs == rhs);
        }


    }

}