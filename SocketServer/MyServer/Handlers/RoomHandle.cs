using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using Common.Proto.Request;
using Common.Proto.Response;

namespace MyServer.Handlers
{
    public class RoomHandle : BaseHandle
    {
        public override RequestCode RequestCode { get { return RequestCode.Room; } }
        public RoomHandle(GameServer server) : base(server)
        {
        }

        public void RoomList(OperationRequest request)
        {
            RoomListResponse response = new RoomListResponse()
            {
                ActionCode = request.message.ActionCode,
                ResponseCode = ResponseCode.Success,
                RoomList = new List<RoomInfo>()
            };

            foreach (var item in server.roomList)
            {
                response.RoomList.Add(new RoomInfo()
                {
                    Id = item.Value.Id,
                    Name = item.Value.Name,
                    RoomState = item.Value.RoomState
                });
            }

            request.session.client.Send(response);
        }

        public void RoomCreate(OperationRequest request)
        {
            server.RoomCreate(request.session);
            request.session.playerData.InitData();
            request.session.client.Send(new RoomCreateResponse()
            {
                Content = "房间创建成功",
                PlayerData = request.session.playerData
            });
        }

        public void RoomJoin(OperationRequest request)
        {
            RoomJoinRequest rjr = request.message as RoomJoinRequest;
            if (!server.roomList.ContainsKey(rjr.RoomId))
            {
                request.session.client.Send(new BaseResponse()
                {
                    ActionCode = ActionCode.Message,
                    ResponseCode = ResponseCode.Fail,
                    Content = "房间不存在！"
                });
                return;
            }
            if (server.roomList[rjr.RoomId].RoomState == RoomState.HouseFull)
            {
                request.session.client.Send(new BaseResponse()
                {
                    ActionCode = ActionCode.Message,
                    ResponseCode = ResponseCode.Fail,
                    Content = "房间已满！"
                });
                return;
            }
            request.session.playerData.InitData();
            //返回玩家信息
            request.session.client.Send(new RoomCreateResponse()
            {
                Content = "加入房间",
                PlayerData = request.session.playerData
            });
            //同步房间信息
            foreach (var player in server.roomList[rjr.RoomId].GetPlayers())
            {
                //把玩家加入消息发送给其他玩家
                player.client.Send(new RoomJoinResponse()
                {
                    ActionCode = ActionCode.RoomJoin,
                    ResponseCode = ResponseCode.Success,
                    PlayerData = request.session.playerData
                });
                if (request.session != player)
                {
                    //把其他玩家信息同步给当前玩家，也可以一次性打包同步数据
                    request.session.client.Send(new RoomJoinResponse()
                    {
                        ActionCode = ActionCode.RoomJoin,
                        ResponseCode = ResponseCode.Success,
                        PlayerData = player.playerData
                    });
                }
            }
            
            //把玩家添加到列表
            server.roomList[rjr.RoomId].AddPlayer(request.session);

        }

        public void RoomQuit(OperationRequest request)
        {
            //从房间移除玩家
            server.RoomQuit(request);
            //给还在房间内的玩家发送退出房间消息
            foreach (var session in request.session.room.GetPlayers())
            {
                session.client.Send(new RoomQuitResponse()
                {
                    PlayerData = request.session.playerData
                });
            }
            request.session.room = null;
        }
    }
}
