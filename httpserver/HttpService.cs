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
          //creates a Stream for the client to read from and write to
          Stream ns = connectionSocket.GetStream();

          StreamReader sr = new StreamReader(ns);
          StreamWriter sw = new StreamWriter(ns);
          sw.AutoFlush = true; // enable automatic flushing

          //input form the stream to another format
          string message = sr.ReadLine();
          Console.WriteLine(message);

          string uritext = "";
          //Puts the stream into an array
          string[] words = message.Split(' ');

         string get = words[0];

         uritext = words[1].Replace("/", "/");

         //sw.WriteLine("You requested " + uritext + "<br>");

         uritext = uritext + ".txt";
         Roottext = RootCatalog + uritext;



         //TESTGET
         string code = "200 OK";
         const string illegalRequest = "400 Illegal request";
         const string illegalMethod = "400 Illegal request";
         const string illegalProtocol = "400 Illegal protocol";
         const string testMethodNotImplemented = "200 xxx";
         const string http10 = "HTTP/1.0";





         //If the file does not exist return error 404 Not Found
         if (!File.Exists(Roottext))
         {
             code = "404 Not Found";
         }

         if (words[0] == "GET")
            if (words[2] == "HTTP/1.0")
            {
               sw.WriteLine(words[2] + " " + code);
            }
            else
            {
               if(words[2].Contains("/"))
               { 
               sw.WriteLine(http10 + " " + illegalProtocol);
               }
               else
               {
                  sw.WriteLine(http10 + " " + illegalRequest);
               }
            }

         if (words[0] == "PLET")
            sw.WriteLine(words[2] + " " + illegalMethod);
            
         if(words[0] == "POST")
            sw.WriteLine(words[2] + " " + testMethodNotImplemented);

         //string[] lines = File.ReadAllLines(Roottext);

         //foreach (string line in lines)
         //{
         //   sw.WriteLine("\t" + line);
         //}

         sr.Close();
         sw.Close();
         connectionSocket.Close();
      }
   }
}
