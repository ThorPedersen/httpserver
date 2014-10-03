using System;
using System.Diagnostics;
using System.IO;
using System.Net.Sockets;

namespace httpserver
{
    public class HttpService
    {

        private static readonly string RootCatalog = "c:/temp"; //path to file
        private readonly EventLog _Log = new EventLog();
        private readonly TcpClient connectionSocket;

        private readonly Stream ns;

        public HttpService(TcpClient connectionSocket)
        {
            this.connectionSocket = connectionSocket;
            //creates a Stream for the client to read from and write to
            ns = connectionSocket.GetStream();
        }

        private string Roottext { get; set; }


        public void SocketHandler()
        {
            //Creates StreamReader and StreamWriter
            var sr = new StreamReader(ns);
            var sw = new StreamWriter(ns);
            sw.AutoFlush = true; // enable automatic flushing

            //input form the stream to another format

            //Reads the html request
            string message = sr.ReadLine();
            _Log.Source = "HttpServer";
            _Log.WriteEntry("Client request: " + message, EventLogEntryType.Information, 2);
            Console.WriteLine(message);

            //Check if the request is empty
            if (message != null)
            {
                //Puts the stream into an array
                string[] words = message.Split(' ');
                string uritext = words[1].Replace("/", "\\");
                

                string code = "200 OK";
                const string illegalRequest = "400 Illegal request";
                const string illegalMethod = "400 Illegal request";
                const string illegalProtocol = "400 Illegal protocol";
                const string testMethodNotImplemented = "200 xxx";
                const string http10 = "HTTP/1.0";

                Roottext = RootCatalog + uritext;
                string extensions = Path.GetExtension(Roottext);
                var cType = new ContentType(extensions);
                var fInfo = new FileInfo(Roottext);

                //If the file does not exist return error 404 Not Found

                if (!File.Exists(Roottext))
                {
                    code = "404 Not Found";
                    sw.WriteLine(http10 + " " + code);
                }
                else
                {
                    if (words[0] == "GET")
                    {
                        if (words[2] == "HTTP/1.0" || words[2] == "HTTP/1.1")
                        {
                            
                            sw.WriteLine(http10 + " " + code);
                            long fInfo2 = fInfo.Length;
                            //Prints out the content length and the content type
                            Console.WriteLine("Content-Length:" + fInfo2);
                        }
                        else
                        {
                            if (words[2].Contains("/"))
                            {
                                sw.WriteLine(http10 + " " + illegalProtocol);
                                Console.WriteLine(" {0} 400 Illegal protocol",http10);
                            }
                            else
                            {
                                sw.WriteLine(http10 + " " + illegalRequest);
                                Console.WriteLine("400 Illegal request");
                            }
                        }
                    }
                    if (words[0] == "POST")
                        sw.WriteLine(words[2] + " " + testMethodNotImplemented);

                    if (words[0] != "POST" || words[0] != "GET")
                        sw.WriteLine(words[2] + " " + illegalMethod);

                   
                    string[] lines = File.ReadAllLines(Roottext);

                    sw.WriteLine("You requested " + uritext);
                    foreach (string line in lines)
                    {
                        sw.WriteLine("\t" + line);
                    }
                }
                //Defines the string DateTime in the string "date"
                string date = DateTime.Today.ToLongDateString() + " " + DateTime.Now.ToLongTimeString();
                Console.WriteLine(date); //Prints out the date
                Console.WriteLine(cType.Exstensiontype());
            }

            sr.Close(); //close streamreader, streamwriter and connectionsockets
            sw.Close();
            connectionSocket.Close();
            _Log.WriteEntry("Server response: ", EventLogEntryType.Information, 3);
        }
    }
}