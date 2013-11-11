using LASI.Core;
using LASI.Core.Patternization;
using LASI.Utilities;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Media;

namespace LASI.App.LexicalElementInfo
{
    static class ContextMenuFactory
    {
        public static ContextMenu ForLexical(ILexical element, IEnumerable<Label> labelsInContext) {
            return element.Match().Yield<ContextMenu>()
                ._<IVerbal>(e => ForVerbal(e, labelsInContext))
                ._<IReferencer>(e => ForReferencer(e, labelsInContext))
                .Result();
        }
        #region Lexical Element Context Menu Construction
        /// <summary>
        /// Creates a context menu specific to the given IVerbal elemenet with context determined by the provided labels.
        /// </summary>
        /// <param name="verbal">The element to create a context menu for.</param>
        /// <param name="labelsInContext">The labels which determine the context in which the menu is to be created.</param>
        /// <returns>A context menu based on the provided IVerbal in the contexxt of the provided labels.</returns>
        static ContextMenu ForVerbal(IVerbal verbal, IEnumerable<Label> labelsInContext) {
            var result = new ContextMenu();
            if (verbal.Subjects.Any()) {
                result.Items.Add(VerbalMenuItemFactory.ForSubject(labelsInContext, verbal));
            }
            if (verbal.DirectObjects.Any()) {
                result.Items.Add(VerbalMenuItemFactory.ForDirectObject(labelsInContext, verbal));
            }
            if (verbal.IndirectObjects.Any()) {
                result.Items.Add(VerbalMenuItemFactory.ForIndirectObject(labelsInContext, verbal));
            }
            if (verbal != null && verbal.ObjectOfThePreoposition != null) {
                result.Items.Add(VerbalMenuItemFactory.ForPrepositionalObject(labelsInContext, verbal));
            }
            return result;
        }
        /// <summary>
        /// Creates a context menu specific to the given IReferencer elemenet with context determined by the provided labels. 
        /// </summary>
        /// <param name="referencer">The element to create a context menu for.</param>
        /// <param name="labelsInContext">The labels which determine the context in which the menu is to be created.</param>
        /// <returns>A context menu based on the provided IReferencer in the contexxt of the provided labels.</returns>
        static ContextMenu ForReferencer(IReferencer referencer, IEnumerable<Label> labelsInContext) {
            var result = new ContextMenu();
            if (referencer.Referent != null && referencer.Referent.Any()) {
                result.Items.Add(ReferencerMenuItemFactory.ForReferredTo(labelsInContext, referencer));
            }
            return result;
        }
        static class VerbalMenuItemFactory
        {
            public static MenuItem ForPrepositionalObject(IEnumerable<Label> labelsInContext, IVerbal verbal) {
                var visitSubjectMI = new MenuItem { Header = "view prepositional object" };
                visitSubjectMI.Click += (sender, e) => {
                    ResetUnassociatedLabelBrushes(labelsInContext, verbal);
                    var labels = from label in labelsInContext
                                 where label.Tag.Equals(verbal.ObjectOfThePreoposition)
                                 select new { label, brush = (label.Tag as ILexical).GetSyntacticColorization() };
                    foreach (var l in labels) {
                        l.label.Foreground = l.brush;
                        l.label.Background = Brushes.Red;
                    }
                };
                return visitSubjectMI;
            }

            public static MenuItem ForIndirectObject(IEnumerable<Label> labelsInContext, IVerbal verbal) {
                var visitSubjectMI = new MenuItem { Header = "view indirect objects" };
                visitSubjectMI.Click += (sender, e) => {
                    ResetUnassociatedLabelBrushes(labelsInContext, verbal);
                    var labels = from r in verbal.IndirectObjects
                                 join label in labelsInContext on r equals label.Tag
                                 select new { label, brush = (label.Tag as ILexical).GetSyntacticColorization() };
                    foreach (var l in labels) {
                        l.label.Foreground = l.brush;
                        l.label.Background = Brushes.Red;
                    }
                };
                return visitSubjectMI;
            }

            public static MenuItem ForDirectObject(IEnumerable<Label> labelsInContext, IVerbal verbal) {
                var visitSubjectMI = new MenuItem { Header = "view direct objects" };
                visitSubjectMI.Click += (sender, e) => {
                    ResetUnassociatedLabelBrushes(labelsInContext, verbal);
                    var labels = from r in verbal.DirectObjects
                                 join label in labelsInContext on r equals label.Tag
                                 select new { label, brush = (label.Tag as ILexical).GetSyntacticColorization() };
                    foreach (var l in labels) {
                        l.label.Foreground = l.brush;
                        l.label.Background = Brushes.Red;
                    }
                };
                return visitSubjectMI;
            }



            public static MenuItem ForSubject(IEnumerable<Label> labelsInContext, IVerbal verbal) {
                var visitSubjectMI = new MenuItem { Header = "view subjects" };
                visitSubjectMI.Click += (sender, e) => {
                    ResetUnassociatedLabelBrushes(labelsInContext, verbal);
                    var labels = from r in verbal.Subjects
                                 join l in labelsInContext on r equals l.Tag
                                 select l;
                    foreach (var l in labels) {
                        l.Foreground = Brushes.Black;
                        l.Background = Brushes.Red;
                    }
                };
                return visitSubjectMI;
            }
        }
        static class ReferencerMenuItemFactory
        {
            public static MenuItem ForReferredTo(IEnumerable<Label> labelsInContext, IReferencer pro) {
                var visitBoundEntity = new MenuItem { Header = "view referred to" };
                visitBoundEntity.Click += (sender, e) => {
                    ResetLabelBrushes(labelsInContext);
                    var labels = from l in labelsInContext
                                 where pro.Referent == l.Tag || l.Tag is NounPhrase &&
                                 pro.Referent.ToHashSet().Overlaps((l.Tag as NounPhrase).Words.OfEntity())
                                 select l;
                    foreach (var l in labels) {
                        l.Foreground = Brushes.White;
                        l.Background = Brushes.Black;
                    }
                };
                return visitBoundEntity;
            }
        }

        #endregion
        #region Helper Methods
        static void ResetLabelBrushes(IEnumerable<Label> labelsInContext) {
            foreach (var l in labelsInContext) {
                l.Foreground = (l.Tag as ILexical).GetSyntacticColorization();
                l.Background = Brushes.White;
            }
        }
        private static void ResetUnassociatedLabelBrushes(IEnumerable<Label> labelsInContext, IVerbal verbal) {
            foreach (var l in from l in labelsInContext
                              let e = l.Tag as IEntity
                              where e != null && !verbal.HasSubjectOrObject(i => i == e)
                              select l) {
                l.Foreground = (l.Tag as ILexical).GetSyntacticColorization();
                l.Background = Brushes.White;
            }
        }
        #endregion
    }
}
