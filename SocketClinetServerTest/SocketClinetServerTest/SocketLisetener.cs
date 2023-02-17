using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace SocketClinetServerTest
{
    public class SocketLisetener
    {
        public async Socket BuildSocketConnection()
        {
            EndPoint serverEndPoint = null;
            int port = 8000;
            IPHostEntry host = Dns.GetHostEntry("localhost");
            IPAddress ipAddress = host.AddressList[0];
            IPEndPoint localEndPoint = new IPEndPoint(ipAddress, 11000);

            var socketLisetener = new Socket(ipAddress.AddressFamily, socketType: SocketType.Stream, protocolType: ProtocolType.Tcp);
            var tcpLisetener = new TcpListener(IPAddress.Any, 3000);
            tcpLisetener.Start();

            var handler = await socketLisetener.AcceptAsync();
            socketLisetener.BeginAccept(new AsyncCallback(AcceptReceiveCallback), socketLisetener);
            socketLisetener.BeginConnect(serverEndPoint, new AsyncCallback(tryBuildConnection), socketLisetener);
            socketLisetener.Blocking = true;
            //socketLisetener.Connect(serverEndPoint, port);
            socketLisetener.BeginAccept();
        }

        private static void tryBuildConnection(IAsyncResult ar)
        {

        }

        private static void AcceptReceiveCallback(IAsyncResult ar)
        {
            // Get the socket that handles the client request.
            Socket listener = (Socket)ar.AsyncState;

            // End the operation and display the received data on the console.
            byte[] Buffer;
            int bytesTransferred;
            Socket handler = listener.EndAccept(out Buffer, out bytesTransferred, ar);
            string stringTransferred = Encoding.ASCII.GetString(Buffer, 0, bytesTransferred);

            Console.WriteLine(stringTransferred);
            Console.WriteLine("Size of data transferred is {0}", bytesTransferred);

            // Create the state object for the asynchronous receive.
            listener.EndAccept(ar);
        }

        private static void BeginAcceptCallbac(IAsyncResult ar)
        {

        }
    }
}

