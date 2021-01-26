using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Diagnostics;
using Start_BFBC2_Servers.XAML;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Threading;

namespace Start_BFBC2_Servers.Class
{ 
    public class Server 
    {
        public int Port
        {
            get;
            private set;
        }
        private string ServerInstancePath;
        private int HeartBeatInterval;
        private bool PlasmaServerLog;
        private bool DisplayAsserts;
        private bool CrashDumpErrors;
        private bool TimeStampLogNames;
        private bool MapPack2Enabled;
        private string Region;

        private Process cmd = null;
        string command = "start Frost.Game.Main_Win32_Final.exe -port 19651 -serverInstancePath \"Instance/\" -heartBeatInterval 20000 -plasmaServerLog 0 -displayAsserts 0 -crashDumpErrors 1 -timeStampLogNames -mapPack2Enabled 1 -region EU";
            
        public string Name
        {
            get;
            private set;
        }

        public bool Running
        {
            get;
            private set;
        }

        public Server(string name, int port)
        {
            SetDefault();
            this.Name = name;
            this.Port = port;
        }

        public void Run()
        {
            if (Running)
                return;

            (new Thread(() =>
            {
                
                    Console.WriteLine($"Starting server {Name}");

                    cmd = new Process();

                    cmd.StartInfo.FileName = "cmd.exe";
                    cmd.StartInfo.UseShellExecute = false;
                    cmd.StartInfo.RedirectStandardOutput = true;
                    cmd.StartInfo.RedirectStandardInput = true;
                    cmd.StartInfo.CreateNoWindow = true;

                    cmd.Start();
                    var command = $"start Frost.Game.Main_Win32_Final.exe -port {Port} -serverInstancePath \"Instance/\" -heartBeatInterval {HeartBeatInterval} -plasmaServerLog {Convert.ToInt32(PlasmaServerLog)} -displayAsserts {Convert.ToInt32(DisplayAsserts)} -crashDumpErrors {Convert.ToInt32(CrashDumpErrors)} -timeStampLogNames {Convert.ToInt32(TimeStampLogNames)} -mapPack2Enabled {Convert.ToInt32(MapPack2Enabled)} -region {Region}";

                    cmd.StandardInput.WriteLine("cd " + SettingsPanel.Path);
                    cmd.StandardInput.WriteLine(command);

                    Console.WriteLine($"{command} \nhas been executed");
            })).Start();

            
            Running = true;
        }

        public void Restart()
        {
            Console.WriteLine("restarting");
            Close();
            Run();
            
        }

        public void Close()
        {

            if (!Running || cmd == null)
            {
                Console.WriteLine($"Server {Name} is not running.");
                return;
            }

            Console.WriteLine("closing...");
            try
            {
                if (!cmd.HasExited)
                {
                    cmd.Close();
                }
            }
            catch (NullReferenceException e)
            {
                Console.WriteLine("server was not started yet.");
            }
            
            Running = false;
        }


        private void SetDefault()
        {
            Port = 19651;
            ServerInstancePath = "Instance/";
            HeartBeatInterval = 20000;
            PlasmaServerLog = false;
            DisplayAsserts = false;
            CrashDumpErrors = true;
            TimeStampLogNames = false;
            MapPack2Enabled = true;
            Region = "EU";
        }

        public string Save()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("port:").Append(Port).Append(Environment.NewLine);
            sb.Append("name:").Append(Name).Append(Environment.NewLine);
            sb.Append("ServerInstancePath:").Append(ServerInstancePath).Append(Environment.NewLine);
            sb.Append("HeartBeatInterval:").Append(HeartBeatInterval).Append(Environment.NewLine);
            sb.Append("PlasmaServerLog:").Append(PlasmaServerLog).Append(Environment.NewLine);
            sb.Append("DisplayAsserts:").Append(DisplayAsserts).Append(Environment.NewLine);
            sb.Append("CrashDumpErrors:").Append(CrashDumpErrors).Append(Environment.NewLine);
            sb.Append("TimeStampLogNames:").Append(TimeStampLogNames).Append(Environment.NewLine);
            sb.Append("MapPack2Enabled:").Append(MapPack2Enabled).Append(Environment.NewLine);
            sb.Append("Region:").Append(Region).Append(Environment.NewLine);
            return sb.ToString();
        }

    }


}
