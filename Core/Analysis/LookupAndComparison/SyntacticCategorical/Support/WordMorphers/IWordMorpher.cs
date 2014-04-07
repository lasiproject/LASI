using System;
using System.Collections.Generic;
namespace LASI.Core.Analysis.LookupAndComparison.Syntactic.Support
{
    /// <summary>
    /// Provides root extraction and derrived form gerenation of words of th Given type.
    /// </summary>
    /// <typeparam name="TWord">Any type which implements LASI.Core.Word</typeparam>
    interface IWordMorpher<TWord> where TWord : Word
    {
        /// <summary>
        /// Returns the base form of the given type of word.
        /// If the word is already in its base form, the text content of the Word will simply be returned.
        /// </summary>
        /// <param name="word">An instance TWord from whose text a root is to be extracted.</param>
        /// <returns>The base form of the given type of word.
        /// If the word is already in its base form, the text content of the Word will simply be returned.</returns>
        string FindRoot(TWord word);

        /// <summary>
        /// Returns the base form of the given type of word.
        /// If the word is already in its base form, the text content of the Word will simply be returned.
        /// </summary>
        /// <param name="wordText">A string whose text represents the lexical form a word of the given type.</param>
        /// <returns>The base form of the given type of word.
        /// If the word is already in its base form, the text content of the Word will simply be returned.</returns>
        string FindRoot(string wordText);

        /// <summary>
        /// Computes and returns the list of all conjugated forms of the word specified by the given text.
        /// </summary>
        /// <param name="search"></param>
        /// <returns>The collection of all conjugated forms of the word specified by the given text.</returns>
        /// <remarks>By convention the resulting collection should include the root of the originally specified word.</remarks>
        IEnumerable<string> GetLexicalForms(string search);
        ///// <summary>
        ///// Computes and returns the list of all conjugated forms of the specified word.
        ///// </summary>
        ///// <param name="search">The word whose lexical forms are to be generated and produced.</param>
        ///// <returns>The collection of all conjugated forms of the specified word.</returns>
        ///// <remarks>By convention the resulting collection should include the root of the originally specified word.</remarks>
        //IEnumerable<string> GetLexicalForms(TWord search);
    }
}
