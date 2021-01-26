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
using Start_BFBC2_Servers.XAML;

namespace Start_BFBC2_Servers
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 

    //start Frost.Game.Main_Win32_Final.exe -port 19651 -serverInstancePath "Instance/" -heartBeatInterval 20000 -plasmaServerLog 0 -displayAsserts 0 -crashDumpErrors 1 -timeStampLogNames -mapPack2Enabled 1 -region EU
    public partial class MainWindow : Window
    {

        private IDictionary<Button, UIElement> bUIdictionary = new Dictionary<Button, UIElement>();

        public MainWindow()
        {
            InitializeComponent();
            var cP = new ConsolePanel();
            var scP = new ServerControlPanel(cP);
            var sP = new SettingsPanel(cP);
            

            CenterView.Children.Add(scP);
            CenterView.Children.Add(sP);
            CenterView.Children.Add(cP);


            bUIdictionary.Add(Serv, scP);
            bUIdictionary.Add(Sett, sP);
            bUIdictionary.Add(Cons, cP);

            ManageButtons(Serv, new RoutedEventArgs());
        }

        
        private void MouseDrag(object sender, MouseButtonEventArgs e)
        {
            try
            {
                DragMove();
            }
            catch (InvalidOperationException)
            {
                Console.WriteLine("Invalid operation");
            }
        }

        private void Highlight(object sender, RoutedEventArgs e)
        {

        }

        private void ManageButtons(object sender, RoutedEventArgs e)
        {
            foreach (KeyValuePair<Button, UIElement> kv in bUIdictionary)
            {
                kv.Value.Visibility = Visibility.Hidden;
            }

            bUIdictionary[(Button)sender].Visibility = Visibility.Visible;

            Console.WriteLine(sender.ToString()  + "changed to visible");
        }
    }
}
