using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Media;
using LASI.Core;
using LASI.Interop;
using LASI.Utilities.Specialized.Enhanced.IList.Linq;

namespace LASI.App.Visualization
{
    using WpfDocuments = System.Windows.Documents;

    internal static class ContextMenuBuilder
    {
        /// <summary>
        /// Creates a context menu based on the context of the given lexical. Returns <c>null</c> if
        /// the lexical does not have significant information from which to build a menu.
        /// </summary>
        /// <param name="element">The element for which to construct a context menu.</param>
        /// <param name="neighboringElements">
        /// The collection of UI objects representating neighboring elements.
        /// </param>
        /// <returns>
        /// A context menu based on the context of the given lexical or <c>null</c> if the lexical
        /// does not have significant information from which to build a menu.
        /// </returns>
        public static ContextMenu ForLexical(ILexical element, IReadOnlyList<WpfDocuments.TextElement> neighboringElements)
        {
            return element.Match()
                .Case((IVerbal v) => ForVerbal(v, neighboringElements))
                .Case((IReferencer r) => ForReferencer(r, neighboringElements))
                .Result();
        }

        #region Lexical Element Context Menu Construction

        /// <summary>
        /// Creates a context menu specific to the given IVerbal elemenet with context determined by
        /// the provided labels.
        /// </summary>
        /// <param name="verbal">The element to create a context menu for.</param>
        /// <param name="neighboringElements">
        /// The labels which determine the context in which the menu is to be created.
        /// </param>
        /// <returns>
        /// A context menu based on the provided IVerbal in the contexxt of the provided labels.
        /// </returns>
        private static ContextMenu ForVerbal(IVerbal verbal, IReadOnlyList<WpfDocuments.TextElement> neighboringElements)
        {
            var result = new ContextMenu()
            {
                Items = {
                    verbal.Subjects.Any() ? VerbalMenuItemFactory.ForSubject(verbal, neighboringElements) : null
                }
            };
            result.Items.Cast<MenuItem>().ToList().ForEach(i => i.Items.Remove(i));
            if (verbal.DirectObjects.Any())
            {
                result.Items.Add(VerbalMenuItemFactory.ForDirectObject(verbal, neighboringElements));
            }
            if (verbal.IndirectObjects.Any())
            {
                result.Items.Add(VerbalMenuItemFactory.ForIndirectObject(verbal, neighboringElements));
            }
            if (verbal.ObjectOfThePreposition != null)
            {
                result.Items.Add(VerbalMenuItemFactory.ForPrepositionalObject(verbal, neighboringElements));
            }
            return result.Items.Count == 0 ? null : result;
        }

        /// <summary>
        /// Creates a context menu specific to the given IReferencer elemenet with context
        /// determined by the provided labels.
        /// </summary>
        /// <param name="referencer">The element to create a context menu for.</param>
        /// <param name="neighboringElements">
        /// The labels which determine the context in which the menu is to be created.
        /// </param>
        /// <returns>
        /// A context menu based on the provided IReferencer in the contexxt of the provided labels.
        /// </returns>
        private static ContextMenu ForReferencer(IReferencer referencer, IReadOnlyList<WpfDocuments.TextElement> neighboringElements)
        {
            var result = new ContextMenu();
            if (referencer.RefersTo != null && referencer.RefersTo.Any())
            {
                result.Items.Add(ReferencerMenuItemFactory.ForReferredTo(neighboringElements, referencer));
            }
            return result.Items.Count == 0 ? null : result;
        }

        private static class VerbalMenuItemFactory
        {
            public static MenuItem ForPrepositionalObject(IVerbal verbal, IReadOnlyList<WpfDocuments.TextElement> neighboringElements)
            {
                var visitSubjectMI = new MenuItem { Header = Text.ViewObjectOfThePrepositionHeader };
                visitSubjectMI.Click += delegate
                {
                    ResetUnassociatedLabelBrushes(neighboringElements, verbal);
                    var labels = from label in neighboringElements
                                 where label.Tag.Equals(verbal.ObjectOfThePreposition)
                                 select new { label, brush = colorMapping[label.Tag as ILexical] };
                    foreach (var l in labels)
                    {
                        l.label.Foreground = l.brush;
                        l.label.Background = Brushes.Red;
                    }
                };
                return visitSubjectMI;
            }

