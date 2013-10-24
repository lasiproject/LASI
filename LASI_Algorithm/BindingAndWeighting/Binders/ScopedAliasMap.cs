using LASI.Algorithm.DocumentStructures;
using LASI.Algorithm.ComparativeHeuristics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LASI.Algorithm.Binding
{
    /// <summary>
    /// Represents an alias Mapping between IEntity instances within a constrained scope.
    /// </summary>
    public class ScopedAliasMap
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the ScopedAliasMap class, its scope constrained by the provided Paragraph.
        /// </summary>
        /// <param name="paragraph"></param>
        private ScopedAliasMap(Paragraph paragraph)
            : this(paragraph.Words, paragraph.Phrases) {
        }

        private ScopedAliasMap(params IEnumerable<ILexical>[] scope) {
            domain = scope.SelectMany(s => s);
            foreach (var e in domain.OfEntity()) {
                assumedAliases[e.Text] = new HashSet<string>(AliasLookup.GetLikelyAliases(e));
            }
        }

        #endregion

        #region Properties and Indexers

        /// <summary>
        /// Returns the IEntity instances which have been so far identified as aliases of the indexing IEntity instance within the lexical scope of the ScopedAliasMap.
        /// </summary>
        /// <param name="key">The IEntity instance whose aliases will be retrieved.</param>
        /// <returns>The IEntity instances which have been so far identified as aliases of the indexing IEntity instance within the lexical scope of the ScopedAliasMap.</returns>
        public IEnumerable<IEntity> this[IEntity key] {
            get {
                return domain.OfEntity().ToDictionary(e => e,
                    e => from aliasString in assumedAliases[e.Text]
                         from i in domain.OfEntity()
                         where i.Text == aliasString
                         select i)[key];
            }
        }

        #endregion

        #region Fields
        IEnumerable<ILexical> domain;
        IDictionary<string, IEnumerable<string>> assumedAliases = new Dictionary<string, IEnumerable<string>>();
        #endregion
    }
}
