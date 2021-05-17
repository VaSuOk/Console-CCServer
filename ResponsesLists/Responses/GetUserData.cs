using Console_CCServer.DataBaseMySQL;
using Console_CCServer.Users;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Net.Sockets;
using System.Text;

namespace Console_CCServer.ResponsesLists.Responses
{
    class GetUserData : Response
    {
        public override string Name => "getUserData";

        public override void Execute(ref NetworkStream stream, string Request, ref User user)
        {
            Console.WriteLine(user.GetID());
            Console.WriteLine(user.GetUserType());
            DataTable temp = new DataTable();
            byte[] data;
            try
            {
                DataBase.Get_Instance().Connect();
                MySqlDataAdapter adapter = new MySqlDataAdapter();
                MySqlCommand command = new MySqlCommand("SELECT * FROM `user` WHERE `ID` = @ID AND `UserType` = @UserType", DataBase.Get_Instance().connection);
                command.Parameters.Add("@UserType", MySqlDbType.VarChar).Value = user.GetUserType();
                command.Parameters.Add("@ID", MySqlDbType.Int32).Value = user.GetID();
                adapter.SelectCommand = command;

                adapter.Fill(temp);

                user = new User(
                                Convert.ToUInt32(temp.Rows[0][0]),
                                User.ConvertToEnum(Convert.ToString(temp.Rows[0][1])),
                                Convert.ToString(temp.Rows[0][2]),
                                Convert.ToString(temp.Rows[0][3]),
                                Convert.ToString(temp.Rows[0][4]),
                                Convert.ToString(temp.Rows[0][5]),
                                Convert.ToInt32(temp.Rows[0][6]),
                                Convert.ToString(temp.Rows[0][7]),
                                Convert.ToString(temp.Rows[0][8]),
                                (byte []) temp.Rows[0][9]

                                );


                DataBase.Get_Instance().Disconnect();

                string responce = String.Format("{0}:{1}:{2}:{3}:{4}:{5}", user.GetID(), user.GetUserType(), user.GetName(), user.GetSurname(),
                    user.GetEmail(), user.GetPhone()/*, ""+user.GetAge(), user.GetRegion(), user.GetSity(), user.GetUserImage()*/);

                byte[] sizeByte = Encoding.Unicode.GetBytes("" + responce.Length);
                stream.Write(sizeByte, 0, sizeByte.Length);

                data = new byte[64];
                //stream.Read(data, 0, data.Length);

                data = Encoding.Unicode.GetBytes(responce);
                stream.Write(data, 0, data.Length);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                data = Encoding.Unicode.GetBytes("-1");
                stream.Write(data, 0, data.Length);
                DataBase.Get_Instance().Disconnect();
                //вивід в логи або в консоль
                
            }
            finally
            {
                //if (stream != null) stream.Close();
            }
        }
    }
}
