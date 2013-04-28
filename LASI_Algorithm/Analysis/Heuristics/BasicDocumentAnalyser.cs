//using LASI.Algorithm.Heuristics;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace LASI.Algorithm.Heuristics
//{
//    public class BasicDocumentAnalyser : IDocumentAnalyzer
//    {
//        public BasicDocumentAnalyser(IList<ILexical> document, int maxResults) {
//            SourceLexicals = document;
//            MaxResults = maxResults;
//        }

//        public virtual ResultSet Analyse() {
//            return DetermineTopVerbials(VerbComparisonFlags.CompareModality | VerbComparisonFlags.CompareTense);
//        }

//        public virtual ResultSet DetermineTopVerbials(VerbComparisonFlags verbFlags) {
//            var temp = from entity in SourceLexicals.OfType<Phrase>().GetNounPhrases().InSubjectRole()
//                       group entity by entity.SubjectOf into knownSubjects
//                       orderby knownSubjects.Count() descending
//                       select knownSubjects into bySubjectsTopVerbials
//                       from subject in bySubjectsTopVerbials
//                       join dirObject in
//                           from entity in SourceLexicals.OfType<Phrase>().GetNounPhrases().InDirectObjectRole()
//                           group entity
//                             by entity.DirectObjectOf.Text
//                               into knownObjects
//                               orderby knownObjects.Count() descending
//                               select knownObjects into dirObjectsByTopVerbials
//                               from objectsByVerbials in dirObjectsByTopVerbials
//                               select objectsByVerbials
//                           on subject.SubjectOf equals dirObject.DirectObjectOf
//                       select new {
//                           subject = subject,
//                           verb = subject.SubjectOf,
//                           directObject = dirObject
//                       };
//            return new ResultSet {
//                TopActions = from vP in temp
//                             select vP.verb,
//                TopEntities = from tmp in temp
//                              from t in new[] { tmp.subject, tmp.directObject }
//                              select t,
//            };

//        }

//        public virtual async Task<ResultSet> AnalyseAsync() {
//            return await Task.Run(() => Analyse());
//        }

//        public int MaxResults {
//            get;
//            protected set;
//        }

//        public IEnumerable<ILexical> SourceLexicals {
//            get;
//            protected set;
//        }

//    }
//    [Flags]
//    public enum VerbComparisonFlags
//    {
//        CompareTense = 1,
//        CompareModality = 2,
//        CompareAdverbials = 4
//    }
//}