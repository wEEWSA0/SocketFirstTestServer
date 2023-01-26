using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocketFirstTestServer.Engines
{
    internal class Room
    {
        private MainHub _mainHub;
        private List<Client> _clients;
        private GameStatistic _game;

        public Room(MainHub mainHub)
        {
            _mainHub = mainHub;
            _clients = new List<Client>();
            _game = new GameStatistic();
        }

        public void RecieveMessageFromClient(SentData sentData, Client client)
        {
            switch (sentData.Command.Type)
            {
                case CommandType.ClientConnected:
                    {
                        _clients.Add(client);
                    }
                    break;
                case CommandType.StringMessage:
                    {
                        var stringValue = sentData.Data as string;

                        Console.WriteLine(stringValue);
                    }
                    break;
                case CommandType.Exception:
                    {
                        throw new Exception();
                    }
            }
        }

        public void CloseRoom()
        {
            for (int i = 0; i < _clients.Count; i++)
            {
                // сообщение о том, что комната распущена
                _clients[i].CloseClientSocket();
            }

            _mainHub.DestroyRoom(this);
        }
    }
}
