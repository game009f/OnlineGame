using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Proto.Response
{
    [ProtoContract]
    [ProtoInclude(4, typeof(RoomListResponse))]
    [ProtoInclude(5, typeof(RoomJoinResponse))]
    [ProtoInclude(6, typeof(RoomCreateResponse))]
    [ProtoInclude(7, typeof(RoomQuitResponse))]
    [ProtoInclude(8, typeof(RoleMoveResponse))]
    [ProtoInclude(9, typeof(RoleAttackResponse))]
    public class BaseResponse
    {
        [ProtoMember(1)]
        public ActionCode ActionCode = ActionCode.None;
        [ProtoMember(2)]
        public ResponseCode ResponseCode = ResponseCode.Success;
        [ProtoMember(3)]
        public string Content;

    }
}
