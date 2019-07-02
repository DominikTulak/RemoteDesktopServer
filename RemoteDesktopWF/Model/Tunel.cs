using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Renci.SshNet;
using System.Windows;
using System.Windows.Forms;

namespace RemoteDesktopWF.Model
{
    class Tunel
    {
        private SshClient client;
        private ForwardedPortRemote port;
        public void Start()
        {
            
            client.Connect();
            client.AddForwardedPort(port);
            port.Start();
            /*
            try
            {
                client.Connect();
                client.AddForwardedPort(port);
                port.Start();

            }
            catch
            {
                MessageBox.Show("Well, shit");
                System.Threading.Thread.Sleep(10000);
                Start();
            }*/

        }
        public void Stop()
        {
            client.Disconnect();
        }
        public Tunel(string host, string user, string password, uint remotePort, uint localPort = 3389)
        {
            client = new SshClient(host, user, password);
            port = new ForwardedPortRemote("127.0.0.1", remotePort, "localhost", localPort);
            
        }
        public SshClient returnClient()
        {
            return client;
        }
        
    }
}
