using System;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace LASI.Utilities.Configuration
{
    public class XmlConfig : ConfigBase
    {
        private XElement XElement;
        public XmlConfig(string filePath) : base(filePath)
        {
            XElement = XElement.Parse(RawConfigData);
        }
        public XmlConfig(Uri uri) : base(uri)
        {
            XElement = XElement.Parse(RawConfigData);
        }
        public XmlConfig(XElement xElement) : base(xElement)
        {
            XElement = xElement;
        }

        public override string this[string name] => this[name, StringComparison.Ordinal];


        public override string this[string name, StringComparison stringComparison] => (
            from element in XElement.Descendants()
            where element.Name.ToString().Equals(name, stringComparison)
            select element.Value).FirstOrDefault();
    }
}