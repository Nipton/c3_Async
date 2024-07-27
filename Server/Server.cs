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
        public static async Task StartServer()
        {
            CancellationTokenSource cts = new CancellationTokenSource();
            CancellationToken token = cts.Token;
            //IPEndPoint remotePoint = new IPEndPoint(IPAddress.Any, 0);
            try
            {
                using UdpClient server = new UdpClient(5000);
                //server.Client.ReceiveTimeout = 10000;
                Console.WriteLine("Сервер запущен. Нажмите любую клавишу для остановки. ");
                //new Thread(() => { Console.ReadKey(); Environment.Exit(0); }).Start();
                Task task = new Task(() => { Console.ReadKey(); Environment.Exit(0); });
                task.Start();
                while (!token.IsCancellationRequested)
                {
                    var buffer = await server.ReceiveAsync();
                    string data = Encoding.UTF8.GetString(buffer.Buffer);
                    //Thread thread = new Thread(() =>
                    //{
                    await Task.Run(async () =>
                    {
                        var mes = Message.FromJson(data);                       
                        Console.WriteLine(mes);
                        Message answerMes = new Message() { Date = DateTime.Now, Name = "Server", Text = "Сервер принял запрос" };
                        if (mes!.Text == "Close")
                        {
                            Console.WriteLine("Сервер заканчивает работу.");
                            answerMes.Text = "Сервер заканчивает работу.";
                            cts.Cancel();
                        }
                        byte[] answer = Encoding.UTF8.GetBytes(answerMes.ToJson());
                        await server.SendAsync(answer, buffer.RemoteEndPoint);
                    });
                    //});
                    //thread.Start();
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