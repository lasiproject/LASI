using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using System.Web.Mvc;
using System.Web.Security;
using DotNetOpenAuth.AspNet;
using Microsoft.Web.WebPages.OAuth;
using WebMatrix.WebData;
using MvcExperimentation.Filters;
using MvcExperimentation.Models;
using LASI.Core;
using System.Web.UI.WebControls;
using System.Drawing;
using LASI.Core.Patternization;

namespace MvcExperimentation.LexicalElementStyling
{
    public class SyntacticStyleMapping : LASI.Interop.ISyntacticColorizer<ILexical, Style>
    {
        /// <summary>
        /// Maps a Lexical element to a CSS Style specification based on its syntactic nature. The returned value is a System.Windows.Media.Brush enumeration member.
        /// </summary>
        /// <param name="syntacticElement">The Lexical for which to get a color based on its syntactic role.</param>
        /// <returns>A System.Windows.Media.Brush enumeration value mapped to the syntactic role of the element.</returns>
        public Style this[ILexical syntacticElement] {
            get {
                return syntacticElement.Match().Yield<Style>()
                  .With<Phrase>(p => p.Match().Yield<Style>()
                          .With<PronounPhrase>(new Style { CssClass = "PronounPhrase", ForeColor = Color.HotPink })
                          .When<NounPhrase>(n => n.Words.OfProperNoun().Any())
                          .Then(new Style { CssClass = "NounPhrase", ForeColor = Color.DarkBlue })
                          .With<NounPhrase>(new Style { CssClass = "NounPhrase", ForeColor = Color.MediumTurquoise })
                          .With<InfinitivePhrase>(new Style { CssClass = "InfinitivePhrase", ForeColor = Color.Teal })
                          .With<IReferencer>(new Style { CssClass = "Referencer", ForeColor = Color.DarkCyan })
                          .With<IEntity>(new Style { CssClass = "Entity", ForeColor = Color.DeepSkyBlue })
                          .With<IVerbal>(new Style { CssClass = "Verbal", ForeColor = Color.Green })
                          .With<IPrepositional>(new Style { CssClass = "Prepositional", ForeColor = Color.DarkOrange })
                          .With<IDescriptor>(new Style { CssClass = "Descriptor", ForeColor = Color.Indigo })
                          .With<IAdverbial>(new Style { CssClass = "Adverbial", ForeColor = Color.Orange })
                      .Result(new Style { CssClass = "LexicalDefaultStyle", ForeColor = Color.Black }))
                  .With<Word>(w => w.Match().Yield<Style>()
                          .With<Adjective>(new Style { CssClass = "Adjective", ForeColor = Color.Indigo })
                          .With<PresentParticipleGerund>(new Style { CssClass = "PresentParticipleGerund", ForeColor = Color.DarkGreen })
                          .With<Verb>(new Style { CssClass = "Verb", ForeColor = Color.Green })
                      .Result(new Style { CssClass = "LexicalDefaultStyle", ForeColor = Color.Black }))
                  .Result();
            }
        }
    }


}
