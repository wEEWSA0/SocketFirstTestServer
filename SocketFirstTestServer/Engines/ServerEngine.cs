using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace SocketFirstTestServer.Engines
{
    internal class ServerEngine
    {
        private Socket _serverSocket;
        private IPEndPoint _ipEndPoint;
        private bool _isWorking = false;

        private MainHub _mainHub;

        public ServerEngine(string ip, int port)
        {
            _serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            _ipEndPoint = new IPEndPoint(IPAddress.Parse(ip), port);

            _mainHub = new MainHub(2);
        }

        private void Log(string msg)
        {
            Console.WriteLine($"LOG: {DateTime.Now} --- {msg}");
        }

        public void StartServer(int listenersCount)
        {
            _serverSocket.Bind(_ipEndPoint);
            _serverSocket.Listen(listenersCount); // проверить что будет, если listenersCount > максимального

            _isWorking = true;
            ServerUpdate();

            Log("SERVER STARTED");
        }

        private void ServerUpdate()
        {
            while (true)
            {
                AcceptClient();

                Console.WriteLine("Запрос клиента принят");
            }
        }

        private void AcceptClient()
        {
            var clientSocket = _serverSocket.Accept();

            _mainHub.AddClient(clientSocket);

            Log($"CLIENT ACCEPT FROM {clientSocket.RemoteEndPoint}");
        }
        
        public void CloseServerSocket()
        {
            _isWorking = false;
            _serverSocket.Close();

            Log($"SERVER FINISHED");
        }
    }
}
