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
        public static void Initialize(System.Windows.Controls.ListBox listBox, System.Windows.Controls.Panel xbuttons, System.Windows.UIElement browseButton, System.Windows.Controls.TextBox lastPathTextBox) {
            runningListBox = listBox;
            xButtons = xbuttons;
            browseForDocButton = browseButton;
            lastDocumentPathTextBox = lastPathTextBox;
        }
        public static bool FileNamePresent(string documentName) {
            return (from alreadyAdded in runningListBox.Items.OfType<System.Windows.Controls.ListViewItem>()
                    select alreadyAdded.Content.ToString()).Any(doc => doc.Trim().ToUpper() == documentName.Trim().ToUpper());
        }
        public static IEnumerable<FileInfo> GetValidFilesInPathList(IEnumerable<string> filePaths) {
            return (from path in filePaths
                    let fileInfo = new FileInfo(path)
                    where AcceptedFormats.Contains(fileInfo.Extension)
                    select fileInfo).Take(MaxDocuments - numberOfDocuments);

        }
        public static void RemoveDocument(string fileName) {
            var remove = (from item in itemsAdded
                          where item.Content.ToString().Substring(0, item.Content.ToString().LastIndexOf('.')) == fileName
                          select item).FirstOrDefault();
            if (remove != null) {
                itemsAdded.Remove(remove);
                runningListBox.Items.Remove(remove);
                --numberOfDocuments;
            }
        }
        public static void AddDocument(string fileName, string filePath) {
            var docEntry = new System.Windows.Controls.ListViewItem {
                Tag = filePath,
                Content = fileName
            };
            var button = new System.Windows.Controls.Button {
                Content = "x",
                Height = 16,
                Width = 16,
                Padding = new System.Windows.Thickness(0.5),

            };
            button.Click += (s, args) => {
                runningListBox.Items.Remove(docEntry);
                xButtons.Children.Remove(button);
                --numberOfDocuments;
                if (IsEmpty) {
                    runningListBox.Opacity = 0.25;
                }
                browseForDocButton.IsEnabled = true;
            };
            xButtons.Children.Add(button);
            runningListBox.Items.Add(docEntry);

            itemsAdded.Add(docEntry);


            lastDocumentPathTextBox.Text = fileName;

            ++numberOfDocuments;
            if (AddingAllowed) {

                runningListBox.Opacity = 1.0;
            } else {
                browseForDocButton.IsEnabled = false;
            }
        }
        static System.Windows.Controls.ListBox runningListBox;
        static List<System.Windows.Controls.ListViewItem> itemsAdded = new List<System.Windows.Controls.ListViewItem>();


        private static int numberOfDocuments;
        public const int MaxDocuments = 5;
        private static System.Windows.Controls.Panel xButtons;
        private static System.Windows.UIElement browseForDocButton;
        private static System.Windows.Controls.TextBox lastDocumentPathTextBox;
        /// <summary>
        /// Gets a value indicating wether or not there is space for at least one additional document in the DocumentManager's working set.
        /// </summary>
        public static bool AddingAllowed {
            get {
                return MaxDocuments - numberOfDocuments > 0;
            }
        }
        /// <summary>
        /// Gets a value indicating wether or not the DocumentManager has any documents in its working set.
        /// </summary>
        public static bool IsEmpty {
            get {
                return numberOfDocuments == 0;
            }
        }
        /// <summary>
        /// Returns true if the file represented by the given file info is locked by the operating system or another application.
        /// </summary>
        /// <param name="file"></param>
        /// <returns>true if the file represented by the given file info is locked by the operating system or another application, false otherwise.</returns>
        public static bool FileIsLocked(FileInfo file) {
            try {
                using (Stream stream = new FileStream(file.FullName, FileMode.Open)) {
                    return false;
                }
            } catch (IOException) {
                return true;
            }
        }


        private static readonly string[] acceptedFormats = { ".doc", ".docx", ".txt", ".pdf" };

        /// <summary>
        /// Gets a string array containing all of the file extensions accepted by the DocumentManager.
        /// </summary>
        public static string[] AcceptedFormats {
            get {
                return acceptedFormats;
            }
        }
    }
}
