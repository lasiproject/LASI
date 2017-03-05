using LASI.Utilities;
using LASI.Utilities.Specialized.Enhanced.IList.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using TaggerInterop;
using System.Collections.Immutable;

namespace LASI.Content
{
    /// <summary>
    /// A static class which encapsulates the operations necessary to manage the working directory of the current user progress.
    /// Client code must call the Initialize method prior to using any of the second methods in this class. 
    /// </summary>
    public static class FileManager
    {
        #region Methods

        #region Initialization

        /// <summary>
        /// Initializes the FileManager, setting its project directory to the given value.
        /// Automatically loads existing files and sets up input paths
        /// </summary>
        /// <param name="projectDirectoryPath">The realRoot directory of the current project</param>
        public static void Initialize(string projectDirectoryPath)
        {
            ProjectName = projectDirectoryPath.Substring(projectDirectoryPath.LastIndexOf('\\') + 1);
            ProjectDirectory = projectDirectoryPath;
            BuildSubDirectories();
            MapInputDirectories();
            Initialized = true;
        }

        /// <summary>
        /// Checks for the existence of the project subject-directories creating them as needed.
        /// </summary>
        private static void BuildSubDirectories()
        {
            new[] {
                ProjectDirectory,
                InputFilesDirectory,
                AnalysisDirectory,
                ResultsDirectory,
                DocFilesDirectory,
                DocxFilesDirectory,
                PdfFilesDirectory,
                TaggedFilesDirectory,
                TxtFilesDirectory,
            }.ForEach(path =>
            {
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
            });
        }

        /// <summary>
        /// Checks for the existence of the extension stratified input file project subject-directories and creates them if they do not exist.
        /// </summary>
        private static void MapInputDirectories()
        {
            foreach (var docPath in Directory.EnumerateFiles(DocFilesDirectory, "*.doc"))
                docFiles = docFiles.Add(new DocFile(docPath));
            foreach (var docPath in Directory.EnumerateFiles(DocxFilesDirectory, "*.docx"))
                docXFiles = docXFiles.Add(new DocXFile(docPath));
            foreach (var docPath in Directory.EnumerateFiles(TxtFilesDirectory, "*.txt"))
                txtFiles = txtFiles.Add(new TxtFile(docPath));
            foreach (var docPath in Directory.EnumerateFiles(PdfFilesDirectory, "*.pdf"))
                pdfFiles = pdfFiles.Add(new PdfFile(docPath));
            foreach (var docPath in Directory.EnumerateFiles(TaggedFilesDirectory, "*.tagged"))
                taggedFiles = taggedFiles.Add(new TaggedFile(docPath));
        }
        #endregion

        #region Insertion Overloads

        /// <summary>
        /// Adds the document indicated by the specified path string to the project
        /// </summary>
        /// <param name="file">The document file to add to the project</param>
        /// 
        /// <returns>An InputFile object which acts as a wrapper around the project relative path of the newly added file.</returns>
        public static TFile AddFile<TFile>(TFile file) where TFile : InputFile => AddFile(file.FullPath) as TFile;

        /// <summary>
        /// Adds the document indicated by the specified path string to the project
        /// </summary>
        /// <param name="path">The path string of the document file to add to the project</param>
        /// 
        /// <returns>An InputFile object which acts as a wrapper around the project relative path of the newly added file.</returns>
        public static InputFile AddFile(string path)
        {
            ThrowIfUninitialized();
            var extension = Path.GetExtension(path).ToLower();
            try
            {
                var originalFile = WrapperMap[extension](path);
                var newPath =
                    extension == ".doc" ? DocFilesDirectory :
                    extension == ".docx" ? DocxFilesDirectory :
                    extension == ".txt" ? TxtFilesDirectory :
                    extension == ".pdf" ? PdfFilesDirectory :
                    extension == ".tagged" ? TaggedFilesDirectory : string.Empty;

                newPath += "\\" + originalFile.FileName;

                File.Copy(originalFile.FullPath, newPath, overwrite: true);
                var newFile = WrapperMap[extension](newPath);

                AddToTypedList(newFile as dynamic);
                return newFile;
            }
            catch (UnsupportedFileTypeException e)
            {
                throw new UnsupportedFileTypeException(extension, e);
            }
        }

