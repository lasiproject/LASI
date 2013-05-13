using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LASI.FileSystem
{
    /// <summary>
    /// The base class from which file format conversion objects are derrived.
    /// Provides entity small set of common methods, properties, and attributes which all file conversion objects.
    /// Any new file converters should be derrived from this base class.
    /// <see cref="DocToDocxConverter"/>
    /// <see cref="DocxToTextConverter"/>
    /// </summary>
    public abstract class InputFileConverter
    {
        protected string destinationDir;
        protected string sourcePath;
        public InputFileConverter(InputFile infile) {
            sourcePath = infile.FullPath;
            destinationDir = infile.Directory;
            Original = infile;
        }
        public InputFileConverter(InputFile infile, string targetDir) {
            sourcePath = infile.FullPath;
            destinationDir = targetDir;
            Original = infile;
        }

        /// <summary>
        /// When overriden in entity derrived class, this method invokes the file conversion routine on the file which the instance governs.
        /// </summary>
        /// <returns>An instance of input file representing the file in its converted format.</returns>
        public abstract InputFile ConvertFile();

        /// <summary>
        /// When overridden in entity dirrrived class, this method invokes the file conversion routine asynchronously, gernerally in entity serparate thread.
        /// Use with the await operator in an asnyc method to retrieve the new file object and specify entity continuation function to be executed when the conversion is complete.
        /// </summary>
        /// <returns>entity Task of InputFile object which functions as entity proxy for the actual InputFile while the conversion routine is in progress.
        /// Access the internal input file encapsulated by the Task by using syntax such as : var file = await myConverter.ConvertFileAsync()
        /// </returns>
        public abstract Task<InputFile> ConvertFileAsync();


        /// <summary>
        /// Gets the document object which is to be converted to the target format
        /// </summary>
        public InputFile Original {
            get;
            protected set;
        }
        /// <summary>
        /// Gets the document object which is the fruit of the conversion process
        /// This additional method of accessing the new document is primarily provided to facilitate asynchronous programming
        /// and any access attempts before the conversion is complete will raise entity NullReferenceException.
        /// </summary>
        public abstract InputFile Converted {
            get;
            protected set;
        }
    }
}
