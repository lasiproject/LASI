//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using System.IO;
//using LASI.Content;

//namespace LASI.Core.Tests
//{


//    /// <summary>
//    ///This is a test class for FileConverterTest and is intended
//    ///to contain all FileConverterTest Unit Tests
//    ///</summary>
//    [TestClass]
//    public class FileConverterTest
//    {
//        private const string TEST_FILE_LOCATION = @"..\..\..\TestDocs\";
//        private const string TEST_FILE_NAME = "Draft_Environmental_Assessment2.docx";
//        private const string TEST_WORKING_PATH = "FileConverterTest";

//        /// <summary>
//        ///Gets or sets the test context which provides
//        ///information about and functionality for the current test run.
//        ///</summary>
//        public TestContext TestContext { get; set; }


//        private class FileHandler
//        {
//            public FileHandler(string testFolderName = TEST_WORKING_PATH, string fileLocation = TEST_FILE_LOCATION, string sourceName = TEST_FILE_NAME)
//            {

//            }
//            public string WorkingDirectory { get { return Path.Combine(TEST_FILE_LOCATION, TEST_WORKING_PATH); } }
//            public string FilePath { get { return Path.Combine(WorkingDirectory, TEST_FILE_NAME); } }
//            public void Init()
//            {
//                if (!Directory.Exists(WorkingDirectory)) { Directory.CreateDirectory(WorkingDirectory); }
//                File.Copy(Path.Combine(TEST_FILE_LOCATION, TEST_FILE_NAME), Path.Combine(WorkingDirectory, TEST_FILE_NAME), overwrite: true);
//            }
//            public void Clean()
//            {
//                Directory.Delete(WorkingDirectory, recursive: true);
//            }
//        }



//        /// <summary>
//        ///A test for ConvertFile
//        ///</summary>
//        public void ConvertFileTestHelper<TSource, TDestination>()
//            where TSource : InputFile
//            where TDestination : InputFile
//        {
//            var fileHandler = new FileHandler();
//            fileHandler.Init();
//            var filePath = fileHandler.FilePath;
//            FileConverter<TSource, TDestination> target = CreateFileConverter<TSource, TDestination>(filePath);
//            TDestination actual;
//            actual = target.ConvertFile() as TDestination;
//            TDestination expected = new TxtFile(filePath.Substring(0, filePath.LastIndexOf('.')) + ".txt") as TDestination;
//            Assert.AreEqual(expected, actual);
//            fileHandler.Clean();
//        }


//        internal virtual FileConverter<TSource, TDestination> CreateFileConverter<TSource, TDestination>(string filePath)
//            where TSource : InputFile
//            where TDestination : InputFile
//        {
//            FileConverter<TSource, TDestination> target = new DocxToTextConverter(
//                new DocXFile(filePath)) as FileConverter<TSource, TDestination>;

//            return target;
//        }

//        [TestMethod]
//        public void ConvertFileTest()
//        {
//            ConvertFileTestHelper<DocXFile, TxtFile>();
//        }

//        /// <summary>
//        ///A test for ConvertFileAsync
//        ///</summary>
//        public void ConvertFileAsyncTestHelper<TSource, TDestination>()
//            where TSource : InputFile
//            where TDestination : InputFile
//        {
//            var fileHandler = new FileHandler();
//            fileHandler.Init();
//            var filePath = fileHandler.FilePath;
//            FileConverter<TSource, TDestination> target = CreateFileConverter<TSource, TDestination>(filePath);
//            TDestination actual;
//            actual = target.ConvertFileAsync().Result;
//            TDestination expected = new TxtFile(filePath.Substring(0, filePath.LastIndexOf('.')) + ".txt") as TDestination;
//            Assert.AreEqual(expected, actual);
//            fileHandler.Clean();
//        }

//        [TestMethod]
//        public void ConvertFileAsyncTest()
//        {
//            ConvertFileAsyncTestHelper<DocXFile, TxtFile>();
//        }
//    }
//}
