using Common.Model;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Proto.Response
{
    [ProtoContract]
    public class RoleAttackResponse : BaseResponse
    {
        [ProtoMember(1)]
        public ArrowData ArrowData;

        public RoleAttackResponse()
        {
            ActionCode = ActionCode.RoleAttack;
            ResponseCode = ResponseCode.Success;
        }
    }
}
