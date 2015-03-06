using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LASI.Utilities;

namespace LASI.Content
{
    using SerializationInfo = System.Runtime.Serialization.SerializationInfo;
    using StreamingContext = System.Runtime.Serialization.StreamingContext;
    /// <summary>
    /// The exception thrown when a conversion between document file formats fails.
    /// </summary>
    [Serializable]
    public class FileConversionFailureException : FileManagerException
    {
        /// <summary>
        /// Initializes a new instance of the FileConversionFailureException with a message based on the supplied fileName, source type, and target type
        /// </summary>
        /// <param name="fileName">The name of the file for which conversion failed.</param>
        /// <param name="sourceType">The extension of the source file format.</param>
        /// <param name="targetType">The extension of the target file format</param>
        internal FileConversionFailureException(string fileName, string sourceType, string targetType) : base($"File conversion failed\nCould not convert {fileName} from {sourceType} to {targetType}.") { }
        /// <summary>
        /// Initializes a new instance of the FileConversionFailureException with a message based on the supplied fileName, source type, and target type
        /// </summary>
        /// <param name="fileName">The name of the file for which conversion failed.</param>
        /// <param name="sourceType">The extension of the source file format.</param>
        /// <param name="targetType">The extension of the target file format</param>
        /// <param name="inner">The exception that is the cause of the current exception. If the innerException
        /// parameter is not null, the current exception is raised in a catch block that
        /// handles the inner exception.</param>
        internal FileConversionFailureException(string fileName, string sourceType, string targetType, Exception inner) : base($"File conversion failed\nCould not convert {fileName} from {sourceType} to {targetType}.", inner) { }
        private FileConversionFailureException(string message) : base(message) { }
        private FileConversionFailureException(string message, Exception inner) : base(message, inner) { }
        private FileConversionFailureException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
    /// <summary>
    /// The exception thrown when methods are invoked or preperties accessed on the FileManager before a call has been made to initialize it.
    /// </summary>
    [Serializable]
    public class FileManagerNotInitializedException : FileManagerException
    {
        /// <summary>
        /// Initializes a new instance of the FileManagerNotInitializedException class with its message string set to message.
        /// </summary> 
        internal FileManagerNotInitializedException()
            : base("File Manager has not been initialized. No directory context in which to operate.")
        {
        }

        private FileManagerNotInitializedException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }


    #region Derived Types
    /// <summary>
    /// The Exception thrown when an attempt is made to add a file of an unsupported type to a project.
    /// </summary>
    [Serializable]
    public class UnsupportedFileTypeAddedException : FileManagerException
    {
        private static readonly string SupportedFormats = FileManager.WrapperMap.SupportedFormats.Format();
        private static string FormatMessage(string unsupportedFormat) => $"Files of type \"{unsupportedFormat}\" are not supported. Supported types are {SupportedFormats}";

        /// <summary>
        /// Initializes a new instance of the UnsupportedFileTypeAddedException class with its message string set to message.
        /// </summary>
        /// <param name="unsupportedFormat">A description of the error. The content of message is intended to be understood</param>
        public UnsupportedFileTypeAddedException(string unsupportedFormat)
            : base(FormatMessage(unsupportedFormat))
        {
        }
        /// <summary>
        /// Initializes a new instance of the UnsupportedFileTypeAddedException class with its message string set to message.
        /// </summary>
        /// <param name="unsupportedFormat">A description of the error. The content of message is intended to be understood</param>
        /// <param name="inner">
        /// The exception that is the cause of the current exception. If the innerException
        /// parameter is not null, the current exception is raised in a catch block that
        /// handles the inner exception.
        /// </param>
        public UnsupportedFileTypeAddedException(string unsupportedFormat, Exception inner)
            : base(FormatMessage(unsupportedFormat), inner)
        {

        }
        /// <summary>
        ///Initializes a new instance of the UnsupportedFileTypeAddedException class with serialized data.
        /// </summary>
        /// <param name="info">
        /// The object that holds the serialized object data about the exception being
        /// thrown.</param>
        /// <param name="context">
        /// The object that holds the serialized object data about the exception being
        /// thrown.</param>
        protected UnsupportedFileTypeAddedException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }

