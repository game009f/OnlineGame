using Common.Model;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Proto.Response
{
    [ProtoContract]
    public class RoomCreateResponse:BaseResponse
    {
        public RoomCreateResponse()
        {
            ActionCode = ActionCode.RoomCreate;
        }

        /// <summary>
        /// 房主初始位置
        /// </summary>
        [ProtoMember(1)]
        public PlayerData PlayerData;
        //其他房间初始信息
    }
}
