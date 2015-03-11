using System.IO;
using System.Linq;
using System.Text;
using LASI.Utilities.Specialized.Enhanced.IList.Linq;
using LASI.Utilities;

namespace LASI.Core.Analysis.Heuristics.WordMorphing
{
    using System;
    using static System.Configuration.ConfigurationManager;
    using ExceptionsInfoMapping = System.Collections.Generic.IReadOnlyDictionary<string, System.Collections.Generic.List<string>>;
    internal class ExcDataManager
    {
        /// <summary>
        /// Intiailizes a new instance of the WordMorpher class.
        /// </summary>
        /// <param name="exceptionFileRelativePath">The path of the file containing WordNet exception information to be loaded by the WordMorpherHelper. This path must be relative to the WordNet Resources Directory.</param>
        internal ExcDataManager(string exceptionFileRelativePath)
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
                                         select line.SplitRemoveEmpty(' ').Select(r => r.Replace('_', '-')).ToList();
                var results = exceptionFileLines
                                        .SelectMany(line => line.Select(e => Pair.Create(line[line.Count - 1], line.Take(line.Count - 1))))
                                        .GroupBy(e => e.First, g => g.Second)
                                        .ToDictionary(e => e.Key, e => e.SelectMany(excs => excs).ToList());

                var mostComplex = results.MaxBy(r => r.Value.Count);
                Logger.Log($"Loaded Exception Information from : {exceptionsFileRelativePath}\nMax most complex line parsed: \"{mostComplex.Value} {mostComplex.Key} -> {mostComplex.Value.Count} entries.\"");
                return results;
            }
        }

        private static Utilities.Configuration.IConfig Config => Core.Configuration.Paths.Settings;

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
