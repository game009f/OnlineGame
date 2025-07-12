using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;
using Common.Model;
using Common.Proto.Response;
using Common.Proto.Request;

public class RoleAttackHandle : BaseHandle
{
    public override void Awake()
    {
        ActionCode = ActionCode.RoleAttack  ;
        base.Awake();
    }

    public void SendRequest(ArrowData arrowData)
    {
        base.SendRequest(new RoleAttackRequest()
        {
            ArrowData = arrowData
        });
    }

    public override void OnResponse(BaseResponse response)
    {
        facade.PlayerMgr.EnqueueResponse(response);
    }

    public void Destroy()
    {
        Destroy(this);
    }
}
