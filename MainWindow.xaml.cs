using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.ComponentModel;

namespace DiagnoseMe
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            outText.Text = GetIP().ToString();

            if (clientCode.AcceptsReturn)
            {
                if (IPAddress.TryParse(clientCode.Text, out IPAddress ip))
                {
                    ServerLink serverLink = new ServerLink();
                    Task<string> inputText = serverLink.Server(ip);

                    string inTo = inputText.Result.ToString();
                    outText.Text = inTo;
                }
            }
            
            
        }

        public static IPAddress GetIP()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip;
                }
            }
            throw new Exception("No network adapters with an IPv4 address in the system!");
        }


    }
}