using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Proto.Response
{
    [ProtoContract]
    public class RoomInfo
    {
        [ProtoMember(1)]
        public int Id { get; set; }
        [ProtoMember(2)]
        public string Name { get; set; }

        [ProtoMember(3)]
        public RoomState RoomState { get; set; }

    }

    [ProtoContract]
    public class RoomListResponse : BaseResponse
    {
        [ProtoMember(1)]
        public List<RoomInfo> RoomList;
    }
}
