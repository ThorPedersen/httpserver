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
   public class HttpService
   {
      private TcpClient connectionSocket;
      private static readonly string RootCatalog = "c:/temp";

      string Roottext { get; set; }

      public HttpService(TcpClient connectionSocket)
      {
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

         string uritext = "";
         string[] words = message.Split(' ');

         uritext = words[1].Replace("/", "/");

         sw.WriteLine("You requested " + uritext);

         uritext = uritext + ".txt";

         ///////////
         //string filepath = RootCatalog + uritext;

         //if (!File.Exists(filepath))
         //{
         //   using (FileStream fs = File.Create(filepath))
         //   {
         //      for (byte i = 0; i < 100; i++)
         //      {
         //         fs.WriteByte(i);
         //      }
         //      string[] txtlines = { "First line", "Second line", "Third line" };
         //      File.WriteAllLines(filepath, txtlines);
         //   }
         //}
         //else
         //{
         //   Console.WriteLine("File \"{0}\" already exists.", uritext);
         //   return;
         //}
         Roottext = RootCatalog + uritext;

         if (Roottext == "c:/temp/favicon.ico.txt")
         {
            Roottext = "c:/temp/filename.html.txt";
         }

         string[] lines = File.ReadAllLines(Roottext);       
         
         foreach (string line in lines)
         {
            
            sw.WriteLine("\t" + line);
         }

         sr.Close();
         sw.Close();
         connectionSocket.Close();
      }
   }
}
