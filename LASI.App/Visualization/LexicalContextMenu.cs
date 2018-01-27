using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using LASI.Core;
using LASI.Utilities.Specialized.Enhanced.IList.Linq;
using static System.Windows.Media.Brushes;
using Item = System.Windows.Controls.MenuItem;
using TextElement = System.Windows.Documents.TextElement;

namespace LASI.App.Visualization
{
    internal static class LexicalContextMenu
    {
        /// <summary>
        /// Creates a context menu based on the context of the given lexical. Returns <c>null</c> if
        /// the lexical does not have significant information from which to build a menu.
        /// </summary>
        /// <param name="run">The <see cref="Run"/> for which to construct a context menu.</param>
        /// <param name="neighboringElements">
        /// The collection of UI objects representing neighboring elements.
        /// </param>
        /// <returns>
        /// A context menu based on the context of the given lexical or <c>null</c> if the lexical
        /// does not have significant information from which to build a menu.
        /// </returns>
        public static void ContextMenu(Run run, IEnumerable<TextElement> neighboringElements)
        {
            run.ContextMenu = createMenu();
            ContextMenu createMenu()
            {
                switch (run.Tag)
                {
                    case IReferencer r when r.RefersTo?.Any() ?? false:
                        return ForReferencer(r, neighboringElements);
                    case IVerbal v when v.HasSubjectOrObject() || v.ObjectOfThePreposition != null:
                        return ForVerbal(v, neighboringElements);
                    default:
                        return run.ContextMenu;
                }
            }
        }

        #region Lexical Element Context Menu Construction

        /// <summary>
        /// Creates a context menu specific to the given IVerbal element with context determined by
        /// the provided labels.
        /// </summary>
        /// <param name="verbal">The <see cref="IVerbal"/> for which to create a menu.</param>
        /// <param name="neighboringElements">
        /// The labels which determine the context in which the menu is to be created.
        /// </param>
        /// <returns>
        /// A context menu based on the provided IVerbal in the context of the provided labels.
        /// </returns>
        private static ContextMenu ForVerbal(IVerbal verbal, IEnumerable<TextElement> neighboringElements)
        {
            var result = new ContextMenu();
            if (verbal.Subjects.Any())
            {
                result.Items.Add(VerbalMenuItemFactory.ForSubjects(verbal, neighboringElements));
            }
            if (verbal.DirectObjects.Any())
            {
                result.Items.Add(VerbalMenuItemFactory.ForDirectObjects(verbal, neighboringElements));
            }
            if (verbal.IndirectObjects.Any())
            {
                result.Items.Add(VerbalMenuItemFactory.ForIndirectObjects(verbal, neighboringElements));
            }
            if (verbal.ObjectOfThePreposition != null)
            {
                result.Items.Add(VerbalMenuItemFactory.ForPrepositionalObject(verbal, neighboringElements));
            }
            return result;
        }

        /// <summary>
        /// Creates a context menu specific to the given IReferencer element with context
        /// determined by the provided labels.
        /// </summary>
        /// <param name="referencer">The <see cref="IReferencer"/> for which to create a menu.</param>
        /// <param name="neighboringElements">
        /// The labels which determine the context in which the menu is to be created.
        /// </param>
        /// <returns>
        /// A context menu based on the provided IReferencer in the context of the provided labels.
        /// </returns>
        private static ContextMenu ForReferencer(IReferencer referencer, IEnumerable<TextElement> neighboringElements) => new ContextMenu
        {
            Items =
                {
                    ReferencerMenuItemFactory.ForReferredTo(referencer, neighboringElements)
                }
        };

        private static class VerbalMenuItemFactory
        {
            public static Item ForPrepositionalObject(IVerbal verbal, IEnumerable<TextElement> neighboringElements)
            {
                var indicatePreopositionalObjects = new Item { Header = Text.ViewObjectOfThePrepositionHeader };
                indicatePreopositionalObjects.Click += (s, e) =>
                {
                    ResetUnassociatedLabelBrushes(neighboringElements, verbal);
                    var associatedElements =
                        from element in neighboringElements
                        where element.Tag?.Equals(verbal.ObjectOfThePreposition) ?? false
                        select (element, background: colorMapping[verbal.ObjectOfThePreposition], foreground: Red);

                    foreach (var (element, background, foreground) in associatedElements)
                    {
                        element.Foreground = foreground;
                        element.Background = background;
                    }
                };
                return indicatePreopositionalObjects;
            }

