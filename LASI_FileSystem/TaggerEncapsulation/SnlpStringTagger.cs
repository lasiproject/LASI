using System;
using System.Configuration;
using System.IO;
using System.Text;
using System.Linq;
namespace SharpNLPTaggingModule
{

    public class StringTagger : SharpNLPTagger
    {
        public StringTagger(TaggingOption option)
            : base(option) {

        }

        public string TagString(string source) {
            SourceText = source;
            return base.ParseViaTaggingMode();

        }


    }
}