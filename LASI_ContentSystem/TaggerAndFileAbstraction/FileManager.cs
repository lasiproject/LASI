using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LASI.Utilities;
using LASI.ContentSystem.TaggerEncapsulation;
using TaggerInterop;
namespace LASI.ContentSystem
{
    /// <summary>
    /// A static class which encapsulates the operations necessary to manage the working directory of the current user progress.
    /// Client code must call the Initialialize method prior to using any of the second methods in this class. 
    /// </summary>
    public static class FileManager
    {
        #region Methods

        /// <summary>
        /// Initializes the FileManager, setting its project directory to the given value.
        /// Automatically loads existing files and sets up input paths
        /// </summary>
        /// <param name="projectDir">The realRoot directory of the current project</param>
        public static void Initialize(string projectDir) {
            ProjectName = projectDir.Substring(projectDir.LastIndexOf('\\') + 1);
            ProjectDir = projectDir;
            InitializeDirProperties();
            CheckProjectDirs();
            Initialized = true;
        }

        private static void InitializeDirProperties() {
            InputFilesDir = ProjectDir + @"\input";
            DocFilesDir = InputFilesDir + @"\doc";
            DocxFilesDir = InputFilesDir + @"\docx";
            PdfFilesDir = InputFilesDir + @"\pdf";
            TextFilesDir = InputFilesDir + @"\text";
            TaggedFilesDir = InputFilesDir + @"\tagged";
            AnalysisDir = ProjectDir + @"\analysis";
            ResultsDir = ProjectDir + @"\results";
        }

        /// <summary>
        /// Checks the existing contents of the current project directory and automatically loads the files it finds. Called by initialize
        /// </summary>
        private static void CheckProjectDirs() {
            CheckProjectDirExistence();
            CheckForInputDirectories();
        }

        /// <summary>
        /// Checks for the existence of the extension statiffied input file project subject-directories and creates them if they do not exist.
        /// </summary>
        private static void CheckForInputDirectories() {
            foreach (var docPath in Directory.EnumerateFiles(DocFilesDir, "*.doc"))
                docFiles.Add(new DocFile(docPath));
            foreach (var docPath in Directory.EnumerateFiles(DocxFilesDir, "*.docx"))
                docXFiles.Add(new DocXFile(docPath));
            foreach (var docPath in Directory.EnumerateFiles(TextFilesDir, "*.txt"))
                textFiles.Add(new TextFile(docPath));
            foreach (var docPath in Directory.EnumerateFiles(PdfFilesDir, "*.pdf"))
                pdfFiles.Add(new PdfFile(docPath));
            foreach (var docPath in Directory.EnumerateFiles(TaggedFilesDir, "*.tagged"))
                taggedFiles.Add(new TaggedFile(docPath));
        }
        /// <summary>
        /// Checks for the existence of the project subject-directories and creates them if they do not exist.
        /// </summary>
        private static void CheckProjectDirExistence() {
            //if (Directory.Exists(ProjectDir)) {
            //    BackupProject();
            //}
            foreach (var path in new[] { 
                ProjectDir,
                InputFilesDir,
                AnalysisDir, 
                ResultsDir, 
                DocFilesDir, 
                DocxFilesDir, 
                PdfFilesDir,
                TaggedFilesDir, 
                TextFilesDir, 
            }) {
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);
            }
        }
        /// <summary>
        /// Returns a value indicating whether a document with the same name as 
        /// the that indicated by the given newPath is already part of the project. 
        /// </summary>
        /// <param name="filePath">A partial or full, extensionless or extensionful, file newPath containing the name of the file to check.</param>
        /// <returns>False if a file with the same name, irrespective of its extension, is part of the project. False otherwise.</returns>
        public static bool HasSimilarFile(string filePath) {
            var fileName = new string(
                filePath.Reverse().
                SkipWhile(c => c != '.').
                Skip(1).TakeWhile(c => c != '\\').
                Reverse().ToArray());
            return !localDocumentNames.Contains(fileName);
        }
        /// <summary>
        /// Returns a value indicating whether a file with the same name as that of the given InputFile, irrespective of its extension, is part of the project. 
        /// </summary>
        /// <param name="inputFile">An an Instance of the InputFile class or one of its descendents.</param>
        /// <returns>False if a file with the same name, irrespective of it'subject extension, is part of the project. False otherwise.</returns>
        public static bool HasSimilarFile(InputFile inputFile) {
            return !localDocumentNames.Contains(inputFile.NameSansExt);
        }


