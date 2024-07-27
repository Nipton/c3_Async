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
        public static async Task ClientStart(string name)
        {
            try
            {

                //IPEndPoint localePoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 6000);
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
                    await client.SendAsync(data, remotePoint);
                    var receiveAnswer = await client.ReceiveAsync();
                    string str = Encoding.UTF8.GetString(receiveAnswer.Buffer);
                    var answer = Message.FromJson(str);
                    Console.WriteLine(answer);
                    if (message.Text == "Close")
                    {
                        Console.WriteLine("Клиент закрывается. Нажмите любую клавишу.");
                        Console.ReadKey();
                        Environment.Exit(0);
                    }
                    //Console.WriteLine(client.Client.LocalEndPoint);
                    //Console.WriteLine(client.Client.RemoteEndPoint);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }
    }
}
