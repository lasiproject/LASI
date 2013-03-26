using System;
using System.Collections.Generic;
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
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class InProgressScreen : Window
    {
        public InProgressScreen() {
            InitializeComponent();
            this.IsVisibleChanged += async (s, e) => await InitPawPrintAlternation();
        }

        private async Task InitPawPrintAlternation() {
            var pawPrints = new[] { pawPrintImg1, pawPrintImg3, pawPrintImg5, pawPrintImg2, pawPrintImg4, pawPrintImg6 }.ToList();
            pawPrints.ForEach(pp => pp.Opacity = 0);
            foreach (var pp in pawPrints) {
                FadeImageOut(pp);
                await Task.Delay(2500);
            }

        }
        private async void FadeImageOut(Image img) {
            while (img.Opacity > 0.0) {
                img.Opacity -= 0.01;
                await Task.Delay(10);
            }
            await Task.Delay(500);
            while (img.Opacity < 1.0) {
                img.Opacity += 0.01;
                await Task.Delay(10);
            }
            FadeImageOut(img);



        }

        private void BindEventHandlers() {

        }

        private void SkipUIDemoButton_Click(object sender, RoutedEventArgs e) {

            WindowManager.ResultsScreen.PositionAt(this.Left, this.Top);
            WindowManager.ResultsScreen.SetTitle(WindowManager.CreateProjectScreen.LastLoadedProjectName + " - L.A.S.I.");
            WindowManager.ResultsScreen.Show();
            this.Hide();
        }
        private void MenuItem_Click_3(object sender, RoutedEventArgs e) {
            this.Close();

        }

        private void TextBox_TextChanged_1(object sender, TextChangedEventArgs e) {

        }

    }
}
