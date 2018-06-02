using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using LASI.Content.FileTypes;
using static System.Linq.Enumerable;
using SerializationInfo = System.Runtime.Serialization.SerializationInfo;
using StreamingContext = System.Runtime.Serialization.StreamingContext;

namespace LASI.Content.Exceptions
{
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
        protected FileManagerException(string message) : base(message) => CollectDirInfo();
        /// <summary>
        /// Initializes a new instance of the FileManagerException class with its message string set to message and containing the provided inner exception.
        /// </summary>
        /// <param name="message">A description of the error. The content of message is intended to be understood</param>
        /// <param name="inner">
        /// The exception that is the cause of the current exception. If the innerException
        /// parameter is not null, the current exception is raised in a catch block that
        /// handles the inner exception.
        /// </param>
        protected FileManagerException(string message, Exception inner) : base(message, inner) => CollectDirInfo();

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
            : base(info, context) => CollectDirInfo();

        /// <inheritdoc />
        public FileManagerException() { }

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
            {
                FilesInProjectDirectories = new DirectoryInfo(FileManager.ProjectDirectory)
                    .EnumerateFiles("*", SearchOption.AllDirectories)
                    .Select(file => FileManager.WrapperMap[file.Extension](file.FullName))
                    .DefaultIfEmpty();
            }
        }
        /// <summary>
        /// Gets data about the contents of the ProjectDirectory when the FileManagerException was constructed.
        /// </summary>
        public IEnumerable<InputFile> FilesInProjectDirectories { get; protected set; } = Empty<InputFile>();
    }
}
