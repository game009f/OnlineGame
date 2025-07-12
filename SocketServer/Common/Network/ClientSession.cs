using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using Common.Tool;
using Common.Proto.Request;
using Common.Proto.Response;

namespace Common.Network
{
    public class ClientSession
    {
        public event Action<BaseRequest> MessageHandler;
        /// <summary>
        /// 异常处理
        /// </summary>
        public event Action<Exception> ExceptionHandle;
        /// <summary>
        /// 断开连接
        /// </summary>
        public event Action Disconnect;

        Server server;
        Socket clientSocket;
        MessageHelp msg = new MessageHelp();

        public bool Connected
        {
            get
            {
                return clientSocket.Connected;
            }
        }

        public string GetIpEndPoint()
        {
            return clientSocket.RemoteEndPoint.ToString();
        }

        public ClientSession(Socket socket, Server server)
        {
            clientSocket = socket;
            this.server = server;
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

        public void Send(BaseResponse message)
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
