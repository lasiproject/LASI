using LASI.ContentSystem;
using LASI.UserInterface.Dialogs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace LASI.UserInterface
{
    /// <summary>
    /// Interaction logic for ExportResultsDialog.xaml
    /// </summary>
    public partial class ProjectPreviewScreen : Window
    {
        public ProjectPreviewScreen() {
            InitializeComponent();
            var titleText = Resources["CurrentProjectName"] as string ?? Title;
        }

        #region Methods

        #region Document Preview Construction

        public async void LoadDocumentPreviews() {
            foreach (var textfile in FileManager.TextFiles) {
                await LoadTextandTabAsync(textfile);
            }
            DocumentPreview.SelectedIndex = 0;
        }

        private async Task LoadTextandTabAsync(TextFile textfile) {
            var processedText = await await textfile.GetTextAsync().ContinueWith(async (t) => {
                var data = await t;
                return data.Split(new[] { "\r\n\r\n", "<paragraph>", "</paragraph>" }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(s => s.Trim())
                    .Aggregate((sum, s) => sum += "\n\t" + s);
            });
            var item = new TabItem {
                Header = textfile.NameSansExt,
                Content = new TextBox {
                    IsReadOnly = true,
                    VerticalScrollBarVisibility = ScrollBarVisibility.Auto,
                    TextWrapping = TextWrapping.Wrap,
                    Text = processedText,
                    FontSize = 12
                },
            };
            DocumentPreview.Items.Add(item);
            DocumentPreview.SelectedItem = item;
        }

        private async Task AddNewDocument(string docPath) {
            var chosenFile = FileManager.AddFile(docPath, true);
            await FileManager.ConvertAsNeededAsync();
            var textfile = FileManager.TextFiles.Where(f => f.NameSansExt == chosenFile.NameSansExt).First();
            await LoadTextandTabAsync(textfile);
            CheckIfAddingAllowed();
        }

        private void CheckIfAddingAllowed() {
            var addingEnabled = DocumentManager.AddingAllowed;
            AddNewDocumentButton.IsEnabled = addingEnabled;
            FileMenuAdd.IsEnabled = addingEnabled;
        }

        #endregion

        #region Named Event Handlers

        private async void StartButton_Click(object sender, RoutedEventArgs e) {
            this.Hide();
            WindowManager.InProgressScreen.Show();
            await WindowManager.InProgressScreen.InitializeParsing();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e) {
            Application.Current.Shutdown();
        }
        private void FileExitMenuItem_Click(object sender, RoutedEventArgs e) {
            this.Close();
            Application.Current.Shutdown();
        }
        private void RemoveCurrentDocument_Click(object sender, RoutedEventArgs e) {
            var docSelected = DocumentPreview.SelectedItem;
            if (docSelected != null) {
                DocumentPreview.Items.Remove(docSelected);
                DocumentManager.RemoveDocument((docSelected as TabItem).Header.ToString());
                FileManager.RemoveFile((docSelected as TabItem).Header.ToString());
                CheckIfAddingAllowed();

            }

        }
        private async void mainGrid_Drop(object sender, DragEventArgs e) {
            if (DocumentManager.AddingAllowed) {
                var validDroppedFiles = DocumentManager.GetValidFilesInPathList(e.Data.GetData(System.Windows.DataFormats.FileDrop, true) as string[]);
                if (!validDroppedFiles.Any()) {
                    MessageBox.Show(this, string.Format("Only the following file formats are accepted:\n{0}", string.Join(", ", DocumentManager.AcceptedFormats)));
                } else if (!validDroppedFiles.Any(fn => !DocumentManager.FileNamePresent(fn.Name))) {
                    MessageBox.Show(this, string.Format("A document named {0} is already part of the project.", validDroppedFiles.First()));
                } else {
                    foreach (var droppedFile in validDroppedFiles) {
                        if (!DocumentManager.FileIsLocked(droppedFile)) {
                            DocumentManager.AddDocument(droppedFile.Name, droppedFile.FullName);
                            await AddNewDocument(droppedFile.FullName);
                        } else {
                            MessageBox.Show(this, string.Format("The document {0} is in use by another process, please close any applications which may be using the file and try again.", droppedFile));
                        }

                    }
                }
            } else {
                MessageBox.Show(this, "A single project may only contain 5 documents.");
            }
        }
        private async void AddNewDocument_Click(object sender, RoutedEventArgs e) {
            var openDialog = new Microsoft.Win32.OpenFileDialog {
                Filter = "LASI File Types|*.docx; *.pdf; *.txt",

            };
            openDialog.ShowDialog(this);
            if (openDialog.FileNames.Count() <= 0) {
                return;
            }


            var file = new FileInfo(openDialog.FileName);
            if (DocumentManager.FileNamePresent(file.Name)) {
                MessageBox.Show(this, string.Format("A document named {0} is already part of the project.", file));
            } else if (!DocumentManager.FileIsLocked(file)) {
                DocumentManager.AddDocument(file.Name, file.FullName);
                await AddNewDocument(file.FullName);
            } else {
                MessageBox.Show(this, string.Format("The document {0} is in use by another process, please close any applications which may be using the file and try again.", file));
            }

        }
        private void openPreferencesMenuItem_Click(object sender, RoutedEventArgs e) {
            var preferences = new PreferencesWindow();
            preferences.Left = (this.Left - preferences.Left) / 2;
            preferences.Top = (this.Top - preferences.Top) / 2;
            var saved = preferences.ShowDialog();
        }

        #endregion

        #endregion
        #region Help Menu

        private void OpenManualMenuItem_Click_1(object sender, RoutedEventArgs e) {
            try {
                System.Diagnostics.Process.Start(System.AppDomain.CurrentDomain.BaseDirectory + @"\Manual.pdf");
            } catch (FileNotFoundException) {
                MessageBox.Show(this, "Unable to locate the User Manual, please contact the LASI team (thelasiproject@gmail.com) for further support.");
            } catch (Exception) {
                MessageBox.Show(this, "Sorry, the manual could not be opened. Please ensure you have a pdf viewer installed.");
            }
        }

        private void openLicensesMenuItem_Click_1(object sender, RoutedEventArgs e) {
            var componentsDisplay = new ComponentInfoDialogWindow {
                Left = this.Left,
                Top = this.Top,
                Owner = this
            };
            componentsDisplay.ShowDialog();
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e) {
            try {
                System.Diagnostics.Process.Start("http://lasi-product.org");
            } catch (Exception) {
                MessageBox.Show(this, "Sorry, the LASI project website could not be opened");
            }
        }

        #endregion


    }
}
