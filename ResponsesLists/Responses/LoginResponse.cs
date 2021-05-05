using Console_CCServer.Users;
using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using Console_CCServer.DataBaseMySQL;
using MySql.Data.MySqlClient;
using System.Data;

namespace Console_CCServer.ResponsesLists.Responses
{
    class LoginResponse : Response
    {
        public override string Name => "login";

        public override void Execute(ref NetworkStream stream, string Request, ref User user)
        {
            DataTable temp = new DataTable();
            byte[] data = new byte[64];
            try
            {
                
                string[] logpass = Request.Split(':');
                DataBase.Get_Instance().Connect();
                MySqlDataAdapter adapter = new MySqlDataAdapter();
                MySqlCommand command = new MySqlCommand("SELECT * FROM `user` WHERE `Login` = @Login AND `Password` = @Password AND `UserType` = @UserType", DataBase.Get_Instance().connection);
                command.Parameters.Add("@UserType", MySqlDbType.VarChar).Value = logpass[1];
                command.Parameters.Add("@Login", MySqlDbType.VarChar).Value = logpass[2];
                command.Parameters.Add("@Password", MySqlDbType.VarChar).Value = logpass[3];
                adapter.SelectCommand = command;

                adapter.Fill(temp);
                if(temp.Rows.Count > 0)
                {
                    data = Encoding.Unicode.GetBytes("1");
                    user = new User(
                                    Convert.ToUInt32(temp.Rows[0][0]),
                                    User.ConvertToEnum(Convert.ToString(temp.Rows[0][1])),
                                    Convert.ToString(temp.Rows[0][2])
                                    );
                }
                else
                {
                    data = Encoding.Unicode.GetBytes("0");
                }
                stream.Write(data, 0, data.Length);
                DataBase.Get_Instance().Disconnect();
            }
            catch(Exception e)
            {
                data = Encoding.Unicode.GetBytes("-1");
                stream.Write(data, 0, data.Length);
                DataBase.Get_Instance().Disconnect();
                //вивід в логи або в консоль
                Console.WriteLine(e.Message);
            }
            
        }
    }
}
