using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Model
{
    [ProtoContract]
    public class ArrowData
    {
        [ProtoMember(1)]
        public string prefabName;
        [ProtoMember(2)]
        public float x;
        [ProtoMember(3)]
        public float y;
        [ProtoMember(4)]
        public float z;
        [ProtoMember(5)]
        public float rotationX;
        [ProtoMember(6)]
        public float rotationY;
        [ProtoMember(7)]
        public float rotationZ;
        [ProtoMember(8)]
        public float rotationW;
        [ProtoMember(9)]
        public float speed;
    }
}
