using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace RemoteDesktopServerGUI.Model
{
    static class ConfigEditor
    {
        private static string pathWithEnv = @"%USERPROFILE%\AppData\Local\RemoteServer\settings.conf";
        public static Config startConfig()
        {
            if (configExists())
            {
                return loadConfig();
            }
            else
            {
                return createConfig();
            }
        }
        private static bool configExists()
        {
            if (File.Exists(Environment.ExpandEnvironmentVariables(pathWithEnv)))
            {
                //Console.WriteLine("trie");
                return true;

            }
            return false;
        }
        private static Config loadConfig()
        {
            StreamReader sr = new StreamReader(Environment.ExpandEnvironmentVariables(pathWithEnv));
            string[] text = sr.ReadToEnd().Split('\n');
            sr.Close();
            Config cf = new Config("185.28.100.185", "tun", "Password", 22224, 3389, true);


            for (int line = 0; line < text.Length; line++)
            {
                var txt = text[line].Split('=');

                try
                {
                    switch (txt[0])
                    {
                        case "remoteHost":
                            cf.host = txt[1].Replace("\r", "");
                            break;
                        case "remotePort":
                            cf.remotePort = Convert.ToUInt32(txt[1]);
                            break;
                        case "localPort":
                            cf.localPort = Convert.ToUInt32(txt[1]);
                            break;
                        case "password":
                            cf.password = txt[1];
                            break;
                        case "username":
                            cf.user = txt[1];
                            break;
                        case "visible":
                            cf.visible = Convert.ToBoolean(txt[1]);
                            break;
                    }
                }
                catch
                {
                    Console.WriteLine("Config Error!");
                }
            }
            return cf;
        }
        private static Config createConfig()
        {
            if (!Directory.Exists(Environment.ExpandEnvironmentVariables(@"%USERPROFILE%\AppData\Local\RemoteServer")))
            {
                Directory.CreateDirectory(Environment.ExpandEnvironmentVariables(@"%USERPROFILE%\AppData\Local\RemoteServer"));
            }

            StreamWriter sw = new StreamWriter(Environment.ExpandEnvironmentVariables(pathWithEnv));
            sw.WriteLine("#RemoteDesktop config file");
            sw.WriteLine("localHost=127.0.0.1");
            sw.WriteLine("remoteHost=185.28.100.185");
            sw.WriteLine("localPort=3389");
            sw.WriteLine("remotePort=22224");
            sw.WriteLine("visible=false");
            sw.Close();
            return loadConfig();
        }
    }
}