        private static void AddToTypedList(DocFile file) => docFiles = docFiles.Add(file);
        private static void AddToTypedList(DocXFile file) => docXFiles = docXFiles.Add(file);
        private static void AddToTypedList(TxtFile file) => txtFiles = txtFiles.Add(file);
        private static void AddToTypedList(PdfFile file) => pdfFiles = pdfFiles.Add(file);
        private static void AddToTypedList(TaggedFile file) => taggedFiles = taggedFiles.Add(file);

        #endregion

        #region Existence Checking and Removal

        /// <summary>
        /// Returns a value indicating whether a document with the same name as 
        /// the that indicated by the given newPath is already part of the project. 
        /// </summary>
        /// <param name="filePath">A partial or full, extension-less or extension-full, file newPath containing the name of the file to check.</param>
        /// <returns>False if a file with the same name, irrespective of its extension, is part of the project. False otherwise.</returns>
        public static bool HasSimilarFile(string filePath)
        {
            var fileName = filePath.Contains('\\') ? System.IO.Path.GetFileNameWithoutExtension(filePath) : filePath.Substring(0, filePath.IndexOf('.') >= 0 ? filePath.IndexOf('.') : filePath.Length);
            return AllDocumentNames.Contains(fileName);
        }
        /// <summary>
        /// Returns a value indicating whether a file with the same name as that of the given InputFile, irrespective of its extension, is part of the project. 
        /// </summary>
        /// <param name="inputFile">An Instance of the InputFile class or one of its descendants.</param>
        /// <returns>False if a file with the same name, irrespective of it's extension, is part of the project. False otherwise.</returns>
        public static bool HasSimilarFile(InputFile inputFile) => HasSimilarFile(inputFile.FullPath);

        /// <summary>
        /// Removes the document represented by InputFile object from the project.
        /// </summary>
        /// <param name="file">The document to remove.</param>
        public static void RemoveFile(InputFile file)
        {
            ThrowIfUninitialized();
            RemoveAllAlikeFiles(file.NameSansExt);
            RemoveFile(file as dynamic);
        }

        /// <summary>
        /// Removes the document at the provided path from the project.
        /// </summary>
        /// <param name="filePath">The path of the document to remove.</param>
        public static void RemoveFile(string filePath)
        {
            ThrowIfUninitialized();
            RemoveAllAlikeFiles(filePath);
        }

        private static void RemoveAllAlikeFiles(string fileName)
        {
            txtFiles = txtFiles.RemoveAll(f => f.NameSansExt.Contains(fileName));
            docFiles = docFiles.RemoveAll(f => f.NameSansExt.Contains(fileName));
            docXFiles = docXFiles.RemoveAll(f => f.NameSansExt.Contains(fileName));
            pdfFiles = pdfFiles.RemoveAll(f => f.NameSansExt.Contains(fileName));
            taggedFiles = taggedFiles.RemoveAll(f => f.NameSansExt.Contains(fileName));
        }
        private static void RemoveFile(TxtFile file) => txtFiles = txtFiles.Remove(file);

        private static void RemoveFile(DocFile file) => docFiles = docFiles.Remove(file);

        private static void RemoveFile(DocXFile file) => docXFiles = docXFiles.Remove(file);

        private static void RemoveFile(PdfFile file) => pdfFiles = pdfFiles.Remove(file);

        #endregion

        #region File Conversion

        /// <summary>
        /// Asynchronously performs the necessary conversions, based on the format of all files within the project.
        /// </summary>
        public static async Task ConvertAsNeededAsync()
        {
            ThrowIfUninitialized();
            await Task.WhenAll(new[]
            {
                ConvertPdfToTextAsync(pdfFiles),
                ConvertDocToTextAsync(docFiles),
                ConvertDocxToTextAsync(docXFiles)
            });
        }