    /// <summary>
    /// The base class for all Exceptions thrown by the FileManager.
    /// </summary>
    [Serializable]
    public abstract class FileManagerException : ContentFileException
    {
        /// <summary>
        /// Initializes a new instance of the FileManagerException class with its message string set to message.
        /// </summary>
        /// <param name="message">A description of the error. The content of message is intended to be understood</param>
        protected FileManagerException(string message)
            : base(message)
        {
            CollectDirInfo();
        }
        /// <summary>
        /// Initializes a new instance of the FileManagerException class with its message string set to message.
        /// </summary>
        /// <param name="message">A description of the error. The content of message is intended to be understood</param>
        /// <param name="inner">
        /// The exception that is the cause of the current exception. If the innerException
        /// parameter is not null, the current exception is raised in a catch block that
        /// handles the inner exception.
        /// </param>
        protected FileManagerException(string message, Exception inner)
            : base(message, inner)
        {
            CollectDirInfo();
        }

        /// <summary>
        ///Initializes a new instance of the FileManagerException class with serialized data.
        /// </summary>
        /// <param name="info">
        /// The object that holds the serialized object data about the exception being
        /// thrown.</param>
        /// <param name="context">
        /// The object that holds the serialized object data about the exception being
        /// thrown.</param>
        protected FileManagerException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            CollectDirInfo();
        }
        /// <summary>
        /// Sets the System.Runtime.Serialization.SerializationInfo with information about the exception.
        /// </summary>
        /// <param name="info">
        /// The System.Runtime.Serialization.SerializationInfo that holds the serialized
        /// object data about the exception being thrown.
        /// </param>
        /// <param name="context">
        ///  The System.Runtime.Serialization.StreamingContext that contains contextual
        ///  information about the source or destination.
        /// </param>
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
        }
        /// <summary>
        /// Sets data about the current contents of the ProjectDirectory at the time the FileManagerException is constructed.
        /// </summary>
        protected void CollectDirInfo()
        {
            if (FileManager.Initialized && FileManager.TxtFiles.Any())
                filesInProjectDirectories = new DirectoryInfo(FileManager.ProjectDirectory).EnumerateFiles("*", SearchOption.AllDirectories)
                                            .Select(di => FileManager.WrapperMap[di.Extension](di.FullName)).DefaultIfEmpty();
        }

        private IEnumerable<InputFile> filesInProjectDirectories = new List<InputFile>();
        /// <summary>
        /// Gets data about the contents of the ProjectDirectory when the FileManagerException was constructed.
        /// </summary>
        public IEnumerable<InputFile> FilesInProjectDirectories
        {
            get
            {
                return filesInProjectDirectories;
            }
            protected set
            {
                filesInProjectDirectories = value;
            }
        }

    }

    /// <summary>
    /// The base class for all file related exceptions within the LASI framework.
    /// </summary>
    [Serializable]
    public abstract class ContentFileException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the FileSystemException class with its message string set to message.
        /// </summary>
        /// <param name="message">A description of the error. The content of message is intended to be understood</param>
        protected ContentFileException(string message)
            : base(message)
        {

        }
        /// <summary>
        /// Initializes a new instance of the FileSystemException class with its message string set to message.
        /// </summary>
        /// <param name="message">A description of the error. The content of message is intended to be understood</param>
        /// <param name="inner">
        /// The exception that is the cause of the current exception. If the innerException
        /// parameter is not null, the current exception is raised in a catch block that
        /// handles the inner exception.
        /// </param>
        protected ContentFileException(string message, Exception inner)
            : base(message, inner)
        {

        }

        /// <summary>
        /// Initializes a new instance of the FileSystemException class with serialized data.
        /// </summary>
        /// <param name="info">
        /// The object that holds the serialized object data about the exception being
        /// thrown.</param>
        /// <param name="context">
        /// The object that holds the serialized object data about the exception being
        /// thrown.</param>
        protected ContentFileException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }

    #endregion


}
