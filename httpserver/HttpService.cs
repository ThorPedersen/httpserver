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

         //while (!string.IsNullOrEmpty(message))
         //{
         //   Console.WriteLine("Client: " + message);
         //   answer = message.ToUpper();
         //   sw.WriteLine(answer);
         //   message = sr.ReadLine();

         //}
         //request.getRequestURI().substring(request.getContextPath().length())

         //string[] words = message.Split(' ');

         //foreach (string word in words)
         //{
         sw.WriteLine(message);

         string uritext = null;
         string[] words = message.Split(' ');

         uritext = words[1].ToString(CultureInfo.InvariantCulture);

         sw.WriteLine("You requested " + uritext);
         //}

         //string path = "someFile.html";

         //string headerMessage = sr.ReadLine();
         //sw.WriteLine(headerMessage);

         //HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://www.stackoverflow.com");
         //request.Method = "GET";
         //HttpWebResponse response = (HttpWebResponse)request.GetResponse();
         //sw.WriteLine(response.Headers);

         //sr.Close();
         //sw.Close();
         //ns.Close();

         //string message1 = "You requested ";
         //sw.WriteLine(message1);
         //message = message.ToUpper();

         //sw.WriteLine(message1);
         //message1 = sr.ReadLine();

         connectionSocket.Close();
      }
   }
}
