using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace httpserver
{
   class HttpService
   {
      private TcpClient connectionSocket;


      public HttpService(TcpClient connectionSocket)
      {
         // TODO: Complete member initialization
         this.connectionSocket = connectionSocket;
      }
      public void SocketHandler()
      {
         Stream ns = connectionSocket.GetStream();
         StreamReader sr = new StreamReader(ns);
         StreamWriter sw = new StreamWriter(ns);
         sw.AutoFlush = true; // enable automatic flushing

         //string message = sr.ReadLine();
         //string answer;

         //while (!string.IsNullOrEmpty(message))
         //{
         //   Console.WriteLine("Client: " + message);
         //   answer = message.ToUpper();
         //   sw.WriteLine(answer);
         //   message = sr.ReadLine();

         //}

         string headerMessage = sr.ReadLine();
         sw.WriteLine(headerMessage);



         //string message1 = "You requested ";
         //sw.WriteLine(message1);
         //message = message.ToUpper();

         //sw.WriteLine(message1);
         //message1 = sr.ReadLine();

         connectionSocket.Close();
      }
   }
}
