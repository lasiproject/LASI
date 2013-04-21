using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace LASI.UserInterface
{
    /// <summary>
    /// This class provides static access to each of the 5 windows defined by the lasi user interface
    /// In addition to this, it defines some useful extension methods which stream the manipulation of windows
    /// </summary>
    internal static class WindowManager
    {
        static WindowManager() {

            CreateProjectScreen = new CreateProjectScreen();
            LoadedProjectScreen = new ProjectPreviewScreen();
            InProgressScreen = new InProgressScreen();
            ResultsScreen = new ResultsScreen();
        }

        public static CreateProjectScreen CreateProjectScreen {
            get;
            set;
        }

        public static StartupScreen StartupScreen {
            get;
            set;
        }

        public static ResultsScreen ResultsScreen {
            get;
            set;
        }


        public static InProgressScreen InProgressScreen {
            get;
            set;
        }

        public static ProjectPreviewScreen LoadedProjectScreen {
            get;
            set;
        }

        public static void PositionAt(this Window window, double left, double top) {
            window.Left = left;
            window.Top = top;
        }
        public static void SetTitle(this Window window, string title) {
            window.Title = title;
        }
        public static void SwapWith(this Window window, Window other) {
            other.PositionAt(window.Left, window.Top);
            other.Show();
            window.Hide();
        }
    }
}
