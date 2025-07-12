using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyServer
{
    public class ConstSettings
    {
        public const string ServerIp = "127.0.0.1";
        public const int Port = 8888;

        //踢掉不存活的客户端时间，单位：秒
        public const int DropIfNotAliveTime = 3;

        //每0.02秒提交一次数据
        public const float DataSubmitTime = 0.02f;

        //是否关闭调试
        public const bool DisableConsolePanelInRuntime = false;
    }
}
