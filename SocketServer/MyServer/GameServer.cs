using Common;
using Common.Network;
using Common.Proto.Request;
using Common.Proto.Response;
using MyServer.Handlers;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MyServer
{
    public class GameServer
    {
        /// <summary>
        /// 请求消息队列
        /// </summary>
        public ConcurrentQueue<OperationRequest> PlayerMessageQueue = new ConcurrentQueue<OperationRequest>();
        //消息发送队列
        private Queue<OperationResponse> _msgQueue = new Queue<OperationResponse>();
        //已登陆的玩家
        private Dictionary<string, PlayerSession> players = new Dictionary<string, PlayerSession>();
        //处理类
        public Dictionary<RequestCode, BaseHandle> handlers = new Dictionary<RequestCode, BaseHandle>();
        //房间
        public Dictionary<int, Room> roomList = new Dictionary<int, Room>();

        public GameServer()
        {
            RegisteHandlers();
        }

        public void RegisteHandlers()
        {
            Type[] types = Assembly.GetAssembly(typeof(BaseHandle)).GetTypes();
            foreach (var hand in types)
            {
                if (!hand.FullName.EndsWith("BaseHandle") && hand.FullName.EndsWith("Handle"))
                {
                    Activator.CreateInstance(hand, new object[] { this });
                }
            }
        }

        public void Start()
        {
            Console.WriteLine("启动服务器");
            Server server = new Server(ConstSettings.ServerIp, ConstSettings.Port);
            server.OnNewClient += (client) =>
            {
                PlayerSession playerSession = new PlayerSession(this, client);
                Console.WriteLine("{1} 新用户:{0}", playerSession.client.GetIpEndPoint(), DateTime.Now.ToString());
            };
            server.Start();
            //处理数据
            LoopFun();
        }

        public void LoopFun()
        {
            int FRAME_TIME = 20;
            int timeStep = FRAME_TIME;

            int lastTimeStamp = Environment.TickCount;
            int delta = 0;
            int frame = 0; //数据帧
            while (true)
            {
                try
                {
                    frame++;
                    int before = Environment.TickCount;
                    FrameLogic();
                    int after = Environment.TickCount;
                    delta = after - before;

                    //修正计算时间
                    timeStep = FRAME_TIME - delta;
                    if (timeStep < 0)
                        timeStep = 0;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                }
                Thread.Sleep(10);
            }
        }

        //游戏一帧的数据
        void FrameLogic()
        {
            lock (this)
            {
                //处理所有客户端请求
                while (PlayerMessageQueue.Count > 0)
                {
                    OperationRequest clientMsg;
                    PlayerMessageQueue.TryDequeue(out clientMsg);

                    BaseHandle handler = handlers[clientMsg.message.RequestCode];
                    string methodName = Enum.GetName(typeof(ActionCode), clientMsg.message.ActionCode);
                    MethodInfo mi = handler.GetType().GetMethod(methodName);
                    if (mi == null)
                    {
                        Console.WriteLine("[警告]在Controller[" + handler.GetType() + "]中没有对应的处理方法:[" + methodName + "]");
                        break;
                    }
                    object[] parameters = new object[] { clientMsg };
                    mi.Invoke(handler, parameters);
                }

                //处理游戏逻辑
                //FixedUpdate();
            }
        }

        public void PlayerDropped(string user)
        {
            if (players.ContainsKey(user))
            {
                lock (this)
                {
                    Console.WriteLine("{2} 踢出玩家:{0} IpEndPoint:{1}", user, players[user].RemoteEndPoint, DateTime.Now.ToString());
                    players[user].Close(); //关闭连接
                    players.Remove(user);
                }
            }
        }

        public void NewPlayer(string user, PlayerSession session)
        {
            lock (this)
            {
                players.Add(user, session);
            }
        }

        public void RoomCreate(PlayerSession player)
        {
            Room room = new Room(this, player.user.UserName);
            room.AddPlayer(player);
            roomList.Add(room.Id, room);
        }

        public BaseResponse RoomJoin(OperationRequest request)
        {
            BaseResponse response = new BaseResponse()
            {
                ActionCode = ActionCode.Message,
                ResponseCode = ResponseCode.Fail
            };

            RoomJoinRequest rjr = request.message as RoomJoinRequest;
            if (!roomList.ContainsKey(rjr.RoomId))
            {
                response.Content = "房间不存在！";
                return response;
            }
            if (roomList[rjr.RoomId].RoomState != RoomState.HouseFull)
            {
                response.Content = "房间已满！";
                return response;
            }

            roomList[rjr.RoomId].AddPlayer(request.session);

            return new RoomJoinResponse()
            {
                ActionCode = ActionCode.RoomJoin,
                ResponseCode = ResponseCode.Success,
                PlayerData = request.session.playerData
            };
        }

        public void RoomQuit(OperationRequest request)
        {
            request.session.room.RemovePlayer(request.session);
        }

        public void RemoveRoom(int roomId)
        {
            roomList.Remove(roomId);
        }
    }
}
