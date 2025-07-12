using Common.Proto.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyServer
{
    /// <summary>
    /// 客服端请求消息载体
    /// </summary>
    public class OperationRequest
    {
        /// <summary>
        /// 发送者
        /// </summary>
        public PlayerSession session;
        /// <summary>
        /// 客户端请求内容
        /// </summary>
        public BaseRequest message;
    }
}
