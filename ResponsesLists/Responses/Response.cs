using Console_CCServer.Users;
using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;

namespace Console_CCServer.ResponsesLists.Responses
{
    public abstract class Response
    {
        public abstract string Name { get; }
        public abstract void Execute(ref TcpClient client, string Request, ref User user);
    }
}
