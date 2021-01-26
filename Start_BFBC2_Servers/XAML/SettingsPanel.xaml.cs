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
    /// Interaction logic for SettingsPanel.xaml
    /// </summary>
    public partial class SettingsPanel : UserControl
    {
        public static string Path {
            get;
            private set;
        }

        private ConsolePanel Console;
        

        public SettingsPanel(ConsolePanel console)
        {
            Console = console;
            InitializeComponent();
            Path = Read.GetPath();
            PathBlock.Text = Path;
        }

        public SettingsPanel(string path)
        {
            Path = path;
        }

        private void Save(object sender,RoutedEventArgs e)
        {
            Path = PathBlock.Text;
            Write.SaveSettings(Path);
        }
    }
}
