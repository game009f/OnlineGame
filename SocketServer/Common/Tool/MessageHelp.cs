using Common.Network;
using Common.Proto.Request;
using Common.Proto.Response;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Tool
{
    public class MessageHelp
    {
        private byte[] data = new byte[1024];
        private int startIndex = 0;
        /// <summary>
        /// 消息数据
        /// </summary>
        public byte[] Data
        {
            get { return data; }
        }
        /// <summary>
        /// 数据长度，数据开始存的起始位置
        /// </summary>
        public int StartIndex
        {
            get { return startIndex; }
        }
        /// <summary>
        /// 剩余可存数据长度
        /// </summary>
        public int RemainSize
        {
            get { return data.Length - startIndex; }
        }

        public static byte[] Serialize(BaseRequest msg)
        {
            byte[] dataMsg = ProtoBufHelp.Serialize(msg);
            byte[] dataLength = BitConverter.GetBytes(dataMsg.Length);
            byte[] newData = dataLength.Concat(dataMsg).ToArray();
            return newData;
        }

        public static byte[] Serialize(BaseResponse msg)
        {
            byte[] dataMsg = ProtoBufHelp.Serialize(msg);
            byte[] dataLength = BitConverter.GetBytes(dataMsg.Length);
            byte[] newData = dataLength.Concat(dataMsg).ToArray();
            return newData;
        }

        /// <summary>
        /// 新增加的数据长度
        /// </summary>
        /// <param name="newDataAmount">新增数据长度</param>
        /// <param name="action">回调</param>
        public void ReadMessage(int newDataAmount, Action<BaseRequest> callback)
        {
            startIndex += newDataAmount;
            while (true)
            {
                try
                {
                    if (startIndex <= 4)
                        return;
                    int count = BitConverter.ToInt32(data, 0);
                    int remain = startIndex - 4;
                    if (remain >= count)
                    {
                        byte[] buffer = new byte[count];
                        Array.Copy(data, 4, buffer, 0, count);
                        BaseRequest bm = ProtoBufHelp.Deserialize<BaseRequest>(buffer);
                        callback?.Invoke(bm);

                        startIndex -= (count + 4);
                        Array.Copy(data, count + 4, data, 0, startIndex);
                    }
                    else
                    {
                        break;
                    }
                }
                catch
                {
                    //如果数据发生错误那么数据归零
                    startIndex = 0;
                    Array.Clear(data, 0, data.Length);
                }
            }
        }

        public void ReadMessage(int newDataAmount, Action<BaseResponse> callback)
        {
            startIndex += newDataAmount;
            while (true)
            {
                try
                {
                    if (startIndex <= 4)
                        return;
                    int count = BitConverter.ToInt32(data, 0);
                    int remain = startIndex - 4;
                    if (remain >= count)
                    {
                        byte[] buffer = new byte[count];
                        Array.Copy(data, 4, buffer, 0, count);
                        BaseResponse bm = ProtoBufHelp.Deserialize<BaseResponse>(buffer);
                        callback?.Invoke(bm);

                        startIndex -= (count + 4);
                        Array.Copy(data, count + 4, data, 0, startIndex);
                    }
                    else
                    {
                        break;
                    }
                }
                catch
                {
                    //如果数据发生错误那么数据归零
                    startIndex = 0;
                    Array.Clear(data, 0, data.Length);
                }
            }
        }
    }
}
