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

namespace Start_BFBC2_Servers.XAML
{
    /// <summary>
    /// Interaction logic for ConsolePanel.xaml
    /// </summary>
    public partial class ConsolePanel : UserControl
    {
        public ConsolePanel()
        {
            InitializeComponent();
        }

        public void WriteLine(string consoleText)
        {
            TextBlock t = new TextBlock();
            t.TextWrapping = TextWrapping.Wrap;
            t.Background = Brushes.Transparent;
            t.Foreground = Brushes.White;
            t.Margin = new Thickness(0, 3, 0, 3);

            t.Inlines.Add(new Run("> "));
            t.Inlines.Add(consoleText);

            Elements.Children.Add(t);
        }
    }
}
