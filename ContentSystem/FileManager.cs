using LASI.ContentSystem.TaggerEncapsulation;
using LASI.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        #region Initialization
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
            TxtFilesDir = InputFilesDir + @"\txt";
            TaggedFilesDir = InputFilesDir + @"\tagged";
            AnalysisDir = ProjectDir + @"\analysis";
            ResultsDir = ProjectDir + @"\results";
        }

        /// <summary>
        /// Checks the existing contents of the current project directory and automatically loads the files it finds. Called by initialize
        /// </summary>
        private static void CheckProjectDirs() {
            CheckDirectoryExistence();
            CheckInputDirectories();
        }

        /// <summary>
        /// Checks for the existence of the extension statiffied input file project subject-directories and creates them if they do not exist.
        /// </summary>
        private static void CheckInputDirectories() {
            foreach (var docPath in Directory.EnumerateFiles(DocFilesDir, "*.doc"))
                docFiles.Add(new DocFile(docPath));
            foreach (var docPath in Directory.EnumerateFiles(DocxFilesDir, "*.docx"))
                docXFiles.Add(new DocXFile(docPath));
            foreach (var docPath in Directory.EnumerateFiles(TxtFilesDir, "*.txt"))
                txtFiles.Add(new TxtFile(docPath));
            foreach (var docPath in Directory.EnumerateFiles(PdfFilesDir, "*.pdf"))
                pdfFiles.Add(new PdfFile(docPath));
            foreach (var docPath in Directory.EnumerateFiles(TaggedFilesDir, "*.tagged"))
                taggedFiles.Add(new TaggedFile(docPath));
        }
        /// <summary>
        /// Checks for the existence of the project subject-directories and creates them if they do not exist.
        /// </summary>
        private static void CheckDirectoryExistence() {
            foreach (var path in new[] { 
                ProjectDir,
                InputFilesDir,
                AnalysisDir, 
                ResultsDir, 
                DocFilesDir, 
                DocxFilesDir, 
                PdfFilesDir,
                TaggedFilesDir, 
                TxtFilesDir, 
            }) {
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);
            }
        }
        #endregion

        #region List Insertion Overloads

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
                    ext == ".txt" ? TxtFilesDir :
                    ext == ".pdf" ? PdfFilesDir :
                    ext == ".tagged" ? TaggedFilesDir : "";

                newPath += "\\" + originalFile.FileName;

                File.Copy(originalFile.FullPath, newPath, overwrite);
                var newFile = WrapperMap[ext](newPath);
                allFileNames.Add(newFile.NameSansExt);
                AddToTypedList(newFile as dynamic);
                return newFile;
            }
            catch (KeyNotFoundException ex) {
                throw new UnsupportedFileTypeAddedException(ext, ex);
            }
        }

        private static void AddToTypedList(DocFile file) {
            docFiles.Add(file);

        }
        private static void AddToTypedList(DocXFile file) {
            docXFiles.Add(file);

        }
        private static void AddToTypedList(TxtFile file) {
            txtFiles.Add(file);

        }
        private static void AddToTypedList(PdfFile file) {
            pdfFiles.Add(file);

        }
        private static void AddToTypedList(TaggedFile file) {
            taggedFiles.Add(file);

        }
        #endregion

        #region Existence Checking and Removal

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
            return !allFileNames.Contains(fileName);
        }
        /// <summary>
        /// Returns a value indicating whether a file with the same name as that of the given InputFile, irrespective of its extension, is part of the project. 
        /// </summary>
        /// <param name="inputFile">An an Instance of the InputFile class or one of its descendents.</param>
        /// <returns>False if a file with the same name, irrespective of it's extension, is part of the project. False otherwise.</returns>
        public static bool HasSimilarFile(InputFile inputFile) {
            return !allFileNames.Contains(inputFile.NameSansExt);
        }

        /// <summary>
        /// Removes all files, regardless of extension, whose names do not match any of the names in the provided collection of file path strings.
        /// </summary>
        /// <param name="filesToKeep">collction of file path strings indicating which files are not to be culled. All others will summarilly executed.</param>
        public static void RemoveAllNotIn(IEnumerable<string> filesToKeep) {
            if (!Initialized) {
                throw new FileManagerNotInitializedException();
            }
            RemoveAllNotIn(from f in filesToKeep
                           select f.IndexOf('.') > 0 ? WrapperMap[f.Substring(f.LastIndexOf('.'))](f) : new TxtFile(f));
        }
        /// <summary>
        /// Removes all files, regardless of extension, whose names do not match any of the names in the provided collection of InputFile objects.
        /// </summary>
        /// <param name="filesToKeep">collection of InputFile objects indicating which files are not to be culled. All others will summarilly executed.</param>
        public static void RemoveAllNotIn(IEnumerable<InputFile> filesToKeep) {
            if (!Initialized) {
                throw new FileManagerNotInitializedException();
            }
            var toRemove = from f in allFileNames
                           where (from k in filesToKeep
                                  where f == k.NameSansExt
                                  select k).Any()
                           select f;
            foreach (var f in toRemove) {
                RemoveAllAlikeFiles(f);
            }
        }

        private static void RemoveAllAlikeFiles(string fileName) {
            txtFiles.RemoveAll(f => f.NameSansExt == fileName);
            docFiles.RemoveAll(f => f.NameSansExt == fileName);
            docXFiles.RemoveAll(f => f.NameSansExt == fileName);
            pdfFiles.RemoveAll(f => f.NameSansExt == fileName);
            taggedFiles = (from f in taggedFiles
                           where f.NameSansExt != fileName
                           select f).ToList();
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

        private static void RemoveFile(TxtFile file) {
            txtFiles.Remove(file);
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


        #endregion

        #region File Conversion
        /// <summary>
        /// Performs the necessary conversions, based on the format of all files within the project.
        /// </summary>
        public static void ConvertAsNeeded() {
            if (!Initialized) {
                throw new FileManagerNotInitializedException();
            }
            ConvertPdfToText();
            ConvertDocToText();
            ConvertDocxToText();
        }

        /// <summary>
        /// Asynchronously performs the necessary conversions, based on the format of all files within the project.
        /// </summary>
        public static async Task ConvertAsNeededAsync() {
            if (!Initialized) {
                throw new FileManagerNotInitializedException();
            }
            try {
                await Task.WhenAll(
                    ConvertPdfToTextAsync(),
                    Task.Run(async () => {
                        try {
                            await ConvertDocToTextAsync();
                        }
                        catch (FileConversionFailureException) { throw; }
                    }),
                    ConvertDocxToTextAsync());
            }
            catch (FileConversionFailureException) {
                throw;
            }
        }


        /// <summary>
        /// Converts all of the .doc files it recieves into .docx files
        /// If no arguments are supplied, it will instead convert all yet unconverted .doc files in the project directory
        /// Results are stored in corresponding project directory
        /// </summary>
        /// <param name="files">0 or more instances of the DocFile class which encapsulate .doc files.</param>
        public static void ConvertDocToText(params DocFile[] files) {
            if (!Initialized) {
                throw new FileManagerNotInitializedException();
            }
            if (files.Length == 0)
                files = docFiles.ToArray();
            foreach (var doc in from file in files
                                where !(from dx in txtFiles
                                        where dx.NameSansExt == file.NameSansExt
                                        select dx).Any()
                                select file) {
                try {
                    try {
                        var docx = new DocToDocXConverter(doc).ConvertFile();
                        var txt = new DocxToTextConverter(docx as DocXFile).ConvertFile();
                        AddFile(txt.FullPath, true);
                        File.Delete(txt.FullPath);
                    }
                    catch (IOException e) { Output.WriteLine(e.Message); throw new FileConversionFailureException(doc.NameSansExt, ".doc", ".txt"); }
                }
                catch (UnauthorizedAccessException) {
                    Output.WriteLine("An exception was thrown when attempting to convert {0} to {1}", doc.FileName);
                    throw;
                }
            }
        }
        /// <summary>
        /// Asynchronously converts all of the .doc files it recieves into .docx files
        /// If no arguments are supplied, it will instead convert all yet unconverted .doc files in the project directory
        /// Results are stored in corresponding project directory
        /// </summary>
        /// <param name="files">0 or more instances of the DocFile class which encapsulate .doc files.</param>
        public static async Task ConvertDocToTextAsync(params DocFile[] files) {
            if (!Initialized) {
                throw new FileManagerNotInitializedException();
            }
            if (files.Length == 0)
                files = docFiles.ToArray();
            foreach (var doc in from file in files
                                where !(from dx in txtFiles
                                        where dx.NameSansExt == file.NameSansExt
                                        select dx).Any()
                                select file) {
                try {
                    try {
                        var docx = await new DocToDocXConverter(doc).ConvertFileAsync();
                        var txt = await new DocxToTextConverter(docx as DocXFile).ConvertFileAsync();
                        AddFile(txt.FullPath, true);
                        File.Delete(txt.FullPath);
                    }
                    catch (Exception e) { Output.WriteLine(e.Message); throw new FileConversionFailureException(doc.NameSansExt, ".doc", ".txt"); }
                }
                catch (UnauthorizedAccessException) {
                    Output.WriteLine("An exception was thrown when attempting to convert {0} to {1}", doc.FileName);
                    throw;
                }
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
                                where !(from dx in txtFiles
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
        public static async Task ConvertPdfToTextAsync(params PdfFile[] files) {
            if (!Initialized) {
                throw new FileManagerNotInitializedException();
            }
            if (files.Length == 0)
                files = pdfFiles.ToArray();
            foreach (var pdf in from file in files
                                where !(from dx in txtFiles
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
                                where !(from dx in txtFiles
                                        where dx.NameSansExt == d.NameSansExt
                                        select dx).Any()
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
                                where !(from dx in txtFiles
                                        where dx.NameSansExt == d.NameSansExt
                                        select dx).Any()
                                select d) {
                var converted = await new DocxToTextConverter(doc).ConvertFileAsync();
                AddFile(converted.FullPath, true);
                File.Delete(converted.FullPath);
            }
        }

        /// <summary>
        /// Invokes the POS tagger on the text files it recieves into storing the newly tagged files
        /// If no arguments are supplied, it will instead convert all yet untagged text files in the project directory
        /// Results are stored in corresponding project directory
        /// </summary>
        /// <param name="files">0 or more instances of the TextFile class which encapsulate text files</param>
        public static void TagTextFiles(params TxtFile[] files) {
            if (!Initialized) {
                throw new FileManagerNotInitializedException();
            }
            if (files.Length == 0)
                files = txtFiles.ToArray();
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
        public static async Task TagTextFilesAsync(params TxtFile[] files) {
            if (!Initialized) {
                throw new FileManagerNotInitializedException();
            }
            if (files.Length == 0)
                files = txtFiles.ToArray();
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

        #endregion

        #region Backup and Cleanup

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
                allFileNames.Clear();
                docFiles.Clear();
                docXFiles.Clear();
                pdfFiles.Clear();
                txtFiles.Clear();
                taggedFiles.Clear();
                ProjectName = null;
                Initialized = false;


            }
            catch (IOException e) {
                Output.WriteLine(e.Message);
                Output.WriteLine("Directory could not be found for forced cleabup");
            }
        }
        #endregion

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
        /// This will be the project name displayed to the user. It corresponds to the project's top level directory
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
        public static string TxtFilesDir {
            get;
            private set;
        }
        /// <summary>
        /// Gets the list of TextFile instances which represent all *.txt files which are included in the project. 
        /// TextFile instances are wrapper objects which provide discrete accessors to relevant *.txt file properties.
        /// </summary>
        public static IReadOnlyList<TxtFile> TxtFiles {
            get {
                return txtFiles;
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
                return docFiles;
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
        private static HashSet<string> allFileNames = new HashSet<string>();
        private static List<DocFile> docFiles = new List<DocFile>();
        private static List<DocXFile> docXFiles = new List<DocXFile>();
        private static List<PdfFile> pdfFiles = new List<PdfFile>();
        private static List<TxtFile> txtFiles = new List<TxtFile>();
        private static List<TaggedFile> taggedFiles = new List<TaggedFile>();
        #endregion
    }

    #region Helper Types
    class WrapperDict : Dictionary<string, Func<string, InputFile>>
    {
        internal WrapperDict()
            : base(
            new Dictionary<string, Func<string, InputFile>> {
                { "txt" , p => new TxtFile(p) },
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


}




