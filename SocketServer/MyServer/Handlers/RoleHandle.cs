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
    public class RoleHandle : BaseHandle
    {
        public override RequestCode RequestCode { get { return RequestCode.Role; } }
        public RoleHandle(GameServer server) : base(server)
        {
        }

        public void RoleMove(OperationRequest request)
        {
            RoleMoveRequest moveRequest = request.message as RoleMoveRequest;
            request.session.playerData.SetPosition(moveRequest.PlayerData.x, moveRequest.PlayerData.y, moveRequest.PlayerData.z);
            request.session.playerData.SetRotation(moveRequest.PlayerData.rotationX, moveRequest.PlayerData.rotationY, moveRequest.PlayerData.rotationZ);
            request.session.playerData.forward = moveRequest.PlayerData.forward;
            //直接转发同步方式，更推荐一次性同步数据
            var players = request.session.room.GetPlayers();
            foreach (var player in players)
            {
                if (player != request.session)
                {
                    player.client.Send(new RoleMoveResponse() { PlayerData = request.session.playerData });
                }
            }
        }

        public void RoleAttack(OperationRequest request)
        {
            RoleAttackRequest rar = request.message as RoleAttackRequest;
            var players = request.session.room.GetPlayers();
            foreach (var player in players)
            {
                if (player != request.session)
                {
                    player.client.Send(new RoleAttackResponse() { ArrowData = rar.ArrowData });
                }
            }
        }
    }
}
