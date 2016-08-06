using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Media;
using LASI.Core;
using LASI.Interop;
using LASI.Utilities.Specialized.Enhanced.IList.Linq;

namespace LASI.App.Visualization
{
    using System;
    using WpfDocuments = System.Windows.Documents;

    internal static class ContextMenuBuilder
    {
        /// <summary>
        /// Creates a context menu based on the context of the given lexical. Returns <c>null</c> if
        /// the lexical does not have significant information from which to build a menu.
        /// </summary>
        /// <param name="element">The element for which to construct a context menu.</param>
        /// <param name="neighboringElementDisplayStructures">
        /// The collection of UI objects representing neighboring elements.
        /// </param>
        /// <returns>
        /// A context menu based on the context of the given lexical or <c>null</c> if the lexical
        /// does not have significant information from which to build a menu.
        /// </returns>
<<<<<<< Updated upstream
        public static ContextMenu ForLexical(ILexical element, IReadOnlyList<WpfDocuments.TextElement> neighboringElementDisplayStructures) => element.Match()
            .Case((IVerbal v) => ForVerbal(v, neighboringElementDisplayStructures))
            .Case((IReferencer r) => ForReferencer(r, neighboringElementDisplayStructures))
            .Result();
=======
        public static ContextMenu ForLexical(ILexical element, IReadOnlyList<WpfDocuments.TextElement> neighboringElementDisplayStructures)
        {
            var createMenuForVerbal = ForVerbal(neighboringElementDisplayStructures);
            var createMenuForReferencer = ForReferencer(neighboringElementDisplayStructures);
            return element.Match()
                .Case(createMenuForVerbal)
                .Case(createMenuForReferencer)
                .Result();
        }
>>>>>>> Stashed changes

        #region Lexical Element Context Menu Construction

        /// <summary>
        /// Creates a context menu specific to the given IVerbal element with context determined by
        /// the provided labels.
        /// </summary>
        /// <param name="verbal">The element to create a context menu for.</param>
        /// <param name="neighboringElements">
        /// The labels which determine the context in which the menu is to be created.
        /// </param>
        /// <returns>
        /// A context menu based on the provided IVerbal in the context of the provided labels.
        /// </returns>
        private static Func<IVerbal, ContextMenu> ForVerbal(IReadOnlyList<WpfDocuments.TextElement> neighboringElements) =>
            verbal =>
            {
                var result = new ContextMenu();
                if (verbal.Subjects.Any())
                {
                    result.Items.Add(VerbalMenuItemFactory.ForSubject(verbal, neighboringElements));
                }
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
            };

        /// <summary>
        /// Creates a context menu specific to the given IReferencer element with context
        /// determined by the provided labels.
        /// </summary>
        /// <param name="referencer">The element to create a context menu for.</param>
        /// <param name="neighboringElements">
        /// The labels which determine the context in which the menu is to be created.
        /// </param>
        /// <returns>
        /// A context menu based on the provided IReferencer in the context of the provided labels.
        /// </returns>
        private static Func<IReferencer, ContextMenu> ForReferencer(IReadOnlyList<WpfDocuments.TextElement> neighboringElements) =>
            referencer =>
            {
                var result = new ContextMenu();
                if (referencer.RefersTo != null && referencer.RefersTo.Any())
                {
                    result.Items.Add(ReferencerMenuItemFactory.ForReferredTo(neighboringElements, referencer));
                }
                return result.Items.Count == 0 ? null : result;
            };

        private static class VerbalMenuItemFactory
        {
            public static MenuItem ForPrepositionalObject(IVerbal verbal, IReadOnlyList<WpfDocuments.TextElement> neighboringElements)
            {
                var visitSubjectMI = new MenuItem { Header = Text.ViewObjectOfThePrepositionHeader };
                visitSubjectMI.Click += delegate
                {
                    ResetUnassociatedLabelBrushes(neighboringElements, verbal);
                    var associatedElements = from element in neighboringElements
                                             where element.Tag.Equals(verbal.ObjectOfThePreposition)
                                             select new { Element = element, Brush = colorMapping[element.Tag as ILexical] };
                    foreach (var a in associatedElements)
                    {
                        a.Element.Foreground = a.Brush;
                        a.Element.Background = Brushes.Red;
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
                var associatedElements = from lexical in associatedLexicalElements
                                         join element in neighboringElements on lexical equals element.Tag
                                         select new { Element = element, NewForegroundColor = colorMapping[element.Tag as ILexical] };
                foreach (var a in associatedElements)
                {
                    a.Element.Foreground = a.NewForegroundColor;
                    a.Element.Background = Brushes.Red;
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
                    var associatedElements = from element in neighboringElements
                                             where pro.RefersTo == element.Tag || element.Tag is NounPhrase &&
                                             pro.RefersTo.Intersect((element.Tag as NounPhrase).Words.OfEntity()).Any()
                                             select element;
                    foreach (var a in associatedElements)
                    {
                        a.Foreground = Brushes.White;
                        a.Background = Brushes.Black;
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
                                    where !verbal.HasSubjectOrObject(i => i == element.Tag)
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