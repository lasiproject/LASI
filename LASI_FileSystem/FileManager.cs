using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace LASI.FileSystem
{
    public static class FileManager
    {
        #region Methods

        /// <summary>
        /// Initializes the FileManager, setting its project directory to the given value.
        /// Automatically loads existing files and sets up input paths
        /// </summary>
        /// <param name="projectDir">The root directory of the current project</param>
        public static void Initialize(string projectDir) {
            ProjectName = projectDir.Substring(projectDir.LastIndexOf('\\') + 1);
            ProjectDir = projectDir;
            InputFilesDir = ProjectDir + @"\input";
            DocFilesDir = InputFilesDir + @"\doc";
            DocxFilesDir = InputFilesDir + @"\docx";
            TextFilesDir = InputFilesDir + @"\text";
            TaggedFilesDir = InputFilesDir + @"\tagged";
            AnalysisDir = @"\analysis";
            ResultsDir = @"\results";
            CheckProjectDirs();
        }

        /// <summary>
        /// Checks the existing contents of the current project directory and automatically loads the files it finds. Called by initialize
        /// </summary>
        private static void CheckProjectDirs() {
            if (!Directory.Exists(ProjectDir)) {
                Directory.CreateDirectory(ProjectDir);
            }
            if (Directory.Exists(DocFilesDir))
                foreach (var docPath in Directory.EnumerateFiles(DocFilesDir))
                    docFiles.Add(new DocFile(docPath));
            else
                Directory.CreateDirectory(DocFilesDir);
            if (Directory.Exists(DocxFilesDir))
                foreach (var docPath in Directory.EnumerateFiles(DocxFilesDir))
                    docXFiles.Add(new DocXFile(docPath));
            else
                Directory.CreateDirectory(DocxFilesDir);
            if (Directory.Exists(TextFilesDir))
                foreach (var docPath in Directory.EnumerateFiles(TextFilesDir))
                    textFiles.Add(new TextFile(docPath));
            else
                Directory.CreateDirectory(TextFilesDir);
            if (Directory.Exists(TaggedFilesDir))
                foreach (var docPath in Directory.EnumerateFiles(TaggedFilesDir))
                    TaggedFiles.Add(new TaggedFile(docPath));
            else
                Directory.CreateDirectory(TaggedFilesDir);
        }

        /// <summary>
        /// Copies the .doc file at the given path to the appropriate subfolder of the current project
        /// </summary>
        /// <param name="sourcePath">The path of the file to add</param>
        public static void AddDocFile(string sourcePath) {
            var FD = new FileData(sourcePath);
            var path = DocFilesDir + "\\" + FD.FileNameWithExt;
            File.Copy(sourcePath, path);
            var file = new DocFile(path);
            docFiles.Add(file);
        }

        /// <summary>
        /// Copies the .docx file at the given path to the appropriate subfolder of the current project
        /// </summary>
        /// <param name="sourcePath">The path of the file to add</param>
        public static void AddDocXFile(string sourcePath) {
            var FD = new FileData(sourcePath);

            var path = DocxFilesDir + "\\" + FD.FileNameWithExt;
            File.Copy(sourcePath, path);
            var file = new DocXFile(path);
            docXFiles.Add(file);
        }


        /// <summary>
        /// Copies the text file at the given path to the appropriate subfolder of the current project
        /// </summary>
        /// <param name="sourcePath">The path of the file to add</param>
        public static void AddTextFile(string sourcePath) {
            var FD = new FileData(sourcePath);
            var path = TextFilesDir + "\\" + FD.FileNameWithExt;
            File.Copy(sourcePath, path);
            var file = new TextFile(path);
            textFiles.Add(file);
        }

        /// <summary>
        /// Copies the .lasi file at the given path to the appropriate subfolder of the current project
        /// </summary>
        /// <param name="sourcePath">The path of the file to add</param>
        public static void AddLasiFile(string sourcePath) {
            var FD = new FileData(sourcePath);

            var path = TaggedFilesDir + "\\" + FD.FileNameWithExt;
            File.Copy(sourcePath, path);
            var file = new TaggedFile(path);
            TaggedFiles.Add(file);
        }

        /// <summary>
        /// Add one or more DocFiles to the project
        /// </summary>
        /// <param name="files">The collection of DocFiles to add</param>
        public static void AddFiles(params DocFile[] files) {
            foreach (var file in files)
                AddDocFile(file.FullPath);
        }

        /// <summary>
        /// Add one or more DocXFiles to the project
        /// </summary>
        /// <param name="files">The collection of DocXFiles to add</param>
        public static void AddFiles(params DocXFile[] files) {
            foreach (var file in files)
                AddDocXFile(file.FullPath);
        }
        /// <summary>
        /// Add one or more TextFiles to the project
        /// </summary>
        /// <param name="files">The collection of TextFiles to add</param>
        public static void AddFiles(params TextFile[] files) {
            foreach (var file in files)
                AddTextFile(file.FullPath);
        }

        /// <summary>
        /// Add one or more TextFiles to the project
        /// </summary>
        /// <param name="files">The collection of TextFiles to add</param>
        public static void AddFiles(params TaggedFile[] files) {
            foreach (var file in files)
                AddLasiFile(file.FullPath);
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
                AddFiles(converted as DocXFile);
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
                                (from dx in docXFiles
                                 where dx.NameSansExt == d.NameSansExt
                                 select dx).Count() == 0
                                select d) {
                var converter = new DocxToTextConverter(doc, TextFilesDir);
                var converted = converter.ConvertFile();
                textFiles.Add(converted as TextFile);
            }
        }

        /// <summary>
        /// Invoked the POS tagger on the text files it recieves into storing the newly tagged files
        /// If no arguments are supplied, it will instead convert all yet untagged text files in the project directory
        /// Results are stored in corresponding project directory
        /// </summary>
        /// <param name="files">0 or more instances of the TextFile class which encapsulate text files</param>
        public static void TagTextFile(params TextFile[] files) {
            if (files.Length == 0)
                files = textFiles.ToArray();
            foreach (var doc in from d in files
                                where
                                (from dx in TaggedFiles
                                 where dx.NameSansExt == d.NameSansExt
                                 select dx).Count() == 0
                                select d) {
                var tagger = new SharpNLPTaggingModule.SharpNLPTagger(TaggingOption.TagAndAggregate, doc.FullPath, doc.PathSansExt + @".tagged");
                tagger.ProcessFile();
                var file = new TaggedFile(tagger.OutputFilePath);
                AddFiles(file);
            }
        }

        /// <summary>
        /// Copies the entire contents of the current project directory to a predetermined, relative path
        /// </summary>
        public static void BackupProject() {
            var desitination = Directory.CreateDirectory(Directory.GetParent(ProjectDir).Parent + @"\backup\" + ProjectName);

            foreach (var file in new DirectoryInfo(ProjectDir).GetFiles("*", SearchOption.AllDirectories)) {
                file.CopyTo(desitination.FullName + file.Directory + file.Name, true);
            }
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the root of the current project directory
        /// </summary>
        public static string ProjectDir {
            get;
            private set;
        }
        /// <summary>
        /// Gets or sets the root of the input file directory
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
        /// Gets or sets the result files directory
        /// </summary>
        public static string ResultsDir {
            get;
            private set;
        }

        /// <summary>
        /// Gets or sets the .tagged files directory
        /// </summary>
        public static string TaggedFilesDir {
            get;
            private set;
        }
        /// <summary>
        /// Gets or sets the .doc files directory
        /// </summary>
        public static string DocFilesDir {
            get;
            private set;
        }
        /// <summary>
        /// Gets or sets the .docx files directory
        /// </summary>
        public static string DocxFilesDir {
            get;
            private set;
        }

        /// <summary>
        /// Gets or sets the .txt files directory
        /// </summary>
        public static string TextFilesDir {
            get;
            private set;
        }
        /// <summary>
        /// Gets or sets the name of the current project.
        /// This will be the project name displayed to the user and it corresponds to the project's top level directory
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