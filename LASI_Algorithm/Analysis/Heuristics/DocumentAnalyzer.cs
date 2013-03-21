using LASI.Algorithm.Heuristics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LASI.Algorithm
{
    public class BasicDocumentAnalyzer : IDocumentAnalyzer
    {
        public BasicDocumentAnalyzer(Document document, int maxResults) {
            SourceDocument = document;
            MaxResults = maxResults;
        }

        public virtual ResultSet Analyse() {
            ResultSet temp =new ResultSet{TopActions= byActionsPerformed = from phrase in SourceDocument.Phrases.GetNounPhrases().InSubjectRole()
                                                  group phrase by phrase.SubjectOf into actionSubjectGroup
                                                  orderby actionSubjectGroup.Count() descending
                                                  select actionSubjectGroup into SG
                                                    from phrase in SourceDocument.Phrases.GetNounPhrases().InDirectObjectRole()
                                    group phrase by phrase.DirectObjectOf into dirObjectGroup
                                    orderby dirObjectGroup.Count() descending
                                    select dirObjectGroup into OB
                                    from performers in OB
                               join receivers in   performers.Key.Text equals receivers.Key.Text
                               select new {
                                   performed,
                                   received
                               } into commonCrossReferenced
                               orderby commonCrossReferenced.performed.Count() descending
                               select new {
                                   TopSubject = commonCrossReferenced.performed,
                                   TopAction = commonCrossReferenced.performed.Key,
                                   TopDirObject = commonCrossReferenced.received
                               }


            throw new NotImplementedException();
        }

        public virtual async Task<ResultSet> AnalyseAsync() {
            return await Task.Run(() => Analyse());
        }

        public int MaxResults {
            get;
            protected set;
        }

        public Document SourceDocument {
            get;
            protected set;
        }

    }
}