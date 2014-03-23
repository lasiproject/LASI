using System;
namespace LASI.Interop
{
    /// <summary>
    /// Provides dynamic, non file driven, access to the functionality of the POS Tagger and TaggedFileParser.
    /// </summary>
    public interface ITagger
    {
        /// <summary>
        /// Parses the contents of a raw, untagged DocFile into a new Document instance.
        /// </summary>
        /// <param name="doc">The raw, untagged DocFile to parse.</param>
        /// <returns>The contents of the DocFile composed into a fully reified LASI.Algorithm.DocumentConstruct.Document instance.</returns> 
        LASI.Core.DocumentStructures.Document DocumentFromDoc(LASI.ContentSystem.DocFile doc);
        /// <summary>
        /// Parses the contents of a raw, untagged DocXFile into a new Document instance.
        /// </summary>
        /// <param name="docx">The raw, untagged DocXFile to parse.</param>
        /// <returns>The contents of the DocXFile composed into a fully reified LASI.Algorithm.DocumentConstruct.Document instance.</returns> 
        LASI.Core.DocumentStructures.Document DocumentFromDocX(LASI.ContentSystem.DocXFile docx);
        /// <summary>
        /// Parses the contents of a raw, untagged PdfFile into a new Document instance.
        /// </summary>
        /// <param name="pdf">The raw, untagged PdfFile to parse.</param>
        /// <returns>The contents of the PdfFile composed into a fully reified LASI.Algorithm.DocumentConstruct.Document instance.</returns> 
        LASI.Core.DocumentStructures.Document DocumentFromPDF(LASI.ContentSystem.PdfFile pdf);
        /// <summary>
        /// Parses the contents of an IRawTextSource containing raw, untagged text. into a new Document instance.
        /// </summary>
        /// <param name="textSource">The IRawTextSource containing raw, untagged text.</param>
        /// <returns>The contents of the TextFile composed into a fully reified LASI.Algorithm.DocumentConstruct.Document instance.</returns> 
        LASI.Core.DocumentStructures.Document DocumentFromRaw(LASI.ContentSystem.IRawTextSource textSource);
        /// <summary>
        /// Parses the contents of a raw, untagged TextFile into a new Document instance.
        /// </summary>
        /// <param name="txt">The raw, untagged TextFile to parse.</param>
        /// <returns>The contents of the TextFile composed into a fully reified LASI.Algorithm.DocumentConstruct.Document instance.</returns> 
        LASI.Core.DocumentStructures.Document DocumentFromRaw(LASI.ContentSystem.TxtFile txt);
        /// <summary>
        /// Parses any number of untagged strings into a new Document instance.
        /// </summary>
        /// <param name="strs">The untagged, raw strings to parse.</param>
        /// <returns>The contents of the raw strings composed into a fully reified LASI.Algorithm.DocumentConstruct.Document instance.</returns>
        /// <remarks>No files are created when calling this function.</remarks>
        LASI.Core.DocumentStructures.Document DocumentFromRaw(System.Collections.Generic.IEnumerable<string> strs);
        /// <summary>
        /// Asynchronously parses the contents of an IRawTextSource containing raw, untagged text and returns a Task&lt;Document&gt; representing the ongoing asynchronous operation.
        /// </summary>
        /// <param name="textSource">The IRawTextSource containing raw, untagged text.</param>
        /// <returns>A Task&lt;Document&gt; which will contain the source text composed into a fully reified LASI.Algorithm.DocumentConstruct.Document instance.</returns> 
        System.Threading.Tasks.Task<LASI.Core.DocumentStructures.Document> DocumentFromRawAsync(LASI.ContentSystem.IRawTextSource textSource);
        /// <summary>
        /// Asynchronously parses the contents of a raw, untagged TextFile into a new Document instance.
        /// </summary>
        /// <param name="txt">The raw, untagged TextFile to parse.</param>
        /// <returns>The contents of the TextFile composed into a fully reified LASI.Algorithm.DocumentConstruct.Document instance.</returns> 
        System.Threading.Tasks.Task<LASI.Core.DocumentStructures.Document> DocumentFromRawAsync(LASI.ContentSystem.TxtFile txt);
        /// <summary>
        /// Parses the contents of an ITaggedTextSource containing tagged strings to parse into a new Document instance.
        /// </summary>
        /// <param name="tagged">The ITaggedTextSource containing tagged strings to parse.</param>
        /// <returns>The contents of the ITaggedTextSource composed into a fully LASI.Algorithm.DocumentConstruct.Document instance.</returns> 
        LASI.Core.DocumentStructures.Document DocumentFromTagged(LASI.ContentSystem.ITaggedTextSource tagged);
        /// <summary>
        /// Parses any number of pre-tagged strings into a new Document instance.
        /// </summary>
        /// <param name="tagged">The pre-tagged, raw strings to parse.</param>
        /// <returns>The contents of the pre-tagged strings composed into a fully LASI.Algorithm.DocumentConstruct.Document instance.</returns>
        /// <remarks>No files are created when calling this function.</remarks>
        LASI.Core.DocumentStructures.Document DocumentFromTagged(System.Collections.Generic.IEnumerable<string> tagged);
        /// <summary>
        /// Asynchronously parses the contents of an ITaggedTextSource containing tagged strings into a new Document instance.
        /// </summary>
        /// <param name="tagged">The ITaggedTextSource containing tagged strings to parse.</param>
        /// <returns>A Task&lt;Document&gt; which will contain the source text composed into a fully reified LASI.Algorithm.DocumentConstruct.Document instance.</returns> 
        System.Threading.Tasks.Task<LASI.Core.DocumentStructures.Document> DocumentFromTaggedAsync(LASI.ContentSystem.ITaggedTextSource tagged);
        /// <summary>
        /// Parses the contents of a TaggedFile into a new Document instance.
        /// </summary>
        /// <param name="taggedFile">The TaggedFile whose contents to parse</param>
        /// <returns>A Task&lt;Document&gt; which, when awaited, will yield the contents of the TaggedFile composed into a fully reified LASI.Algorithm.DocumentConstruct.Document instance.</returns>
        System.Threading.Tasks.Task<LASI.Core.DocumentStructures.Document> DocumentFromTaggedAsync(LASI.ContentSystem.TaggedFile taggedFile);
        /// <summary>
        /// Parses the contents of an IRawTextSource with the tagger and returns an ITaggedTextSource containing the result.
        /// </summary>
        /// <param name="textSource">The IRawTextSource containing untagged, raw strings to parse.</param>
        /// <returns>An ITaggedTextSource containing the result. The form is identical to what it would be appear in a tagged file.</returns> 
        LASI.ContentSystem.ITaggedTextSource TaggedFromRaw(LASI.ContentSystem.IRawTextSource textSource);
        /// <summary>
        /// Parses any number of untagged strings with the tagger and returns a single string containing the tagged result.
        /// </summary>
        /// <param name="strs">The untagged, raw strings to parse.</param>
        /// <returns>A single string containing the tagged result. The form is identical to what it would be appear in a tagged file.</returns>
        /// <remarks>No files are created when calling this function.</remarks>
        string TaggedFromRaw(System.Collections.Generic.IEnumerable<string> strs);
        /// <summary>
        /// Asynchronously parses the contents of an IRawTextSource with the tagger and returns a Task&lt;ITaggedTextSource&gt; containing the result.
        /// </summary>
        /// <param name="textSource">The IRawTextSource containing untagged, raw strings to parse.</param>
        /// <returns>A Task&lt;ITaggedTextSource&gt; which will contain the result. The form is identical to what it would be appear in a tagged file.</returns> 
        System.Threading.Tasks.Task<LASI.ContentSystem.ITaggedTextSource> TaggedFromRawAsync(LASI.ContentSystem.IRawTextSource textSource);
    }
}
