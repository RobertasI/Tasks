using System;
using System.Net;
using System.Net.Sockets;

namespace Requests
{
    public class Server
    {
        private static string documentRoot = "C:/";

        public static void Main(string[] args)
        {
            StartServer();
        }

        public static void StartServer()
        {
            Console.WriteLine("Do you want to edit default Document Root (C:/)? y/n");
            var answerForDocumentRoot = Console.ReadLine();
            if (answerForDocumentRoot == "y")
            {
                Console.WriteLine("Enter new Document Root");
                documentRoot = Console.ReadLine();
            }
            Console.WriteLine("Document Root: " + documentRoot);
            
            Console.WriteLine("Enter port number: ");
            var portNumber = Convert.ToInt32(Console.ReadLine());
            if (portNumber < 0 || portNumber > 65535)
            {
                Console.WriteLine("Port number is wrong. Set to default '80'");
                portNumber = 80;
            }
           
            IPAddress ipAddress = IPAddress.Parse("127.0.0.1");
            IPEndPoint endpoint = new IPEndPoint(ipAddress, portNumber);
            Console.WriteLine(string.Format("Listening port:{0}", portNumber));
            Console.WriteLine();
            Socket listener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            listener.Bind(endpoint);
            Console.WriteLine("Waiting for http requests...\r\n");

            CallbackHandler callbackHandler = new CallbackHandler(documentRoot);
            while (true)
            {
                callbackHandler.allDone.Reset();
                listener.Listen(10);
                listener.BeginAccept(new AsyncCallback(callbackHandler.AcceptCallback), listener);
                callbackHandler.allDone.WaitOne();
            }
        }
    }
}
