using System.IO;

namespace LASI.App.Extensions
{
    /// <summary>
    /// Provides extension methods for working with the <see cref="FileSystemInfo"/>, <see cref="FileInfo"/>, and <see cref="DirectoryInfo"/> types.
    /// </summary>
    public static class FileSystemInfoExtensions
    {
        /// <summary>
        /// Deconstructs the specified <see cref="FileInfo"/> into a triple of name, extension and directory values.
        /// </summary>
        /// <param name="file">The <see cref="FileInfo"/> to deconstruct.</param>
        /// <returns>A triple of name, extension and directory values.</returns>
        public static(string name, string extension, string directory) Deconstruct(this FileInfo file) => (file.Name, file.Extension, file.Directory.FullName);
    }
}