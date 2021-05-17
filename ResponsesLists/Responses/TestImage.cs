using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Net.Sockets;
using System.Text;
using Console_CCServer.Users;
namespace Console_CCServer.ResponsesLists.Responses
{
    class TestImage : Response
    {
        public override string Name => "image";

        public override void Execute(ref NetworkStream stream, string Request, ref User user)
        {
         /*   Bitmap tImage = new Bitmap(@"D:\KPK\ДИПЛОМ\SOFT\Console CCServer\Penguins.jpg");
            byte[] bStream = ImageToByte(tImage);
             */
            
            FileStream fs = new FileStream(@"D:\KPK\ДИПЛОМ\SOFT\Console CCServer\Penguins.jpg", FileMode.Open, FileAccess.Read);
            byte[] bStream = new byte[fs.Length];
            fs.Read(bStream, 0, Convert.ToInt32(fs.Length));
            fs.Close();
           
            byte[] sizeByte = Encoding.Unicode.GetBytes(""+bStream.Length);
            stream.Write(sizeByte, 0, sizeByte.Length);
            //
            byte[] data = new byte[64];
            stream.Read(data, 0, data.Length);
            //
            stream.Write(bStream, 0, bStream.Length);
        }
        static byte[] ImageToByte(System.Drawing.Image iImage)
        {
            MemoryStream mMemoryStream = new MemoryStream();
            iImage.Save(mMemoryStream, System.Drawing.Imaging.ImageFormat.Gif);
            return mMemoryStream.ToArray();
        }
    }
}
