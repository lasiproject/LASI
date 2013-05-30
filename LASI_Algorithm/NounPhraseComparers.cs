using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LASI.Algorithm.Thesauri;

namespace LASI.Algorithm
{
    public static class NounPhraseComparers
    {
        private static Textual textual = new Textual();
        private static Similarity similarity = new Similarity();
        private static Alias alias = new Alias();
        private static AliasOrSimilarity aliasOrSimilarity = new AliasOrSimilarity();

        ///// <summary>
        ///// Alias based comparer where if not textually equivalent if will check to see if the NounPhrases are aliases for each
        ///// </summary>
        public static Alias Alias {
            get {
                return alias;
            }
        }
        /// <summary>
        /// Comparer which checks for textual, alias, or similarity based Equivalence.
        /// </summary>
        public static AliasOrSimilarity AliasOrSimilarity {
            get {
                return aliasOrSimilarity;
            }
        }
        /// <summary>
        /// Similarity based comparer where if not textually equivalent if will check to see if the NounPhrases are similar tp eachother.
        /// </summary>
        public static Similarity Similarity {
            get {
                return similarity;
            }
        }
        /// <summary>
        /// Text based comparer which compares the literal text of two NounPhrases to see if they are Identical.
        /// All other comparers provided here perform literal text comparions implicitely.
        /// </summary>
        public static Textual Textual {
            get {
                return textual;
            }
        }
    }
    public class Textual : IEqualityComparer<NounPhrase>
    {
        protected internal Textual() {
        }
        public bool Equals(NounPhrase x, NounPhrase y) {
            return x.Text == y.Text;
        }

        public int GetHashCode(NounPhrase obj) {
            return 1;
        }
    }
    public class Similarity : IEqualityComparer<NounPhrase>
    {
        protected internal Similarity() {
        }
        public bool Equals(NounPhrase x, NounPhrase y) {
            return x.Text == y.Text || x.IsSimilarTo(y);
        }

        public int GetHashCode(NounPhrase obj) {
            return 1;
        }
    }
    public class Alias : IEqualityComparer<NounPhrase>
    {
        protected internal Alias() {
        }
        public bool Equals(NounPhrase x, NounPhrase y) {
            return x.Text == y.Text || x.IsAliasFor(y);
        }

        public int GetHashCode(NounPhrase obj) {
            return 1;
        }
    }
    public class AliasOrSimilarity : IEqualityComparer<NounPhrase>
    {
        protected internal AliasOrSimilarity() {
        }
        public bool Equals(NounPhrase x, NounPhrase y) {
            return x.Text == y.Text || x.IsAliasFor(y) || x.IsSimilarTo(y);
        }

        public int GetHashCode(NounPhrase obj) {
            return 1;
        }
    }

}
