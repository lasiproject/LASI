using LASI.Algorithm.DocumentConstructs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LASI.Algorithm.Analysis.Binders
{
    public class ScopedAliasMap
    {
        public ScopedAliasMap(Paragraph paragraph)
            : this(paragraph.Words, paragraph.Phrases) {
        }

        private ScopedAliasMap(params IEnumerable<ILexical>[] scope) {
            domain = scope.SelectMany(s => s);
            foreach (var e in domain.GetEntities()) {
                assumedAliases[e.Text] = new HashSet<string>(Lookup.LexicalLookup.GetLikelyAliases(e));
            }
        }



        IEnumerable<ILexical> domain;
        IDictionary<string, IEnumerable<string>> assumedAliases = new Dictionary<string, IEnumerable<string>>();
        public IEnumerable<IEntity> this[IEntity key] {
            get {
                return domain.GetEntities().ToDictionary(e => e,
                    e => from aliasString in assumedAliases[e.Text]
                         from i in domain.GetEntities()
                         where i.Text == aliasString
                         select i)[key];
            }
        }
    }
}
