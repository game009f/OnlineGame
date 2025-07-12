using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyServer.Handlers
{
    public abstract class BaseHandle
    {
        /// <summary>
        /// 请求代码，分配Handle用
        /// </summary>
        public abstract RequestCode RequestCode { get; }

        public GameServer server;

        public BaseHandle(GameServer server)
        {
            this.server = server;
            server.handlers.Add(RequestCode, this);
            Console.WriteLine("注册{0}",this.ToString());
        }
    }
}
