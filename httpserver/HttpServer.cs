using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace httpserver
{
    public class HttpServer
    {
        public static readonly int DefaultPort = 8888;
        private readonly EventLog _log = new EventLog();
        private bool _loop = true;

        private static Thread inputThread;
        private static AutoResetEvent getInput, gotInput;
        private static string _input;

        static HttpServer()
        {
            getInput = new AutoResetEvent(false);
            gotInput = new AutoResetEvent(false);
            inputThread = new Thread(reader);
            inputThread.IsBackground = true;
            inputThread.Start();
        }

        private static void reader()
        {
            while (true)
            {
                getInput.WaitOne();
                _input = Console.ReadLine();
                gotInput.Set();
            }
        }

        public static string ReadLine(int timeOutMillisecs)
        {
            getInput.Set();
            bool success = gotInput.WaitOne(timeOutMillisecs);
            if (success)
                return _input;
            else
                throw new TimeoutException("User did not provide input within the timelimit.");
        }
        public void StartServer()
        {
            //creates a server socket/listner/ server startup message using port 8888
            _log.Source = "HttpServer";
            TcpListener serverSocket = new TcpListener(DefaultPort);
            serverSocket.Start();
            _log.WriteEntry("Server startup.", EventLogEntryType.Information, 1);
            Console.WriteLine("Server is activated");
            Console.WriteLine("You must refresh browser to put command in effect.");
            TcpClient connectionSocket = null;

            //Server stays open until shutdown is written in console
            while (_loop == true)
            {
                using (connectionSocket)
                {
                    connectionSocket = serverSocket.AcceptTcpClient();

                    var service = new HttpService(connectionSocket);

                    Task.Run(() => service.SocketHandler());
                }

                //Server will wait for a response from console for 1 seconds. After that, a refresh of the browser is
                //needed in order to activate whatever command you wrote in console.
                //Right now the only command available is 'shutdown'.
                //This is so that you can change browser site without needing to type in console to refresh browser
                //because of the readline that prompts you to write in order for the script to continue
                string name = null;
                try
                {
                    //Console.WriteLine("You have 1 seconds to input data.");
                    name = HttpServer.ReadLine(1000);

                }
                catch (TimeoutException)
                {
                    //Console.WriteLine("You waited too long. Please refresh the browser to activate inputs.");

                }
                if (name == "shutdown")
                {
                    _loop = false;
                    serverSocket.Stop();
                    _log.WriteEntry("Server shutdown.", EventLogEntryType.Information, 4);
                }
            }
        }
    }
}