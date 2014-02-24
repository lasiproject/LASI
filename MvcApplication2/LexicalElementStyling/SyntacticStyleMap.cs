using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using System.Web.Mvc;
using System.Web.Security;
using DotNetOpenAuth.AspNet;
using Microsoft.Web.WebPages.OAuth;
using WebMatrix.WebData;
using LASI.Core;
using System.Web.UI.WebControls;
using System.Drawing;
using LASI.Core.Patternization;

namespace LASI.WebService
{
    public class SyntacticStyleMap : LASI.Interop.ISyntacticColorizer<ILexical, Style>
    {
        /// <summary>
        /// Maps a Lexical element to a CSS Style specification based on its syntactic nature. The returned value is a System.Windows.Media.Brush enumeration member.
        /// </summary>
        /// <param name="syntacticElement">The Lexical for which to get a color based on its syntactic role.</param>
        /// <returns>A System.Windows.Media.Brush enumeration value mapped to the syntactic role of the element.</returns>
        public Style this[ILexical syntacticElement] {
            get {
                return syntacticElement.Match().Yield<Style>()
                  .With<Phrase>(px => px.Match().Yield<Style>()
                          .With<PronounPhrase>(new Style { CssClass = "Referencer" })
                          .When<NounPhrase>(n => n.Words.OfProperNoun().Any())
                          .Then(new Style { CssClass = "NounPhrase Proper" })
                          .With<NounPhrase>(new Style { CssClass = "NounPhrase", })
                          .With<InfinitivePhrase>(new Style { CssClass = "Infinitive", })
                          .With<IReferencer>(new Style { CssClass = "Referencer", })
                          .With<IEntity>(new Style { CssClass = "Entity", })
                          .With<IVerbal>(new Style { CssClass = "Verbal", })
                          .With<IPrepositional>(new Style { CssClass = "Prepositional", })
                          .With<IDescriptor>(new Style { CssClass = "Descriptor", })
                          .With<IAdverbial>(new Style { CssClass = "Adverbial", })
                          .With<IConjunctive>(new Style { CssClass = "Conjunctive", })
.Result(new Style { CssClass = "LexicalDefaultStyle", }))
                  .With<Word>(w => w.Match().Yield<Style>()
                          .With<Adjective>(new Style { CssClass = "Adjective", })
                          .With<PresentParticipleGerund>(new Style { CssClass = "PresentParticipleGerund", })
                          .With<Verb>(new Style { CssClass = "Verbal", }).With<IConjunctive>(new Style { CssClass = "Conjunctive", })
                      .Result(new Style { CssClass = "LexicalDefaultStyle", }))
                  .Result();
            }
        }
    }


}
