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
      public static readonly int DefaultPort = 8888;
      public void StartServer()
      {
         TcpListener serverSocket = new TcpListener(DefaultPort);
         serverSocket.Start();
         Console.WriteLine("Server is activated");
         while (true)
         {
            TcpClient connectionSocket = null;
            using (connectionSocket)
            {
               connectionSocket = serverSocket.AcceptTcpClient();
               
               var service = new HttpService(connectionSocket);

               Task.Run(() => service.SocketHandler());
            }

         }
      }
   }
}