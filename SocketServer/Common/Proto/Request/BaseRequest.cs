using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Proto.Request
{
    [ProtoContract]
    [ProtoInclude(3, typeof(UserLoginRequest))]
    [ProtoInclude(4, typeof(UserRegisterRequest))]
    [ProtoInclude(5, typeof(RoomJoinRequest))]
    [ProtoInclude(6, typeof(RoleMoveRequest))]
    [ProtoInclude(7, typeof(RoleAttackRequest))]
    public class BaseRequest
    {
        [ProtoMember(1)]
        public RequestCode RequestCode;
        [ProtoMember(2)]
        public ActionCode ActionCode;
    }
}
