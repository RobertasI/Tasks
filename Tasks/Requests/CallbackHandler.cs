using System;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace Requests
{
    public class CallbackHandler
    {
        private string documentRoot;
        public ManualResetEvent allDone = new ManualResetEvent(false);

        public CallbackHandler(string documentRoot)
        {
            this.documentRoot = documentRoot;
        }

        public void AcceptCallback(IAsyncResult ar)
        {
            Socket listener = (Socket)ar.AsyncState;

            if (listener != null)
            {
                Socket handler = listener.EndAccept(ar);

                Console.WriteLine("Client connected: {0}\r\n", handler.RemoteEndPoint);

                allDone.Set();

                StateObject state = new StateObject();
                state.workSocket = handler;
                handler.BeginReceive(state.buffer, 0, 1024, 0, new AsyncCallback(sendCallback), state);
            }
        }

        private void sendCallback(IAsyncResult ar)
        {
            StateObject state = (StateObject)ar.AsyncState;
            Socket handler = state.workSocket;

            int receiveLength = handler.EndReceive(ar);
            string requestString = Encoding.UTF8.GetString(state.buffer, 0, receiveLength);
            string[] stringsOfRequest = requestString.Split();

            Console.WriteLine(requestString);
            
            if (stringsOfRequest.Length <= 1)
            {
                return;
            }
            
            string file = stringsOfRequest[1];

            var filePath = file.EndsWith("/") ? documentRoot + "index.html" : documentRoot + file;

            ResponseHandler responseHandler = new ResponseHandler();
            responseHandler.GetResponse(handler, filePath);
        }

    }
}
