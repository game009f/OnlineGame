using Common.Model;
using Common.Network;
using Common.Proto.Request;
using MyServer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyServer
{
    public class PlayerSession
    {
        public GameServer server;
        public ClientSession client;

        /// <summary>
        /// 用户信息
        /// </summary>
        public User user;

        /// <summary>
        /// 玩家所在房间
        /// </summary>
        public Room room;

        /// <summary>
        /// 玩家数据
        /// </summary>
        public PlayerData playerData;
        /// <summary>
        /// 远程地址端口
        /// </summary>
        public string RemoteEndPoint = "";

        public PlayerSession(GameServer gameServer, ClientSession clientSession)
        {
            server = gameServer;
            client = clientSession;
            RemoteEndPoint = clientSession.GetIpEndPoint();
            client.MessageHandler += OnMessage;
            client.Disconnect += () =>
            {
                if (room != null)
                {
                    room.RemovePlayer(this);
                    room = null;
                }
                playerData = null;
                if (user != null)
                    gameServer.PlayerDropped(user.UserName);
            };
            playerData = new PlayerData();
        }

        public void Close()
        {
            
            if (room != null)
            {
                room.RemovePlayer(this);
                //待修改
                //server.PlayerMessageQueue.Enqueue(new OperationRequest()
                //{
                //    session = this,
                //    message = new BaseRequest()
                //    {
                //        RequestCode = Common.RequestCode.Room,
                //        ActionCode = Common.ActionCode.RoomQuit
                //    }
                //});
            }
            user = null;
            client.Close();
        }

        public void OnMessage(BaseRequest message)
        {
            if (message == null) return;
            Console.WriteLine("{3} IpEndPoint:{0} RequestCode:{1} ActionCode:{2}", client.GetIpEndPoint(), message.RequestCode, message.ActionCode, DateTime.Now.ToString());
            if (user == null)
            {
                //用户没有登陆前只能发送登录和注册请求
                if (!(message is UserLoginRequest) && !(message is UserRegisterRequest))
                {
                    client.Close();
                    return;
                }
            }
            //进入消息队列
            server.PlayerMessageQueue.Enqueue(new OperationRequest()
            {
                session = this,
                message = message
            });
        }
    }
}