        /// <summary>
        /// Performs the necessary conversions, based on the format of all files within the project.
        /// </summary>
        public static void ConvertAsNeeded() {
            if (!Initialized) {
                throw new FileManagerNotInitializedException();
            }
            ConvertDocFiles();
            ConvertDocxToText();
            TagTextFiles();
        }

        /// <summary>
        /// Asynchronously performs the necessary conversions, based on the format of all files within the project.
        /// </summary>
        public static async Task ConvertAsNeededAsync() {
            if (!Initialized) {
                throw new FileManagerNotInitializedException();
            }
            await Task.WhenAll(
                ConvertDocxToTextAsync(),
                ConvertPdfFilesAsync()
                );
        }



        #region List Insertion Overloads

        private static void AddToTypedList(DocXFile file) {
            docXFiles.Add(file);

        }
        private static void AddToTypedList(DocFile file) {
            docFiles.Add(file);

        }
        private static void AddToTypedList(TextFile file) {
            textFiles.Add(file);

        }
        private static void AddToTypedList(PdfFile file) {
            pdfFiles.Add(file);

        }
        private static void AddToTypedList(TaggedFile file) {
            taggedFiles.Add(file);

        }
        #endregion

        /// <summary>
        /// Removes all files, regardless of extension, whose names do not match any of the names in the provided collection of file path strings.
        /// </summary>
        /// <param name="filesToKeep">collction of file path strings indicating which files are not to be culled. All others will summarilly executed.</param>
        public static void RemoveAllNotIn(IEnumerable<string> filesToKeep) {
            if (!Initialized) {
                throw new FileManagerNotInitializedException();
            }
            RemoveAllNotIn(from f in filesToKeep
                           select f.IndexOf('.') > 0 ? WrapperMap[f.Substring(f.LastIndexOf('.'))](f) : new TextFile(f));
        }
        /// <summary>
        /// Removes all files, regardless of extension, whose names do not match any of the names in the provided collection of InputFile objects.
        /// </summary>
        /// <param name="filesToKeep">collection of InputFile objects indicating which files are not to be culled. All others will summarilly executed.</param>
        public static void RemoveAllNotIn(IEnumerable<InputFile> filesToKeep) {
            if (!Initialized) {
                throw new FileManagerNotInitializedException();
            }
            var toRemove = from f in localDocumentNames
                           where (from k in filesToKeep
                                  where f == k.NameSansExt
                                  select k).Any()
                           select f;
            foreach (var f in toRemove) {
                RemoveAllAlikeFiles(f);
            }
        }

        private static void RemoveAllAlikeFiles(string fileName) {
            textFiles.RemoveAll(f => f.NameSansExt == fileName);
            docFiles.RemoveAll(f => f.NameSansExt == fileName);
            docXFiles.RemoveAll(f => f.NameSansExt == fileName);
            pdfFiles.RemoveAll(f => f.NameSansExt == fileName);
            taggedFiles = (from f in taggedFiles
                           where f.NameSansExt != fileName
                           select f).ToList();
        }
        /// <summary>
        /// Adds the document indicated by the specified path string to the project
        /// </summary>
        /// <param name="path">The path string of the document file to add to the project</param>
        /// <param name="overwrite">True to overwrite existing documents within the project with the same name, False otherwise. Defaults to False</param>
        /// <returns>An InputFile object which acts as a wrapper around the project relative path of the newly added file.</returns>
        public static InputFile AddFile(string path, bool overwrite = true) {
            if (!Initialized) {
                throw new FileManagerNotInitializedException();
            }
            var ext = path.Substring(path.LastIndexOf('.')).ToLower();
            try {
                var originalFile = FileManager.WrapperMap[ext](path);
                var newPath =
                    ext == ".docx" ? DocxFilesDir :
                    ext == ".doc" ? DocFilesDir :
                    ext == ".txt" ? TextFilesDir :
                    ext == ".pdf" ? PdfFilesDir :
                    ext == ".tagged" ? TaggedFilesDir : "";

                newPath += "\\" + originalFile.FileName;

                File.Copy(originalFile.FullPath, newPath, overwrite);
                var newFile = WrapperMap[ext](newPath);
                localDocumentNames.Add(newFile.NameSansExt);
                AddToTypedList(newFile as dynamic);
                return originalFile;
            } catch (KeyNotFoundException ex) {
                throw new UnsupportedFileTypeAddedException(ext, ex);
            }
        }

