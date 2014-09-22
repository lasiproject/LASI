using LASI.Core.DocumentStructures;
using LASI.ContentSystem;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LASI.Interop
{
    using TaggerImpl = LASI.ContentSystem.TaggerEncapsulation.Tagger;
    /// <summary>
    /// Provides dynamic, non file driven, access to the functionality of the POS Tagger and TaggedFileParser.
    /// </summary>
    public class Tagger
    {
        private readonly TaggerImpl taggerImpl = new TaggerImpl();
        /// <summary>
        /// Parses any number of untagged strings into a new Document instance.
        /// </summary>
        /// <param name="strs">The untagged, raw strings to parse.</param>
        /// <returns>The contents of the raw strings composed into a fully reified LASI.Algorithm.DocumentConstruct.Document instance.</returns>
        /// <remarks>No files are created when calling this function.</remarks>
        public virtual Document DocumentFromRaw(IEnumerable<string> strs) { return taggerImpl.DocumentFromRaw(strs); }
        /// <summary>
        /// Parses the contents of a raw, untagged TextFile into a new Document instance.
        /// </summary>
        /// <param name="txt">The raw, untagged TextFile to parse.</param>
        /// <returns>The contents of the TextFile composed into a fully reified LASI.Algorithm.DocumentConstruct.Document instance.</returns> 
        public virtual Document DocumentFromRaw(TxtFile txt) { return taggerImpl.DocumentFromRaw(txt); }
        /// <summary>
        /// Parses the contents of a raw, untagged DocXFile into a new Document instance.
        /// </summary>
        /// <param name="docx">The raw, untagged DocXFile to parse.</param>
        /// <returns>The contents of the DocXFile composed into a fully reified LASI.Algorithm.DocumentConstruct.Document instance.</returns> 
        public virtual Document DocumentFromDocX(DocXFile docx) { return taggerImpl.DocumentFromDocX(docx); }
        /// <summary>
        /// Parses the contents of a raw, untagged PdfFile into a new Document instance.
        /// </summary>
        /// <param name="pdf">The raw, untagged PdfFile to parse.</param>
        /// <returns>The contents of the PdfFile composed into a fully reified LASI.Algorithm.DocumentConstruct.Document instance.</returns> 
        public virtual Document DocumentFromPDF(PdfFile pdf) { return taggerImpl.DocumentFromPDF(pdf); }
        /// <summary>
        /// Parses the contents of a raw, untagged DocFile into a new Document instance.
        /// </summary>
        /// <param name="doc">The raw, untagged DocFile to parse.</param>
        /// <returns>The contents of the DocFile composed into a fully reified LASI.Algorithm.DocumentConstruct.Document instance.</returns> 
        public virtual Document DocumentFromDoc(DocFile doc) { return taggerImpl.DocumentFromDoc(doc); }
        /// <summary>
        /// Asynchronously parses the contents of a raw, untagged TextFile into a new Document instance.
        /// </summary>
        /// <param name="txt">The raw, untagged TextFile to parse.</param>
        /// <returns>The contents of the TextFile composed into a fully reified LASI.Algorithm.DocumentConstruct.Document instance.</returns> 
        public virtual async Task<Document> DocumentFromRawAsync(TxtFile txt) { return await taggerImpl.DocumentFromRawAsync(txt); }
        /// <summary>
        /// Parses the contents of an IRawTextSource containing raw, untagged text. into a new Document instance.
        /// </summary>
        /// <param name="textSource">The IRawTextSource containing raw, untagged text.</param>
        /// <returns>The contents of the TextFile composed into a fully reified LASI.Algorithm.DocumentConstruct.Document instance.</returns> 
        public virtual Document DocumentFromRaw(IRawTextSource textSource) { return taggerImpl.DocumentFromRaw(textSource); }
        /// <summary>
        /// Asynchronously parses the contents of an IRawTextSource containing raw, untagged text and returns a Task&lt;Document&gt; representing the ongoing asynchronous operation.
        /// </summary>
        /// <param name="textSource">The IRawTextSource containing raw, untagged text.</param>
        /// <returns>A Task&lt;Document&gt; which will contain the source text composed into a fully reified LASI.Algorithm.DocumentConstruct.Document instance.</returns> 
        public virtual async Task<Document> DocumentFromRawAsync(IRawTextSource textSource) { return await taggerImpl.DocumentFromRawAsync(textSource); }
        /// <summary>
        /// Parses the contents of an ITaggedTextSource containing tagged strings to parse into a new Document instance.
        /// </summary>
        /// <param name="tagged">The ITaggedTextSource containing tagged strings to parse.</param>
        /// <returns>The contents of the ITaggedTextSource composed into a fully LASI.Algorithm.DocumentConstruct.Document instance.</returns> 
        public virtual Document DocumentFromTagged(ITaggedTextSource tagged) { return DocumentFromTagged(tagged); }
        /// <summary>
        /// Asynchronously parses the contents of an ITaggedTextSource containing tagged strings into a new Document instance.
        /// </summary>
        /// <param name="tagged">The ITaggedTextSource containing tagged strings to parse.</param>
        /// <returns>A Task&lt;Document&gt; which will contain the source text composed into a fully reified LASI.Algorithm.DocumentConstruct.Document instance.</returns> 
        public virtual async Task<Document> DocumentFromTaggedAsync(ITaggedTextSource tagged) { return await taggerImpl.DocumentFromTaggedAsync(tagged); }
        /// <summary>
        /// Parses any number of untagged strings with the tagger and returns a single string containing the tagged result.
        /// </summary>
        /// <param name="strs">The untagged, raw strings to parse.</param>
        /// <returns>A single string containing the tagged result. The form is identical to what it would be appear in a tagged file.</returns>
        /// <remarks>No files are created when calling this function.</remarks>
        public virtual string TaggedFromRaw(IEnumerable<string> strs) { return taggerImpl.TaggedFromRaw(strs); }
        /// <summary>
        /// Parses any number of pre-tagged strings into a new Document instance.
        /// </summary>
        /// <param name="tagged">The pre-tagged, raw strings to parse.</param>
        /// <returns>The contents of the pre-tagged strings composed into a fully LASI.Algorithm.DocumentConstruct.Document instance.</returns>
        /// <remarks>No files are created when calling this function.</remarks>
        public virtual Document DocumentFromTagged(IEnumerable<string> tagged) { return DocumentFromTagged(tagged); }
        /// <summary>
        /// Parses the contents of a TaggedFile into a new Document instance.
        /// </summary>
        /// <param name="taggedFile">The TaggedFile whose contents to parse</param>
        /// <returns>A Task&lt;Document&gt; which, when awaited, will yield the contents of the TaggedFile composed into a fully reified LASI.Algorithm.DocumentConstruct.Document instance.</returns>
        public virtual async Task<Document> DocumentFromTaggedAsync(TaggedFile taggedFile) { return await DocumentFromTaggedAsync(taggedFile); }
        /// <summary>
        /// Parses the contents of an IRawTextSource with the tagger and returns an ITaggedTextSource containing the result.
        /// </summary>
        /// <param name="textSource">The IRawTextSource containing untagged, raw strings to parse.</param>
        /// <returns>An ITaggedTextSource containing the result. The form is identical to what it would be appear in a tagged file.</returns> 
        public virtual ITaggedTextSource TaggedFromRaw(IRawTextSource textSource) { return taggerImpl.TaggedFromRaw(textSource); }
        /// <summary>
        /// Asynchronously parses the contents of an IRawTextSource with the tagger and returns a Task&lt;ITaggedTextSource&gt; containing the result.
        /// </summary>
        /// <param name="textSource">The IRawTextSource containing untagged, raw strings to parse.</param>
        /// <returns>A Task&lt;ITaggedTextSource&gt; which will contain the result. The form is identical to what it would be appear in a tagged file.</returns> 
        public virtual async Task<ITaggedTextSource> TaggedFromRawAsync(IRawTextSource textSource) { return await taggerImpl.TaggedFromRawAsync(textSource); }
    }
}
