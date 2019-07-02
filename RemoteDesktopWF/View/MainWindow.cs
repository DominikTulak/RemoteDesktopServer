using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Win32;
using RemoteDesktopWF.Model;

namespace RemoteDesktopWF
{
    public partial class MainWindow : Form
    {
        string version = "1.3.0";
        public MainWindow f;
        Tunel rd;
        Config conf;
        int port;
        bool? rdpEnabled;
        string unknown = "???";
        public MainWindow()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
        }

        private void notifyIcon1_DoubleClick(object sender, EventArgs e)
        {
            f.Show();
            f.WindowState = FormWindowState.Normal;
        }

        private void notifyIcon1_Click(object sender, EventArgs e)
        {
            f.Show();
        }

        private void MainWindow_Resize(object sender, EventArgs e)
        {
            if (f.WindowState == FormWindowState.Minimized)
            {
                f.Hide();
                //f.ShowInTaskbar = false;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (button1.Text == "Stop")
            {
                button1.Text = "Start";
                rd.Stop();
                System.Threading.Thread.Sleep(1000);
                editText();
            }
            else
            {
                button1.Text = "Stop";
                System.Threading.Thread.Sleep(1000);
                rd.Start();
                editText();
            }
        }
        private void editText()
        {
            statusBox.Text = "#Debug info:";
            statusBox.Text += Environment.NewLine + $"Local user: {System.Security.Principal.WindowsIdentity.GetCurrent().Name}";
            statusBox.Text += Environment.NewLine + $"RDP enabled: {(rdpEnabled!= null? rdpEnabled.ToString():unknown)}";
            statusBox.Text += Environment.NewLine + $"Remote host: {conf.host}";
            statusBox.Text += Environment.NewLine + $"Remote user: {conf.user}";
            statusBox.Text += Environment.NewLine + $"Local Port: {conf.localPort}";
            statusBox.Text += Environment.NewLine + $"Remote Port: {port}";
            statusBox.Text += Environment.NewLine + "STATUS: " + (rd.returnClient().IsConnected ? "Běží..." : "Přerušeno!");
            notifyIcon1.Text = (rd.returnClient().IsConnected ? "Běží..." : "Přerušeno!");
            VersionLabel.Text = String.Format("v{0}",version);

        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                string cmd = "reg add \"HKEY_LOCAL_MACHINE\\SYSTEM\\CurrentControlSet\\Control\\Terminal Server\" /v fDenyTSConnections /t REG_DWORD /d 0 /f";
                string cmd2 = "reg add \"HKEY_LOCAL_MACHINE\\SYSTEM\\CurrentControlSet\\Control\\Lsa\" /v LimitBlankPasswordUse /t REG_DWORD /d 0 /f";
                System.Diagnostics.Process process = new System.Diagnostics.Process();
                System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
                startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                startInfo.FileName = "cmd.exe";
                startInfo.Arguments = cmd;
                startInfo.Verb = "runas";
                process.StartInfo = startInfo;
                process.Start();

                System.Diagnostics.Process process2 = new System.Diagnostics.Process();
                System.Diagnostics.ProcessStartInfo startInfo2 = new System.Diagnostics.ProcessStartInfo();
                startInfo2.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                startInfo2.FileName = "cmd.exe";
                startInfo2.Arguments = cmd2;
                startInfo2.Verb = "runas";
                process2.StartInfo = startInfo2;
                process2.Start();
            } catch { MessageBox.Show("Povolení RDP musí být povoleno jako správce!", "Chyba v povolování RDP", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            View.HostSet hs = new View.HostSet();
            hs.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            MainWindow mw = new MainWindow();
            mw.Show();
            this.Close();
        }

        private void MainWindow_Load(object sender, EventArgs e)
        {
            f = this;
            conf = ConfigEditor.startConfig();
            
            try { rd = new Tunel(conf.host, conf.user, conf.password, conf.remotePort, conf.localPort); rd.Start(); port = Convert.ToInt32(conf.remotePort); }
            catch (Exception ex)
            {
                for (int i = 0; i < conf.AlternativePorts.Count; i++)
                {
                    try
                    {
                        System.Threading.Thread.Sleep(500);
                        
                        rd = new Tunel(conf.host, conf.user, conf.password, conf.AlternativePorts[i], conf.localPort);
                        rd.Start();
                        port = Convert.ToInt32(conf.AlternativePorts[i]);
                        editText();
                        return;
                    }
                    catch { }
                }
                MessageBox.Show("Failed");

                System.Threading.Thread.Sleep(10000);
                MainWindow_Load(this, new EventArgs());
                //MessageBox.Show(ex.ToString());
            }

            editText();
        }
    }
}
