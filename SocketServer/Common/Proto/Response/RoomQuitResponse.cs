using Common.Model;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Proto.Response
{
    [ProtoContract]
    public class RoomQuitResponse:BaseResponse
    {
        [ProtoMember(1)]
        public PlayerData PlayerData;
        public RoomQuitResponse()
        {
            ActionCode = ActionCode.RoomQuit;
        }
    }
}
