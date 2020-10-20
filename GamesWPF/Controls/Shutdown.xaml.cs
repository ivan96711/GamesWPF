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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GamesWPF.Controls
{
    /// <summary>
    /// Логика взаимодействия для Shutdown.xaml
    /// </summary>
    public partial class Shutdown : UserControl
    {
        public Shutdown()
        {
            InitializeComponent();
        }

        private void ContentControl_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void ContentControl_MouseLeftButtonUp_1(object sender, MouseButtonEventArgs e)
        {
            var mainWin = Application.Current.Windows.Cast<Window>().FirstOrDefault(window => window is MainWindow) as MainWindow;
            if (mainWin.WindowState == WindowState.Normal)
            {
                mainWin.WindowStyle = WindowStyle.SingleBorderWindow;
                mainWin.WindowState = WindowState.Maximized;
                mainWin.WindowStyle = WindowStyle.None;

                ////Get Existing 'WindowChrome' Properties.
                //var captionHeight = mainWin.chrome.CaptionHeight;

                ////Set Existing 'WindowChrome' Properties.
                //mainWin.chrome.GlassFrameThickness = new Thickness(2d);

                ////Assign a New 'WindowChrome'.
                //chrome = new System.Windows.Shell.WindowChrome();
            }
            else
            {
                mainWin.WindowStyle = WindowStyle.SingleBorderWindow;
                mainWin.WindowState = WindowState.Normal;
                mainWin.WindowStyle = WindowStyle.None;
            }
        }

        private void ContentControl_MouseLeftButtonUp_2(object sender, MouseButtonEventArgs e)
        {
            var mainWin = Application.Current.Windows.Cast<Window>().FirstOrDefault(window => window is MainWindow) as MainWindow;
            mainWin.WindowStyle = WindowStyle.SingleBorderWindow;
            mainWin.WindowState = WindowState.Minimized;
            mainWin.WindowStyle = WindowStyle.None;
        }
    }
}
