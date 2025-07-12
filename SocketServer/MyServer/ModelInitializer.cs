using MyServer.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyServer
{
    public class ModelInitializer : DropCreateDatabaseIfModelChanges<GameServerContext>
    {
        protected override void Seed(GameServerContext context)
        {
            ///初始化数据 可以添加管理员什么的
            //用户
            List<User> listUser = new List<User>() {
                new User() { UserName="admin", Password="admin"},
                new User() { UserName="user01", Password="user01"},
                new User() { UserName="user02", Password="user02"},
                new User() { UserName="cab1", Password="cab1"},
            };
            context.Users.AddRange(listUser.ToArray());

            context.SaveChanges();
            //base.Seed(context);
        }
    }
}
