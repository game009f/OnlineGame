using Common.Model;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Proto.Response
{
    [ProtoContract]
    public class RoleMoveResponse:BaseResponse
    {
        /// <summary>
        /// 房主初始位置
        /// </summary>
        [ProtoMember(1)]
        public PlayerData PlayerData;

        public RoleMoveResponse()
        {
            ActionCode = ActionCode.RoleMove;
            ResponseCode = ResponseCode.Success;
        }
    }
}
