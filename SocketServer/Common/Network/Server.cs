using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net;
using System.Net.Sockets;
using Common;

namespace Common.Network
{
    public class Server
    {
        public event Action<ClientSession> OnNewClient;

        Socket serverSocket;
        IPEndPoint iPEndPoint;
        public Server(string ipStr, int port)
        {
            iPEndPoint = new IPEndPoint(IPAddress.Parse(ipStr), port);
        }

        public void Start()
        {
            serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            serverSocket.Bind(iPEndPoint);
            serverSocket.Listen(0);
            serverSocket.BeginAccept(AcceptCallBack, null);
        }

        void AcceptCallBack(IAsyncResult ar)
        {
            Socket clientSocket = serverSocket.EndAccept(ar);
            ClientSession session = new ClientSession(clientSocket, this);
            session.Start();
            OnNewClient?.Invoke(session);
            serverSocket.BeginAccept(AcceptCallBack, null);
        }

    }
}
