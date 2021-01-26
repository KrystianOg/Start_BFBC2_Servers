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
using System.Windows.Navigation;
using System.Windows.Shapes;

using Start_BFBC2_Servers.Class;

namespace Start_BFBC2_Servers.XAML
{
    /// <summary>
    /// Interaction logic for ServerControl.xaml
    /// </summary>
    public partial class ServerControl : UserControl
    {
        public Server Server
        {
            get;
            private set;
        }

        private ServerControlPanel cp;
        private ConsolePanel Console;
        
        public ServerControl(Server server,ServerControlPanel cp,ConsolePanel console)
        {
            Console = console;
            this.Server = server;
            InitializeComponent();
            this.ServerName.Content = server.Name;
            this.cp = cp;
        }

       public void Run_Server(object sender, RoutedEventArgs e)
       {
            Console.WriteLine($"Starting server {Server.Name} on port {Server.Port}");
            Server.Run();
            SetOnlineIcon();
       }

        public void Restart_Server(object sender,RoutedEventArgs e)
        {
            Console.WriteLine($"Restarting server {Server.Name} on port {Server.Port}");
            Server.Restart();
            SetOnlineIcon();
        }

        public void Close_Server(object sender,RoutedEventArgs e)
        {
            Console.WriteLine($"Closing server {Server.Name} on port {Server.Port}");
            Server.Close();
            SetOnlineIcon();
        }

        private void Delete_Server(object sender,RoutedEventArgs e)
        {
            Server.Close();
            File.Delete(Write.assemblyD + @"\servers\" + Server.Name + ".txt");
            Server = null;
            cp.RemoveServer(this);

            Console.WriteLine($"Successfully deleted server {Server.Name}");

        }

        
        private void SetOnlineIcon()
        {
            if (Server.Running)
                Online.Fill = new SolidColorBrush(Color.FromArgb(255, 0, 255, 0));
            else
                Online.Fill = new SolidColorBrush(Color.FromArgb(255, 255, 0, 0));
        }

        public bool? IsChecked()
        {
            return CB.IsChecked;
        }
    }
}