            public static MenuItem ForIndirectObject(IVerbal verbal, IReadOnlyList<WpfDocuments.TextElement> neighboringElements)
            {
                var visitSubjectMI = new MenuItem { Header = Text.ViewIndirectObjectsHeader };
                visitSubjectMI.Click += delegate
                {
                    ResetUnassociatedLabelBrushes(neighboringElements, verbal);
                    BuildAndExecuteLabelTransformations(neighboringElements, verbal.IndirectObjects);
                };
                return visitSubjectMI;
            }

            public static MenuItem ForDirectObject(IVerbal verbal, IReadOnlyList<WpfDocuments.TextElement> neighboringElements)
            {
                var visitSubjectMI = new MenuItem { Header = Text.ViewDirectObjectsHeader };
                visitSubjectMI.Click += delegate
                {
                    ResetUnassociatedLabelBrushes(neighboringElements, verbal);
                    BuildAndExecuteLabelTransformations(neighboringElements, verbal.DirectObjects);
                };
                return visitSubjectMI;
            }

            public static MenuItem ForSubject(IVerbal verbal, IReadOnlyList<WpfDocuments.TextElement> neighboringElements)
            {
                var visitSubjectMI = new MenuItem { Header = Text.ViewSubjectsHeader };
                visitSubjectMI.Click += delegate
                {
                    ResetUnassociatedLabelBrushes(neighboringElements, verbal);
                    BuildAndExecuteLabelTransformations(neighboringElements, verbal.Subjects);
                };
                return visitSubjectMI;
            }

            private static void BuildAndExecuteLabelTransformations(IReadOnlyList<WpfDocuments.TextElement> neighboringElements, IEnumerable<ILexical> associatedLexicalElements)
            {
                var transformations = from r in associatedLexicalElements
                                      join label in neighboringElements on r equals label.Tag
                                      select new { Label = label, NewForegroundColor = colorMapping[label.Tag as ILexical] };
                foreach (var l in transformations)
                {
                    l.Label.Foreground = l.NewForegroundColor;
                    l.Label.Background = Brushes.Red;
                }
            }
            private static class Text
            {
                public const string ViewSubjectsHeader = "View Subjects";
                public const string ViewDirectObjectsHeader = "View Direct Objects";
                public const string ViewIndirectObjectsHeader = "View Indirect Objects";
                public const string ViewObjectOfThePrepositionHeader = "View Object Of The Preposition";
            }
        }

        private static class ReferencerMenuItemFactory
        {
            public static MenuItem ForReferredTo(IReadOnlyList<WpfDocuments.TextElement> neighboringElements, IReferencer pro)
            {
                var visitBoundEntity = new MenuItem { Header = "View Referred To" };
                visitBoundEntity.Click += delegate
                {
                    ResetLabelBrushes(neighboringElements);
                    var labels = from l in neighboringElements
                                 where pro.RefersTo == l.Tag || l.Tag is NounPhrase &&
                                 pro.RefersTo.Intersect((l.Tag as NounPhrase).Words.OfEntity()).Any()
                                 select l;
                    foreach (var label in labels)
                    {
                        label.Foreground = Brushes.White;
                        label.Background = Brushes.Black;
                    }
                };
                return visitBoundEntity;
            }
        }

        #endregion Lexical Element Context Menu Construction

        #region Helper Methods

        private static void ResetLabelBrushes(IReadOnlyList<WpfDocuments.TextElement> neighboringElements)
        {
            foreach (var element in neighboringElements)
            {
                element.Foreground = colorMapping[element.Tag as ILexical];
                element.Background = Brushes.White;
            }
        }

        private static void ResetUnassociatedLabelBrushes(IReadOnlyList<WpfDocuments.TextElement> neighboringElements, IVerbal verbal)
        {
            foreach (var element in from element in neighboringElements
                                    let entity = element.Tag as IEntity
                                    where entity != null && !verbal.HasSubjectOrObject(i => i == entity)
                                    select element)
            {
                element.Foreground = MapToColor(element.Tag);
                element.Background = Brushes.White;
            }
        }

        private static Brush MapToColor(object tag) => colorMapping[tag as ILexical];

        #endregion Helper Methods

        private static readonly Interop.Visualization.IStyleProvider<ILexical, Brush> colorMapping = new SyntacticColorMap();
    }
}