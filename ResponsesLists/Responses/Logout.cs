using Console_CCServer.Users;
using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;

namespace Console_CCServer.ResponsesLists.Responses
{
    class Logout : Response
    {
        public override string Name => "logout";

        public override void Execute(ref NetworkStream stream, string Request, ref User user)
        {
            user = new User();
            byte[] data = new byte[64];
            data = Encoding.Unicode.GetBytes("1");
            stream.Write(data, 0, data.Length);
        }
    }
}
