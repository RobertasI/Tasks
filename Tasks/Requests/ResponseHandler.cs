using System.IO;
using System.Net.Sockets;
using System.Text;

namespace Requests
{
    public class ResponseHandler
    {
        public void GetResponse(Socket client, string filePath)
        {
            if (File.Exists(filePath))
            {
                send200Response(client, filePath);
            }
            else
            {
                send404response(client);
            }
        }

        private void send404response(Socket client)
        {
            string statusLine = "HTTP/1.1 404 Not Found\r\n";
            string responseHeader = "Content-Type: text/html\r\n";
            client.Send(Encoding.UTF8.GetBytes(statusLine));
            client.Send(Encoding.UTF8.GetBytes(responseHeader));
            client.Send(Encoding.UTF8.GetBytes("\r\n"));
            client.Send(Encoding.UTF8.GetBytes("<html><head><title>Not Found</title></head><body><div>Not Found</div></body></html>"));
            client.Close();
        }

        private void send200Response(Socket client, string filePath)
        {
            string statusLine = "HTTP/1.1 200 OK\r\n";
            string responseHeader = "Content-Type: text/html\r\n";
            client.Send(Encoding.UTF8.GetBytes(statusLine));
            client.Send(Encoding.UTF8.GetBytes(responseHeader));
            client.Send(Encoding.UTF8.GetBytes("\r\n"));
            client.SendFile(filePath);
            client.Close();
        }
    }
}