        /// <summary>
        /// Removes the document represented by InputFile object from the project.
        /// </summary>
        /// <param name="file">The document to remove.</param>
        public static void RemoveFile(InputFile file) {
            if (!Initialized) {
                throw new FileManagerNotInitializedException();
            }
            RemoveAllAlikeFiles(file.NameSansExt);

            RemoveFile(file as dynamic);
        }
        /// <summary>
        /// Removes the document at the provided path from the project.
        /// </summary>
        /// <param name="filePath">The path of the document to remove.</param>
        public static void RemoveFile(string filePath) {
            if (!Initialized) {
                throw new FileManagerNotInitializedException();
            }
            RemoveAllAlikeFiles(filePath);
        }

        private static void RemoveFile(TextFile file) {
            textFiles.Remove(file);
        }
        private static void RemoveFile(DocFile file) {
            docFiles.Remove(file);
        }
        private static void RemoveFile(DocXFile file) {
            docXFiles.Remove(file);
        }
        private static void RemoveFile(PdfFile file) {
            pdfFiles.Remove(file);
        }
        /// <summary>
        /// Converts all of the .doc files it recieves into .docx files
        /// If no arguments are supplied, it will instead convert all yet unconverted .doc files in the project directory
        /// Results are stored in corresponding project directory
        /// </summary>
        /// <param name="files">0 or more instances of the DocFile class which encapsulate .doc files.</param>
        public static void ConvertDocFiles(params DocFile[] files) {
            if (!Initialized) {
                throw new FileManagerNotInitializedException();
            }
            if (files.Length == 0)
                files = docFiles.ToArray();
            foreach (var doc in from d in files
                                where !(from dx in docXFiles
                                        where dx.NameSansExt == d.NameSansExt
                                        select dx).Any()
                                select d) {
                var converted = new DocToDocXConverter(doc).ConvertFile();
                AddFile(converted.FullPath, true);
                File.Delete(converted.FullPath);
            }
        }
        /// <summary>
        /// Asynchronously converts all of the .doc files it recieves into .docx files
        /// If no arguments are supplied, it will instead convert all yet unconverted .doc files in the project directory
        /// Results are stored in corresponding project directory
        /// </summary>
        /// <param name="files">0 or more instances of the DocFile class which encapsulate .doc files.</param>
        public static async Task ConvertDocFilesAsync(params DocFile[] files) {
            if (!Initialized) {
                throw new FileManagerNotInitializedException();
            }
            if (files.Length == 0)
                files = docFiles.ToArray();
            foreach (var doc in from d in files
                                where !(from dx in docXFiles
                                        where dx.NameSansExt == d.NameSansExt
                                        select dx).Any()
                                select d) {
                var converted = await new DocToDocXConverter(doc).ConvertFileAsync();
                AddFile(converted.FullPath, true);
                File.Delete(converted.FullPath);
            }
        }
        /// <summary>
        /// Converts all of the .pdf files it recieves into .txt files
        /// If no arguments are supplied, it will instead convert all yet unconverted .pdf files in the project directory
        /// Results are stored in corresponding project directory
        /// </summary>
        /// <param name="files">0 or more instances of the PdfFile class which encapsulate .pdf files.</param>
        public static void ConvertPdfToText(params PdfFile[] files) {
            if (!Initialized) {
                throw new FileManagerNotInitializedException();
            }
            if (files.Length == 0)
                files = pdfFiles.ToArray();
            foreach (var pdf in from file in files
                                where !(from dx in textFiles
                                        where dx.NameSansExt == file.NameSansExt
                                        select dx).Any()
                                select file) {
                var converted = new PdfToTextConverter(pdf).ConvertFile();
                AddFile(converted.FullPath, true);
                File.Delete(converted.FullPath);
            }
        }
        /// <summary>
        /// Asynchronously converts all of the .pdf files it recieves into .txt files
        /// If no arguments are supplied, it will instead convert all yet unconverted .pdf files in the project directory
        /// Results are stored in corresponding project directory
        /// </summary>
        /// <param name="files">0 or more instances of the PdfFile class which encapsulate .pdf files.</param>
        public static async Task ConvertPdfFilesAsync(params PdfFile[] files) {
            if (!Initialized) {
                throw new FileManagerNotInitializedException();
            }
            if (files.Length == 0)
                files = pdfFiles.ToArray();
            foreach (var pdf in from file in files
                                where !(from dx in textFiles
                                        where dx.NameSansExt == file.NameSansExt
                                        select dx).Any()
                                select file) {
                var converted = await new PdfToTextConverter(pdf).ConvertFileAsync();
                AddFile(converted.FullPath, true);
                File.Delete(converted.FullPath);
            }
        }


