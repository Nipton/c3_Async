using Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    internal class Server
    {
         public static void StartServer()
        {
            IPEndPoint remotePoint = new IPEndPoint(IPAddress.Any, 0);
            try
            {
                using UdpClient server = new UdpClient(5000);
                server.Client.ReceiveTimeout = 10000;
                Console.WriteLine("Сервер запущен. Нажмите любую клавишу для остановки. ");
                new Thread(() => { Console.ReadKey(); Environment.Exit(0); }).Start();
                while (true)
                {
                    var buffer = server.Receive(ref remotePoint);
                    string data = Encoding.UTF8.GetString(buffer);
                    Thread thread = new Thread(() =>
                    {
                        var mes = Message.FromJson(data);
                        Console.WriteLine(mes);
                        Message answerMes = new Message() { Date = DateTime.Now, Name = "Server", Text = "Сервер принял запрос" };
                        byte[] answer = Encoding.UTF8.GetBytes(answerMes.ToJson());
                        server.Send(answer, remotePoint);
                    });
                    thread.Start();
                }
            }
            catch (SocketException)
            {
                Console.WriteLine("Превышено время ожидания. Сервер заканчивает работу.");
                Environment.Exit(0);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

        }
    }
}
