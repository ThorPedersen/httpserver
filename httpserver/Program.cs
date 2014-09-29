using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace httpserver
{
    class Program
    {
       /// <summary>
       /// Evt make a class for logging
       /// </summary>
       /// <param name="args"></param>
        static void Main(string[] args)
        {
            Console.WriteLine("Hello http server");
            HttpServer _httpServer = new HttpServer();
            _httpServer.StartServer();
        }
    }
}
