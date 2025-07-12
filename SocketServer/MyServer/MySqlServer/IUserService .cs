using MyServer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyServer.MySqlServer
{
    interface IUserService : IServerBase<User>
    {
        User Login(string user, string password);

        User Register(string user, string password);
    }
}
