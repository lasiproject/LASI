using LASI;
using LASI.Algorithm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace LASI.ContentSystem.Serialization.XML
{
    public interface ILexicalWriter<in S, out T, in W> : IDisposable
        where W : System.Xml.XmlWriter
        where T : ILexical
        where S : IEnumerable<ILexical>
    {
        void Write(S resultSet, string resultSetTitle, DegreeOfOutput degreeOfOutput);
        Task WriteAsync(S resultSet, string resultSetTitle, DegreeOfOutput degreeOfOutput);
        System.Xml.XmlWriter Target {
            get;
        }
    }
    public enum DegreeOfOutput
    {
        TopWeighted,
        Graphed,
        Comprehensive,
        ContextSpecific
    }
}
