using Common;
using Common.Proto.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyServer
{
    public class Room
    {
        /// <summary>
        /// 计数器
        /// </summary>
        private static int Counter = 0;
        public int Id { get; set; }
        public string Name { get; set; }

        public RoomState RoomState { get; set; }

        /// <summary>
        /// 房间最大在线人数
        /// </summary>
        public int MaximumOnline = 3;

        private List<PlayerSession> users { get; set; }

        private GameServer server;

        public Room(GameServer server,string name)
        {
            this.server = server;
            this.Id = Counter;
            Counter += 1;
            this.Name = name;
            RoomState = RoomState.Normal;
            users = new List<PlayerSession>();
        }

        public List<PlayerSession> GetPlayers()
        {
            return users;
        }

        public void AddPlayer(PlayerSession player)
        {
            player.room = this;
            users.Add(player);
            if (users.Count >= MaximumOnline)
                RoomState = RoomState.HouseFull;
        }

        public void RemovePlayer(PlayerSession session)
        {
            users.Remove(session);
            if (users.Count <= 0)
            {
                //没人了就移除房间
                server.RemoveRoom(this.Id);
            }
            if (users.Count < MaximumOnline)
                RoomState = RoomState.Normal;
        }
    }
}
