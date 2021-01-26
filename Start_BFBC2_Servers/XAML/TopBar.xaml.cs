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

using Start_BFBC2_Servers.Class;
namespace Start_BFBC2_Servers.XAML
{
    /// <summary>
    /// Interaction logic for TopBar.xaml
    /// </summary>
    public partial class TopBar : UserControl
    {

        public TopBar()
        {
            InitializeComponent();

        }

        private void exit_Click(object sender, RoutedEventArgs e)
        {
            //zapis 
            Write.SaveSettings(SettingsPanel.Path);
            Write.SaveServerData(ServerControlPanel.GetServers());
            Application.Current.Shutdown();

        }

        private void maximize_Click(object sender, RoutedEventArgs e)
        {
            Window window = Window.GetWindow(this);
            if (window.WindowState.Equals(WindowState.Maximized))
            {
                Maximize.Source = new BitmapImage(new Uri(@"/Images/maximize.png",UriKind.Relative));
                window.WindowState = WindowState.Normal;
            }
            else
            {
                Maximize.Source = new BitmapImage(new Uri(@"/Images/exit_fullscreen.png", UriKind.Relative));
                window.WindowState = WindowState.Maximized;
            }
        }

        private void minimize_Click(object sender, RoutedEventArgs e)
        {
            Window window = Window.GetWindow(this);
            window.WindowState = WindowState.Minimized;
        }
    }
}
