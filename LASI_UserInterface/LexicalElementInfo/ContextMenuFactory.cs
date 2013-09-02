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
    static class ContextMenuFactory
    {
        #region Label Context Menu Construction

        public static ContextMenu MakeVerbalContextMenu(IEnumerable<Label> labelsInContext, IVerbal verbal) {
            var result = new ContextMenu();
            if (verbal.Subjects.Any()) {
                result.Items.Add(CreateVerbalSubjectMenuItem(labelsInContext, verbal));
            }
            if (verbal.DirectObjects.Any()) {
                result.Items.Add(CreateVerbalDirectObjectMenuItem(labelsInContext, verbal));
            }
            if (verbal.IndirectObjects.Any()) {
                result.Items.Add(CreateVerbalIndirectObjectMenuItem(labelsInContext, verbal));
            }
            if (verbal != null && verbal.ObjectOfThePreoposition != null) {
                result.Items.Add(CreateVerbalPrepositionalObjectMenuItem(labelsInContext, verbal));
            }
            return result;
        }

        private static MenuItem CreateVerbalPrepositionalObjectMenuItem(IEnumerable<Label> labelsInContext, IVerbal verbal) {
            var visitSubjectMI = new MenuItem { Header = "view prepositional object" };
            visitSubjectMI.Click += (sender, e) => {
                var objlabels = from label in labelsInContext
                                where label.Tag.Equals(verbal.ObjectOfThePreoposition)
                                select new { label = label, foreBrush = (label.Tag as ILexical).GetBrush() };
                foreach (var l in objlabels) {
                    l.label.Foreground = l.foreBrush;
                    l.label.Background = Brushes.Red;
                }
            };
            return visitSubjectMI;
        }

        private static MenuItem CreateVerbalIndirectObjectMenuItem(IEnumerable<Label> labelsInContext, IVerbal verbal) {
            var visitSubjectMI = new MenuItem { Header = "view indirect objects" };
            visitSubjectMI.Click += (sender, e) => {
                var objlabels = from r in verbal.IndirectObjects
                                join label in labelsInContext on r equals label.Tag
                                select new { label, foreBrush = (label.Tag as ILexical).GetBrush() };
                foreach (var l in objlabels) {
                    l.label.Foreground = l.foreBrush;
                    l.label.Background = Brushes.Red;
                }
            };
            return visitSubjectMI;
        }

        private static MenuItem CreateVerbalDirectObjectMenuItem(IEnumerable<Label> labelsInContext, IVerbal verbal) {
            var visitSubjectMI = new MenuItem { Header = "view direct objects" };
            visitSubjectMI.Click += (sender, e) => {
                var objlabels = from r in verbal.DirectObjects
                                join l in labelsInContext on r equals l.Tag
                                select l;
                foreach (var l in objlabels) {
                    l.Foreground = Brushes.Black;
                    l.Background = Brushes.Red;
                }
            };
            return visitSubjectMI;
        }

        private static MenuItem CreateVerbalSubjectMenuItem(IEnumerable<Label> labelsInContext, IVerbal verbal) {
            var visitSubjectMI = new MenuItem { Header = "view subjects" };
            visitSubjectMI.Click += (sender, e) => {
                var objlabels = from r in verbal.Subjects
                                join l in labelsInContext on r equals l.Tag
                                select l;
                foreach (var l in objlabels) {
                    l.Foreground = Brushes.Black;
                    l.Background = Brushes.Red;
                }
            };
            return visitSubjectMI;
        }
        public static ContextMenu MakePronounContextMenu(IEnumerable<Label> labelsInContext, IPronoun pro) {
            var result = new ContextMenu();
            if (pro.RefersTo != null && pro.RefersTo.Any()) {
                result.Items.Add(CreatePronounReferredToMenuItem(labelsInContext, pro));
            }
            return result;
        }
        private static MenuItem CreatePronounReferredToMenuItem(IEnumerable<Label> labelsInContext, IPronoun pro) {
            var visitBoundEntity = new MenuItem { Header = "view referred to" };
            visitBoundEntity.Click += (sender, e) => {
                var objlabels = from l in labelsInContext
                                where l.Tag == pro.RefersTo
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
