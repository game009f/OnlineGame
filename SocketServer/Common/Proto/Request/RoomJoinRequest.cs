using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Proto.Request
{
    [ProtoContract]
    public class RoomJoinRequest : BaseRequest
    {
        public RoomJoinRequest()
        {
            RequestCode = RequestCode.Room;
            ActionCode = ActionCode.RoomJoin;
        }

        [ProtoMember(1)]
        public int RoomId;
    }
}
