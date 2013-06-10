using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LASI.UserInterface
{
    internal static class DocumentManager
    {
        public static void Initialize(System.Windows.Controls.ListBox listBox, System.Windows.Controls.Panel xbuttons, System.Windows.UIElement browseButton, System.Windows.Controls.TextBox lastPathTextBox)
        {
            runningListBox = listBox;
            xButtons = xbuttons;
            browseForDocButton = browseButton;
            lastDocumentPathTextBox = lastPathTextBox;
        }
        public static bool FileNamePresent(string documentName)
        {
            return (from alreadyAdded in runningListBox.Items.OfType<System.Windows.Controls.ListViewItem>()
                    select alreadyAdded.Content.ToString()).Contains(documentName);
        }
        public static IEnumerable<FileInfo> GetValidFilesInPathList(IEnumerable<string> filePaths)
        {
            return (from path in filePaths
                    let fileInfo = new FileInfo(path)
                    where AcceptedFormats.Contains(fileInfo.Extension)
                    select fileInfo).Take(MaxDocuments - NumberOfDocuments);

        }
        public static void AddUserDocument(string fileName, string filePath)
        {
            var docEntry = new System.Windows.Controls.ListViewItem
            {
                Tag = filePath,
                Content = fileName
            };
            var button = new System.Windows.Controls.Button
            {
                Content = "x",
                Height = 16,
                Width = 16,
                Padding = new System.Windows.Thickness(0.5),
                //Style = FindResource("xButton") as System.Windows.Style
            };


            button.Click += (s, args) =>
            {

                runningListBox.Items.Remove(docEntry);
                xButtons.Children.Remove(button);
                DocumentManager.NumberOfDocuments--;
                if (DocumentManager.IsEmpty) {

                    runningListBox.Opacity = 0.25;
                }

                browseForDocButton.IsEnabled = true;


            };


            xButtons.Children.Add(button);
            runningListBox.Items.Add(docEntry);
            lastDocumentPathTextBox.Text = fileName;
            DocumentManager.NumberOfDocuments++;
            if (DocumentManager.AddingAllowed) {

                runningListBox.Opacity = 100;
            }

            if (DocumentManager.IsEmpty) {
                browseForDocButton.IsEnabled = false;
            }
        }
        static System.Windows.Controls.ListBox runningListBox;



        private static int numberOfDocuments;
        public const int MaxDocuments = 5;
        private static System.Windows.Controls.Panel xButtons;
        private static System.Windows.UIElement browseForDocButton;
        private static System.Windows.Controls.TextBox lastDocumentPathTextBox;

        public static bool AddingAllowed
        {
            get
            {
                return MaxDocuments - numberOfDocuments > 0;
            }
        }
        public static bool IsEmpty
        {
            get
            {
                return numberOfDocuments == 0;
            }
        }

        public static int NumberOfDocuments
        {
            get
            {
                return DocumentManager.numberOfDocuments;
            }
            set
            {
                DocumentManager.numberOfDocuments = value;
            }
        }
        /// <summary>
        /// Thanks to ChrisW @ stackoverflow.com for this lightweight check.
        /// Returns true if the given file info is locked.
        /// </summary>
        /// <param name="file"></param>
        /// <returns>true if the given file info is locked, false otherwise.</returns>
        public static bool FileIsLocked(FileInfo file)
        {
            FileStream stream = null;
            try {
                stream = file.Open(FileMode.Open, FileAccess.ReadWrite, FileShare.None);
            }
            catch (IOException) {
                //the file is unavailable because it is:
                //still being written to
                //or being processed by another thread
                //or does not exist (has already been processed)
                return true;
            }
            finally {
                if (stream != null)
                    stream.Close();
            }

            //file is not locked
            return false;
        }
        private static readonly string[] acceptedFormats = { ".docx", ".txt", ".pdf" };

        public static string[] AcceptedFormats
        {
            get
            {
                return acceptedFormats;
            }
        }
    }
}
