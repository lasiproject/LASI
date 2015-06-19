using System.Collections.Generic;
using System.Threading.Tasks;
using LASI.Core;
using TaggerInterop;

namespace LASI.Content.Tagging
{
    /// <summary>
    /// Provides dynamic, non file driven, access to the functionality of the POS Tagger and TaggedFileParser.
    /// </summary>
    public class Tagger
    {
        /// <summary>
        /// Initializes a new instance of the Tagger Class.
        /// </summary>
        public Tagger() : this(TaggerMode.TagAndAggregate) { }

        /// <summary>
        /// Initializes a new instance of the Tagger Class which will use the specified <see cref="TaggerInterop.TaggerMode"/>.
        /// </summary>
        public Tagger(TaggerMode taggerMode)
        {
            TaggerMode = taggerMode;
        }
        /// <summary>
        /// Parses the contents of a raw, untagged TxtFile into a new Document instance.
        /// </summary>
        /// <param name="txt">The raw, untagged TxtFile to parse.</param>
        /// <returns>
        /// The contents of the TxtFile composed into a fully reified
        /// <see cref="Document"/> instance.
        /// </returns>
        public Document DocumentFromTxt(TxtFile txt) => DocumentFromRaw(txt);

        /// <summary>
        /// Parses the contents of a raw, untagged DocFile into a new Document instance.
        /// </summary>
        /// <param name="doc">The raw, untagged DocFile to parse.</param>
        /// <returns>
        /// The contents of the DocFile composed into a fully reified
        /// <see cref="Document"/> instance.
        /// </returns>
        public Document DocumentFromDoc(DocFile doc)
        {
            var docx = new DocToDocXConverter(doc).ConvertFile() as DocXFile;
            var txt = new DocxToTextConverter(docx).ConvertFile();
            return new TaggedSourceParser(new TaggedFile(new SharpNLPTagger(this.TaggerMode, txt.FullPath).ProcessFile())).LoadDocument(txt.NameSansExt);
        }

        /// <summary>
        /// Parses the contents of a raw, untagged DocXFile into a new Document instance.
        /// </summary>
        /// <param name="docx">The raw, untagged DocXFile to parse.</param>
        /// <returns>
        /// The contents of the DocXFile composed into a fully reified
        /// <see cref="Document"/> instance.
        /// </returns>
        public Document DocumentFromDocX(DocXFile docx)
        {
            var txt = new DocxToTextConverter(docx).ConvertFile();
            return new TaggedSourceParser(new TaggedFile(new SharpNLPTagger(this.TaggerMode, txt.FullPath).ProcessFile())).LoadDocument(txt.NameSansExt);
        }

        /// <summary>
        /// Parses the contents of a raw, untagged PdfFile into a new Document instance.
        /// </summary>
        /// <param name="pdf">The raw, untagged PdfFile to parse.</param>
        /// <returns>
        /// The contents of the PdfFile composed into a fully reified
        /// <see cref="Document"/> instance.
        /// </returns>
        public Document DocumentFromPdf(PdfFile pdf)
        {
            var txt = new PdfToTextConverter(pdf).ConvertFile();
            return new TaggedSourceParser(new TaggedFile(new SharpNLPTagger(this.TaggerMode, txt.FullPath).ProcessFile())).LoadDocument(txt.NameSansExt);
        }

        /// <summary>
        /// Parses any number of untagged strings into a new Document instance.
        /// </summary>
        /// <param name="strs">The untagged, raw strings to parse.</param>
        /// <returns>
        /// The contents of the raw strings composed into a fully reified
        /// <see cref="Document"/> instance.
        /// </returns>
        /// <remarks>No files are created when calling this function.</remarks>
        public Document DocumentFromRaw(IEnumerable<string> strs) => DocumentFromTagged(TaggedFromRaw(new RawTextFragment(strs, string.Empty)));

        /// <summary>
        /// Parses the contents of an IRawTextSource containing raw, untagged text. into a new
        /// Document instance.
        /// </summary>
        /// <param name="raw">The IRawTextSource containing raw, untagged text.</param>
        /// <returns>
        /// The contents of the TextFile composed into a fully reified
        /// <see cref="Document"/> instance.
        /// </returns>
        public Document DocumentFromRaw(IRawTextSource raw) => DocumentFromTagged(new TaggedTextFragment(new QuickTagger(TaggerMode).TagTextSource(raw.LoadText()), raw.Name));

