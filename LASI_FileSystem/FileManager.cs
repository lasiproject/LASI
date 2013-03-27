using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LASI.FileSystem.FileTypes;
namespace LASI.FileSystem
{
    /// <summary>
    /// a static class which encapsulates the operations necessary to manage the working directory of the current user progress.
    /// Client code must call the Initialialize method prior to using any of the other methods in this class. 
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
            InputFilesDir = ProjectDir + @"\input";
            DocFilesDir = InputFilesDir + @"\doc";
            DocxFilesDir = InputFilesDir + @"\docx";
            TextFilesDir = InputFilesDir + @"\text";
            TaggedFilesDir = InputFilesDir + @"\tagged";
            AnalysisDir = ProjectDir + @"\analysis";
            ResultsDir = ProjectDir + @"\results";
            CheckProjectDirs();
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
            foreach (var docPath in Directory.EnumerateFiles(DocFilesDir))
                docFiles.Add(new DocFile(docPath));
            foreach (var docPath in Directory.EnumerateFiles(DocxFilesDir))
                docXFiles.Add(new DocXFile(docPath));
            foreach (var docPath in Directory.EnumerateFiles(TextFilesDir))
                textFiles.Add(new TextFile(docPath));
            foreach (var docPath in Directory.EnumerateFiles(TaggedFilesDir))
                TaggedFiles.Add(new TaggedFile(docPath));
        }
        /// <summary>
        /// Checks for the existence of the project subject-directories and creates them if they do not exist.
        /// </summary>
        private static void CheckProjectDirExistence() {
            foreach (var path in new[] { ProjectDir,
                InputFilesDir,
                AnalysisDir, 
                ResultsDir, 
                DocFilesDir, 
                DocxFilesDir, 
                TaggedFilesDir, 
                TextFilesDir, 
            }) {
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);
            }
        }

        /// <summary>
        /// Performs the necessary conversions, based on the format of all files within the project.
        /// </summary>
        public static void ConvertAsNeeded() {
            ConvertDocFiles();
            ConvertDocxToText();
            TagTextFiles();
        }

        /// <summary>
        /// Asynchronously performs the necessary conversions, based on the format of all files within the project.
        /// </summary>
        public static async Task ConvertAsNeededAsync() {
            await ConvertDocFilesAsync();
            await ConvertDocxToTextAsync();
            await TagTextFilesAsync();
        }


        /// <summary>
        /// Copies the .doc file at the given path to the appropriate subfolder of the current project
        /// </summary>
        /// <param name="sourcePath">The path of the file to add</param>
        public static void AddDocFile(string sourcePath, bool overwrite = false) {
            var FD = new FileData(sourcePath);
            var path = DocFilesDir + "\\" + FD.FileNameWithExt;
            File.Copy(sourcePath, path, overwrite);
            var file = new DocFile(path);
            docFiles.Add(file);
        }

        /// <summary>
        /// Copies the .docx file at the given path to the appropriate subfolder of the current project
        /// </summary>
        /// <param name="sourcePath">The path of the file to add</param>
        public static void AddDocXFile(string sourcePath, bool overwrite = false) {
            var FD = new FileData(sourcePath);
            var path = DocxFilesDir + "\\" + FD.FileNameWithExt;
            File.Copy(sourcePath, path, overwrite);
            var file = new DocXFile(path);
            docXFiles.Add(file);
        }


        /// <summary>
        /// Copies the text file at the given path to the appropriate subfolder of the current project
        /// </summary>
        /// <param name="sourcePath">The path of the file to add</param>
        public static void AddTextFile(string sourcePath, bool overwrite = false) {
            var FD = new FileData(sourcePath);
            var path = TextFilesDir + "\\" + FD.FileNameWithExt;
            File.Copy(sourcePath, path, overwrite);
            var file = new TextFile(path);
            textFiles.Add(file);
        }

        public static void AddFile(string path, bool overwrite = false) {
            switch (path.Substring(path.LastIndexOf('.')).ToLower()) {
                case ".txt":
                    AddTextFile(path, overwrite);
                    break;
                case ".docx":
                    AddDocXFile(path, overwrite);
                    break;
                case ".doc":
                    AddDocFile(path, overwrite);
                    break;
                default:
                    throw new InvalidOperationException();
            }
        }
        /// <summary>
        /// Converts all of the .doc files it recieves into .docx files
        /// If no arguments are supplied, it will instead convert all yet unconverted .doc files in the project directory
        /// Results are stored in corresponding project directory
        /// </summary>
        /// <param name="files">0 or more instances of the DocFile class which encapsulate .doc files</param>
        public static void ConvertDocFiles(params DocFile[] files) {
            if (files.Length == 0)
                files = docFiles.ToArray();
            foreach (var doc in from d in files
                                where
                                (from dx in docXFiles
                                 where dx.NameSansExt == d.NameSansExt
                                 select dx).Count() == 0
                                select d) {
                var converter = new DocToDocXConverter(doc);
                var converted = converter.ConvertFile();
                AddDocXFile(converted.FullPath);
                File.Delete(converted.FullPath);
            }
        }
        /// <summary>
        /// Asynchronously converts all of the .doc files it recieves into .docx files
        /// If no arguments are supplied, it will instead convert all yet unconverted .doc files in the project directory
        /// Results are stored in corresponding project directory
        /// </summary>
        /// <param name="files">0 or more instances of the DocFile class which encapsulate .doc files</param>
        public static async Task ConvertDocFilesAsync(params DocFile[] files) {
            if (files.Length == 0)
                files = docFiles.ToArray();
            foreach (var doc in from d in files
                                where (from dx in docXFiles
                                       where dx.NameSansExt == d.NameSansExt
                                       select dx).Count() == 0
                                select d) {
                var converter = new DocToDocXConverter(doc);
                var converted = await converter.ConvertFileAsync();
                AddDocXFile(converted.FullPath);
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
            if (files.Length == 0)
                files = docXFiles.ToArray();
            foreach (var doc in from d in files
                                where
                                (from dx in textFiles
                                 where dx.NameSansExt == d.NameSansExt
                                 select dx).Count() == 0
                                select d) {
                var converter = new DocxToTextConverter(doc);
                var converted = converter.ConvertFile();
                AddTextFile(converted.FullPath);
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
            if (files.Length == 0)
                files = docXFiles.ToArray();
            foreach (var doc in from d in files
                                where
                                (from dx in textFiles
                                 where dx.NameSansExt == d.NameSansExt
                                 select dx).Count() == 0
                                select d) {
                var converted = await new DocxToTextConverter(doc).ConvertFileAsync();

                AddTextFile(converted.FullPath);
                File.Delete(converted.FullPath);
            }
        }
        /// <summary>
        /// Invokes the POS tagger on the text files it recieves into storing the newly tagged files
        /// If no arguments are supplied, it will instead convert all yet untagged text files in the project directory
        /// Results are stored in corresponding project directory
        /// </summary>
        /// <param name="files">0 or more instances of the TextFile class which encapsulate text files</param>
        public static void TagTextFiles(params TextFile[] files) {
            if (files.Length == 0)
                files = textFiles.ToArray();
            foreach (var doc in from d in files
                                where
                                (from dx in TaggedFiles
                                 where dx.NameSansExt == d.NameSansExt
                                 select dx).Count() == 0
                                select d) {
                var tagger = new SharpNLPTaggingModule.SharpNLPTagger(TaggingOption.TagAndAggregate, doc.FullPath, TaggedFilesDir + "\\" + doc.NameSansExt + ".tagged");
                tagger.ProcessFile();
                var filePath = tagger.OutputFilePath;
                TaggedFiles.Add(new TaggedFile(filePath));
            }
        }
        /// <summary>
        ///Asynchronously Invokes the POS tagger on the text files it recieves into storing the newly tagged files
        /// If no arguments are supplied, it will instead convert all yet untagged text files in the project directory
        /// Results are stored in corresponding project directory
        /// </summary>
        /// <param name="files">0 or more instances of the TextFile class which encapsulate text files</param>
        public static async Task TagTextFilesAsync(params TextFile[] files) {
            if (files.Length == 0)
                files = textFiles.ToArray();
            foreach (var doc in from d in files
                                where
                                (from dx in TaggedFiles
                                 where dx.NameSansExt == d.NameSansExt
                                 select dx).Count() == 0
                                select d) {
                var tagger = new SharpNLPTaggingModule.SharpNLPTagger(TaggingOption.TagAndAggregate, doc.FullPath, TaggedFilesDir + "\\" + doc.NameSansExt + ".tagged");

                await Task.Run(() => tagger.ProcessFile());

                TaggedFiles.Add(new TaggedFile(tagger.OutputFilePath));
            }
        }

        /// <summary>
        /// Copies the entire contents of the current project directory to a predetermined, relative path
        /// </summary>
        public static void BackupProject() {
            var projd = new DirectoryInfo(ProjectDir);
            var pard = new DirectoryInfo(projd.Parent.FullName);
            var desitination = Directory.CreateDirectory(pard.FullName + "\\backup\\" + ProjectName);
            foreach (var file in new DirectoryInfo(ProjectDir).GetFiles("*", SearchOption.AllDirectories)) {
                if (!Directory.Exists(file.Directory.Name))
                    desitination.CreateSubdirectory(file.Directory.Parent.Name + "\\" + file.Directory.Name);
                file.CopyTo(desitination.FullName + "\\" + file.Directory.Parent.Name + "\\" + file.Directory.Name + "\\" + file.Name, true);
            }
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the realRoot of the current project directory
        /// </summary>
        public static string ProjectDir {
            get;
            private set;
        }
        /// <summary>
        /// Gets or sets the realRoot of the input file directory
        /// </summary>
        public static string InputFilesDir {
            get;
            private set;
        }
        /// <summary>
        /// Gets or sets the path of the analysis directory which stores temporary files during analysis
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
        /// Gets the name of the current project.
        /// This will be the project name displayed to the user and it corresponds to the project'd top level directory
        /// </summary>
        public static string ProjectName {
            get;
            private set;
        }


        #endregion

        #region Fields

        static List<DocFile> docFiles = new List<DocFile>();
        static List<DocXFile> docXFiles = new List<DocXFile>();
        static List<TextFile> textFiles = new List<TextFile>();
        static List<TaggedFile> TaggedFiles = new List<TaggedFile>();

        #endregion

    }
}