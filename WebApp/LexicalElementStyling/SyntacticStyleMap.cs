using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;
using DotNetOpenAuth.AspNet;
using Microsoft.Web.WebPages.OAuth;
using WebMatrix.WebData;
using LASI.Core;
using System.Web.UI.WebControls;
using System.Drawing;
using LASI.Core.PatternMatching;

namespace LASI.WebApp
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
                          .With<PronounPhrase>(new Style { CssClass = "referencer" })
                          .When<NounPhrase>(n => n.Words.OfProperNoun().Any())
                          .Then(new Style { CssClass = "nounphrase proper" })
                          .With<NounPhrase>(new Style { CssClass = "nounphrase", })
                          .With<InfinitivePhrase>(new Style { CssClass = "infinitive", })
                          .With<IReferencer>(new Style { CssClass = "referencer", })
                          .With<IEntity>(new Style { CssClass = "entity", })
                          .With<IVerbal>(new Style { CssClass = "verbal", })
                          .With<IPrepositional>(new Style { CssClass = "prepositional", })
                          .With<IDescriptor>(new Style { CssClass = "descriptor", })
                          .With<IAdverbial>(new Style { CssClass = "adverbial", })
                          .With<IConjunctive>(new Style { CssClass = "conjunctive", })
                        .Result(new Style { CssClass = "lexical-default-style", }))
                    .With<Word>(w => w.Match().Yield<Style>()
                          .With<Adjective>(new Style { CssClass = "adjective", })
                          .With<PresentParticipleGerund>(new Style { CssClass = "presentparticiplegerund", })
                          .With<Verb>(new Style { CssClass = "verbal", }).With<IConjunctive>(new Style { CssClass = "conjunctive", })
                    .Result(new Style { CssClass = "lexical-default-style", }))
                .Result();
            }
        }
    }


}
