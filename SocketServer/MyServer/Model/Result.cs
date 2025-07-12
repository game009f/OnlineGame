using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyServer.Model
{
    public class Result
    {
        public int Id { get; set; }

        public int UserId { get; set; }
        /// <summary>
        /// 比赛总次数
        /// </summary>
        public int TotalCount { get; set; }
        /// <summary>
        /// 胜利次数
        /// </summary>
        public int WinCount { get; set; }
    }
}
