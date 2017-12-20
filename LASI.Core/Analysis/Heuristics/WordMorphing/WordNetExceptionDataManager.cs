using System.IO;
using System.Linq;
using System.Text;
using LASI.Utilities;
using LASI.Utilities.Specialized.Enhanced.IList.Linq;

namespace LASI.Core.Analysis.Heuristics.WordMorphing
{
    using System;
    using static System.Configuration.ConfigurationManager;
    using ExceptionsInfoMapping = System.Collections.Generic.IReadOnlyDictionary<string, System.Collections.Generic.IEnumerable<string>>;

    internal class WordNetExceptionDataManager
    {
        /// <summary>
        /// Initializes a new instance of the WordMorpher class.
        /// </summary>
        /// <param name="exceptionFileRelativePath">
        /// The path of the file containing WordNet exception information to be loaded by the WordMorpherHelper. This path must be relative to the WordNet Resources Directory.
        /// </param>
        internal WordNetExceptionDataManager(string exceptionFileRelativePath)
        {
            this.exceptionsFileRelativePath = exceptionFileRelativePath;
            excMapping = new Lazy<ExceptionsInfoMapping>(() =>
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
                    var results = from line in exceptionFileLines
                                      //.SelectMany(list => list.Select((item) => Pair.Create(item, list)))
                                      //.ToLookup(line => line.Select(e => Pair.Create(line[line.Count - 1], line.GetRange(0, line.Count - 1))))
                                  group line by line.Last()
                                            into g
                                  let values = g.SelectMany(x => x).ToList()
                                  let count = values.Count
                                  let exceptions = values.AsEnumerable()
                                  select (key: g.Key, exceptions, count);
                    //.ToDictionary(e => e.Key, e => e.Select(excs => excs).ToList());

                    var mostComplex = results.MaxBy(r => r.count);
                    Logger.Log($"Loaded Exception Information from : {exceptionsFileRelativePath}\nMax most complex line parsed: \"{mostComplex.exceptions} {mostComplex.key} -> {mostComplex.count} entries.\"");
                    return results.ToDictionary(x => x.key, x => x.exceptions);
                }
            });
        }

        private Lazy<ExceptionsInfoMapping> excMapping;
        public ExceptionsInfoMapping ExcMapping => excMapping.Value;

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
