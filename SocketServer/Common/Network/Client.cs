using Common.Proto.Request;
using Common.Proto.Response;
using Common.Tool;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Common.Network
{
    public class Client
    {
        public event Action<BaseResponse> MessageHandler;
        /// <summary>
        /// 异常处理
        /// </summary>
        public event Action<Exception> ExceptionHandle;

        /// <summary>
        /// 断开连接
        /// </summary>
        public event Action Disconnect;

        Socket clientSocket;
        IPEndPoint iPEndPoint;
        MessageHelp msg = new MessageHelp();

        public bool Connected
        {
            get
            {
                return clientSocket.Connected;
            }
        }

        public Client(string ipStr, int port)
        {
            iPEndPoint = new IPEndPoint(IPAddress.Parse(ipStr), port);
            TryConnect();
        }

        public bool TryConnect()
        {
            try
            {
                clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                clientSocket.Connect(iPEndPoint);
                return true;
            }
            catch (Exception ex)
            {
                ExceptionHandle?.Invoke(ex);
            }
            return false;
        }

        public void Start()
        {
            if (clientSocket == null || !clientSocket.Connected)
            {
                return;
            }
            clientSocket.BeginReceive(msg.Data, msg.StartIndex, msg.RemainSize, SocketFlags.None, ReceiveCallBack, clientSocket);
        }

        void ReceiveCallBack(IAsyncResult ar)
        {
            try
            {
                int count = clientSocket.EndReceive(ar);
                if (count <= 0)
                    throw new Exception("连接中断!");
                msg.ReadMessage(count, MessageHandler);
                Start();
            }
            catch (Exception ex)
            {
                ExceptionHandle?.Invoke(ex);
                Close();
            }
        }

        public void Close()
        {
            clientSocket?.Close();
            Disconnect?.Invoke();
        }

        public void Send(BaseRequest message)
        {
            if (message == null) return;
            try
            {
                byte[] data = MessageHelp.Serialize(message);
                clientSocket.Send(data);
            }
            catch (Exception ex)
            {
                ExceptionHandle?.Invoke(ex);
            }
        }
    }
}
