using System.IO;
using System.Linq;
using System.Text;
using LASI.Utilities.Specialized.Enhanced.IList.Linq;
using LASI.Utilities;

namespace LASI.Core.Analysis.Heuristics.WordMorphing
{
    using System;
    using static System.Configuration.ConfigurationManager;
    using ExceptionsInfoMapping = System.Collections.Generic.Dictionary<string, System.Collections.Generic.List<string>>;
    internal class WordNetExceptionDataManager
    {
        /// <summary>
        /// Initializes a new instance of the WordMorpher class.
        /// </summary>
        /// <param name="exceptionFileRelativePath">The path of the file containing WordNet exception information to be loaded by the WordMorpherHelper. This path must be relative to the WordNet Resources Directory.</param>
        internal WordNetExceptionDataManager(string exceptionFileRelativePath)
        {
            this.exceptionsFileRelativePath = exceptionFileRelativePath;
            excMapping = new Lazy<ExceptionsInfoMapping>(LoadExceptionFile);
        }
        private Lazy<ExceptionsInfoMapping> excMapping;
        public ExceptionsInfoMapping ExcMapping => excMapping.Value;
        private ExceptionsInfoMapping LoadExceptionFile()
        {
            using (var reader =
                new StreamReader(
                detectEncodingFromByteOrderMarks: true,
                encoding: Encoding.ASCII, bufferSize: 4096, stream:
                    new FileStream(
                        path: ExceptionsFilePath,
                        access: FileAccess.Read,
                        mode: FileMode.Open,
                        share: FileShare.Read,
                        options: FileOptions.SequentialScan, bufferSize: 4096
                    )
                )
            )
            {
                Logger.Log($"Loading Exception Information from : {exceptionsFileRelativePath}\n");
                var exceptionFileLines = from line in reader.ReadToEnd().SplitRemoveEmpty('\r', '\n')
                                         select line.SplitRemoveEmpty(' ').Select(word => word.Replace('_', ' ')).ToList();
                var results = exceptionFileLines
                                        //.SelectMany(list => list.Select((item) => Pair.Create(item, list)))
                                        //.ToLookup(line => line.Select(e => Pair.Create(line[line.Count - 1], line.GetRange(0, line.Count - 1))))
                                        .GroupBy(e => e.Last())
                                        .ToDictionary(e => e.Key, e => e.SelectMany(x => x).ToList());
                //.ToDictionary(e => e.Key, e => e.Select(excs => excs).ToList());

                var mostComplex = results.MaxBy(r => r.Value.Count);
                Logger.Log($"Loaded Exception Information from : {exceptionsFileRelativePath}\nMax most complex line parsed: \"{mostComplex.Value} {mostComplex.Key} -> {mostComplex.Value.Count} entries.\"");
                return results;
            }
        }

        private static Utilities.Configuration.IConfig Config => Configuration.Paths.Settings;

        static string ResourcesDirectory =>
            Config != null ?
            Config["ResourcesDirectory"] :
            AppSettings["ResourcesDirectory"];

        static string WordnetFileDirectory =>
            ResourcesDirectory + (Config != null ? Config["WordnetFileDirectory"] :
            AppSettings["WordnetFileDirectory"]);
        string ExceptionsFilePath => WordnetFileDirectory + exceptionsFileRelativePath;
        private readonly string exceptionsFileRelativePath;
    }
}
using System;

class Foo {
  public ClassVsProp ClassVsProp { get; }

  public void M(object o) {
    if ((string) o == ClassVsProp.Constant) { } // OK
  
    // error CS0176: Member 'ClassVsProp.Constant' cannot be accessed
    // with an instance reference; qualify it with a type name instead
    if (o is ClassVsProp.Constant) { }

    switch (o) {
      case ClassVsProp.Constant: // OK
        break;
    }
  }
}

class ClassVsProp {
  public const string Constant = "abc";
}