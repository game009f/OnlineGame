using System.Collections;
using System.Collections.Generic;
using Common.Proto.Request;
using Common.Proto.Response;
using UnityEngine;

public class RegisterHandle : BaseHandle
{
    private RegisterPanel registerPanel;

    public override void Awake()
    {
        ActionCode = Common.ActionCode.Register;
        registerPanel = GetComponent<RegisterPanel>();
        base.Awake();
    }

    public void SendRequest(string user, string pwd)
    {
        UserRegisterRequest msg = new UserRegisterRequest()
        {
            UserName = user,
            Password = pwd
        };
        base.SendRequest(msg);
    }

    public override void OnResponse(BaseResponse responseMessage)
    {
        registerPanel.OnRegisterResponse(responseMessage);
    }
}
