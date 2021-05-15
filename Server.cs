using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace Console_CCServer
{
    class Server
    {
        #region Data fields
        const int port = 8888;
        static TcpListener listener;
        #endregion
        #region Methods
        static void Main(string[] args)
        {
            try
            {
                listener = new TcpListener(IPAddress.Parse("127.0.0.1"), port);
                listener.Start();
                Console.WriteLine("Очікування підключення...");

                while (true)
                {
                    
                    TcpClient client = listener.AcceptTcpClient();
                    Client clientObject = new Client(client);

                    // створення нового потоку, для обслуговування іншого клієнта
                    Thread clientThread = new Thread(new ThreadStart(clientObject.Process));
                    clientThread.Start();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                if (listener != null)
                    listener.Stop();
            }
        }
        #endregion
    }
}