        /// <summary>
        /// Converts all of the .docx files it recieves into text files
        /// If no arguments are supplied, it will instead convert all yet unconverted .docx files in the project directory
        /// Results are stored in corresponding project directory
        /// </summary>
        /// <param name="files">0 or more instances of the DocXFile class which encapsulate .docx files</param>
        public static void ConvertDocxToText(params DocXFile[] files) {
            if (!Initialized) {
                throw new FileManagerNotInitializedException();
            }
            if (files.Length == 0)
                files = docXFiles.ToArray();
            foreach (var doc in from d in files
                                where
                                (from dx in textFiles
                                 where dx.NameSansExt == d.NameSansExt
                                 select dx).Count() == 0
                                select d) {
                var converted = new DocxToTextConverter(doc).ConvertFile();
                AddFile(converted.FullPath, true);
                File.Delete(converted.FullPath);
            }
        }
        /// <summary>
        /// Asynchronously converts all of the .docx files it recieves into text files
        /// If no arguments are supplied, it will instead convert all yet unconverted .docx files in the project directory
        /// Results are stored in corresponding project directory
        /// </summary>
        /// <param name="files">0 or more instances of the DocXFile class which encapsulate .docx files</param>
        public static async Task ConvertDocxToTextAsync(params DocXFile[] files) {
            if (!Initialized) {
                throw new FileManagerNotInitializedException();
            }
            if (files.Length == 0)
                files = docXFiles.ToArray();
            foreach (var doc in from d in files
                                where !(from dx in textFiles
                                        where dx.NameSansExt == d.NameSansExt
                                        select dx).Any()
                                select d) {
                await TextConvertAsync(doc);
            }
        }

        private static async Task TextConvertAsync(DocXFile doc) {
            var converted = await new DocxToTextConverter(doc).ConvertFileAsync();

            AddFile(converted.FullPath, true);
            File.Delete(converted.FullPath);
        }
        /// <summary>
        /// Invokes the POS tagger on the text files it recieves into storing the newly tagged files
        /// If no arguments are supplied, it will instead convert all yet untagged text files in the project directory
        /// Results are stored in corresponding project directory
        /// </summary>
        /// <param name="files">0 or more instances of the TextFile class which encapsulate text files</param>
        public static void TagTextFiles(params TextFile[] files) {
            if (!Initialized) {
                throw new FileManagerNotInitializedException();
            }
            if (files.Length == 0)
                files = textFiles.ToArray();
            foreach (var doc in from d in files
                                where !(from dx in taggedFiles
                                        where dx.NameSansExt == d.NameSansExt
                                        select dx).Any()
                                select d) {
                var tagger = new SharpNLPTagger(
                    TaggerMode.TagAndAggregate, doc.FullPath,
                    TaggedFilesDir + "\\" + doc.NameSansExt + ".tagged");
                var tf = new TaggedFile(tagger.ProcessFile());
                AddFile(tf.FullPath, true);
            }
        }
        /// <summary>
        ///Asynchronously Invokes the POS tagger on the text files it recieves into storing the newly tagged files
        /// If no arguments are supplied, it will instead convert all yet untagged text files in the project directory
        /// Results are stored in corresponding project directory
        /// </summary>
        /// <param name="files">0 or more instances of the TextFile class which encapsulate text files</param>
        public static async Task TagTextFilesAsync(params TextFile[] files) {
            if (!Initialized) {
                throw new FileManagerNotInitializedException();
            }
            if (files.Length == 0)
                files = textFiles.ToArray();
            var tasks = (from d in files
                         where !(from dx in taggedFiles
                                 where dx.NameSansExt == d.NameSansExt
                                 select dx).Any()
                         select
                             new SharpNLPTagger(TaggerMode.TagAndAggregate, d.FullPath, TaggedFilesDir + "\\" + d.NameSansExt + ".tagged").ProcessFileAsync()).ToList();


            while (tasks.Any()) {
                var tagged = await Task.WhenAny(tasks);
                taggedFiles.Add(new TaggedFile(await tagged));
                tasks.Remove(tagged);
            }


        }


