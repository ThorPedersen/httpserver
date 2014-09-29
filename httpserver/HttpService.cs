using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace httpserver
{
   class HttpService
   {
      private TcpClient connectionSocket;
      private static readonly string RootCatalog = "c:/temp";

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

         string message = sr.ReadLine();

         sw.WriteLine(message);

         string uritext = null;
         string[] words = message.Split(' ');

         uritext = words[1].Replace("/", "/");

         sw.WriteLine("You requested " + uritext);



         ///////////
         string[] lines = System.IO.File.ReadAllLines(RootCatalog + uritext + ".txt");

         
         //FileStream fileStream = new FileStream(RootCatalog + uritext, FileMode.Open);
         foreach (string line in lines)
         {
            // Use a tab to indent each line of the file.
            sw.WriteLine("\t" + line);
         }
         //foreach (var lines in file)
         //{
         //   sw.WriteLine((string) lines);
         //}

         //fileStream.Close();
         //////////
         connectionSocket.Close();
      }
   }
}
