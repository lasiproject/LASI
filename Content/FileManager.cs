using LASI.Utilities;
using LASI.Utilities.Specialized.Enhanced.IList.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using TaggerInterop;
using System.Collections.Immutable;
using System.Collections;

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
                docFiles.Add(new DocFile(docPath));
            foreach (var docPath in Directory.EnumerateFiles(DocxFilesDirectory, "*.docx"))
                docXFiles.Add(new DocXFile(docPath));
            foreach (var docPath in Directory.EnumerateFiles(TxtFilesDirectory, "*.txt"))
                txtFiles.Add(new TxtFile(docPath));
            foreach (var docPath in Directory.EnumerateFiles(PdfFilesDirectory, "*.pdf"))
                pdfFiles.Add(new PdfFile(docPath));
            foreach (var docPath in Directory.EnumerateFiles(TaggedFilesDirectory, "*.tagged"))
                taggedFiles.Add(new TaggedFile(docPath));
        }
        #endregion

        #region List Insertion Overloads

        /// <summary>
        /// Adds the document indicated by the specified path string to the project
        /// </summary>
        /// <param name="path">The path string of the document file to add to the project</param>
        /// 
        /// <returns>An InputFile object which acts as a wrapper around the project relative path of the newly added file.</returns>
        public static InputFile AddFile(string path)
        {
            ThrowIfUninitialized();
            var ext = path.Substring(path.LastIndexOf('.')).ToLower();
            try
            {
                var originalFile = WrapperMap[ext](path);
                var newPath =
                    ext == ".doc" ? DocFilesDirectory :
                    ext == ".docx" ? DocxFilesDirectory :
                    ext == ".txt" ? TxtFilesDirectory :
                    ext == ".pdf" ? PdfFilesDirectory :
                    ext == ".tagged" ? TaggedFilesDirectory : string.Empty;

                newPath += "\\" + originalFile.FileName;

                File.Copy(originalFile.FullPath, newPath, overwrite: true);
                var newFile = WrapperMap[ext](newPath);

                AddToTypedList(newFile as dynamic);
                return newFile;
            }
            catch (KeyNotFoundException ex)
            {
                throw new UnsupportedFileTypeAddedException(ext, ex);
            }
        }

        private static void AddToTypedList(DocFile file) => docFiles.Add(file);
        private static void AddToTypedList(DocXFile file) => docXFiles.Add(file);
        private static void AddToTypedList(TxtFile file) => txtFiles.Add(file);
        private static void AddToTypedList(PdfFile file) => pdfFiles.Add(file);
        private static void AddToTypedList(TaggedFile file) => taggedFiles.Add(file);

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
        /// Removes all files, regardless of extension, whose names do not match any of the names in the provided collection of file path strings.
        /// </summary>
        /// <param name="filesToKeep">A collection of file path strings indicating which files are not to be culled. All others will summarily executed.</param>
        public static void RemoveAllFilesNotIn(IEnumerable<string> filesToKeep)
        {
            ThrowIfUninitialized();
            RemoveAllNotIn(filesToKeep.Select(fileName => fileName.IndexOf('.') > 0 ? WrapperMap[fileName.Substring(fileName.LastIndexOf('.'))](fileName) : new TxtFile(fileName)));
        }
        /// <summary>
        /// Removes all files, regardless of extension, whose names do not match any of the names in the provided collection of InputFile objects.
        /// </summary>
        /// <param name="filesToKeep">collection of InputFile objects indicating which files are not to be culled. All others will summarily executed.</param>
        public static void RemoveAllNotIn(IEnumerable<InputFile> filesToKeep)
        {
            ThrowIfUninitialized();
            foreach (var f in AllDocumentNames.Except(taggedFiles.Select(tagged => tagged.NameSansExt)))
            {
                RemoveAllAlikeFiles(f);
            }
        }

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
            txtFiles.RemoveAll(f => f.NameSansExt.Contains(fileName));
            docFiles.RemoveAll(f => f.NameSansExt.Contains(fileName));
            docXFiles.RemoveAll(f => f.NameSansExt.Contains(fileName));
            pdfFiles.RemoveAll(f => f.NameSansExt.Contains(fileName));
            taggedFiles.RemoveAll(f => f.NameSansExt.Contains(fileName));
        }
        private static void RemoveFile(TxtFile file) => txtFiles.Remove(file);

        private static void RemoveFile(DocFile file) => docFiles.Remove(file);

        private static void RemoveFile(DocXFile file) => docXFiles.Remove(file);

        private static void RemoveFile(PdfFile file) => pdfFiles.Remove(file);

        #endregion

        #region File Conversion
        /// <summary>
        /// Performs the necessary conversions, based on the format of all files within the project.
        /// </summary>
        public static void ConvertAsNeeded()
        {
            ThrowIfUninitialized();
            ConvertPdfToText();
            ConvertDocToText();
            ConvertDocxToText();
        }

        /// <summary>
        /// Asynchronously performs the necessary conversions, based on the format of all files within the project.
        /// </summary>
        public static async Task ConvertAsNeededAsync()
        {
            ThrowIfUninitialized();
            try
            {
                var tasks = new
                {
                    ConvertPdf = ConvertPdfToTextAsync(pdfFiles.ToArray()),
                    ConvertDocs = ConvertDocToTextAsync(docFiles.ToArray()),
                    ConvertDocx = ConvertDocxToTextAsync(docXFiles.ToArray())
                };
                await Task.WhenAll(tasks.ConvertPdf, tasks.ConvertDocs, tasks.ConvertDocx);
            }
            catch (FileConversionFailureException e)
            {
                e.Log();
                throw;
            }
        }


        /// <summary>
        /// Converts all of the .doc files it receives into .docx files
        /// If no arguments are supplied, it will instead convert all yet unconverted .doc files in the project directory
        /// Results are stored in corresponding project directory
        /// </summary>
        /// <param name="files">0 or more instances of the DocFile class which encapsulate .doc files.</param>
        public static IEnumerable<TxtFile> ConvertDocToText(params DocFile[] files)
        {
            ThrowIfUninitialized();
            var convertedFiles = new System.Collections.Concurrent.ConcurrentBag<TxtFile>();
            foreach (var document in (files.Length > 0 ? files.AsEnumerable() : docFiles).Except<InputFile>(taggedFiles))
            {
                try
                {
                    var docx = new DocToDocXConverter(document as DocFile).ConvertFile();
                    var txt = new DocxToTextConverter(docx).ConvertFile();
                    AddFile(txt.FullPath);
                    File.Delete(txt.FullPath);
                    convertedFiles.Add(txt);
                }
                catch (IOException e)
                {
                    RecordConversionFailure(document, e);
                    //throw new FileConversionFailureException(document.NameSansExt, ".doc", ".txt");
                    throw;
                }
                catch (UnauthorizedAccessException e)
                {
                    RecordConversionFailure(document, e);
                    throw;
                }
            }
            return convertedFiles;
        }
        /// <summary>
        /// Asynchronously converts all of the .doc files it receives into .docx files
        /// If no arguments are supplied, it will instead convert all yet unconverted .doc files in the project directory
        /// Results are stored in corresponding project directory
        /// </summary>
        /// <param name="files">0 or more instances of the DocFile class which encapsulate .doc files.</param>
        public static async Task<IEnumerable<TxtFile>> ConvertDocToTextAsync(params DocFile[] files)
        {
            ThrowIfUninitialized();
            var convertedFiles = new System.Collections.Concurrent.ConcurrentBag<TxtFile>();
            foreach (var document in (files.Length > 0 ? files.AsEnumerable() : docFiles).Except<InputFile>(taggedFiles))
            {
                try
                {
                    var docx = await new DocToDocXConverter(document as DocFile).ConvertFileAsync();
                    var txt = await new DocxToTextConverter(docx as DocXFile).ConvertFileAsync();
                    AddFile(txt.FullPath);
                    File.Delete(txt.FullPath);
                    File.Delete(docx.FullPath);
                }
                catch (IOException e)
                {
                    RecordConversionFailure(document, e);
                    //throw new FileConversionFailureException(document.NameSansExt, ".doc", ".txt", e);
                    throw;
                }
                catch (UnauthorizedAccessException e)
                {
                    RecordConversionFailure(document, e);
                    throw;
                }
            }
            return convertedFiles;
        }

        /// <summary>
        /// Converts all of the .docx files it receives into text files
        /// If no arguments are supplied, it will instead convert all yet unconverted .docx files in the project directory
        /// Results are stored in corresponding project directory
        /// </summary>
        /// <param name="files">0 or more instances of the DocXFile class which encapsulate .docx files</param>
        public static IEnumerable<TxtFile> ConvertDocxToText(params DocXFile[] files)
        {
            ThrowIfUninitialized();
            foreach (var doc in files.ExceptBy(taggedFiles, (InputFile file) => file.NameSansExt))
            {
                var converted = new DocxToTextConverter(doc as DocXFile).ConvertFile();
                AddFile(converted.FullPath);
                File.Delete(converted.FullPath);
                yield return converted;
            }
        }

        /// <summary>
        /// Asynchronously converts all of the .docx files it receives into text files
        /// If no arguments are supplied, it will instead convert all yet unconverted .docx files in the project directory
        /// Results are stored in corresponding project directory
        /// </summary>
        /// <param name="files">0 or more instances of the DocXFile class which encapsulate .docx files</param>
        public static async Task<IEnumerable<TxtFile>> ConvertDocxToTextAsync(params DocXFile[] files)
        {
            ThrowIfUninitialized();
            var convertedFiles = new System.Collections.Concurrent.ConcurrentBag<TxtFile>();
            foreach (var doc in (files.Length > 0 ? files.AsEnumerable() : docXFiles).Except<InputFile>(taggedFiles))
            {
                var converted = await new DocxToTextConverter(doc as DocXFile).ConvertFileAsync();
                convertedFiles.Add(converted);
                AddFile(converted.FullPath);
                File.Delete(converted.FullPath);
            }
            return convertedFiles;
        }

        /// <summary>
        /// Converts all of the .pdf files it receives into .txt files
        /// If no arguments are supplied, it will instead convert all yet unconverted .pdf files in the project directory
        /// Results are stored in corresponding project directory
        /// </summary>
        /// <param name="files">0 or more instances of the PdfFile class which encapsulate .pdf files.</param>
        public static IEnumerable<TxtFile> ConvertPdfToText(params PdfFile[] files)
        {
            ThrowIfUninitialized();
            foreach (var pdf in (files.Length > 0 ? files.AsEnumerable() : pdfFiles).Except<InputFile>(taggedFiles))
            {
                var converted = new PdfToTextConverter(pdf as PdfFile).ConvertFile();
                AddFile(converted.FullPath);
                File.Delete(converted.FullPath); yield return converted;
            }
        }

        /// <summary>
        /// Asynchronously converts all of the .pdf files it receives into .txt files
        /// If no arguments are supplied, it will instead convert all yet unconverted .pdf files in the project directory
        /// Results are stored in corresponding project directory
        /// </summary>
        /// <param name="files">0 or more instances of the PdfFile class which encapsulate .pdf files.</param>
        public static async Task<IEnumerable<TxtFile>> ConvertPdfToTextAsync(params PdfFile[] files)
        {
            ThrowIfUninitialized();
            var convertedFiles = new System.Collections.Concurrent.ConcurrentBag<TxtFile>();
            foreach (var pdf in (files.Length > 0 ? files.AsEnumerable() : pdfFiles).Except<InputFile>(taggedFiles))
            {
                var converted = await new PdfToTextConverter(pdf as PdfFile).ConvertFileAsync();
                convertedFiles.Add(converted);
                AddFile(converted.FullPath);
                File.Delete(converted.FullPath);
            }
            return convertedFiles;
        }

        /// <summary>
        /// Invokes the POS tagger on the text files it receives into storing the newly tagged files
        /// If no arguments are supplied, it will instead convert all yet untagged text files in the project directory
        /// Results are stored in corresponding project directory
        /// </summary>
        /// <param name="files">0 or more instances of the TextFile class which encapsulate text files</param>
        public static void TagTextFiles(params TxtFile[] files)
        {
            ThrowIfUninitialized();
            foreach (var doc in (files.Length > 0 ? files.AsEnumerable() : txtFiles).Except<InputFile>(taggedFiles))
            {
                var tagger = new SharpNLPTagger(
                    TaggerMode.TagAndAggregate, doc.FullPath,
                    TaggedFilesDirectory + "\\" + doc.NameSansExt + ".tagged");
                var tf = new TaggedFile(tagger.ProcessFile());
                AddFile(tf.FullPath);
            }
        }

        /// <summary>
        ///Asynchronously Invokes the POS tagger on the text files it receives into storing the newly tagged files
        /// If no arguments are supplied, it will instead convert all yet untagged text files in the project directory
        /// Results are stored in corresponding project directory
        /// </summary>
        /// <param name="files">0 or more instances of the TextFile class which encapsulate text files</param>
        public static async Task TagTextFilesAsync(params TxtFile[] files)
        {
            ThrowIfUninitialized();
            var tasks = (
                from file in (files.Length > 0 ? files.AsEnumerable() : txtFiles).Except<InputFile>(taggedFiles)
                select new SharpNLPTagger(TaggerMode.TagAndAggregate, file.FullPath, TaggedFilesDirectory + "\\" + file.NameSansExt + ".tagged").ProcessFileAsync()
                ).ToList();
            while (tasks.Any())
            {
                var tagged = await Task.WhenAny(tasks);
                var taggedFile = await tagged;
                taggedFiles.Add(new TaggedFile(taggedFile));
                tasks.Remove(tagged);
            }


        }

        private static void RecordConversionFailure(InputFile doc, Exception e)
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
                Directory.Delete(ProjectDirectory, true);
                docFiles.Clear();
                docXFiles.Clear();
                pdfFiles.Clear();
                txtFiles.Clear();
                taggedFiles.Clear();
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
        public static IReadOnlyList<TaggedFile> TaggedFiles => taggedFiles;

        /// <summary>
        /// Gets the list of TextFile instances which represent all *.txt files which are included in the project. 
        /// TextFile instances are wrapper objects which provide discrete accessors to relevant *.txt file properties.
        /// </summary>
        public static IReadOnlyList<TxtFile> TxtFiles => txtFiles;

        /// <summary>
        /// Gets the newPath of the analysis directory which stores temporary files during analysis
        /// </summary>
        public static string AnalysisDirectory => ProjectDirectory + @"\analysis";

        /// <summary>
        /// Gets the list of DocFile instances which represent all *.doc files which are included in the project. 
        /// DocFile instances are wrapper objects which provide discrete accessors to relevant *.doc file properties.
        /// </summary>
        public static IReadOnlyList<DocFile> DocFiles => docFiles;

        /// <summary>
        /// Gets the .doc files directory
        /// </summary>
        public static string DocFilesDirectory => InputFilesDirectory + @"\doc";

        /// <summary>
        /// Gets the list of DocXFile instances which represent all *.docx files which are included in the project. 
        /// DocXFile instances are wrapper objects which provide discrete accessors to relevant *.docx file properties.
        /// </summary>
        public static IReadOnlyList<DocXFile> DocXFiles => docXFiles;

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
        public static IReadOnlyList<PdfFile> PdfFiles => pdfFiles;

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
        internal static readonly ExtensionWrapperMap WrapperMap = new ExtensionWrapperMap(path => { throw new UnsupportedFileTypeAddedException("unmapped " + path); });
        #endregion
        public static IEnumerable<string> AcceptedFileFormats
        {
            get
            {
                yield return "TXT";
                yield return "DOC";
                yield return "DOCX";
                yield return "PDF";
            }
        }

        /// <summary>
        /// Gets the names of all documents in the current project. Ignoring file extensions.
        /// </summary>
        /// <returns>The names of all documents in the current project. Ignoring file extensions.</returns>
        public static IEnumerable<string> AllDocumentNames => AllFiles.Select(file => file.NameSansExt).ToImmutableHashSet(StringComparer.OrdinalIgnoreCase);
        /// <summary>
        /// Gets all input files in the current project.
        /// </summary>
        /// <returns>All input files in the current project.</returns>
        public static IEnumerable<InputFile> AllFiles
        {
            get
            {
                foreach (var txt in txtFiles) { yield return txt; }
                foreach (var pdf in pdfFiles) { yield return pdf; }
                foreach (var doc in docFiles) { yield return doc; }
                foreach (var docx in docXFiles) { yield return docx; }
                foreach (var tagged in taggedFiles) { yield return tagged; }
            }
        }
        #region Fields

        private static List<DocFile> docFiles = new List<DocFile>();
        private static List<DocXFile> docXFiles = new List<DocXFile>();
        private static List<PdfFile> pdfFiles = new List<PdfFile>();
        private static List<TaggedFile> taggedFiles = new List<TaggedFile>();
        private static List<TxtFile> txtFiles = new List<TxtFile>();
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

        public ExtensionWrapperMap() : this(extension => { throw new UnsupportedFileTypeAddedException(extension); }) { }

        /// <summary>
        /// Gets all of the file extensions, which are supported.
        /// </summary>
        public IEnumerable<string> SupportedFormats => mapping.Keys;

        private readonly Func<string, InputFile> unsupportedHandler;

        private IDictionary<string, Func<string, InputFile>> mapping = new Dictionary<string, Func<string, InputFile>>(StringComparer.OrdinalIgnoreCase)
        {
            [".txt"] = p => new TxtFile(p),
            [".doc"] = p => new DocFile(p),
            [".docx"] = p => new DocXFile(p),
            [".pdf"] = p => new PdfFile(p),
            [".tagged"] = p => new TaggedFile(p)
        };
        /// <summary>
        /// Returns a function which can be invoked to instantiate an InputFile Wrapper corresponding to the given file extension.
        /// </summary>
        /// <param name="fileExtension">The file extension which for which to retrieve the appropriate InputFile instantiator function.</param>
        /// <returns>A function which can be invoked to instantiate an InputFile Wrapper corresponding to the given file extension.</returns>
        public Func<string, InputFile> this[string fileExtension] => mapping.GetValueOrDefault(fileExtension, unsupportedHandler);

    }
    #endregion
}








