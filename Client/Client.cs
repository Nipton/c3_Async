using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    internal class Client
    {
        public static void ClientStart(string name)
        {
            try
            {

                IPEndPoint remotePoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 5000);
                Message message = new Message();
                using UdpClient client = new UdpClient();

                message.Name = name;
                bool flag = true;
                while (flag)
                {
                    message.Text = Console.ReadLine();
                    if(message.Text == "Exit")
                        flag = false;
                    message.Date = DateTime.Now;
                    var data = Encoding.UTF8.GetBytes(message.ToJson());
                    client.Send(data, remotePoint);
                    var receiveAnswer = client.Receive(ref remotePoint);
                    string str = Encoding.UTF8.GetString(receiveAnswer);
                    var answer = Message.FromJson(str);
                    Console.WriteLine(answer);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }
    }
}
