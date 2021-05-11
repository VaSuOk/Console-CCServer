using Console_CCServer.ResponsesLists.ResponseLists;
using Console_CCServer.Users;
using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;

namespace Console_CCServer
{
    class Client
    {
        #region Data fields
        private TcpClient client;
        private ResponseList responseList;
        private User user;
        #endregion
        #region Methods
        public Client(TcpClient tcpClient /*+ тип юзера*/)
        {
            client = tcpClient;
            user = new User();
            switch (user.GetUserType())
            {
                case UserType.Unregistered:
                    {
                        responseList = UnregisteredResponsesList.GetInstance();
                        break;
                    }
                case UserType.Customer:
                    {
                        responseList = CustomerResponsesList.GetInstance();
                        break;
                    }
                case UserType.Worker:
                    {
                        responseList = WorkerResponsesList.GetInstance();
                        break;
                    }
                case UserType.Moderator:
                    {
                        responseList = ModeratorsResponsesList.GetInstance();
                        break;
                    }
            }
        }

        public void Process()
        {
            NetworkStream stream = null;
            try
            {
                stream = client.GetStream();
                byte[] data = new byte[64]; // буфер для отримання даних
                while (true)
                {
                    // отримуємо запит
                    StringBuilder builder = new StringBuilder();
                    int bytes = 0;
                    do
                    {
                        bytes = stream.Read(data, 0, data.Length);
                        builder.Append(Encoding.Unicode.GetString(data, 0, bytes));
                    }
                    while (stream.DataAvailable);

                    string message = builder.ToString();
                    //Порівняння запиту клієнта з уже наявними

                    foreach (var response in responseList.GetResponseList())
                    {
                        if (response.Name == message.Split(':')[0])
                        {
                            Console.WriteLine(builder.ToString());
                            response.Execute(ref stream, message, ref user);
                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                if (stream != null)
                    stream.Close();
                if (client != null)
                    client.Close();
            }
        }
        #endregion
    }
}
