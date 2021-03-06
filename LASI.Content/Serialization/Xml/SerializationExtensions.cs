﻿using LASI;
using LASI.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Utilities;

namespace LASI.Content.Serialization.Xml
{
    /// <summary>
    /// Provides extension methods for creating XML markup structures representing ILexical instances.
    /// </summary>
    public static class SerializationExtensions
    {
        /// <summary>
        /// Produces an XElement representation of the Entity.
        /// </summary>
        /// <param name="entity">The Entity for which to obtain an XElement.</param>
        /// <returns>An XElement representation of the Entity.</returns>
        public static XElement ToXElement(this IEntity entity) =>
            new XElement(ElementNames[entity],
                new XAttribute("Weight",
                    entity.Weight),
                new XAttribute("MetaWeight",
                    entity.MetaWeight),
                new XElement("SubjectOf",
                    ElementNames[entity.SubjectOf]),
                new XElement("DirectObjectOf",
                    ElementNames[entity.DirectObjectOf.Match((IVerbal v) => v)]),
                new XElement("IndirectObjectOf",
                    ElementNames[entity.IndirectObjectOf]),
                new XElement("BoundPronouns",
                    from r in entity.Referencers
                    select new XElement("Referees", ElementNames[r])),
                new XElement("Descriptors",
                    from d in entity.Descriptors
                    select new XElement("DescribedBy", ElementNames[d])),
                new XElement("Possessions",
                    from p in entity.Possessions
                    select new XElement("Possesses", ElementNames[p])));

        private static readonly NodeNameMapper ElementNames = new NodeNameMapper();

    }
}
