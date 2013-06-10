using LASI.FileSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.IO;
using LASI.FileSystem.FileTypes;
using System.Threading.Tasks;

namespace LASI.UserInterface
{
    /// <summary>
    /// Interaction logic for DialogToProcedeToResults.xaml
    /// </summary>
    public partial class ProjectPreviewScreen : Window
    {
        public ProjectPreviewScreen()
        {
            InitializeComponent();
            var titleText = WindowManager.CreateProjectScreen.LastLoadedProjectName;
            if (titleText != null)
                Title = titleText;
            BindEventHandlers();
            this.Closing += (s, e) => Application.Current.Shutdown();
        }



        public async void LoadDocumentPreviews()
        {
            foreach (var textfile in FileManager.TextFiles) {
                await LoadTextandTab(textfile);
            }
            DocumentPreview.SelectedIndex = 0;
        }

        private async Task LoadTextandTab(FileSystem.FileTypes.TextFile textfile)
        {
            using (StreamReader reader = new StreamReader(textfile.FullPath)) {
                var data = reader.ReadToEnd();
                var docu = await reader.ReadToEndAsync().ContinueWith((t) =>
                {
                    return (from d in data.Split(new[] { "\r\n\r\n", "<paragraph>", "</paragraph>" }, StringSplitOptions.RemoveEmptyEntries)
                            select d.Trim()).ToList().Aggregate("", (sum, s) => sum += "\n\t" + s);
                });

                var item = new TabItem
                {
                    Header = textfile.NameSansExt,
                    Content = new TextBox
                    {
                        IsReadOnly = true,
                        VerticalScrollBarVisibility = ScrollBarVisibility.Auto,
                        TextWrapping = TextWrapping.Wrap,
                        Text = docu,
                        FontSize = 12
                    },
                    Focusable = true

                };
                DocumentPreview.Items.Add(item);
                DocumentPreview.SelectedItem = item;
            }
        }



        private void BindEventHandlers()
        {

            this.Closing += (s, e) => Application.Current.Shutdown();

        }
        private async void StartButton_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            WindowManager.InProgressScreen.Show();

            await WindowManager.InProgressScreen.InitializeParsing();


        }

        private void backButton_Click_1(object sender, RoutedEventArgs e)
        {
            WindowManager.CreateProjectScreen.PositionAt(this.Left, this.Top);
            WindowManager.CreateProjectScreen.Show();
            this.Hide();
        }




        private void FileExitMenuItem_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void TextBox_TextChanged_1(object sender, TextChangedEventArgs e)
        {

        }

        private void RemoveCurrentDocument_Click(object sender, RoutedEventArgs e)
        {
            var docSelected = DocumentPreview.SelectedItem;
            if (docSelected != null) {
                DocumentPreview.Items.Remove(docSelected);
                FileManager.RemoveFile((docSelected as TabItem).Header.ToString());
                CheckIfAddingAllowed();
            }

        }
        private async void mainGrid_Drop(object sender, DragEventArgs e)
        {
            var validDroppedFiles = DocumentManager.GetValidFilesInPathList(e.Data.GetData(System.Windows.DataFormats.FileDrop, true) as string[]);
            if (!validDroppedFiles.Any()) {
                MessageBox.Show(string.Format("Only the following file formats are accepted:\n{0}", DocumentManager.AcceptedFormats.Aggregate((sum, current) => sum += ", " + current)));
            } else if (!validDroppedFiles.Any(fn => !DocumentManager.FileNamePresent(fn.Name))) {
                MessageBox.Show(string.Format("A document named {0} is already part of the project.", validDroppedFiles.First()));
            } else {
                foreach (var droppedFile in validDroppedFiles) {
                    DocumentManager.AddUserDocument(droppedFile.Name, droppedFile.FullName);
                    await AddNewDocument(droppedFile.FullName);
                }
            }
        }
        private async void AddNewDocument_Click(object sender, RoutedEventArgs e)
        {
            var openDialog = new Microsoft.Win32.OpenFileDialog
            {
                Filter = "LASI File Types|*.docx; *.pdf; *.txt",

            };
            openDialog.ShowDialog(this);
            if (openDialog.FileNames.Count() <= 0) {
                return;
            }


            var docPath = openDialog.FileName;
            await AddNewDocument(docPath);

        }

        private async Task AddNewDocument(string docPath)
        {
            var chosenFile = FileManager.AddFile(docPath, true);

            await FileManager.ConvertAsNeededAsync();

            var textfile = FileManager.TextFiles.Where(f => f.NameSansExt == chosenFile.NameSansExt).First();

            await LoadTextandTab(textfile);
            CheckIfAddingAllowed();
        }

        private void CheckIfAddingAllowed()
        {
            var addingEnabled = DocumentPreview.Items.Count == 5 ? false : true;
            AddNewDocumentButton.IsEnabled = addingEnabled;
            FileMenuAdd.IsEnabled = addingEnabled;
        }

        private void Window_DragOver(object sender, DragEventArgs e)
        {

        }

        private void openPreferencesMenuItem_Click(object sender, RoutedEventArgs e)
        {
            
        }




    }
}
