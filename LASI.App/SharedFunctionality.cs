using LASI.Utilities;
using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Configuration;
using LASI.Content.Serialization;
using LASI.App.Dialogs;
using System.Linq;
using LASI.App.Extensions;

namespace LASI.App
{
    using static Logger;
    static class SharedFunctionality
    {
        internal static void LaunchLASIWebsite(Window window)
        {
            try
            {
                System.Diagnostics.Process.Start(ConfigurationManager.AppSettings["ProjectWebsite"]);
            }
            catch (Exception x)
            {
                window.ShowMessage(UiMessages.UnableToReachLASIWebSite);
                Log(x.Message);
                Log(x);
            }
        }

        internal static void OpenManualWithInstalledViewer(Window window)
        {
            Action<string> alert = window.ShowMessage;
            try
            {
                System.Diagnostics.Process.Start(AppDomain.CurrentDomain.BaseDirectory + @"\Manual.pdf");
            }
            catch (FileNotFoundException e)
            {
                alert(UiMessages.UnableToLocateManual);
                Log(e.Message);
                Log(e);
            }
            catch (Exception e)
            {
                alert(UiMessages.UnableToOpenManual);
                Log(e.Message);
                Log(e);
            }
        }

        internal static async Task HandleDropAddAsync(Window window, DragEventArgs e, Func<FileInfo, Task> processValid)
        {
            if (!DocumentManager.CanAdd)
            {
                window.ShowMessage(UiMessages.DocumentLimitExceeded);
            }
            else
            {
                var data = e.Data.GetData(DataFormats.FileDrop, true);
                var validFiles = DocumentManager.GetValidFilesInPathList(data as string[]);

                if (validFiles.Any())
                {
                    foreach (var file in validFiles)
                    {
                        var fileNamePresent = DocumentManager.HasFileWithName(file.Name);
                        window.MessageIf(fileNamePresent, $"A document named {file} is already part of the current project.");
                        if (!fileNamePresent)
                        {
                            window.MessageIf(
                                !DocumentManager.AbleToOpen(file),
                                $@"The document {file} is in use by another process. Please close any applications which may be using the file and try again.");
                            if (!DocumentManager.AbleToOpen(file))
                            {
                                await processValid(file);
                            }
                        }
                    }
                }
                window.MessageIf(validFiles.Any, $"Cannot add a file of type {data}. The following formats are supported {UiMessages.ValidDocumentFormats}");

            }
        }

        internal static void HandleDropAdd(Window window, DragEventArgs e, Action<FileInfo> forValid)
        {
            Action<string> alert = window.ShowMessage;
            if (!DocumentManager.CanAdd)
            {
                alert(UiMessages.DocumentLimitExceeded);
            }
            else
            {
                var data = e.Data.GetData(DataFormats.FileDrop, true);
                var validFiles = DocumentManager.GetValidFilesInPathList(data as string[]);
                if (!validFiles.Any())
                {
                    alert($"Cannot add a file of type {data}; " + UiMessages.ValidDocumentFormats);
                }
                else
                {
                    foreach (var file in validFiles)
                    {
                        if (DocumentManager.HasFileWithName(file.Name))
                        {
                            alert($"A document named {file} is already part of the current project.");
                        }
                        else if (!DocumentManager.AbleToOpen(file))
                        {
                            alert($@"The document {file} cannot be opened and may be in use by another process.
                                     Please close any applications which may be using the file and try again.");
                        }
                        else
                        {
                            forValid(file);
                        }
                    }
                }
            }
        }
        internal static ILexicalSerializer<Core.ILexical, object> CreateSerializer() => SerializerFactory.Create(Properties.Settings.Default.OutputFormat);
        internal static void DisplayPreferencesWindow(Window source)
        {
            var preferences = new PreferencesWindow();
            preferences.Left = (source.Left - preferences.Left) / 2;
            preferences.Top = (source.Top - preferences.Top) / 2;
            var saved = preferences.ShowDialog();
        }
        static class UiMessages
        {
            public const string UnableToReachLASIWebSite = "Sorry, the LASI project website could not be opened";
            public const string UnableToLocateManual = "Unable to locate the User Manual, please write to us at thelasiproject@gmail.com for further support.";
            public const string UnableToOpenManual = "Sorry, the manual could not be opened. Please ensure you have a pdf viewer installed.";
            public static readonly string ValidDocumentFormats = $"The following file formats are accepted:\n{DocumentManager.AcceptedFormats}";
            public static readonly string DocumentLimitExceeded = $"A single project may have a maximum of {DocumentManager.MaxDocuments} documents.";
        }
    }
}
//namespace LASI.App.Commands
//{
//    static class MyApplicationCommands
//    {
//        static MyApplicationCommands() { CommandManager.RegisterClassCommandBinding(typeof(MyApplicationCommands), new CommandBinding(exitCommand)); }
//        public static RoutedUICommand exitCommand => new RoutedUICommand("Exit", "Exit", typeof(MyApplicationCommands));
//    }
//}