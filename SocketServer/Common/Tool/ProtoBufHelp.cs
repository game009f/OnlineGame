using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using ProtoBuf;

namespace Common.Tool
{
    public class ProtoBufHelp
    {
        public static byte[] Serialize(object obj)
        {
            using (var memory = new MemoryStream())
            {
                Serializer.Serialize(memory, obj);
                return memory.ToArray();
            }
        }

        public static T Deserialize<T>(byte[] data)
        {
            using (var memory = new MemoryStream(data))
                return Serializer.Deserialize<T>(memory);
        }
    }
}
