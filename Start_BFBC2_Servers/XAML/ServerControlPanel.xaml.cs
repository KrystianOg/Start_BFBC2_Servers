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
    /// Interaction logic for ServerControlPanel.xaml
    /// </summary>
    /// 


    public partial class ServerControlPanel : UserControl
    {
        private static List<ServerControl> Servers = new List<ServerControl>();
        private ConsolePanel Console;
        public ServerControlPanel(ConsolePanel console)
        {
            Console = console;
            InitializeComponent();

            //read
            var servers = Read.GetServerData();

            foreach (Server s in servers)
            {
                Servers.Add(new ServerControl(s, this,Console));
            }

            foreach (ServerControl s in Servers)
            {
                ServersView.Children.Add(s);
            }

            Back(BackB, new RoutedEventArgs());
        }

        public void RunAllServers(object sender, RoutedEventArgs e)
        {
            foreach (var server in Servers)
            {
                if ((bool)server.IsChecked()&&!server.Server.Running)
                {
                    server.Run_Server(sender,e);
                }
            }
        }

        private void ToAddServerPanel(object sender, RoutedEventArgs e)
        {
            AddPanel.Visibility = Visibility.Visible;
            Viewer.Visibility = Visibility.Hidden;
        }

        private void Back(object sender, RoutedEventArgs e)
        {
            AddPanel.Visibility = Visibility.Hidden;
            Viewer.Visibility = Visibility.Visible;
        }

        private void AddServer(object sender, RoutedEventArgs e)
        {

            try
            {
                var name = nameBox.Text;
                var port = int.Parse(portBox.Text);

                foreach (ServerControl sc in Servers)
                {
                    if (port.Equals(sc.Server.Port))
                        throw new PortAlreadyInUseException(port);
                }

                var newS = new ServerControl(new Server(name, port), this,Console);
                Servers.Add(newS);
                ServersView.Children.Add(newS);

                Back(BackB, new RoutedEventArgs());
                Console.WriteLine($"Server \'{nameBox.Text}\' successfully created on port {portBox.Text}.");
            }
            catch (FormatException fe)
            {
                //napis ze null
                Console.WriteLine(fe.Message);
            } catch (PortAlreadyInUseException pe)
            {
                Console.WriteLine(pe.Message);
            }


        }

        public void RemoveServer(ServerControl control)
        {
            ServersView.Children.Remove(control);
            Servers.Remove(control);
        }

        public static List<Server> GetServers()
        {
            List<Server> servers = new List<Server>();
            foreach (ServerControl s in Servers)
            {
                servers.Add(s.Server);
            }
            return servers;
        }
    }

    public class PortAlreadyInUseException : Exception
    {
        public PortAlreadyInUseException(int port) :base($"Port {port} is already in use. Try other one.")
        {

        }

    }
}
