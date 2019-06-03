using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RemoteDesktopServerGUI.Model
{
    class Config
    {
        public string host;
        public string user;
        public string password;
        public uint remotePort;
        public uint localPort;
        public bool visible;
        public Config(string host, string user, string password, uint remotePort, uint localPort, bool visible)
        {
            this.host = host;
            this.user = user;
            this.password = password;
            this.remotePort = remotePort;
            this.localPort = localPort;
            this.visible = visible;
        }
    }
}
