using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using LASI.Utilities;
using static System.Linq.Enumerable;

namespace LASI.App
{
    internal static class DocumentManager
    {
        public static void AddDocument(string fileName, string filePath)
        {
            var docEntry = new ListViewItem
            {
                Tag = filePath,
                Content = fileName
            };
            var button = new Button
            {
                Content = "x",
                Height = 16,
                Width = 16,
                Padding = new Thickness(0.5)
            };

            button.Click += (s, e) =>
            {
                RunningListDisplay.Items.Remove(docEntry);
                XButtons.Children.Remove(button);
                DocumentCount -= 1;
                if (IsEmpty)
                {
                    RunningListDisplay.Opacity = 0.25;
                }
                BrowseForDocButton.IsEnabled = true;
            };
            XButtons.Children.Add(button);
            RunningListDisplay.Items.Add(docEntry);

            ItemsAdded.Add(docEntry);

            LastDocumentPathTextBox.Text = fileName;
            DocumentCount += 1;

            if (CanAdd)
            {
                RunningListDisplay.Opacity = 1.0;
            }
            else
            {
                BrowseForDocButton.IsEnabled = false;
            }

            NumberOfDocumentsChanged(RunningListDisplay, new RoutedPropertyChangedEventArgs<double>(DocumentCount - 1, DocumentCount));
        }

        public static IEnumerable<FileInfo> GetValidFilesInPathList(IEnumerable<string> paths)
        {
            var validFiles =
                from path in paths
                let contentsIfDir = Directory.Exists(path)
                       ? Directory.EnumerateFileSystemEntries(path)
                       : Empty<string>()
                let dirContentsOrFile = contentsIfDir.Any()
                    ? GetValidFilesInPathList(contentsIfDir)
                    : Repeat(new FileInfo(path), 1)
                from file in dirContentsOrFile
                where AcceptedFormats.Contains(file.Extension)
                select file;

            return validFiles.Take(MaxDocuments - DocumentCount);
        }

        public static bool HasFileWithName(string name)
        {
            return RunningListDisplayItems
                .Any(matches);

            bool matches(ListViewItem item) => item.Content.ToString().Trim().EqualsIgnoreCase(name.Trim());
        }

        public static void Initialize(ListBox listBox, Panel xbuttons, UIElement browseButton, TextBox lastPathTextBox)
        {
            RunningListDisplay = listBox;
            XButtons = xbuttons;
            BrowseForDocButton = browseButton;
            LastDocumentPathTextBox = lastPathTextBox;
        }

        public static void RemoveByFileName(string name)
        {
            var remove = ItemsAdded.FirstOrDefault(matchesTargetName);
            if (remove != null)
            {
                RemoveFile(remove);
            }

            NumberOfDocumentsChanged(RunningListDisplay, new RoutedPropertyChangedEventArgs<double>(DocumentCount + 1, DocumentCount));

            bool matchesTargetName(ListViewItem item) => item.Content.ToString().Substring(0, item.Content.ToString().LastIndexOf('.')) == name;
        }

        public static void RemoveAll() => ItemsAdded.ToList().ForEach(RemoveFile);

        static void RemoveFile(ListViewItem remove)
        {
            ItemsAdded.Remove(remove);
            RunningListDisplay.Items.Remove(remove);
            DocumentCount -= 1;
        }

        /// <summary>
        /// Returns true if the file represented by the given file info is locked by the operating system or another application. 
        /// </summary>
        /// <param name="file"></param>
        /// <returns> <c> true </c> if the file represented by the given file info is locked by the operating system or another application; otherwise, <c> false </c>. </returns>
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

        static IEnumerable<ListViewItem> RunningListDisplayItems => RunningListDisplay.Items.OfType<ListViewItem>();

        /// <summary>
        /// Gets a value indicating whether or not there is space for at least one additional document in the DocumentManager's working set. 
        /// </summary>
        public static bool CanAdd => MaxDocuments - DocumentCount > 0;

        /// <summary>
        /// Gets a value indicating whether or not the DocumentManager has any documents in its working set. 
        /// </summary>
        public static bool IsEmpty => !HasAny;

        /// <summary>
        /// Gets a value indicating whether or not the DocumentManager has any documents in its working set. 
        /// </summary>
        public static bool HasAny => DocumentCount > 0;

        /// <summary>
        /// e Gets a string array containing all of the file extensions accepted by the DocumentManager. 
        /// </summary>
        public static IEnumerable<string> AcceptedFormats { get; } = new[] { ".doc", ".docx", ".txt", ".pdf" };

        public const string FileTypeFilter = "Documents File Types|*.doc; *.docx; *.pdf; *.txt";

        /// <summary>
        /// The maximum number of documents which can be added by the user. 
        /// </summary>
        public const int MaxDocuments = 10;

        static UIElement BrowseForDocButton;
        static int DocumentCount;
        static ICollection<ListViewItem> ItemsAdded = new List<ListViewItem>();
        static TextBox LastDocumentPathTextBox;
        static ListBox RunningListDisplay;
        static Panel XButtons;

        #region Events

        public static event RoutedPropertyChangedEventHandler<double> NumberOfDocumentsChanged = delegate { };

        #endregion Events
    }
}
