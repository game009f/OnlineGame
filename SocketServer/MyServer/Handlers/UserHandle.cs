using Common;
using Common.Proto.Request;
using Common.Proto.Response;
using MyServer.Model;
using MyServer.MySqlServer;
using MyServer.MySqlServer.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyServer.Handlers
{
    public class UserHandle : BaseHandle
    {
        IUserService userService;
        public override RequestCode RequestCode
        {
            get { return RequestCode.User; }
        }

        public UserHandle(GameServer server) : base(server)
        {
            userService = new UserService();
        }

        public void Login(OperationRequest request)
        {
            UserLoginRequest urm = request.message as UserLoginRequest;
            User user = userService.Login(urm.UserName, urm.Password);
            if (user == null)
            {
                request.session.client.Send(new BaseResponse()
                {
                    ActionCode = ActionCode.Message,
                    ResponseCode = ResponseCode.Fail,
                    Content = "账号或者密码错误！"
                });
            }
            else
            {
                request.session.user = user;
                request.session.playerData.name = user.UserName;
                //如果该玩家在线则踢掉
                server.PlayerDropped(user.UserName);
                //添加的登陆玩家
                server.NewPlayer(user.UserName, request.session);
                request.session.client.Send(new BaseResponse()
                {
                    ActionCode = urm.ActionCode,
                    ResponseCode = ResponseCode.Success,
                    Content = "登陆成功！"
                });
            }
        }

        public void Register(OperationRequest request)
        {
            UserRegisterRequest urm = request.message as UserRegisterRequest;
            User user = userService.Register(urm.UserName, urm.Password);
            if (user == null)
            {
                request.session.client.Send(new BaseResponse()
                {
                    ActionCode = ActionCode.Message,
                    ResponseCode = ResponseCode.Fail,
                    Content = "注册失败，用户已经存在！"
                });
            }
            else
            {
                request.session.client.Send(new BaseResponse()
                {
                    ActionCode = urm.ActionCode,
                    ResponseCode = ResponseCode.Success,
                    Content = "注册成功！"
                });
            }
        }

    }
}
