using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SocketFirstTestServer.Engines
{
    internal class Client
    {
        private Socket _clientSocket;
        private Room _room;

        public Client(Socket socket, Room room)
        {
            _clientSocket = socket;
            _room = room;

            Command command = new Command(CommandType.ClientConnected);
            SentData sentData = new SentData(command);
            _room.RecieveMessageFromClient(sentData, this);

            StartReceivingMessagesFromClient();
        }

        public void SendMessageToClient(SentData sentData)
        {
            string stringValue = JsonSerializer.Serialize(sentData);

            byte[] outputBytes = Encoding.Unicode.GetBytes(stringValue);

            SendMessageToClient(outputBytes);
        }

        public void SendMessageToClient(byte[] outputBytes)
        {
            _clientSocket.Send(outputBytes);

            Log($"MESSAGE TO CLIENT SENT: {outputBytes}");
        }

        private void StartReceivingMessagesFromClient()
        {
            while (true)
            {
                var messageAsString = ReceiveMessageAsString();

                SentData sentData = JsonSerializer.Deserialize<SentData>(messageAsString);
                _room.RecieveMessageFromClient(sentData, this);
            }
        }
        
        #region Hide
        public void CloseClientSocket()
        {
            _clientSocket.Shutdown(SocketShutdown.Both);
            _clientSocket.Close();

            Log($"CLIENT FINISHED");
        }

        private void Log(string msg)
        {
            Console.WriteLine($"LOG: {DateTime.Now} --- {msg}");
        }

        private string ReceiveMessageAsString()
        {
            StringBuilder messageBuilder = new StringBuilder();
            do
            {
                byte[] inputBytes = new byte[1024];
                int countBytes = _clientSocket.Receive(inputBytes);
                messageBuilder.Append(Encoding.Unicode.GetString(inputBytes, 0, countBytes));
            } while (_clientSocket.Available > 0);

            string messageFromClient = messageBuilder.ToString();

            Log($"MESSAGE FROM CLIENT RECIEVED: {messageFromClient}");

            return messageFromClient;
        }
        #endregion
    }
}
