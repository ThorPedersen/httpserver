using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace httpserver
{
    public class HttpServer
    {
        //public static readonly int DefaultPort = 8888;
        public void StartServer()
        {
            string name = "localhost";
            IPAddress[] addrs = Dns.GetHostEntry(name).AddressList;
            
            TcpListener serverSocket = new TcpListener(8888);
            serverSocket.Start();

            while (true)
            {
                TcpClient connectionSocket = serverSocket.AcceptTcpClient();
                Console.WriteLine("Server is activated");
                HttpService service = new HttpService(connectionSocket);

                Task.Run(() => service.SocketHandler());
                
            }
        }
    }
}