using SocketFirstTestClient.Sockets;

namespace SocketFirstTestClient
{
    public partial class Form1 : Form
    {
        private ClientEngine _clientEngine;

        private const string ServerIp = "";
        private const int ServerPort = 156;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            _clientEngine = new ClientEngine();
        }

        private void StartButton_Click(object sender, EventArgs e)
        {

        }
    }
}