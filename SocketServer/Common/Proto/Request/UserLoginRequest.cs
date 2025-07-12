using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Proto.Request
{
    [ProtoContract]
    public class UserLoginRequest : BaseRequest
    {
        public UserLoginRequest()
        {
            RequestCode = RequestCode.User;
            ActionCode = ActionCode.Login;
        }

        [ProtoMember(1)]
        public string UserName;

        [ProtoMember(2)]
        public string Password;
    }
}
