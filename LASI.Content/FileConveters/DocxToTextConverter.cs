using System.IO;
using System.IO.Compression;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using LASI.Content.FileTypes;
using LASI.Utilities;

namespace LASI.Content.FileConveters
{
    /// <summary>
    /// An input file converter specialized to extract the written content from a .docx (Microsoft word 2003+ open XML file format)
    /// and create a text file containing this content as raw text.
    /// </summary>
    public sealed class DocxToTextConverter : FileConverter<DocXFile, TxtFile>
    {
        /// <summary>
        /// Constructs a new instance which will govern the conversion InputFile instance provided.
        /// The converted file will be placed in the same directory as the original.
        /// </summary>
        /// <param name="infile">The DocXFile instance representing the document to convert.</param>
        public DocxToTextConverter(DocXFile infile) : base(infile)
        {
            DestinationInfo = new FileData(DestinationDir + infile.FileName);
        }

        #region Methods

        /// <summary>
        /// Converts the .docx document into zip archive, deleting any preexisting conversion to zip.
        /// </summary>
        private void DocxToZip()
        {
            var zipName = Path.Combine(DestinationInfo.Directory, DestinationInfo.FileNameSansExt);
            File.Copy(Original.FullPath, zipName + ".zip", overwrite: true);

            if (Directory.Exists(zipName))
            {
                try
                {
                    Directory.Delete(zipName, recursive: true);
                }
                catch (IOException e)
                {
                    Logger.Log(e.Message);
                    throw;
                }
            }
            using (var ZipArch = new ZipArchive(
                stream: new FileStream(zipName + ".zip", FileMode.Open),
                mode: ZipArchiveMode.Read,
                leaveOpen: false)
            )
            {
                if (Directory.Exists(zipName))
                {
                    try
                    {
                        Directory.Delete(zipName, recursive: true);
                    }
                    catch (IOException e)
                    {
                        Logger.Log(e.Message);
                        throw;
                    }
                }
                ZipFileExtensions.ExtractToDirectory(ZipArch, zipName);
                XmlFile = GetRelevantXMLFile(ZipArch);
            }
        }

        /// <summary>
        /// Converts the document governed by this instance from .docx binary format to .txt ASCII text format.
        /// </summary>
        /// <returns>An input document object representing the newly converted file
        /// Note that both the original and converted document objects can be also be accessed independently via instance properties</returns>
        public override TxtFile ConvertFile()
        {
            ExtractAndConfigure();

            using (var xmlReader = XmlReader.Create(CreateInputStream(XmlFile.FullPath), XmlReaderSettings))
            using (var writer = new StreamWriter(
                stream: new FileStream(DestinationInfo.FullPathSansExt + ".txt", FileMode.Create),
                encoding: Encoding.UTF8,
                bufferSize: 100
            ))
            {
                xmlReader.ReadStartElement();
                while (xmlReader.Read())
                {
                    ProcessNode(xmlReader, writer);
                }
            }

            Converted = new TxtFile(Original.PathSansExt + ".txt");

            return Converted;
        }

        /// <summary>
        /// This method invokes the file conversion routine asynchronously, generally in a separate thread.
        /// Use with the await operator in an async method to retrieve the new file object and specify a
        /// continuation function to be executed when the conversion is complete.
        /// </summary>
        /// <returns>A The A Task&lt;TextFile&gt; object which functions as a proxy for the actual InputFile while the conversion routine is in progress.
        /// Access the internal input file encapsulated by the Task by using syntax such as : var file = await myConverter.ConvertFileAsync()
        /// </returns>
        public override async Task<TxtFile> ConvertFileAsync()
        {
            ExtractAndConfigure();

            using (var xmlReader = XmlReader.Create(CreateInputStream(XmlFile.FullPath), XmlReaderSettings))
            using (var writer = new StreamWriter(
                stream: new FileStream(DestinationInfo.FullPathSansExt + ".txt", FileMode.Create),
                encoding: Encoding.UTF8)
            )
            {
                xmlReader.ReadStartElement();
                while (await xmlReader.ReadAsync())
                {
                    await ProcessNodeAsync(xmlReader, writer);
                }
            }

            Converted = new TxtFile(Original.PathSansExt + ".txt");

            return Converted;
        }

        private void ExtractAndConfigure()
        {
            DocxToZip();
            XmlFile = new XmlFile(DestinationInfo.Directory + DestinationInfo.FileNameSansExt + @"\word\document.xml");
        }

        private static Stream CreateInputStream(string fullPath) => new FileStream(
                path: fullPath,
                mode: FileMode.Open,
                access: FileAccess.Read,
                share: FileShare.Read,
                bufferSize: 1024,
                options: FileOptions.Asynchronous
            );

        private static async Task ProcessNodeAsync(XmlReader xmlReader, StreamWriter writer)
        {
            if (xmlReader.Name == "w:p")
            {
                await writer.WriteAsync("<paragraph>");
            }
            var value = xmlReader.Value;
            if (!string.IsNullOrWhiteSpace(value))
            {
                await writer.WriteAsync(value);
            }
            if (xmlReader.Name.Contains("tbl"))
            {
                await xmlReader.SkipAsync();
            }
            if (xmlReader.Name == "w:p")
            {
                await writer.WriteAsync("</paragraph>\n");
            }
        }

        private static void ProcessNode(XmlReader xmlReader, StreamWriter writer)
        {
            if (xmlReader.Name == "w:p")
            {
                writer.Write("<paragraph>");
            }
            var value = xmlReader.Value;
            if (!string.IsNullOrWhiteSpace(value))
            {
                writer.Write(value);
            }
            if (xmlReader.Name.Contains("tbl"))
            {
                xmlReader.Skip();
            }
            if (xmlReader.Name == "w:p")
            {
                writer.Write("</paragraph>\n");
            }
        }

        /// <summary>
        /// Extracts the XML file containing the significant text of the of the docx file from its corresponding zip file.
        /// </summary>
        /// <param name="arch">The object which represents the zip file from which to extract.</param>
        /// <returns>An Instance of GenericXMLFile which wraps the extracted .xml.</returns>
        private XmlFile GetRelevantXMLFile(ZipArchive arch)
        {
            var extractedFile = arch.GetEntry(@"word/document.xml");
            var absolutePath = Original.PathSansExt + @"/" + extractedFile.FullName;
            return new XmlFile(absolutePath);
        }
        #endregion

        #region Properties

        /// <summary>
        /// Utility object containing information specifying details about the converted file before it is created.
        /// </summary>
        private FileData DestinationInfo { get; }

        /// <summary>
        /// Gets or sets the XmlFile which contains the significant text of the .docx document.
        /// </summary>
        private InputFile XmlFile { get; set; }

        /// <summary>
        /// The <see cref="System.Xml.XmlReaderSettings"/> to use for reading the underlying contents of the docx file.
        /// </summary>
        private static XmlReaderSettings XmlReaderSettings => new XmlReaderSettings
        {
            CloseInput = true,
            Async = true,
            IgnoreWhitespace = true
        };

        #endregion
    }
}
