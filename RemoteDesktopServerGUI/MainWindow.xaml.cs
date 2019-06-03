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
using Hardcodet.Wpf.TaskbarNotification;
using RemoteDesktopServerGUI.Model;

namespace RemoteDesktopServerGUI
{
    /// <summary>
    /// Interakční logika pro MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string _status;
        public string status { get { return _status; } set { _status = value; TB1.Content = _status; }}
        private string _shortStatus;
        public string shortStatus { get { return _shortStatus; } set { _shortStatus = value; TB2.Text = _shortStatus; }}
        MainWindow instance;
        Tunel rd;
        Config conf;
            
        public MainWindow()
        {
            instance = this;
            
            InitializeComponent();
            this.DataContext = status;
            
            status = "LOlol";
            shortStatus = "ICOOOOO";
            conf = ConfigEditor.startConfig();
            rd = new Tunel(conf.host, conf.user, conf.password, conf.remotePort);
            rd.Start();
            //instance.Hide();

        }
        public void hide(object sender, MouseEventArgs e)
        {
            instance.Show();
            instance.WindowState = WindowState.Normal;
            MessageBox.Show("Click");
        }


        
    }
}
