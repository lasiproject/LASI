using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LASI.Core.Configuration
{
    public static class Paths
    {/// <summary>
     /// TODO: refactor this into another kind of explicit dependency
     /// </summary>
        internal static LASI.Utilities.Configuration.IConfig Settings { get; set; }
        static string ResourcesDirectory => Settings != null ? Settings["ResourcesDirectory"] : ConfigurationManager.AppSettings["ResourcesDirectory"];
        static string WordnetPath => ResourcesDirectory + (Settings != null ? Settings["WordnetFileDirectory"] : ConfigurationManager.AppSettings["WordnetFileDirectory"]);
        public static string ScrabbleDict => WordnetPath + "dictionary.txt";
        public static class WordNet
        {
            public static string Noun => WordnetPath + "data.noun";
            public static string Verb => WordnetPath + "data.verb";
            public static string Adjective => WordnetPath + "data.adj";
            public static string Adverb => WordnetPath + "data.adv";
        }
        public static class Names
        {
            static string BasePath => ResourcesDirectory + (Settings != null ? Settings["NameDataDirectory"] : ConfigurationManager.AppSettings["NameDataDirectory"]);
            public static string Last => BasePath + "last.txt";
            public static string Female => BasePath + "femalefirst.txt";
            public static string Male => BasePath + "malefirst.txt";
        }

    }
}
