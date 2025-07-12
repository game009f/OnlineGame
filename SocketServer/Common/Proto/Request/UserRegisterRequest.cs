using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Proto.Request
{
    [ProtoContract]
    public class UserRegisterRequest : BaseRequest
    {
        public UserRegisterRequest()
        {
            RequestCode = RequestCode.User;
            ActionCode = ActionCode.Register;
        }

        [ProtoMember(1)]
        public string UserName;

        [ProtoMember(2)]
        public string Password;
    }
}
