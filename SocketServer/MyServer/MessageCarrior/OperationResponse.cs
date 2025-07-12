using Common.Proto.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyServer
{
    public class OperationResponse
    {
        /// <summary>
        /// 只发给某个玩家
        /// </summary>
        public string sendto;
        /// <summary>
        /// 不包含某个玩家
        /// </summary>
        public string except;
        /// <summary>
        /// 服务器响应内容
        /// </summary>
        public BaseResponse message;
    }
}
