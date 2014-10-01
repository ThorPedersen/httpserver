using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace httpserver
{
    class StatusHandling
    {
        private readonly List<string> _list = new List<string> { "201 Created", "202 Accepted", "204 No Content", "301 Moved Permanently", "302 Moved Temporarily", "304 Not Modified", "400 Bad Request", "401 Unauthorized", "403 Forbidden", "404 Not Found", "500 Internal Server Error", "501 Not Implemented", "502 Bad Gateway", "503 Service Unavailable" };
        private string _errorMessege = "200 OK";
        private readonly string _clientRequest;
        private readonly string _path;

        private void StatusLookUp()
        {
            string[] words = _clientRequest.Split(' ');
            if (!File.Exists(_path))
            {
                _errorMessege = ErrorHandler(404);
            }
            if (words[0] != "GET" && words[0] != "POST")
            {
                _errorMessege = ErrorHandler(400);
            }
            
            if (words[2] != "HTTP/1.1")
            {
                _errorMessege = ErrorHandler(400);
            }
        }


        //Lo
        public string ErrorHandler(int id)
        {
            return _list.Find(x => x.Contains(Convert.ToString(id)));
        }

        public string ServerRespons()
        {
            return _errorMessege;
        }

    }
}
