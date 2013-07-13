using System;
using System.Configuration;
using System.IO;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using LASI.FileSystem.TaggerEncapsulation;

namespace SharpNatrualLanguageProcessing
{

    public sealed class QuickTagger : SharpNLPTagger
    {
        public QuickTagger(TaggerMode option)
            : base(option) {

        }
        public LASI.Algorithm.ITaggedTextSource TagTextSource(LASI.Algorithm.IRawTextSource source) {
            SourceText = base.PreProcessText(source.GetText());
            return new LASI.Algorithm.TaggedTextChunk(base.ParseViaTaggingMode(), source.DataName);

        }
        public async Task<LASI.Algorithm.ITaggedTextSource> TagTextSourceAsync(LASI.Algorithm.IRawTextSource source) {
            SourceText = base.PreProcessText(source.GetText());
            return new LASI.Algorithm.TaggedTextChunk(await base.ParseViaTaggingModeAsync(), source.DataName);

        }
        public string TagString(string source) {
            SourceText = base.PreProcessText(source);
            return base.ParseViaTaggingMode();

        }



    }
}