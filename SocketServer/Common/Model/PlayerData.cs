using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Model
{
    /// <summary>
    /// 玩家数据
    /// </summary>
    [ProtoContract]
    public class PlayerData
    {
        [ProtoMember(1)]
        public string name;
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
        public float forward;
        [ProtoMember(9)]
        public RoleType roleType;

        public PlayerData()
        {
            name = "";
        }

        Random rd = new Random();
        public void InitData()
        {
            if ((rd.Next() % 2) == 0)
                roleType = RoleType.Red;
            else
                roleType = RoleType.Blue;
            x = y = z = rotationX = rotationY = rotationZ = forward = 0;
        }

        public void SetPosition(float x, float y, float z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public void SetRotation(float x, float y, float z)
        {
            this.rotationX = x;
            this.rotationY = y;
            this.rotationZ = z;
        }
    }
}