        /// <summary>
        /// Copies the entire contents of the current project directory to a predetermined, relative path
        /// </summary>
        public static void BackupProject() {
            if (!Initialized) {
                throw new FileManagerNotInitializedException();
            }
            var projd = new DirectoryInfo(ProjectDir);
            var pard = new DirectoryInfo(projd.Parent.FullName);
            var desitination = Directory.CreateDirectory(pard.FullName + "\\backup\\" + ProjectName);
            foreach (var file in new DirectoryInfo(ProjectDir).GetFiles("*", SearchOption.AllDirectories)) {
                if (!Directory.Exists(file.Directory.Name))
                    desitination.CreateSubdirectory(file.Directory.Parent.Name + "\\" + file.Directory.Name);
                file.CopyTo(desitination.FullName + "\\" + file.Directory.Parent.Name + "\\" + file.Directory.Name + "\\" + file.Name, true);
            }
        }
        /// <summary>
        /// Deletes everything from the current Project directory.
        /// </summary>
        public static void DecimateProject() {
            if (!Initialized) {
                throw new FileManagerNotInitializedException();
            }
            try {
                Directory.Delete(ProjectDir, true);
            } catch (IOException e) {
                Output.WriteLine(e.Message);
                Output.WriteLine("Directory could not be found for forced cleabup");
            }
        }
        #endregion

        #region Properties

        /// <summary>
        /// Gets the Absolute Path of Current Project Folder of the current project directory
        /// </summary>
        public static string ProjectDir {
            get;
            private set;
        }
        /// <summary>
        /// Gets the name of the current project.
        /// This will be the project name displayed to the user and it corresponds to the project'subject top level directory
        /// </summary>
        public static string ProjectName {
            get;
            private set;
        }
        /// <summary>
        /// Gets the realRoot of the input file directory
        /// </summary>
        public static string InputFilesDir {
            get;
            private set;
        }
        /// <summary>
        /// Gets the newPath of the analysis directory which stores temporary files during analysis
        /// </summary>
        public static string AnalysisDir {
            get;
            private set;
        }

        /// <summary>
        /// Gets the result files directory
        /// </summary>
        public static string ResultsDir {
            get;
            private set;
        }

        /// <summary>
        /// Gets the .tagged files directory
        /// </summary>
        public static string TaggedFilesDir {
            get;
            private set;
        }
        /// <summary>
        /// Gets the .doc files directory
        /// </summary>
        public static string DocFilesDir {
            get;
            private set;
        }

        /// <summary>
        /// Gets the .pdf files directory
        /// </summary>
        public static string PdfFilesDir {
            get;
            private set;
        }
        /// <summary>
        /// Gets the .docx files directory
        /// </summary>
        public static string DocxFilesDir {
            get;
            private set;
        }

        /// <summary>
        /// Gets the .txt files directory
        /// </summary>
        public static string TextFilesDir {
            get;
            private set;
        }


