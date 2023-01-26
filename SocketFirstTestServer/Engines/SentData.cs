using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocketFirstTestServer.Engines
{
    internal class SentData
    {
        public Command Command { get; private set; }
        public Object Data { get; private set; }

        public SentData(Command command)
        {
            Command = command;
        }

        public SentData(Command command, Object data)
        {
            Command = command;
            Data = data;
        }
    }
}
