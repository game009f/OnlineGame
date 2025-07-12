using Common.Model;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Proto.Response
{
    [ProtoContract]
    public class RoomJoinResponse:BaseResponse
    {
        [ProtoMember(1)]
        public PlayerData PlayerData;

        public RoomJoinResponse()
        {
            ActionCode = ActionCode.RoomJoin;
            ResponseCode = ResponseCode.Success;
        }
    }
}
