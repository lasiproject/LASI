//using LASI.Utilities.TypedSwitch;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using LASI.Algorithm.Analysis.Heuristics;

//namespace LASI.Algorithm.Heuristics
//{
//    class BasicWeightApplyer : IDocumentAnalyzer
//    {

//        public ResultSet Analyse() {
//            SourceLexicals.AsParallel().ForAll((ILexical l) => {

//                new Switch(l)
//                    .Case<Word>(w => {
//                        w.Weight = 10;
//                    })
//                    .Case<Phrase>(p => {
//                        p.Weight = 27;
//                    })
//                    .Default<ILexical>(unknown => {
//                        throw new UnknownLexicalConstructException(unknown.Text);
//                    });

//            });

//            var topActions = from l in SourceLexicals
//                             where l is ITransitiveVerbal
//                             orderby l.Weight
//                             select l as ITransitiveVerbal;

//            var topEntities = from l in SourceLexicals
//                              where l is IEntity
//                              orderby l.Weight
//                              select l as IEntity;
//            return new ResultSet {
//                TopActions = topActions.Take(MaxResults),
//                TopEntities = topEntities.Take(MaxResults)
//            };
//        }

//        public Task<ResultSet> AnalyseAsync() {
//            throw new NotImplementedException();
//        }

//        public int MaxResults {
//            get {
//                throw new NotImplementedException();
//            }
//        }

//        public IEnumerable<ILexical> SourceLexicals {
//            get {
//                throw new NotImplementedException();
//            }
//        }
//    }
//}