        /// <summary>
        /// Gets the list of TextFile instances which represent all *.txt files which are included in the project. 
        /// TextFile instances are wrapper objects which provide discrete accessors to relevant *.txt file properties.
        /// </summary>
        public static IReadOnlyList<TextFile> TextFiles {
            get {
                return textFiles;
            }
        }
        /// <summary>
        /// Gets the list of DocXFile instances which represent all *.docx files which are included in the project. 
        /// DocXFile instances are wrapper objects which provide discrete accessors to relevant *.docx file properties.
        /// </summary>
        public static IReadOnlyList<DocXFile> DocXFiles {
            get {
                return docXFiles;
            }
        }
        /// <summary>
        /// Gets the list of DocFile instances which represent all *.doc files which are included in the project. 
        /// DocFile instances are wrapper objects which provide discrete accessors to relevant *.doc file properties.
        /// </summary>
        public static IReadOnlyList<DocFile> DocFiles {
            get {
                return DocFiles;
            }
        }
        /// <summary>
        /// Gets the list of PdfFile instances which represent all *.pdf files which are included in the project. 
        /// PdfFile instances are wrapper objects which provide discrete accessors to relevant *.pdf file properties.
        /// </summary>
        public static IReadOnlyList<PdfFile> PdfFiles {
            get {
                return pdfFiles;
            }
        }
        /// <summary>
        /// Gets the list of TaggedFile instances which represent all *.tagged files which are included in the project. 
        /// TaggedFile instances are wrapper objects which provide discrete accessors to relevant *.tagged file properties.
        /// </summary>
        public static IReadOnlyList<TaggedFile> TaggedFiles {
            get {
                return taggedFiles;
            }
        }
        /// <summary>
        /// Gets a value indicating if the FileManager has been initializes.
        /// </summary>
        public static bool Initialized {
            get;
            private set;
        }

        internal static readonly WrapperDict WrapperMap = new WrapperDict();

        #endregion


        #region Fields



        private static HashSet<string> localDocumentNames = new HashSet<string>();

        private static List<DocFile> docFiles = new List<DocFile>();

        private static List<DocXFile> docXFiles = new List<DocXFile>();

        private static List<PdfFile> pdfFiles = new List<PdfFile>();

        private static List<TextFile> textFiles = new List<TextFile>();

        private static List<TaggedFile> taggedFiles = new List<TaggedFile>();

