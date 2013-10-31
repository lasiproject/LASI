using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
namespace LASI.ContentSystem
{
    /// <summary>
    /// An input file converter specialized to extract the written content from a .docx (Microsoft word 2003+ open XML file format)
    /// and create a text file containing this content as raw text.
    /// </summary>
    public class DocxToTextConverter : FileConverter<DocXFile, TextFile>
    {
        #region Constructors

        /// <summary>
        /// Constructs a new instance which will govern the conversion InputFile instance provided.
        /// The converted file will be placed in the same diretory as the original.
        /// </summary>
        /// <param name="infile">The DocXFile instance representing the document to convert.</param>
        public DocxToTextConverter(DocXFile infile) :
            base(infile) {
            DestinationInfo = new FileData(destinationDir + infile.FileName);
        }

        #endregion

        #region Methods

        /// <summary>
        /// Converts the .docx document into zip archive, deleting any preexisting conversion to zip.
        /// </summary>
        /// <returns>An object referring to the newly created zipfile.</returns>
        protected virtual ZipArchive DocxToZip() {
            string zipName = DestinationInfo.Directory + DestinationInfo.FileNameSansExt + ".zip";
            File.Copy(Original.FullPath, zipName, true);

            if (Directory.Exists(DestinationInfo.Directory + DestinationInfo.FileNameSansExt)) {
                Directory.Delete(DestinationInfo.Directory + DestinationInfo.FileNameSansExt, true);
            }
            ZipArch = new ZipArchive(new FileStream(zipName, FileMode.Open), ZipArchiveMode.Read, false);
            ZipArch.ExtractToDirectory(DestinationInfo.Directory + DestinationInfo.FileNameSansExt);
            XmlFile = GetRelevantXMLFile(ZipArch);
            return ZipArch;
        }

        /// <summary>
        /// Converts the document governed by this instance from .docx binary format to .txt ascii text format.
        /// </summary>
        /// <returns>An input document object representing the newly converted file
        /// Note that both the original and converted document objects can be also be accessed independtly via instance properties</returns>
        public override TextFile ConvertFile() {
            DocxToZip();
            XmlFile = new GenericXMLFile(DestinationInfo.Directory + DestinationInfo.FileNameSansExt + @"\word\document.xml");
            using (XmlReader xmlReader = XmlReader.Create(new FileStream(XmlFile.FullPath, FileMode.Open, FileAccess.Read), new XmlReaderSettings {
                IgnoreWhitespace = true
            })) {
                using (var writer = new StreamWriter(
                    new FileStream(DestinationInfo.FullPathSansExt + ".txt",
                        FileMode.Create),
                        Encoding.UTF8, 100)) {
                    xmlReader.ReadStartElement();
                    while (xmlReader.Read()) {
                        if (xmlReader.Name == "w:p") {
                            writer.Write("<paragraph>");
                        }
                        var value = xmlReader.Value;
                        if (!string.IsNullOrWhiteSpace(value)) {
                            writer.Write(value);

                        }

                        if (xmlReader.Name.Contains("tbl"))
                            xmlReader.Skip();


                        if (xmlReader.Name == "w:p") {
                            writer.Write("</paragraph>\n");
                        }

                    }

                }
            }
            Converted = new TextFile(Original.PathSansExt + ".txt");
            return Converted;

        }
        /// <summary>
        /// Extracts the xml file containing the significant text of the of the docx file from its corresponding zip file.
        /// </summary>
        /// <param name="arch">The object which represents the zip file from which to extract.</param>
        /// <returns>An Instance of GenericXMLFile which wraps the extracted .xml.</returns>
        GenericXMLFile GetRelevantXMLFile(ZipArchive arch) {
            var extractedFile = arch.GetEntry(@"word/document.xml");
            var absolutePath = Original.PathSansExt + @"/" + extractedFile.FullName;
            return new GenericXMLFile(absolutePath);
        }
        /// <summary>
        /// This method invokes the file conversion routine asynchronously, gernerally in a serparate thread.
        /// Use with the await operator in an asnyc method to retrieve the new file object and specify a continuation function to be executed when the conversion is complete.
        /// </summary>
        /// <returns>A The A Task&lt;InputFile&gt; object which functions as a proxy for the actual InputFile while the conversion routine is in progress.
        /// Access the internal input file encapsulated by the Task by using syntax such as : var file = await myConverter.ConvertFileAsync()
        /// </returns>
        public async override Task<TextFile> ConvertFileAsync() {
            return await Task.Run(() => ConvertFile());
        }

        #endregion

        #region Properties

        /// <summary>
        /// Utility object containing information specifying details about the converted file before it is created.
        /// </summary>
        private FileData DestinationInfo {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the XmlFile which contains the significant text of the .docx document.
        /// </summary>
        protected virtual InputFile XmlFile {
            get;
            set;
        }
        /// <summary>
        /// Gets or sets the ziparchive which is created during the conversion process.
        /// </summary>
        protected virtual ZipArchive ZipArch {
            get;
            set;
        }

        /// <summary>
        /// Gets the document object which is the fruit of the conversion process
        /// This additional method of accessing the new document is primarily provided to facilitate asynchronous programming
        /// and any access attempts before the conversion is complete will raise a NullReferenceException.
        /// </summary>
        public override TextFile Converted {
            get;
            protected set;
        }

        #endregion
    }
}
