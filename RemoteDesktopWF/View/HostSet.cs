using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RemoteDesktopWF.View
{
    public partial class HostSet : Form
    {
        public HostSet()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            pass.PasswordChar = '*';
            this.KeyPreview = true;
            var conf = Model.ConfigEditor.startConfig();
            localHost.Text = "localhost";
            localPort.Text = conf.localPort.ToString();
            remoteHost.Text = conf.host.ToString();
            remotePort.Text = conf.remotePort.ToString();
            user.Text = conf.user;
            pass.Text = conf.password;
            switch (conf.AlternativePorts.Count)
            {
                case 0:
                    break;
                case 1:
                    port1.Text = conf.AlternativePorts[0].ToString();
                    break;
                case 2:
                    port1.Text = conf.AlternativePorts[0].ToString();
                    port2.Text = conf.AlternativePorts[1].ToString();
                    break;
                case 3:
                    port1.Text = conf.AlternativePorts[0].ToString();
                    port2.Text = conf.AlternativePorts[1].ToString();
                    port3.Text = conf.AlternativePorts[2].ToString();
                    break;
                default:
                    port2.Text = conf.AlternativePorts[0].ToString();
                    port1.Text = conf.AlternativePorts[1].ToString();
                    port3.Text = conf.AlternativePorts[2].ToString();
                    break;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                var AlternativePorts = new List<uint>();
                if (port1.Text != "") { AlternativePorts.Add(uint.Parse(port1.Text)); }
                if (port2.Text != "") { AlternativePorts.Add(uint.Parse(port2.Text)); }
                if (port3.Text != "") { AlternativePorts.Add(uint.Parse(port3.Text)); }
                Model.ConfigEditor.WriteToFile(new Model.Config(remoteHost.Text, user.Text, pass.Text, uint.Parse(remotePort.Text),uint.Parse(localPort.Text),true, AlternativePorts ));
                MessageBox.Show("Úspěšně uloženo!", "Nastavení hostitele", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            } catch { MessageBox.Show("Chyba v konfiguraci!", "Nastavení hostitele", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }


        private void HostSet_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Modifiers == Keys.Control
                && e.KeyCode == Keys.P)
            {
                
                if (pass.PasswordChar == '*')
                {
                    pass.PasswordChar = '\0';
                }
                else
                {
                    pass.PasswordChar = '*';
                }
            }
        }
    }
}
