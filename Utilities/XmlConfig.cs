using System;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace LASI.Utilities
{
    internal class XmlConfig : ConfigBase
    {
        private XElement xml;
        public XmlConfig(string filePath) : base(filePath) {
            xml = XElement.Parse(RawConfigData);
        }
        public XmlConfig(Uri uri) : base(uri) {
            xml = XElement.Parse(RawConfigData);
        }

        public override string this[string name] => this[name, StringComparison.CurrentCulture];


        public override string this[string name, StringComparison stringComparison] => (
            from element in xml.Descendants()
            where element.Name.ToString().Equals(name, stringComparison)
            select element.Value).FirstOrDefault();
    }
}