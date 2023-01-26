using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocketFirstTestServer.Engines
{
    internal class Command
    {
        public CommandType Type { get; private set; }

        public Command(CommandType type)
        {
            Type = type;
        }
    }

    enum CommandType
    {
        Exception,
        ClientConnected,
        StringMessage
    }
}
