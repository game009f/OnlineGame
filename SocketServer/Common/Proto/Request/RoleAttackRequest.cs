using Common.Model;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Proto.Request
{
    [ProtoContract]
    public class RoleAttackRequest:BaseRequest
    {
        [ProtoMember(1)]
        public ArrowData ArrowData;

        public RoleAttackRequest()
        {
            RequestCode = RequestCode.Role;
            ActionCode = ActionCode.RoleAttack;
        }
    }
}
