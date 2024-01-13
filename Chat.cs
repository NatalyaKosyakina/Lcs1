using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Lcs1
{
    internal class Chat
    {
  

        public static void Server()
        {
            IPEndPoint localEP = new IPEndPoint(IPAddress.Any, 0);
            UdpClient ucl = new UdpClient(12345);
            Console.WriteLine("Сервер ожидает сообщения от клиента");
            string servMessage = String.Empty;

            ConsoleKeyInfo info = Console.ReadKey(true);

            Thread thread = new Thread(() => {
            while (true)
            {
                try
                {
                    byte[] buffer = ucl.Receive(ref localEP);
                    string str1 = Encoding.UTF8.GetString(buffer);

                    Message? somemessage = Message.FromJson(str1);
                    if (somemessage != null)
                    {
                        Console.WriteLine(somemessage.ToString());

                        Message newmessage = new Message("server", "Сообщение получено");
                        string js = newmessage.ToJson();
                        byte[] bytes = Encoding.UTF8.GetBytes(js);
                        ucl.Send(bytes, localEP);
                        
                        }
                    else { Console.WriteLine("Некорректное сообщение");
                        
                        }
                }
                catch (Exception e) { Console.WriteLine(e.Message); }
                    info = Console.ReadKey(true);
                    if (info.Key.Equals(ConsoleKey.Escape))
                    {
                        break;
                    }

            }
            });
            thread.Start();
            
        }
        
        public static void Client(string nik)
        {
            IPEndPoint localEP = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 12345);
            UdpClient ucl = new UdpClient();

            //Thread tr = new Thread(() =>
            {
                while (true)
                {
                    Console.WriteLine("Введите сообщение");
                    string text = Console.ReadLine();
                    if (String.IsNullOrEmpty(text) || text.Equals("Exit"))
                    {
                        break;
                    }
                    Message newmessage = new Message(nik, text);
                    string js = newmessage.ToJson();
                    byte[] bytes = Encoding.UTF8.GetBytes(js);
                    ucl.Send(bytes, localEP);

                    byte[] buffer = ucl.Receive(ref localEP);
                    string str1 = Encoding.UTF8.GetString(buffer);
                    Message? somemessage = Message.FromJson(str1);
                    Console.WriteLine(somemessage);
                }
            //});
            }
        }

        /*using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Net.Sockets;
    using System.Text;
    using System.Threading.Tasks;

    namespace Sem2Task2
    {
        internal class Server
        {
            public static void AcceptMsg()
            {
                IPEndPoint ep = new IPEndPoint(IPAddress.Any, 0);
                UdpClient udpClient = new UdpClient(5050);

                while (true)
                {

                    byte[] buffer = udpClient.Receive(ref ep);
                    string data = Encoding.UTF8.GetString(buffer);

                    Thread tr = new Thread(() =>
                    {
                        Message msg = Message.fromJson(data);
                        Console.WriteLine(msg.ToString());
                        Message responseMsg = new Message("Server", "Message accept on serv!");
                        string responseMsgJs = responseMsg.toJson();
                        byte[] responseDate = Encoding.UTF8.GetBytes(responseMsgJs);
                        udpClient.Send(responseDate);
                    }); 
                }
            }
        }*/
    }
}
