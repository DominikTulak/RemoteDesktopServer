using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Renci.SshNet;

namespace RemoteDesktopServerGUI.Model
{
    class Tunel
    {
        private SshClient client;
        private ForwardedPortRemote port;
        public void Start()
        {
            
        }
        public void Stop()
        {
            client.Disconnect();
        }
        public Tunel(string host, string user, string password, uint remotePort, uint localPort = 3389)
        {
            client = new SshClient(host, user, password);
            port = new ForwardedPortRemote("127.0.0.1", remotePort, "127.0.0.1", localPort);
            
            client.Connect();
            client.AddForwardedPort(port);
            port.Start();
        }
    }
}