        /// <summary>
        /// Asynchronously converts all of the .doc files it receives into .docx files
        /// If no arguments are supplied, it will instead convert all yet unconverted .doc files in the project directory
        /// Results are stored in corresponding project directory
        /// </summary>
        /// <param name="files">0 or more instances of the DocFile class which encapsulate .doc files.</param>
        private static async Task<IEnumerable<TxtFile>> ConvertDocToTextAsync(IEnumerable<DocFile> files)
        {
            ThrowIfUninitialized();
            var convertedFiles = new System.Collections.Concurrent.ConcurrentBag<TxtFile>();
            foreach (var document in files.Except<InputFile>(taggedFiles))
            {
                try
                {
                    var docx = await new DocToDocXConverter(document as DocFile).ConvertFileAsync();
                    var txt = await new DocxToTextConverter(docx as DocXFile).ConvertFileAsync();
                    var added = AddFile(txt);
                    convertedFiles.Add(added);
                    File.Delete(txt.FullPath);
                    File.Delete(docx.FullPath);
                }
                catch (IOException e)
                {
                    LogConversionFailure(document, e);
                    throw;
                }
                catch (UnauthorizedAccessException e)
                {
                    LogConversionFailure(document, e);
                    throw;
                }
            }
            return convertedFiles;
        }

        /// <summary>
        /// Asynchronously converts all of the .docx files it receives into text files
        /// If no arguments are supplied, it will instead convert all yet unconverted .docx files in the project directory
        /// Results are stored in corresponding project directory
        /// </summary>
        /// <param name="files">0 or more instances of the DocXFile class which encapsulate .docx files</param>
        private static async Task<IEnumerable<TxtFile>> ConvertDocxToTextAsync(IEnumerable<DocXFile> files)
        {
            ThrowIfUninitialized();
            var convertedFiles = new System.Collections.Concurrent.ConcurrentBag<TxtFile>();
            foreach (var docx in files.ExceptBy(taggedFiles, file => file.NameSansExt, file => file.NameSansExt))
            {
                var converted = await new DocxToTextConverter(docx as DocXFile).ConvertFileAsync();
                convertedFiles.Add(converted);
                AddFile(converted.FullPath);
                File.Delete(converted.FullPath);
            }
            return convertedFiles;
        }

        /// <summary>
        /// Asynchronously converts all of the .pdf files it receives into .txt files
        /// If no arguments are supplied, it will instead convert all yet unconverted .pdf files in the project directory
        /// Results are stored in corresponding project directory
        /// </summary>
        /// <param name="files">0 or more instances of the PdfFile class which encapsulate .pdf files.</param>
        private static async Task<IEnumerable<TxtFile>> ConvertPdfToTextAsync(IEnumerable<PdfFile> files)
        {
            ThrowIfUninitialized();
            var convertedFiles = new System.Collections.Concurrent.ConcurrentBag<TxtFile>();
            foreach (var pdf in files.Except<InputFile>(taggedFiles))
            {
                var converted = await new PdfToTextConverter(pdf as PdfFile).ConvertFileAsync();
                convertedFiles.Add(converted);
                AddFile(converted.FullPath);
                File.Delete(converted.FullPath);
            }
            return convertedFiles;
        }

        private static void LogConversionFailure(InputFile doc, Exception e)
        {
            Logger.Log($"An {e.GetType()} was thrown when attempting to convert {doc.FileName} to plain text.\n{e.StackTrace}");
        }
        #endregion

        #region Backup and Cleanup

