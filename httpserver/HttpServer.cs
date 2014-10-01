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
          //creates a server socket/listner/ server startup message using port 8888
          TcpListener serverSocket = new TcpListener(DefaultPort);
          serverSocket.Start();
          Console.WriteLine("Server is activated");

          //Server stays open
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