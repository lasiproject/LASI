using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace LASI.UserInterface
{
    static class SharedScreenFunctionality
    {
        internal static void ProcessOpenWebsiteRequest(Window sourceOfRequest)
        {
            try {
                System.Diagnostics.Process.Start("http://lasi-project.org");
            }
            catch (Exception) {
                MessageBox.Show(sourceOfRequest, "Sorry, the LASI project website could not be opened");
            }
        }
        internal static void ProcessOpenManualRequest(Window sourceOfRequest)
        {
            try {
                System.Diagnostics.Process.Start(System.AppDomain.CurrentDomain.BaseDirectory + @"\Manual.pdf");
            }
            catch (FileNotFoundException) {
                MessageBox.Show(sourceOfRequest, "Unable to locate the User Manual, please contact the LASI team (thelasiproject@gmail.com) for further support.");
            }
            catch (Exception) {
                MessageBox.Show(sourceOfRequest, "Sorry, the manual could not be opened. Please ensure you have a pdf viewer installed.");
            }
        }
        internal static void OpenPreferencesWindow(Window sourceOfRequest)
        {
            var preferences = new PreferencesWindow();
            preferences.Left = (sourceOfRequest.Left - preferences.Left) / 2;
            preferences.Top = (sourceOfRequest.Top - preferences.Top) / 2;
            var saved = preferences.ShowDialog();
        }

        internal static async Task HandleDropAddAttemptAsync(Window targetWindow, DragEventArgs e, Func<FileInfo, Task> validDocumentProcess)
        {
            if (DocumentManager.AddingAllowed) {
                var filesInValidFormats = DocumentManager.GetValidFilesInPathList(e.Data.GetData(System.Windows.DataFormats.FileDrop, true) as string[]);
                if (filesInValidFormats.None()) {
                    MessageBox.Show(targetWindow, string.Format("Only the following file formats are accepted:\n{0}", string.Join(",", DocumentManager.AcceptedFormats)));
                } else {
                    foreach (var droppedFile in filesInValidFormats) {
                        if (DocumentManager.FileNamePresent(droppedFile.Name)) {
                            MessageBox.Show(targetWindow, string.Format("A document named {0} is already part of the projects.", droppedFile));
                        } else {
                            if (DocumentManager.FileIsLocked(droppedFile)) {
                                MessageBox.Show(targetWindow, string.Format("The document {0} is in use by another process, please close any applications which may be using the file and try again.", droppedFile));
                            } else {
                                await validDocumentProcess(droppedFile);
                            }
                        }
                    }
                }
            } else {
                MessageBox.Show(targetWindow, string.Format("A single project may only contain {0} documents.", DocumentManager.MaxDocuments));
            }
        }
        internal static void HandleDropAddAttempt(Window targetWindow, DragEventArgs e, Action<FileInfo> validDocumentProcess)
        {
            if (DocumentManager.AddingAllowed) {
                var filesInValidFormats = DocumentManager.GetValidFilesInPathList(e.Data.GetData(System.Windows.DataFormats.FileDrop, true) as string[]);
                if (filesInValidFormats.None()) {
                    MessageBox.Show(targetWindow, string.Format("Only the following file formats are accepted:\n{0}", string.Join(",", DocumentManager.AcceptedFormats)));
                } else {
                    foreach (var droppedFile in filesInValidFormats) {
                        if (DocumentManager.FileNamePresent(droppedFile.Name)) {
                            MessageBox.Show(targetWindow, string.Format("A document named {0} is already part of the projects.", droppedFile));
                        } else {
                            if (DocumentManager.FileIsLocked(droppedFile)) {
                                MessageBox.Show(targetWindow, string.Format("The document {0} is in use by another process, please close any applications which may be using the file and try again.", droppedFile));
                            } else {
                                validDocumentProcess(droppedFile);
                            }
                        }
                    }
                }
            } else {
                MessageBox.Show(targetWindow, string.Format("A single project may only contain {0} documents.", DocumentManager.MaxDocuments));
            }
        }
    }
}
