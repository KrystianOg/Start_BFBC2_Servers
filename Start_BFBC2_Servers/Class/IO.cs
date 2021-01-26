using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Start_BFBC2_Servers.Class
{
    public class Write
    {
        public static string assemblyD = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

        public static void SaveSettings(string saveString)
        {
            var path = assemblyD + @"\settings.txt";

            CreateFilePath(path);
            File.WriteAllText(path, saveString);
            Console.WriteLine("saved settings");
        }

        public static void SaveServerData(List<Server> serverData)
        {

            foreach(Server s in serverData)
            {
                var path = assemblyD + @"\servers\"+s.Name.Replace(" ","_")+@".txt";

                CreateFilePath(path);
                File.WriteAllText(path, s.Save());
            }
            Console.WriteLine("saved servs");

        }

        public static void CreateFilePath(string path)
        {
            new FileInfo(path).Directory.Create();
        }

        
    }

    public class Read
    {
        public static string GetPath()
        {
            var path = Write.assemblyD + @"\settings.txt";
            Console.WriteLine(path);
            Write.CreateFilePath(path);


            try
            {
                string line = File.ReadAllText(path);
                Console.WriteLine(line);
                return line;
            }
            catch(FileNotFoundException e)
            {
                Console.WriteLine(e.Message);

            }

            return "C:/";
        }

        public static List<Server> GetServerData()
        {
            var path = Write.assemblyD + @"\servers\";
            List<Server> servers = new List<Server>();

            Write.CreateFilePath(path);

            foreach(string file in Directory.EnumerateFiles(path, "*.txt"))
            {
                string[] lines = File.ReadAllLines(file);


                int port = 0;
                string name = "";

                foreach(string s in lines)
                {
                    var split = s.Split(new char[] {':'},2);
                    if (split[0].Equals("port"))
                        port = int.Parse(split[1]);
                    else if (split[0].Equals("name"))
                        name = split[1];

                }

                servers.Add(new Server(name,port));
            }

            return servers;
        }

    }
}
