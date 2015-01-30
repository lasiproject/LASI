using LASI.Core;
 using LASI.Interop;
using LASI.Utilities;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Media;

namespace LASI.App.LexicalElementInfo
{
    using System;
    using WpfDocuments = System.Windows.Documents;
    static class ContextMenuFactory
    {
        public static ContextMenu ForLexical(ILexical element, IEnumerable<WpfDocuments.TextElement> neighboringElements) {
            return element.Match()
                .Case((IVerbal v) => ForVerbal(v, neighboringElements))
                .Case((IReferencer r) => ForReferencer(r, neighboringElements))
                .Result();
        }
        #region Lexical Element Context Menu Construction
        /// <summary>
        /// Creates a context menu specific to the given IVerbal elemenet with context determined by the provided labels.
        /// </summary>
        /// <param name="verbal">The element to create a context menu for.</param>
        /// <param name="neighboringElements">The labels which determine the context in which the menu is to be created.</param>
        /// <returns>A context menu based on the provided IVerbal in the contexxt of the provided labels.</returns>
        static ContextMenu ForVerbal(IVerbal verbal, IEnumerable<WpfDocuments.TextElement> neighboringElements) {
            var result = new ContextMenu();
            if (verbal.Subjects.Any()) {
                result.Items.Add(VerbalMenuItemFactory.ForSubject(neighboringElements, verbal));
            }
            if (verbal.DirectObjects.Any()) {
                result.Items.Add(VerbalMenuItemFactory.ForDirectObject(neighboringElements, verbal));
            }
            if (verbal.IndirectObjects.Any()) {
                result.Items.Add(VerbalMenuItemFactory.ForIndirectObject(neighboringElements, verbal));
            }
            if (verbal != null && verbal.ObjectOfThePreposition != null) {
                result.Items.Add(VerbalMenuItemFactory.ForPrepositionalObject(neighboringElements, verbal));
            }
            return result;
        }
        /// <summary>
        /// Creates a context menu specific to the given IReferencer elemenet with context determined by the provided labels. 
        /// </summary>
        /// <param name="referencer">The element to create a context menu for.</param>
        /// <param name="neighboringElements">The labels which determine the context in which the menu is to be created.</param>
        /// <returns>A context menu based on the provided IReferencer in the contexxt of the provided labels.</returns>
        static ContextMenu ForReferencer(IReferencer referencer, IEnumerable<WpfDocuments.TextElement> neighboringElements) {
            var result = new ContextMenu();
            if (referencer.RefersTo != null && referencer.RefersTo.Any()) {
                result.Items.Add(ReferencerMenuItemFactory.ForReferredTo(neighboringElements, referencer));
            }
            return result;
        }
        static class VerbalMenuItemFactory
        {
            public static MenuItem ForPrepositionalObject(IEnumerable<WpfDocuments.TextElement> neighboringElements, IVerbal verbal) {
                var visitSubjectMI = new MenuItem { Header = "view prepositional object" };
                visitSubjectMI.Click += (sender, e) => {
                    ResetUnassociatedLabelBrushes(neighboringElements, verbal);
                    var labels = from label in neighboringElements
                                 where label.Tag.Equals(verbal.ObjectOfThePreposition)
                                 select new { label, brush = colorMapping[label.Tag as ILexical] };
                    foreach (var l in labels) {
                        l.label.Foreground = l.brush;
                        l.label.Background = Brushes.Red;
                    }
                };
                return visitSubjectMI;
            }

            public static MenuItem ForIndirectObject(IEnumerable<WpfDocuments.TextElement> neighboringElements, IVerbal verbal) {
                var visitSubjectMI = new MenuItem { Header = "view indirect objects" };
                visitSubjectMI.Click += (sender, e) => {
                    ResetUnassociatedLabelBrushes(neighboringElements, verbal);
                    BuildAndExecuteLabelTransformations(verbal.IndirectObjects, neighboringElements);
                };
                return visitSubjectMI;
            }

            public static MenuItem ForDirectObject(IEnumerable<WpfDocuments.TextElement> neighboringElements, IVerbal verbal) {
                var visitSubjectMI = new MenuItem { Header = "view direct objects" };
                visitSubjectMI.Click += (sender, e) => {
                    ResetUnassociatedLabelBrushes(neighboringElements, verbal);
                    BuildAndExecuteLabelTransformations(verbal.DirectObjects, neighboringElements);

                };
                return visitSubjectMI;
            }

            private static void BuildAndExecuteLabelTransformations(IEnumerable<ILexical> associatedElements, IEnumerable<WpfDocuments.TextElement> neighboringElements) {
                var transformations = from r in associatedElements
                                      join label in neighboringElements on r equals label.Tag
                                      select new { Label = label, NewForegroundColor = colorMapping[label.Tag as ILexical] };
                foreach (var l in transformations) {
                    l.Label.Foreground = l.NewForegroundColor;
                    l.Label.Background = Brushes.Red;
                }
            }

            public static MenuItem ForSubject(IEnumerable<WpfDocuments.TextElement> neighboringElements, IVerbal verbal) {
                var visitSubjectMI = new MenuItem { Header = "view subjects" };
                visitSubjectMI.Click += (sender, e) => {
                    ResetUnassociatedLabelBrushes(neighboringElements, verbal);
                    BuildAndExecuteLabelTransformations(verbal.Subjects, neighboringElements);
                };
                return visitSubjectMI;
            }
        }
        static class ReferencerMenuItemFactory
        {
            public static MenuItem ForReferredTo(IEnumerable<WpfDocuments.TextElement> neighboringElements, IReferencer pro) {
                var visitBoundEntity = new MenuItem { Header = "view referred to" };
                visitBoundEntity.Click += (sender, e) => {
                    ResetLabelBrushes(neighboringElements);
                    var labels = from l in neighboringElements
                                 where pro.RefersTo == l.Tag || l.Tag is NounPhrase &&
                                 pro.RefersTo.Intersect((l.Tag as NounPhrase).Words.OfEntity()).Any()
                                 select l;
                    foreach (var label in labels) {
                        label.Foreground = Brushes.White;
                        label.Background = Brushes.Black;
                    }
                };
                return visitBoundEntity;
            }
        }
        #endregion
        #region Helper Methods
        static void ResetLabelBrushes(IEnumerable<WpfDocuments.TextElement> neighboringElements) {
            foreach (var element in neighboringElements) {
                element.Foreground = colorMapping[element.Tag as ILexical];
                element.Background = Brushes.White;
            }
        }
        private static void ResetUnassociatedLabelBrushes(IEnumerable<WpfDocuments.TextElement> neighboringElements, IVerbal verbal) {
            foreach (var l in from l in neighboringElements
                              let e = l.Tag as IEntity
                              where e != null && !verbal.HasSubjectOrObject(i => i == e)
                              select l) {
                l.Foreground = colorMapping[l.Tag as ILexical];
                l.Background = Brushes.White;
            }
        }
        #endregion

        private static readonly IStyleProvider<ILexical, Brush> colorMapping = new SyntacticColorMap();

    }
}
