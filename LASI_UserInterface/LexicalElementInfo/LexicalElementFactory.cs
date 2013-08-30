using LASI.Algorithm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;

namespace LASI.UserInterface.LexicalElementInfo
{
    static class LexicalElementFactory
    {
        #region Label Context Menu Construction

        public static void CreateVerbPhraseLabelMenu(List<Label> phraseLabels, Label phraseLabel, VerbPhrase vP) {
            if (vP.Subjects.Any()) { phraseLabel.ContextMenu.Items.Add(CreateLabelSubjectMenu(phraseLabels, vP)); }
            if (vP.DirectObjects.Any()) { phraseLabel.ContextMenu.Items.Add(CreateLabelDirectObjectMenu(phraseLabels, vP)); }
            if (vP.IndirectObjects.Any()) { phraseLabel.ContextMenu.Items.Add(CreateLabelIndirectObjectMenu(phraseLabels, vP)); }
            if (vP != null && vP.ObjectOfThePreoposition != null) { phraseLabel.ContextMenu.Items.Add(CreateLabelPrepositionalObjectMenu(phraseLabels, vP)); }
        }

        public static MenuItem CreateLabelPrepositionalObjectMenu(List<Label> phraseLabels, VerbPhrase vP) {
            var visitSubjectMI = new MenuItem { Header = "view prepositional object" };
            visitSubjectMI.Click += (sender, e) => {
                var objlabels = from label in phraseLabels
                                where label.Tag.Equals(vP.ObjectOfThePreoposition)
                                select new { label = label, foreBrush = (label.Tag as ILexical).GetBrush() };
                foreach (var l in objlabels) {
                    l.label.Foreground = l.foreBrush;
                    l.label.Background = Brushes.Red;
                }
            };
            return visitSubjectMI;
        }

        public static MenuItem CreateLabelIndirectObjectMenu(List<Label> phraseLabels, VerbPhrase vP) {
            var visitSubjectMI = new MenuItem { Header = "view indirect objects" };
            visitSubjectMI.Click += (sender, e) => {
                var objlabels = from r in vP.IndirectObjects
                                join label in phraseLabels on r equals label.Tag
                                select new { label, foreBrush = (label.Tag as ILexical).GetBrush() };
                foreach (var l in objlabels) {
                    l.label.Foreground = l.foreBrush;
                    l.label.Background = Brushes.Red;
                }
            };
            return visitSubjectMI;
        }

        public static MenuItem CreateLabelDirectObjectMenu(List<Label> phraseLabels, VerbPhrase vP) {
            var visitSubjectMI = new MenuItem { Header = "view direct objects" };
            visitSubjectMI.Click += (sender, e) => {
                var objlabels = from r in vP.DirectObjects
                                join l in phraseLabels on r equals l.Tag
                                select l;
                foreach (var l in objlabels) {
                    l.Foreground = Brushes.Black;
                    l.Background = Brushes.Red;
                }
            };
            return visitSubjectMI;
        }

        public static MenuItem CreateLabelSubjectMenu(List<Label> phraseLabels, VerbPhrase vP) {
            var visitSubjectMI = new MenuItem { Header = "view subjects" };
            visitSubjectMI.Click += (sender, e) => {
                var objlabels = from r in vP.Subjects
                                join l in phraseLabels on r equals l.Tag
                                select l;
                foreach (var l in objlabels) {
                    l.Foreground = Brushes.Black;
                    l.Background = Brushes.Red;
                }
            };
            return visitSubjectMI;
        }

        public static MenuItem CreatePronounPhraseLabelMenu(List<Label> phraseLabels, PronounPhrase pronounPhrase) {
            var visitBoundEntity = new MenuItem { Header = "view referred to" };
            visitBoundEntity.Click += (sender, e) => {
                var objlabels = from l in phraseLabels
                                where l.Tag == pronounPhrase.RefersTo
                                select l;
                foreach (var l in objlabels) {
                    l.Foreground = Brushes.White;
                    l.Background = Brushes.Black;
                }
            };
            return visitBoundEntity;
        }

        #endregion

    }
}
