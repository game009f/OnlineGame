using Common.Model;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Proto.Request
{
    [ProtoContract]
    public class RoleMoveRequest:BaseRequest
    {
        [ProtoMember(1)]
        public PlayerData PlayerData;

        public RoleMoveRequest()
        {
            RequestCode = RequestCode.Role;
            ActionCode = ActionCode.RoleMove;
        }
    }
}
