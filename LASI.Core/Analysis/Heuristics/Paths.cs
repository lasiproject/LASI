using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LASI.Core.Configuration
{
    /// <summary>
    /// Provides access to the location of shared static resource files.
    /// </summary>
    public static class Paths
    {
        /// <summary>
        /// TODO: refactor this into another kind of explicit dependency
        /// </summary>
        internal static Utilities.Configuration.IConfig Settings { get; set; }
        static string ResourcesDirectory => Settings != null ? Settings["ResourcesDirectory"] : ConfigurationManager.AppSettings["ResourcesDirectory"];
        static string WordnetPath => ResourcesDirectory + (Settings != null ? Settings["WordnetFileDirectory"] : ConfigurationManager.AppSettings["WordnetFileDirectory"]);
        /// <summary>
        /// The location of the text file containing the Scrabble dictionary.
        /// </summary>
        public static string ScrabbleDict => WordnetPath + "dictionary.txt";
        /// <summary>
        /// Provides the locations of the Princeton WordNet resource files.
        /// </summary>
        public static class WordNet
        {
            /// <summary>
            /// The location of the data.noun file.
            /// </summary>
            public static string Noun => WordnetPath + "data.noun";
            /// <summary>
            /// The location of the data.verb file.
            /// </summary>
            public static string Verb => WordnetPath + "data.verb";
            /// <summary>
            /// The location of the data.adj file.
            /// </summary>
            public static string Adjective => WordnetPath + "data.adj";
            /// <summary>
            /// The location of the data.adv file.
            /// </summary>
            public static string Adverb => WordnetPath + "data.adv";
        }
        /// <summary>
        /// Provides the locations of the name data resource files.
        /// </summary>
        public static class Names
        {
            private static string BasePath => ResourcesDirectory + (Settings != null ? Settings["NameDataDirectory"] : ConfigurationManager.AppSettings["NameDataDirectory"]);
            /// <summary>
            /// The location of the file containing last names.
            /// </summary>
            public static string Last => BasePath + "last.txt";
            /// <summary>
            /// The location of the file containing female first names.
            /// </summary>
            public static string Female => BasePath + "femalefirst.txt";
            /// <summary>
            /// The location of the file containing male first names.
            /// </summary>
            public static string Male => BasePath + "malefirst.txt";
        }

    }
}
