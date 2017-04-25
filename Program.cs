using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace HTTPServer
{
    class Client
    {
        public Client(TcpClient Client)
        {
            string HTML = "<html><body><h1><b><span style=\"color: red\">Vau! It works..</span></b></h1></body></html>";

            string Str = "HTTP/1.1 200 OK\nContent-type: text/html\nContent-Length:" + HTML.Length.ToString() + "\n\n" + HTML;

            byte[] Buffer = Encoding.ASCII.GetBytes(Str);

            Client.GetStream().Write(Buffer, 0, Buffer.Length);

            Client.Close();
        }
    }

    class Server
    {
        TcpListener Listener;

        public Server(int Port)
        {
            Listener = new TcpListener(IPAddress.Any, Port);
            Listener.Start();

            while (true)
            {
                new Client(Listener.AcceptTcpClient());
            }
        }

        ~Server()
        {
            if(Listener !=null)
            {
                Listener.Stop();
            }
        }

        static void Main(string[] args)
        {

            new Server(80);
        }
    }
}
