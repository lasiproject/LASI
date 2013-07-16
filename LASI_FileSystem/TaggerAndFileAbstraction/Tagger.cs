using LASI.Algorithm;
using LASI.Algorithm.DocumentConstructs;
using LASI.FileSystem;
using LASI.FileSystem.TaggerEncapsulation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LASI.FileSystem
{
    /// <summary>
    /// Provides dynamic, non file driven, access to the functionality of the POS Tagger and TaggedFileParser.
    /// </summary>
    public static class Tagger
    {
        /// <summary>
        /// Parses any number of untagged strings into a new Document instance.
        /// </summary>
        /// <param name="strs">The untagged, raw strings to parse.</param>
        /// <returns>The contents of the raw strings composed into a fully reified LASI.Algorithm.DocumentConstruct.Document instance.</returns>
        /// <remarks>No files are created when calling this function.</remarks>
        public static Document DocumentFromRaw(params string[] strs) {
            return DocumentFromTagged(TaggedFromRaw(strs));
        }
        /// <summary>
        /// Parses the contents of a raw, untagged TextFile into a new Document instance.
        /// </summary>
        /// <param name="strs">The raw, untagged TextFile to parse.</param>
        /// <returns>The contents of the TextFile composed into a fully reified LASI.Algorithm.DocumentConstruct.Document instance.</returns> 
        public static Document DocumentFromRaw(TextFile txt) {
            var doc = new TaggedFileParser(new SharpNLPTagger(TaggerMode, txt.FullPath).ProcessFile()).LoadDocument();
            doc.FileName = txt.NameSansExt;
            return doc;
        }
        /// <summary>
        /// Asynchronously parses the contents of a raw, untagged TextFile into a new Document instance.
        /// </summary>
        /// <param name="strs">The raw, untagged TextFile to parse.</param>
        /// <returns>The contents of the TextFile composed into a fully reified LASI.Algorithm.DocumentConstruct.Document instance.</returns> 
        public static async Task<Document> DocumentFromRawAsync(TextFile txt) {
            var doc = await new TaggedFileParser(await new SharpNLPTagger(TaggerMode, txt.FullPath).ProcessFileAsync()).LoadDocumentAsync();
            doc.FileName = txt.NameSansExt;
            return doc;
        }
        public static Document DocumentFromRaw(IRawTextSource txt) {
            var doc = DocumentFromRaw(txt.GetText());
            doc.FileName = txt.Name;
            return doc;
        }
        public static async Task<Document> DocumentFromRawAsync(IRawTextSource txt) {
            var doc = DocumentFromRaw(await txt.GetTextAsync());
            doc.FileName = txt.Name;
            return doc;
        }


        public static Document DocumentFromTagged(ITaggedTextSource tagged) {
            var doc = new TaggedFileParser(tagged.GetText()).LoadDocument();
            doc.FileName = tagged.Name;
            return doc;
        }
        public static async Task<Document> DocumentFromTaggedAsync(ITaggedTextSource tagged) {
            var doc = await new TaggedFileParser(tagged.GetText()).LoadDocumentAsync();
            doc.FileName = tagged.Name;
            return doc;
        }

        /// <summary>
        /// Parses any number of untagged strings with the tagger and returns a single string containing the tagged result.
        /// </summary>
        /// <param name="strs">The untagged, raw strings to parse.</param>
        /// <returns>A single string containing the tagged result. The form is identical to what it would be appear in a tagged file.</returns>
        /// <remarks>No files are created when calling this function.</remarks>
        public static string TaggedFromRaw(params string[] strs) {
            return new QuickTagger(TaggerMode).TagTextSource(String.Join(" ", strs));
        }
        /// <summary>
        /// Parses any number of pre-tagged strings into a new Document instance.
        /// </summary>
        /// <param name="tagged">The pre-tagged, raw strings to parse.</param>
        /// <returns>The contents of the pre-tagged strings composed into a fully LASI.Algorithm.DocumentConstruct.Document instance.</returns>
        /// <remarks>No files are created when calling this function.</remarks>
        public static Document DocumentFromTagged(params string[] tagged) {
            return new TaggedFileParser(String.Join(" ", tagged)).LoadDocument();
        }
        /// <summary>
        /// Parses the contents of a TaggedFile into a new Document instance.
        /// </summary>
        /// <param name="taggedFile">The TaggedFile whose contents to parse</param>
        /// <returns>The contents of the TaggedFile composed into a fully reified LASI.Algorithm.DocumentConstruct.Document instance.</returns>
        public static async Task<Document> DocumentFromTaggedAsync(TaggedFile taggedFile) {
            var doc = await new TaggedFileParser(taggedFile).LoadDocumentAsync();
            doc.FileName = taggedFile.Name;
            return doc;
        }



        public static ITaggedTextSource TaggedFromRaw(IRawTextSource textSource) {
            return new QuickTagger(TaggerMode).TagTextSource(textSource);

        }
        public static async Task<ITaggedTextSource> TaggedFromRawAsync(IRawTextSource textSource) {
            return await new QuickTagger(TaggerMode).TagTextSourceAsync(textSource);

        }


        public static async Task<TaggedFile> TaggedFromRawAsync(TextFile inputFile) {
            return await new SharpNLPTagger(TaggerMode, inputFile.FullPath).ProcessFileAsync();
        }
        public static TaggedFile TaggedFromRaw(TextFile inputFile) {
            return new SharpNLPTagger(TaggerMode, inputFile.FullPath).ProcessFile();
        }


        /// <summary>
        /// Gets or sets the default mode the tagger will operate under. The default value is set to TagAndAggregate
        /// </summary>
        private static TaggerMode TaggerMode {
            get;
            set;
        }


        static Tagger() {
            TaggerMode = TaggerMode.TagAndAggregate;
        }


    }
}