        /// <summary>
        /// Asynchronously parses the contents of an IRawTextSource containing raw, untagged text
        /// and returns a Task&lt;Document&gt; representing the ongoing asynchronous operation.
        /// </summary>
        /// <param name="raw">The IRawTextSource containing raw, untagged text.</param>
        /// <returns>
        /// A Task&lt;Document&gt; which will contain the source text composed into a fully reified
        /// <see cref="Document"/> instance.
        /// </returns>
        public async Task<Document> DocumentFromRawAsync(IRawTextSource raw)
        {
            var rawText = await raw.LoadTextAsync();
            var taggedText = await new QuickTagger(TaggerMode).TagTextSourceAsync(rawText);
            return await new TaggedSourceParser(new TaggedTextFragment(taggedText, raw.Name)).LoadDocumentAsync(raw.Name);
        }

        /// <summary>
        /// Parses the contents of an ITaggedTextSource containing tagged strings to parse into a
        /// new Document instance.
        /// </summary>
        /// <param name="tagged">The ITaggedTextSource containing tagged strings to parse.</param>
        /// <returns>
        /// The contents of the ITaggedTextSource composed into a fully
        /// <see cref="Document"/> instance.
        /// </returns>
        public Document DocumentFromTagged(ITaggedTextSource tagged) => new TaggedSourceParser(tagged).LoadDocument(tagged.Name);

        /// <summary>
        /// Parses any number of pre-tagged strings into a new Document instance.
        /// </summary>
        /// <param name="tagged">The pre-tagged, raw strings to parse.</param>
        /// <returns>
        /// The contents of the pre-tagged strings composed into a fully
        /// <see cref="Document"/> instance.
        /// </returns>
        /// <remarks>No files are created when calling this function.</remarks>
        public Document DocumentFromTagged(IEnumerable<string> tagged) => new TaggedSourceParser(new TaggedTextFragment(tagged, "anonymous")).LoadDocument();

        /// <summary>
        /// Asynchronously parses the contents of an ITaggedTextSource containing tagged strings
        /// into a new Document instance.
        /// </summary>
        /// <param name="tagged">The ITaggedTextSource containing tagged strings to parse.</param>
        /// <returns>
        /// A Task&lt;Document&gt; which will contain the source text composed into a fully reified
        /// <see cref="Document"/> instance.
        /// </returns>
        public async Task<Document> DocumentFromTaggedAsync(ITaggedTextSource tagged) => await new TaggedSourceParser(tagged).LoadDocumentAsync(tagged.Name);

        /// <summary>
        /// Parses any number of untagged strings with the tagger and returns a single string
        /// containing the tagged result.
        /// </summary>
        /// <param name="strs">The untagged, raw strings to parse.</param>
        /// <returns>
        /// A single string containing the tagged result. The form is identical to what it would be
        /// appear in a tagged file.
        /// </returns>
        /// <remarks>No files are created when calling this function.</remarks>
        public string TaggedFromRaw(IEnumerable<string> strs) => new QuickTagger(TaggerMode).TagTextSource(string.Join(" ", strs));

        /// <summary>
        /// Parses the contents of an IRawTextSource with the tagger and returns an
        /// ITaggedTextSource containing the result.
        /// </summary>
        /// <param name="textSource">The IRawTextSource containing untagged, raw strings to parse.</param>
        /// <returns>
        /// An ITaggedTextSource containing the result. The form is identical to what it would be
        /// appear in a tagged file.
        /// </returns>
        public ITaggedTextSource TaggedFromRaw(IRawTextSource textSource) => new TaggedTextFragment(new QuickTagger(TaggerMode).TagTextSource(textSource.LoadText()), textSource.Name);

        /// <summary>
        /// Asynchronously parses the contents of an IRawTextSource with the tagger and returns a
        /// Task&lt;ITaggedTextSource&gt; containing the result.
        /// </summary>
        /// <param name="textSource">The IRawTextSource containing untagged, raw strings to parse.</param>
        /// <returns>
        /// A Task&lt;ITaggedTextSource&gt; which will contain the result. The form is identical to
        /// what it would be appear in a tagged file.
        /// </returns>
        public async Task<ITaggedTextSource> TaggedFromRawAsync(IRawTextSource textSource) => new TaggedTextFragment(await new QuickTagger(TaggerMode).TagTextSourceAsync(textSource.LoadText()), textSource.Name);

        /// <summary>
        /// Gets or sets the default mode the tagger will operate under. The default value is set to TagAndAggregate
        /// </summary>
        public TaggerMode TaggerMode { get; }
    }
}