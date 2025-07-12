using MyServer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyServer.MySqlServer.Impl
{
    class UserService : ServiceBase<User>, IUserService
    {
        public User Login(string user, string password)
        {
            var list = base.GetEntity<int>(x => x.UserName == user && x.Password == password, x => x.Id, false).ToList();
            if (list.Count() >= 1)
            {
                return list[0];
            }
            return null;
        }

        public User Register(string user, string password)
        {
            //用户已经存在
            var list = base.GetEntity<int>(x => x.UserName == user, x => x.Id, false).ToList();
            if (list.Count() >= 1)
            {
                return null;
            }

            User user1 = new User()
            {
                UserName = user,
                Password = password
            };
            return base.Add(user1);
        }
    }
}
