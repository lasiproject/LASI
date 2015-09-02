using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using LASI.Utilities;

namespace LASI.App
{
    internal static class DocumentManager
    {
        public static void AddDocument(string fileName, string filePath)
        {
            var docEntry = new ListViewItem { Tag = filePath, Content = fileName };
            var button = new Button { Content = "x", Height = 16, Width = 16, Padding = new Thickness(0.5), };
            button.Click += delegate
            {
                runningListDisplay.Items.Remove(docEntry);
                xButtons.Children.Remove(button);
                --documentCount;
                if (IsEmpty)
                {
                    runningListDisplay.Opacity = 0.25;
                }
                browseForDocButton.IsEnabled = true;
            };
            xButtons.Children.Add(button);
            runningListDisplay.Items.Add(docEntry);

            itemsAdded.Add(docEntry);

            lastDocumentPathTextBox.Text = fileName;
            ++documentCount;
            if (CanAdd)
            {
                runningListDisplay.Opacity = 1.0;
            }
            else
            {
                browseForDocButton.IsEnabled = false;
            }

            NumberOfDocumentsChanged?.Invoke(runningListDisplay, new RoutedPropertyChangedEventArgs<double>(documentCount - 1, documentCount));
        }

        public static IEnumerable<FileInfo> GetValidFilesInPathList(IEnumerable<string> paths)
        {
            var validFiles = from path in paths
                             let contentsIfDir = Directory.Exists(path) ? Directory.EnumerateFileSystemEntries(path) : new string[] { }
                             let dirContentsOrFile = contentsIfDir.Any() ? GetValidFilesInPathList(contentsIfDir) : new[] { new FileInfo(path) }
                             from file in dirContentsOrFile
                             where AcceptedFormats.Contains(file.Extension)
                             select file;
            return validFiles.Take(MaxDocuments - documentCount);
        }

        public static bool HasFileWithName(string name) => runningListDisplay.Items.OfType<ListViewItem>().Any(item => item.Content.ToString().Trim().EqualsIgnoreCase(name.Trim()));

        public static void Initialize(ListBox listBox, Panel xbuttons, UIElement browseButton, TextBox lastPathTextBox)
        {
            runningListDisplay = listBox;
            xButtons = xbuttons;
            browseForDocButton = browseButton;
            lastDocumentPathTextBox = lastPathTextBox;
        }

        public static void RemoveByFileName(string name)
        {
            var remove = itemsAdded.FirstOrDefault(item => item.Content.ToString().Substring(0, item.Content.ToString().LastIndexOf('.')) == name);
            if (remove != null)
            {
                itemsAdded.Remove(remove);
                runningListDisplay.Items.Remove(remove);
                --documentCount;
            }
            NumberOfDocumentsChanged?.Invoke(runningListDisplay, new RoutedPropertyChangedEventArgs<double>(documentCount + 1, documentCount));
        }

        /// <summary>
        /// Returns true if the file represented by the given file info is locked by the operating
        /// system or another application.
        /// </summary>
        /// <param name="file"></param>
        /// <returns>
        /// <c>true</c> if the file represented by the given file info is locked by the operating
        /// system or another application; otherwise, <c>false</c>.
        /// </returns>
        public static bool AbleToOpen(FileInfo file)
        {
            try
            {
                using (Stream stream = new FileStream(file.FullName, FileMode.Open))
                {
                    return stream.CanRead;
                }
            }
            catch (IOException e)
            {
                Logger.Log(e.Message);
                Logger.Log(e.StackTrace);
                return false;
            }
        }

        /// <summary>
        /// Gets a value indicating whether or not there is space for at least one additional
        /// document in the DocumentManager's working set.
        /// </summary>
        public static bool CanAdd => MaxDocuments - documentCount > 0;

        /// <summary>
        /// Gets a value indicating whether or not the DocumentManager has any documents in its
        /// working set.
        /// </summary>
        public static bool IsEmpty => documentCount == 0;

        /// <summary>
        /// e Gets a string array containing all of the file extensions accepted by the DocumentManager.
        /// </summary>
        public static IEnumerable<string> AcceptedFormats => acceptedFormats;

        public const string FileTypeFilter = "Documents File Types|*.doc; *.docx; *.pdf; *.txt";

        /// <summary>
        /// The maximum number of documents which can be added by the user.
        /// </summary>
        public const int MaxDocuments = 10;

        private static readonly string[] acceptedFormats = { ".doc", ".docx", ".txt", ".pdf" };
        private static UIElement browseForDocButton;
        private static int documentCount;
        private static ICollection<ListViewItem> itemsAdded = new List<ListViewItem>();
        private static TextBox lastDocumentPathTextBox;
        private static ListBox runningListDisplay;
        private static Panel xButtons;

        #region Events

        public static event RoutedPropertyChangedEventHandler<double> NumberOfDocumentsChanged;

        #endregion Events
    }
}