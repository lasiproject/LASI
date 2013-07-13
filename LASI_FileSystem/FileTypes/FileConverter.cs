using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LASI.FileSystem
{
    /// <summary>
    /// The base class from which file format conversion objects are derrived.
    /// Provides a small set of common methods, properties, and attributes which all file conversion objects.
    /// Any new file converters should be derrived from this base class.
    /// <see cref="DocToDocXConverter"/>
    /// <see cref="DocxToTextConverter"/>
    /// </summary>
    public abstract class FileConverter
    {
        /// <summary>
        /// The location where the converted file will be saved.
        /// </summary>
        protected string destinationDir;
        /// <summary>
        /// The location of the source file.
        /// </summary>
        protected string sourcePath;
        /// <summary>
        /// Initializes a new instance of the FileConverter class.
        /// </summary>
        /// <param name="infile">The file to convert.</param>
        protected FileConverter(InputFile infile) {
            sourcePath = infile.FullPath;
            destinationDir = infile.Directory;
            Original = infile;
        }
        /// <summary>
        /// Initializes a new instance of the FileConverter class.
        /// </summary>
        /// <param name="infile">The file to convert.</param>
        /// <param name="targetDir">The location in which to save the converted file.</param>
        protected FileConverter(InputFile infile, string targetDir) {
            sourcePath = infile.FullPath;
            destinationDir = targetDir;
            Original = infile;
        }

        /// <summary>
        /// When overriden in a derrived class, this method invokes the file conversion routine on the file which the instance governs.
        /// </summary>
        /// <returns>An instance of input file representing the file in its converted format.</returns>
        public abstract InputFile ConvertFile();

        /// <summary>
        /// When overridden in a dirrrived class, this method invokes the file conversion routine asynchronously, gernerally in a serparate thread.
        /// Use with the await operator in an asnyc method to retrieve the new file object and specify a continuation function to be executed when the conversion is complete.
        /// </summary>
        /// <returns>A Task of InputFile object which functions as a proxy for the actual InputFile while the conversion routine is in progress.
        /// Access the internal input file encapsulated by the Task by using syntax such as : var file = await myConverter.ConvertFileAsync()
        /// </returns>
        public abstract Task<InputFile> ConvertFileAsync();


        /// <summary>
        /// Gets the document object which is to be converted to the from format
        /// </summary>
        public InputFile Original {
            get;
            protected set;
        }
        /// <summary>
        /// Gets the document object which is the fruit of the conversion process
        /// This additional method of accessing the new document is primarily provided to facilitate asynchronous programming
        /// and any access attempts before the conversion is complete will raise a NullReferenceException.
        /// </summary>
        public abstract InputFile Converted {
            get;
            protected set;
        }
    }
}
