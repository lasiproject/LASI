using LASI.Core;
using LASI.Core.Patternization;
using LASI.Utilities;
using LASI.ContentSystem;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MvcApplication2.LexicalElementInfo
{
    public class RelationshipMenuEntry
    {
        internal RelationshipMenuEntry(int elementId, string entryText, IEnumerable<int> relatedElementIds)
        {
            ElementId = elementId;
            EntryText = entryText;
            RelatedElementIds = relatedElementIds;
        }
        public int ElementId { get; private set; }
        public string EntryText { get; private set; }

        public IEnumerable<int> RelatedElementIds { get; private set; }
    }
    public class ElementContextMenuMapping
    {
        internal ElementContextMenuMapping(IEnumerable<RelationshipMenuEntry> menuEntries)
        {
            MenuEntries = menuEntries;
        }
        public IEnumerable<RelationshipMenuEntry> MenuEntries { get; private set; }

    }
    public class ElementWithMenuData
    {
        internal ElementWithMenuData(ElementWithId element, ElementContextMenuMapping menuMappingData)
        {
            Id = element.Id;
            Lexical = element.Element;
            MenuMappingData = menuMappingData;
        }
        public ILexical Lexical { get; set; }
        public int Id { get; private set; }
        public ElementContextMenuMapping MenuMappingData { get; private set; }
    }
    public class ElementWithId
    {
        public ILexical Element { get; set; }
        public int Id { get; set; }
    }

    public static class ContextMenuBuilder
    {

        public static IEnumerable<ElementWithId> BindClientSideIds(IEnumerable<ILexical> elements)
        {
            // Pairs each element with an unique identifier. Assumes that the number of elements supplied is no more than int.MaxValue
            return Enumerable.Range(0, int.MaxValue).Zip(elements, (id, element) => new ElementWithId { Id = id, Element = element });

        }
        static ElementContextMenuMapping ForVerbal(IVerbal verbal, int verbalId, IEnumerable<ElementWithId> elementsWithId)
        {
            return new ElementContextMenuMapping(new[] { 
                new RelationshipMenuEntry(verbalId, 
                    "View Subjects",
                    elementsWithId.IdsWhere(e => verbal.HasSubject(s => s == e))), 
                new RelationshipMenuEntry(verbalId,
                    "View Direct Objects",
                    elementsWithId.IdsWhere(e => verbal.HasDirectObject(o => o == e))), 
                new RelationshipMenuEntry(verbalId, 
                    "View Indirect Objects"
                    , elementsWithId.IdsWhere(e => verbal.HasIndirectObject(o => o == e))),
                new RelationshipMenuEntry(verbalId,
                    "View Prepositional Ojbects",
                    elementsWithId.IdsWhere(e => verbal.ObjectOfThePreoposition == e)) 
            });
        }
        static IEnumerable<int> IdsWhere(this IEnumerable<ElementWithId> elementsWithId, Func<ILexical, bool> predicate)
        {
            return from e in elementsWithId where predicate(e.Element) select e.Id;
        }

        public static ElementWithMenuData ForLexical(ElementWithId e, IEnumerable<ElementWithId> elementsInContext)
        {
            return e.Element.Match().Yield<ElementWithMenuData>()
                .With<IVerbal>(v => new ElementWithMenuData(e, ForVerbal(v, e.Id, elementsInContext)))
                //.With<IReferencer>(e => ForReferencer(e, elementsInContext))
                .Result(new ElementWithMenuData(e, new ElementContextMenuMapping(new RelationshipMenuEntry[] { })));
        }


    }
}
