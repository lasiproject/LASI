using LASI.Utilities;
using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Configuration;
using LASI.Content.Serialization;
using LASI.App.Dialogs;
using System.Collections.Generic;
using System.Linq;

namespace LASI.App
{
    static class SharedFunctionality
    {
        static class UiMessages
        {
            public static readonly string UnableToReachLASIWebSite = "Sorry, the LASI project website could not be opened";
            public static readonly string UnableToLocateManual = "Unable to locate the User Manual, please write to us at thelasiproject@gmail.com for further support.";
            public static readonly string UnableToOpenManual = "Sorry, the manual could not be opened. Please ensure you have a pdf viewer installed.";
            public static readonly string ValidDocumentFormats = $"The following file formats are accepted:\n{DocumentManager.AcceptedFormats}";
            public static readonly string DocumentLimitExceeded = $"A single project may have a maximum of {DocumentManager.MAX_DOCUMENTS} documents.";

        }

        internal static void LaunchLASIWebsite(Window source) {
            try {
                System.Diagnostics.Process.Start(ConfigurationManager.AppSettings["ProjectWebsite"]);
            } catch (Exception x) {
                MessageBox.Show(source, UiMessages.UnableToReachLASIWebSite);
                Output.WriteLine(x.Message); Output.WriteLine(x);
            }
        }
        internal static void OpenManualWithInstalledViewer(Window source) {
            try {
                System.Diagnostics.Process.Start(System.AppDomain.CurrentDomain.BaseDirectory + @"\Manual.pdf");
            } catch (FileNotFoundException) {
                MessageBox.Show(source, UiMessages.UnableToLocateManual);
            } catch (Exception x) {
                MessageBox.Show(source, UiMessages.UnableToOpenManual);
                Output.WriteLine(x.Message); Output.WriteLine(x);
            }
        }
        public static void DisplayMessage(this Window window, string message) {
            MessageBox.Show(window, message);
        }
        public static void DisplayMessageWhen(this Window window, bool condition, string message) {
            if (condition) { window.DisplayMessage(message); }
        }
        internal static async Task HandleDropAddAsync(Window source, DragEventArgs e, Func<FileInfo, Task> processValid) {
            if (!DocumentManager.CanAdd) {
                MessageBox.Show(source, UiMessages.DocumentLimitExceeded);
            } else {
                var data = e.Data.GetData(DataFormats.FileDrop, true);
                var validFiles = DocumentManager.GetValidFilesInPathList(data as string[]);

                if (validFiles.Any()) {
                    foreach (var file in validFiles) {
                        var fileNamePresent = DocumentManager.HasFileWithName(file.Name);
                        source.DisplayMessageWhen(fileNamePresent, $"A document named {file} is already part of the current project.");
                        if (!fileNamePresent) {
                            source.DisplayMessageWhen(file.UnableToOpen(), $"The document {file} is in use by another process, please close any applications which may be using the file and try again.");
                            if (!file.UnableToOpen()) {
                                await processValid(file);
                            }
                        }
                    }
                }
                source.DisplayMessageWhen(!validFiles.Any(), $"Cannot add a file of type {data}; " + UiMessages.ValidDocumentFormats);

            }
        }


        internal static void HandleDropAdd(Window source, DragEventArgs e, Action<FileInfo> whereValid) {
            if (!DocumentManager.CanAdd) {
                MessageBox.Show(source, UiMessages.DocumentLimitExceeded);
            } else {
                var data = e.Data.GetData(DataFormats.FileDrop, true);
                var validFiles = DocumentManager.GetValidFilesInPathList(data as string[]);
                if (!validFiles.Any()) {
                    MessageBox.Show(source, $"Cannot add a file of type {data}; " + UiMessages.ValidDocumentFormats);
                } else {
                    foreach (var file in validFiles) {
                        if (DocumentManager.HasFileWithName(file.Name)) {
                            source.DisplayMessage($"A document named {file} is already part of the current project.");
                        } else {
                            if (file.UnableToOpen()) {
                                source.DisplayMessage($"The document {file} is in use by another process, please close any applications which may be using the file and try again.");
                            } else {
                                whereValid(file);
                            }
                        }
                    }
                }
            }
        }

        internal static ILexicalSerializer<Core.ILexical, object> CreateSerializer() {
            var format = Properties.Settings.Default.OutputFormat;
            return SerializerFactory.Create(format);
        }
        internal static void DisplayPreferencesWindow(Window source) {
            var preferences = new PreferencesWindow();
            preferences.Left = (source.Left - preferences.Left) / 2;
            preferences.Top = (source.Top - preferences.Top) / 2;
            var saved = preferences.ShowDialog();
        }
    }
}
namespace LASI.App.Commands
{
    static class MyApplicationCommands
    {
        static MyApplicationCommands() { CommandManager.RegisterClassCommandBinding(typeof(MyApplicationCommands), new CommandBinding(exitCommand)); }
        public static RoutedUICommand exitCommand { get { return new RoutedUICommand("Exit", "Exit", typeof(MyApplicationCommands)); } }
    }
}