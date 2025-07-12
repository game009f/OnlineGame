using Common;
using Common.Proto.Request;
using Common.Proto.Response;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoginHandle : BaseHandle
{
    private LoginPanel loginPanel;
    public override void Awake()
    {
        ActionCode = ActionCode.Login;
        loginPanel = GetComponent<LoginPanel>();
        base.Awake();
    }

    public void SendRequest(string username, string password)
    {
        UserLoginRequest message = new UserLoginRequest()
        {
            UserName = username,
            Password = password
        };
        base.SendRequest(message);
    }

    public override void OnResponse(BaseResponse responseMessage)
    {
        loginPanel.OnLoginResponse(responseMessage);
    }
}