        #endregion
    }

    #region Helper Types
    class WrapperDict : Dictionary<string, Func<string, InputFile>>
    {
        internal WrapperDict()
            : base(
            new Dictionary<string, Func<string, InputFile>> {
                { "txt" , p => new TextFile(p) },
                { "doc" , p => new DocFile(p) },
                { "docx" , p => new DocXFile(p) },
                { "pdf" , p=> new PdfFile(p) },
                { "tagged" , p => new TaggedFile(p) }
        }) {
        }

        public new Func<string, InputFile> this[string fileExtension] {
            get {
                return base[fileExtension.Replace(".", "")];
            }
        }

    }

    #endregion

    #region Exception Types
    /// <summary>
    /// The exception thrown when methods are invoked or preperties accessed on the FilaManager before a call has been made to initialize it.
    /// </summary>
    [Serializable]
    public class FileManagerNotInitializedException : FileManagerException
    {
        /// <summary>
        /// Initializes a new instance of the FileManagerException class with with its message string set to message.
        /// </summary> 
        public FileManagerNotInitializedException()
            : base("File Manager has not been initialized. No directory context in which to operate.") {
        }

        private FileManagerNotInitializedException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
            : base(info, context) {
        }
    }
    /// <summary>
    /// The Exception thrown when an attempt is made to add a file of an ussuported type to a project.
    /// </summary>
    [Serializable]
    public class UnsupportedFileTypeAddedException : FileManagerException
    {
        /// <summary>
        /// Initializes a new instance of the UnsupportedFileTypeAddedException class with with its message string set to message.
        /// </summary>
        /// <param name="unsupportedFormat">A description of the error. The content of message is intended to be understood</param>
        public UnsupportedFileTypeAddedException(string unsupportedFormat)
            : this(unsupportedFormat, null) {
        }
        /// <summary>
        /// Initializes a new instance of the UnsupportedFileTypeAddedException class with with its message string set to message.
        /// </summary>
        /// <param name="unsupportedFormat">A description of the error. The content of message is intended to be understood</param>
        /// <param name="inner">
        /// The exception that is the cause of the current exception. If the innerException
        /// parameter is not null, the current exception is raised in a catch block that
        /// handles the inner exception.
        /// </param>
        public UnsupportedFileTypeAddedException(string unsupportedFormat, Exception inner)
            : base(
            String.Format(
            "Files of type \"{0}\" are not supported. Supported types are {1}, {2}, {3}, and {4}",
            unsupportedFormat,
            from k in FileManager.WrapperMap.Keys.Take(4)
            select k), inner) {

        }
        /// <summary>
        /// Initializes a new instance of the UnsupportedFileTypeAddedException class with with its message string set to message.
        /// </summary>
        /// <param name="info">
        /// The object that holds the serialized object data about the exception being
        /// thrown.</param>
        /// <param name="context">
        /// The object that holds the serialized object data about the exception being
        /// thrown.</param>
        private UnsupportedFileTypeAddedException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
            : base(info, context) {
        }
    }

    /// <summary>
    /// The base class for all Exceptions thrown by the FileManager.
    /// </summary>
    [Serializable]
    public abstract class FileManagerException : FileSystemException
    {
        /// <summary>
        /// Initializes a new instance of the FileManagerException class with with its message string set to message.
        /// </summary>
        /// <param name="message">A description of the error. The content of message is intended to be understood</param>
        protected FileManagerException(string message)
            : base(message) {
            CollectDirInfo();
        }
        /// <summary>
        /// Initializes a new instance of the FileManagerException class with with its message string set to message.
        /// </summary>
        /// <param name="message">A description of the error. The content of message is intended to be understood</param>
        /// <param name="inner">
        /// The exception that is the cause of the current exception. If the innerException
        /// parameter is not null, the current exception is raised in a catch block that
        /// handles the inner exception.
        /// </param>
        protected FileManagerException(string message, Exception inner)
            : base(message, inner) {
            CollectDirInfo();
        }

        /// <summary>
        /// Initializes a new instance of the FileManagerException class with with its message string set to message.
        /// </summary>
        /// <param name="info">
        /// The object that holds the serialized object data about the exception being
        /// thrown.</param>
        /// <param name="context">
        /// The object that holds the serialized object data about the exception being
        /// thrown.</param>
        protected FileManagerException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
            : base(info, context) {
            CollectDirInfo();
        }
        /// <summary>
        /// Sets data about the current contents of the ProjectDirectory at the time the FileManagerException is constructed.
        /// </summary>
        protected virtual void CollectDirInfo() {
            filesInProjectDirectories = from internalFile in new DirectoryInfo(FileManager.ProjectDir).EnumerateFiles("*", SearchOption.AllDirectories)
                                        select FileManager.WrapperMap[internalFile.Extension](internalFile.FullName);
        }

        private IEnumerable<InputFile> filesInProjectDirectories = new List<InputFile>();
        /// <summary>
        /// Gets data about the contents of the ProjectDirectory when the FileManagerException was constructed.
        /// </summary>
        public IEnumerable<InputFile> FilesInProjectDirectories {
            get {
                return filesInProjectDirectories;
            }
            protected set {
                filesInProjectDirectories = value;
            }
        }

    }

    /// <summary>
    /// The base class for all file related exceptions within the LASI framework.
    /// </summary>
    [Serializable]
    public abstract class FileSystemException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the FileSystemException class with with its message string set to message.
        /// </summary>
        /// <param name="message">A description of the error. The content of message is intended to be understood</param>
        protected FileSystemException(string message)
            : base(message) {

        }
        /// <summary>
        /// Initializes a new instance of the FileSystemException class with with its message string set to message.
        /// </summary>
        /// <param name="message">A description of the error. The content of message is intended to be understood</param>
        /// <param name="inner">
        /// The exception that is the cause of the current exception. If the innerException
        /// parameter is not null, the current exception is raised in a catch block that
        /// handles the inner exception.
        /// </param>
        protected FileSystemException(string message, Exception inner)
            : base(message, inner) {

        }

        /// <summary>
        /// Initializes a new instance of the FileSystemException class with with its message string set to message.
        /// </summary>
        /// <param name="info">
        /// The object that holds the serialized object data about the exception being
        /// thrown.</param>
        /// <param name="context">
        /// The object that holds the serialized object data about the exception being
        /// thrown.</param>
        protected FileSystemException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
            : base(info, context) {
        }
    }

    #endregion

    #region Internal Extension Method Providers

    internal static class InputFileExtensions
    {
        public static dynamic AsDynamic(this InputFile inputFile) {
            return inputFile;
        }
    }

    #endregion

}