        /// <summary>
        /// Copies the entire contents of the current project directory to a predetermined, relative path
        /// </summary>
        public static void BackupProject()
        {
            ThrowIfUninitialized();
            var projd = new DirectoryInfo(ProjectDirectory);
            var pard = new DirectoryInfo(projd.Parent.FullName);
            var desitination = Directory.CreateDirectory(pard.FullName + "\\backup\\" + ProjectName);
            foreach (var file in new DirectoryInfo(ProjectDirectory).GetFiles("*", SearchOption.AllDirectories))
            {
                if (!Directory.Exists(file.Directory.Name))
                {
                    desitination.CreateSubdirectory(file.Directory.Parent.Name + "\\" + file.Directory.Name);
                }
                file.CopyTo(desitination.FullName + "\\" + file.Directory.Parent.Name + "\\" + file.Directory.Name + "\\" + file.Name, true);
            }
        }
        /// <summary>
        /// Deletes everything from the current Project directory.
        /// </summary>
        public static void DecimateProject()
        {
            ThrowIfUninitialized();
            try
            {
                Directory.Delete(ProjectDirectory, recursive: true);
                docFiles = ImmutableList<DocFile>.Empty;
                docXFiles = ImmutableList<DocXFile>.Empty;
                pdfFiles = ImmutableList<PdfFile>.Empty;
                txtFiles = ImmutableList<TxtFile>.Empty;
                taggedFiles = ImmutableList<TaggedFile>.Empty;
                ProjectName = null;
                Initialized = false;
            }
            catch (IOException e)
            {
                Logger.Log(e.Message);
                throw;
            }
        }
        private static void ThrowIfUninitialized() { if (!Initialized) throw new FileManagerNotInitializedException(); }

        #endregion

        #endregion

        #region Properties
        /// <summary>
        /// Gets a value indicating if the FileManager has been initializes.
        /// </summary>
        public static bool Initialized { get; private set; }

        /// <summary>
        /// Gets the Absolute Path of Current Project Folder of the current project directory
        /// </summary>
        public static string ProjectDirectory { get; private set; }
        /// <summary>
        /// Gets the name of the current project.
        /// This will be the project name displayed to the user. It corresponds to the project's top level directory
        /// </summary>
        public static string ProjectName { get; private set; }
        /// <summary>
        /// Gets the list of TaggedFile instances which represent all *.tagged files which are included in the project. 
        /// TaggedFile instances are wrapper objects which provide discrete accessors to relevant *.tagged file properties.
        /// </summary>
        public static IEnumerable<TaggedFile> TaggedFiles => taggedFiles;

        /// <summary>
        /// Gets the list of TextFile instances which represent all *.txt files which are included in the project. 
        /// TextFile instances are wrapper objects which provide discrete accessors to relevant *.txt file properties.
        /// </summary>
        public static IEnumerable<TxtFile> TxtFiles => txtFiles;

        /// <summary>
        /// Gets the newPath of the analysis directory which stores temporary files during analysis
        /// </summary>
        public static string AnalysisDirectory => ProjectDirectory + @"\analysis";

        /// <summary>
        /// Gets the list of DocFile instances which represent all *.doc files which are included in the project. 
        /// DocFile instances are wrapper objects which provide discrete accessors to relevant *.doc file properties.
        /// </summary>
        public static IEnumerable<DocFile> DocFiles => docFiles;

        /// <summary>
        /// Gets the .doc files directory
        /// </summary>
        public static string DocFilesDirectory => InputFilesDirectory + @"\doc";

        /// <summary>
        /// Gets the list of DocXFile instances which represent all *.docx files which are included in the project. 
        /// DocXFile instances are wrapper objects which provide discrete accessors to relevant *.docx file properties.
        /// </summary>
        public static IEnumerable<DocXFile> DocXFiles => docXFiles;

        /// <summary>
        /// Gets the .docx files directory
        /// </summary>
        public static string DocxFilesDirectory => InputFilesDirectory + @"\docx";

        /// <summary>
        /// Gets the realRoot of the input file directory
        /// </summary>
        public static string InputFilesDirectory => ProjectDirectory + @"\input";
        /// <summary>
        /// Gets the list of PdfFile instances which represent all *.pdf files which are included in the project. 
        /// PdfFile instances are wrapper objects which provide discrete accessors to relevant *.pdf file properties.
        /// </summary>
        public static IEnumerable<PdfFile> PdfFiles => pdfFiles;

        /// <summary>
        /// Gets the .pdf files directory
        /// </summary>
        public static string PdfFilesDirectory => InputFilesDirectory + @"\pdf";

        /// <summary>
        /// Gets the result files directory
        /// </summary>
        public static string ResultsDirectory => ProjectDirectory + @"\results";
        /// <summary>
        /// Gets the .tagged files directory
        /// </summary>
        public static string TaggedFilesDirectory => InputFilesDirectory + @"\tagged";

