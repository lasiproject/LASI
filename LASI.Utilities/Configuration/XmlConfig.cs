using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace LASI.Utilities.Configuration
{
    /// <summary>
    /// An XML Based configuration source.
    /// </summary>
    public class XmlConfig : LoadableConfigBase, IConfig
    {
        readonly IDictionary<string, string> data;

        /// <summary>
        /// Initializes a new instance of the XmlConfig class from the specified XML file.
        /// </summary>
        /// <param name="filePath">The location of the XML file from which to retrieve configuration information to construct the XmlConfig instance.</param>
        public XmlConfig(string filePath)
        {
            data = MakeDictionary(XElement.Parse(ReadConfigDataFromFile(filePath)));
        }

        IDictionary<string, string> MakeDictionary(XElement xml) => xml.Descendants().ToDictionary(e => e.Name.ToString(), e => e.Value);

        /// <summary>
        /// Initializes a new instance of the XmlConfig class from the XML data located at the specified <see cref="Uri"/>.
        /// </summary>
        /// <param name="uri">The Uri from which to retrieve configuration information to construct the XmlConfig instance.</param>
        public XmlConfig(Uri uri)
        {
            var xElement = XElement.Parse(DownloadRemoteConfigData(uri));
            data = MakeDictionary(xElement);
        }
        /// <summary>
        /// Initializes a new instance of the XmlConfig class from the specified <see cref="XElement"/>.
        /// </summary>
        /// <param name="xElement">The XElement from which to construct the XmlConfig instance.</param>
        public XmlConfig(XElement xElement)
        {
            data = MakeDictionary(xElement);
        }
        /// <summary>
        /// Gets the value with the specified name.
        /// </summary>
        /// <param name="name">The name of the value to retrieve.</param>
        /// <returns>The value with the specified name.</returns>
        public string this[string name] => data.GetValueOrDefault(name);
    }
}