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
                return new Style {
                    CssClass =
                    syntacticElement.Match().Yield<string>()
                    .With((Phrase px) => px.Match().Yield<string>()
                        .With((PronounPhrase p) => "referencer")
                        .With((NounPhrase n) => n.Words.OfProperNoun().Any() ? "nounphrase proper" : "nounphrase")
                        .With((InfinitivePhrase i) => "infinitive")
                        .With((IReferencer r) => "referencer")
                        .With((IEntity e) => "entity")
                        .With((IVerbal v) => "verbal")
                        .With((IPrepositional p) => "prepositional")
                        .With((IDescriptor p) => "descriptor")
                        .With((IAdverbial a) => "adverbial")
                        .With((IConjunctive c) => "conjunctive").Result())
                   .With((Word w) => w.Match().Yield<string>()
                         .With((Adjective p) => "adjective")
                         .With((PresentParticipleGerund p) => "presentparticiplegerund")
                         .With((Verb p) => "verbal")
                         .With((IConjunctive p) => "conjunctive").Result())
                    .Result("lexical-default-style")
                };




            }
        }
    }
}