        /// <summary>
        /// Gets the .txt files directory
        /// </summary>
        public static string TxtFilesDirectory => InputFilesDirectory + @"\txt";
        #endregion
        public static IReadOnlyList<string> AcceptedFileFormats = ImmutableList.Create("TXT", "DOC", "DOCX", "PDF");

        /// <summary>
        /// Gets the names of all documents in the current project. Ignoring file extensions.
        /// </summary>
        /// <returns>The names of all documents in the current project. Ignoring file extensions.</returns>
        public static IEnumerable<string> AllDocumentNames => AllFiles.Select(file => file.NameSansExt).Distinct(StringComparer.OrdinalIgnoreCase);
        /// <summary>
        /// Gets all input files in the current project.
        /// </summary>
        /// <returns>All input files in the current project.</returns>
        public static IEnumerable<InputFile> AllFiles
        {
            get
            {
                foreach (var txt in TxtFiles)
                    yield return txt;
                foreach (var pdf in PdfFiles)
                    yield return pdf;
                foreach (var doc in DocFiles)
                    yield return doc;
                foreach (var docx in DocXFiles)
                    yield return docx;
            }
        }

        internal static readonly ExtensionWrapperMap WrapperMap = new ExtensionWrapperMap(path => { throw new UnsupportedFileTypeException("unmapped " + path); });

        #region Fields

        private static IImmutableList<DocFile> docFiles = ImmutableList<DocFile>.Empty;
        private static IImmutableList<DocXFile> docXFiles = ImmutableList<DocXFile>.Empty;
        private static IImmutableList<PdfFile> pdfFiles = ImmutableList<PdfFile>.Empty;
        private static IImmutableList<TaggedFile> taggedFiles = ImmutableList<TaggedFile>.Empty;
        private static IImmutableList<TxtFile> txtFiles = ImmutableList<TxtFile>.Empty;

        #endregion
    }

    #region Helper Types
    /// <summary>
    /// Defines mappings between file extensions and functions which construct their respective wrappers.
    /// </summary>
    /// <remarks>Wrapper types are format enforcing classes derived from InputFile</remarks>
    /// <seealso cref="LASI.Content.InputFile"/>
    public class ExtensionWrapperMap
    {
        /// <summary>
        /// Initializes a new instance of the ExtensionWrapperMap class.
        /// </summary>
        /// <param name="unsupportedFileHandler">The specifies the manner in which unsupported extensions are handled.</param>
        public ExtensionWrapperMap(Func<string, InputFile> unsupportedFileHandler)
        {
            unsupportedHandler = unsupportedFileHandler;
        }
        /// <summary>
        /// Initializes a new instance of the ExtensionWrapperMap class that will throw for unknown extensions.
        /// </summary>
        public ExtensionWrapperMap() : this(extension => { throw new UnsupportedFileTypeException(extension); }) { }

        /// <summary>
        /// Gets all of the file extensions, which are supported.
        /// </summary>
        public IEnumerable<string> SupportedFormats => mapping.Keys;

        private readonly Func<string, InputFile> unsupportedHandler;

        private static readonly IReadOnlyDictionary<string, Func<string, InputFile>> mapping = new Dictionary<string, Func<string, InputFile>>()
        {
            [".txt"] = path => new TxtFile(path),
            [".doc"] = path => new DocFile(path),
            [".docx"] = path => new DocXFile(path),
            [".pdf"] = path => new PdfFile(path),
            [".tagged"] = path => new TaggedFile(path)
        }.ToImmutableDictionary(StringComparer.OrdinalIgnoreCase);

        /// <summary>
        /// Returns a function which can be invoked to instantiate an InputFile Wrapper corresponding to the given file extension.
        /// </summary>
        /// <param name="fileExtension">The file extension which for which to retrieve the appropriate InputFile instantiator function.</param>
        /// <returns>A function which can be invoked to instantiate an InputFile Wrapper corresponding to the given file extension.</returns>
        public Func<string, InputFile> this[string fileExtension] => mapping.ToDictionary().GetValueOrDefault(fileExtension, unsupportedHandler);

    }
    #endregion
}