            public static Item ForIndirectObjects(IVerbal verbal, IEnumerable<TextElement> neighboringElements)
            {
                var indicateIndirectObjects = new Item { Header = Text.ViewIndirectObjectsHeader };
                indicateIndirectObjects.Click += (s, e) =>
                {
                    ResetUnassociatedLabelBrushes(neighboringElements, verbal);
                    BuildAndExecuteLabelTransformations(neighboringElements, verbal.IndirectObjects);
                };
                return indicateIndirectObjects;
            }

            public static Item ForDirectObjects(IVerbal verbal, IEnumerable<TextElement> neighboringElements)
            {
                var indicateDirectObjects = new Item { Header = Text.ViewDirectObjectsHeader };
                indicateDirectObjects.Click += (s, e) =>
                {
                    ResetUnassociatedLabelBrushes(neighboringElements, verbal);
                    BuildAndExecuteLabelTransformations(neighboringElements, verbal.DirectObjects);
                };
                return indicateDirectObjects;
            }

            public static Item ForSubjects(IVerbal verbal, IEnumerable<TextElement> neighboringElements)
            {
                var indicateSubjects = new Item { Header = Text.ViewSubjectsHeader };
                indicateSubjects.Click += (s, e) =>
                {
                    ResetUnassociatedLabelBrushes(neighboringElements, verbal);
                    BuildAndExecuteLabelTransformations(neighboringElements, verbal.Subjects);
                };
                return indicateSubjects;
            }

            private static void BuildAndExecuteLabelTransformations(IEnumerable<TextElement> neighboringElements, IEnumerable<ILexical> associatedLexicalElements)
            {
                var associatedElements = from lexical in associatedLexicalElements
                                         join element in neighboringElements on lexical equals element.Tag
                                         select (element, newForegroundColor: colorMapping[lexical]);
                foreach (var (element, newForegroundColor) in associatedElements)
                {
                    element.Foreground = newForegroundColor;
                    element.Background = Red;
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
            public static Item ForReferredTo(IReferencer referencer, IEnumerable<TextElement> neighboringElements)
            {
                var indicateReferredTo = new Item { Header = "View Referred To" };
                indicateReferredTo.Click += (s, e) =>
                {
                    ResetLabelBrushes(neighboringElements);
                    var associatedElements = from element in neighboringElements
                                             where referencer.RefersTo == element.Tag || element.Tag is NounPhrase &&
                                             referencer.RefersTo.Intersect((element.Tag as NounPhrase).Words.OfEntity()).Any()
                                             select element;
                    foreach (var a in associatedElements)
                    {
                        a.Foreground = White;
                        a.Background = Black;
                    }
                };
                return indicateReferredTo;
            }
        }

        #endregion Lexical Element Context Menu Construction

        #region Helper Methods

        private static void ResetLabelBrushes(IEnumerable<TextElement> neighboringElements)
        {
            foreach (var element in neighboringElements)
            {
                element.Foreground = colorMapping[element.Tag as ILexical];
                element.Background = White;
            }
        }

        private static void ResetUnassociatedLabelBrushes(IEnumerable<TextElement> neighboringElements, IVerbal verbal)
        {
            var elements = from element in neighboringElements
                           where !verbal.HasSubjectOrObject(i => i == element.Tag)
                           select element;
            foreach (var element in elements)
            {
                element.Foreground = colorMapping[element.Tag as ILexical];
                element.Background = White;
            }
        }


        #endregion Helper Methods

        private static readonly Interop.Visualization.IStyleProvider<ILexical, Brush> colorMapping = new SyntacticColorMapping();
    }
}
