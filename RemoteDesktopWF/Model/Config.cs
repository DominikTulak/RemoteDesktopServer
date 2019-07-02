using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RemoteDesktopWF.Model
{
    class Config
    {
        private List<uint> empty = null;
        public string host;
        public string user;
        public string password;
        public uint remotePort;
        public List<uint> AlternativePorts;
        public uint localPort;
        public bool visible;
        public static Config config;
        public Config(string host, string user, string password, uint remotePort, uint localPort, bool visible, List<uint> AlternativePorts = null)
        {
            this.host = host;
            this.user = user;
            this.password = password;
            this.remotePort = remotePort;
            this.localPort = localPort;
            this.visible = visible;
            if(AlternativePorts == null)
            {
                this.AlternativePorts = new List<uint>();
            } else { this.AlternativePorts = AlternativePorts; }
            
        }
    }
}
