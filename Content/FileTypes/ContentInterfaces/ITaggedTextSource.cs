using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LASI.Content
{
    /// <summary>
    /// Exposes the behaviors of a source of tagged text and an associated name
    /// </summary>
    public interface ITaggedTextSource
    {
        /// <summary>
        /// Returns a string containing all of the tagged text in the ITaggedTextSource.
        /// </summary>
        /// <returns>A string containing all of the tagged text in the ITaggedTextSource.</returns>
        string GetText();
        /// <summary>
        /// Returns a Task&lt;string&gt; which when awaited yields all of the tagged text in the ITaggedTextSource.
        /// </summary>
        /// <returns>A Task&lt;string&gt; which when awaited yields all of the tagged text in the ITaggedTextSource.</returns>
        Task<string> GetTextAsync();
        /// <summary>
        /// Gets the name associated with the ITaggedTextSource.
        /// </summary>
        string Name { get; }
    }
}
