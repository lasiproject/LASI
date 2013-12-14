using LASI;
using LASI.Core;
using LASI.Core.DocumentStructures;
using LASI.ContentSystem;
using LASI.ContentSystem.TaggerEncapsulation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaggerInterop;

namespace LASI.ContentSystem
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
        public static Document DocumentFromRaw(IEnumerable<string> strs) {
            return DocumentFromTagged(TaggedFromRaw(new RawTextFragment(strs, string.Empty)));
        }
        /// <summary>
        /// Parses the contents of a raw, untagged TextFile into a new Document instance.
        /// </summary>
        /// <param name="txt">The raw, untagged TextFile to parse.</param>
        /// <returns>The contents of the TextFile composed into a fully reified LASI.Algorithm.DocumentConstruct.Document instance.</returns> 
        public static Document DocumentFromRaw(TextFile txt) {
            var doc = new TaggedSourceParser(new TaggedFile(new SharpNLPTagger(TaggerMode, txt.FullPath).ProcessFile())).LoadDocument();
            doc.Name = txt.NameSansExt;
            return doc;
        }
        /// <summary>
        /// Parses the contents of a raw, untagged DocXFile into a new Document instance.
        /// </summary>
        /// <param name="docx">The raw, untagged DocXFile to parse.</param>
        /// <returns>The contents of the DocXFile composed into a fully reified LASI.Algorithm.DocumentConstruct.Document instance.</returns> 
        public static Document DocumentFromDocX(DocXFile docx) {
            var txt = new DocxToTextConverter(docx).ConvertFile();
            var doc = new TaggedSourceParser(new TaggedFile(new SharpNLPTagger(TaggerMode, txt.FullPath).ProcessFile())).LoadDocument();
            doc.Name = txt.NameSansExt;
            return doc;
        }
        /// <summary>
        /// Parses the contents of a raw, untagged PdfFile into a new Document instance.
        /// </summary>
        /// <param name="pdf">The raw, untagged PdfFile to parse.</param>
        /// <returns>The contents of the PdfFile composed into a fully reified LASI.Algorithm.DocumentConstruct.Document instance.</returns> 
        public static Document DocumentFromPDF(PdfFile pdf) {
            var txt = new PdfToTextConverter(pdf).ConvertFile();
            var doc = new TaggedSourceParser(new TaggedFile(new SharpNLPTagger(TaggerMode, txt.FullPath).ProcessFile())).LoadDocument();
            doc.Name = txt.NameSansExt;
            return doc;
        }
        /// <summary>
        /// Parses the contents of a raw, untagged DocFile into a new Document instance.
        /// </summary>
        /// <param name="doc">The raw, untagged DocFile to parse.</param>
        /// <returns>The contents of the DocFile composed into a fully reified LASI.Algorithm.DocumentConstruct.Document instance.</returns> 
        public static Document DocumentFromDoc(DocFile doc) {
            var docx = new DocToDocXConverter(doc).ConvertFile() as DocXFile;
            var txt = new DocxToTextConverter(docx).ConvertFile();
            var result = new TaggedSourceParser(new TaggedFile(new SharpNLPTagger(TaggerMode, txt.FullPath).ProcessFile())).LoadDocument();
            result.Name = txt.NameSansExt;
            return result;
        }
        /// <summary>
        /// Asynchronously parses the contents of a raw, untagged TextFile into a new Document instance.
        /// </summary>
        /// <param name="txt">The raw, untagged TextFile to parse.</param>
        /// <returns>The contents of the TextFile composed into a fully reified LASI.Algorithm.DocumentConstruct.Document instance.</returns> 
        public static async Task<Document> DocumentFromRawAsync(TextFile txt) {
            var doc = await new TaggedSourceParser(new TaggedFile(await new SharpNLPTagger(TaggerMode, txt.FullPath).ProcessFileAsync())).LoadDocumentAsync();
            doc.Name = txt.NameSansExt;
            return doc;
        }
        /// <summary>
        /// Parses the contents of an IRawTextSource containing raw, untagged text. into a new Document instance.
        /// </summary>
        /// <param name="textSource">The IRawTextSource containing raw, untagged text.</param>
        /// <returns>The contents of the TextFile composed into a fully reified LASI.Algorithm.DocumentConstruct.Document instance.</returns> 
        public static Document DocumentFromRaw(IUntaggedTextSource textSource) {
            var doc = DocumentFromTagged(new TaggedTextFragment(new QuickTagger(TaggerMode).TagTextSource(textSource.GetText()), textSource.Name));
            doc.Name = textSource.Name;
            return doc;
        }
        /// <summary>
        /// Asynchronously parses the contents of an IRawTextSource containing raw, untagged text and returns a Task&lt;Document&gt; representing the ongoing asynchronous operation.
        /// </summary>
        /// <param name="textSource">The IRawTextSource containing raw, untagged text.</param>
        /// <returns>A Task&lt;Document&gt; which will contain the source text composed into a fully reified LASI.Algorithm.DocumentConstruct.Document instance.</returns> 
        public static async Task<Document> DocumentFromRawAsync(IUntaggedTextSource textSource) {
            var doc = DocumentFromTagged(new TaggedTextFragment(await new QuickTagger(TaggerMode).TagTextSourceAsync(textSource.GetText()), textSource.Name));
            doc.Name = textSource.Name;
            return doc;
        }

        /// <summary>
        /// Parses the contents of an ITaggedTextSource containing tagged strings to parse into a new Document instance.
        /// </summary>
        /// <param name="tagged">The ITaggedTextSource containing tagged strings to parse.</param>
        /// <returns>The contents of the ITaggedTextSource composed into a fully LASI.Algorithm.DocumentConstruct.Document instance.</returns> 
        public static Document DocumentFromTagged(ITaggedTextSource tagged) {
            var doc = new TaggedSourceParser(tagged).LoadDocument();
            doc.Name = tagged.Name;
            return doc;
        }
        /// <summary>
        /// Asynchronously parses the contents of an ITaggedTextSource containing tagged strings into a new Document instance.
        /// </summary>
        /// <param name="tagged">The ITaggedTextSource containing tagged strings to parse.</param>
        /// <returns>A Task&lt;Document&gt; which will contain the source text composed into a fully reified LASI.Algorithm.DocumentConstruct.Document instance.</returns> 
        public static async Task<Document> DocumentFromTaggedAsync(ITaggedTextSource tagged) {
            var doc = await new TaggedSourceParser(tagged).LoadDocumentAsync();
            doc.Name = tagged.Name;
            return doc;
        }

        /// <summary>
        /// Parses any number of untagged strings with the tagger and returns a single string containing the tagged result.
        /// </summary>
        /// <param name="strs">The untagged, raw strings to parse.</param>
        /// <returns>A single string containing the tagged result. The form is identical to what it would be appear in a tagged file.</returns>
        /// <remarks>No files are created when calling this function.</remarks>
        public static string TaggedFromRaw(IEnumerable<string> strs) {
            return new QuickTagger(TaggerMode).TagTextSource(string.Join(" ", strs));
        }
        /// <summary>
        /// Parses any number of pre-tagged strings into a new Document instance.
        /// </summary>
        /// <param name="tagged">The pre-tagged, raw strings to parse.</param>
        /// <returns>The contents of the pre-tagged strings composed into a fully LASI.Algorithm.DocumentConstruct.Document instance.</returns>
        /// <remarks>No files are created when calling this function.</remarks>
        public static Document DocumentFromTagged(IEnumerable<string> tagged) {
            return new TaggedSourceParser(new TaggedTextFragment(tagged, "anonymous")).LoadDocument();
        }
        /// <summary>
        /// Parses the contents of a TaggedFile into a new Document instance.
        /// </summary>
        /// <param name="taggedFile">The TaggedFile whose contents to parse</param>
        /// <returns>A Task&lt;Document&gt; which, when awaited, will yield the contents of the TaggedFile composed into a fully reified LASI.Algorithm.DocumentConstruct.Document instance.</returns>
        public static async Task<Document> DocumentFromTaggedAsync(TaggedFile taggedFile) {
            var doc = await new TaggedSourceParser(taggedFile).LoadDocumentAsync();
            doc.Name = taggedFile.Name;
            return doc;
        }


        /// <summary>
        /// Parses the contents of an IRawTextSource with the tagger and returns an ITaggedTextSource containing the result.
        /// </summary>
        /// <param name="textSource">The IRawTextSource containing untagged, raw strings to parse.</param>
        /// <returns>An ITaggedTextSource containing the result. The form is identical to what it would be appear in a tagged file.</returns> 
        public static ITaggedTextSource TaggedFromRaw(IUntaggedTextSource textSource) {
            return new TaggedTextFragment(new QuickTagger(TaggerMode).TagTextSource(textSource.GetText()), textSource.Name);
        }
        /// <summary>
        /// Asynchronously parses the contents of an IRawTextSource with the tagger and returns a Task&lt;ITaggedTextSource&gt; containing the result.
        /// </summary>
        /// <param name="textSource">The IRawTextSource containing untagged, raw strings to parse.</param>
        /// <returns>A Task&lt;ITaggedTextSource&gt; which will contain the result. The form is identical to what it would be appear in a tagged file.</returns> 
        public static async Task<ITaggedTextSource> TaggedFromRawAsync(IUntaggedTextSource textSource) {
            return new TaggedTextFragment(await new QuickTagger(TaggerMode).TagTextSourceAsync(textSource.GetText()), textSource.Name);

        }


        /// <summary>
        /// Gets or sets the default mode the tagger will operate under. The default value is set to TagAndAggregate
        /// </summary>
        public static TaggerMode TaggerMode {
            get;
            set;
        }

        static Tagger() {
            TaggerMode = TaggerMode.TagAndAggregate;
        }


    }
}
