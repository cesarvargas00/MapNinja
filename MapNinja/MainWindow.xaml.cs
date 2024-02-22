using MapNinja.Connection;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Media;
using System.Diagnostics;
using System.Threading.Tasks;
using Windows.UI;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace MapNinja
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainWindow : Window
    {
        private OBSWebSocket obs;
        public MainWindow()
        {
            this.InitializeComponent();
            obs = new OBSWebSocket();
        }

        public void DisableButtonsAndText()
        {
            connectButton.IsEnabled = false;
            ipAddress.IsEnabled = false;
            keyPress.IsEnabled = false;
        }

        public void EnableButtonsAndText()
        {
            connectButton.IsEnabled = true;
            ipAddress.IsEnabled = true;
            keyPress.IsEnabled = true;
        }


        //192.168.68.100
        public async Task ConnectToWebSocket()
        {
            statusLabel.Text = "";
            connectButton.Content = "Connecting...";
            DisableButtonsAndText();
            this.obs = new OBSWebSocket();
            Task t = this.obs.ConnectToWebSocket(ipAddress.Text);
            await t;
            if (obs.isConnected) { 
                connectButton.Content = "Disconnect";
                connectButton.IsEnabled = true;
                statusLabel.Text = "Connected!";
                // change statusLabel color to green
                statusLabel.Foreground = new SolidColorBrush(Color.FromArgb(255,0,255,150));
            } else
            {
                EnableButtonsAndText();
                connectButton.Content = "Connect";
            }

        }

        private async void connectButton_Click(object sender, RoutedEventArgs e)
        {
            if (!obs.isConnected)
            {
                try {
                    await ConnectToWebSocket();
                } catch (System.Exception ex)
                {
                    Debug.WriteLine("Deu ruim: " + ex.Message);
                    EnableButtonsAndText();
                    connectButton.Content = "Connect";
                    statusLabel.Text = "Failed to connect to OBS WebSocket.";
                }
                
            }
            else
            {
                obs.Dispose();
                EnableButtonsAndText();
                Debug.WriteLine("Disconnected from OBS WebSocket.");
                connectButton.Content = "Connect";
                statusLabel.Text = "";
                statusLabel.Foreground = new SolidColorBrush(Color.FromArgb(255,0,0,0));
            }
        }
    }
}
