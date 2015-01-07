using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LASI.Core.Heuristics
{
    public static partial class Lexicon
    {
        private static class Paths
        {
            static readonly string resourcesDirectory = ConfigurationManager.AppSettings["ResourcesDirectory"];
            static readonly string wordnetPath = resourcesDirectory + ConfigurationManager.AppSettings["WordnetFileDirectory"];
            public static string ScrabbleDict => wordnetPath + "dictionary.txt";
            public static class WordNet
            {
                public static string Noun => wordnetPath + "data.noun";
                public static string Verb => wordnetPath + "data.verb";
                public static string Adjective => wordnetPath + "data.adj";
                public static string Adverb => wordnetPath + "data.adv";
            }
            public static class Names
            {
                static readonly string basePath = resourcesDirectory + ConfigurationManager.AppSettings["NameDataDirectory"];
                public static string Last => basePath + "last.txt";
                public static string Female => basePath + "femalefirst.txt";
                public static string Male => basePath + "malefirst.txt";
            }

        }
    }
}
