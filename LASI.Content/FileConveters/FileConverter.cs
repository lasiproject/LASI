using System.Threading.Tasks;
using LASI.Content.FileTypes;

namespace LASI.Content.FileConveters
{
    /// <summary>
    /// The base class from which file format conversion objects are derived.
    /// Provides a small set of common methods, properties, and attributes which all file conversion objects.
    /// Any new file converters should be derived from this base class.
    /// <seealso cref="DocToDocXConverter"/>
    /// <seealso cref="DocxToTextConverter"/>
    /// </summary>
    public abstract class FileConverter<TSource, TDestination>
        where TSource : InputFile
        where TDestination : InputFile
    {
        /// <summary>
        /// The location of the source file.
        /// </summary>
        protected string sourcePath;
        /// <summary>
        /// Initializes a new instance of the FileConverter class.
        /// </summary>
        /// <param name="infile">The file to convert.</param>
        protected FileConverter(TSource infile)
        {
            Original = infile;
            sourcePath = infile.FullPath;
            DestinationDir = infile.Directory;
        }
        /// <summary>
        /// Initializes a new instance of the FileConverter class.
        /// </summary>
        /// <param name="infile">The file to convert.</param>
        /// <param name="targetDir">The location in which to save the converted file.</param>
        protected FileConverter(TSource infile, string targetDir)
        {
            sourcePath = infile.FullPath;
            DestinationDir = targetDir;
            Original = infile;
        }

        /// <summary>
        /// When overridden in a derived class, this method invokes the file conversion routine on the file which the instance governs.
        /// </summary>
        /// <returns>An instance of input file representing the file in its converted format.</returns>
        public abstract TDestination ConvertFile();

        /// <summary>
        /// When overridden in a derived class, this method invokes the file conversion routine asynchronously, generally in a separate thread.
        /// Use with the await operator in an async method to retrieve the new file object and specify a continuation function to be executed when the conversion is complete.
        /// </summary>
        /// <returns>A Task&lt;InputFile&gt; object which functions as a proxy for the actual InputFile while the conversion routine is in progress.
        /// Access the internal input file encapsulated by the Task by using syntax such as : var file = await myConverter.ConvertFileAsync()
        /// </returns>
        public abstract Task<TDestination> ConvertFileAsync();


        /// <summary>
        /// Gets the document which is to be converted to the destination format
        /// </summary>
        public TSource Original { get; protected set; }
        /// <summary>
        /// Gets the document object which is the fruit of the conversion process
        /// This additional method of accessing the new document is primarily provided to facilitate asynchronous programming
        /// and any access attempts before the conversion is complete will raise a NullReferenceException.
        /// </summary>
        public TDestination Converted { get; protected set; }

        /// <summary>
        /// The location where the converted file will be saved.
        /// </summary>
        protected string DestinationDir { get; }
    }
}
