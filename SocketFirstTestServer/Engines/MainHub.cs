using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace SocketFirstTestServer.Engines
{
    internal class MainHub
    {
        private List<Socket> _clientSockets;
        private List<Room> _rooms;

        private int _clientsPerRoom;

        public MainHub(int clientsPerRoom)
        {
            _clientsPerRoom = clientsPerRoom;
            _rooms = new List<Room>();
            _clientSockets = new List<Socket>();
        }

        public void AddClient(Socket clientSocket)
        {
            _clientSockets.Add(clientSocket);

            if (_clientSockets.Count >= _clientsPerRoom)
            {
                List<Socket> clientSockets = new List<Socket>();

                for (int i = 0; i < _clientsPerRoom; i++)
                {
                    var socket = _clientSockets[i];

                    clientSockets.Add(socket);
                    _clientSockets.Remove(socket);
                }

                ConnectToRoom(clientSockets);
            }
        }

        private void ConnectToRoom(List<Socket> clientSocketsList)
        {
            Room room = new Room(this);

            _rooms.Add(room);

            for (int i = 0; i < clientSocketsList.Count; i++)
            {
                Client client = new Client(clientSocketsList[i], room);
            }
        }

        public void DestroyRoom(Room room)
        {
            _rooms.Remove(room);
        }
    }
}
