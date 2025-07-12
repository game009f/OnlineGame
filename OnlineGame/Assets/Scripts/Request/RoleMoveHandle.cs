using Common.Model;
using Common.Proto.Request;
using Common.Proto.Response;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoleMoveHandle : BaseHandle
{
    public Transform LocalRoleTransform;
    public PlayerMove playerMove;
    public bool isReady = false;

    private float timer = 0;
    private float interval = 0.06f;
    
    public override void Awake()
    {
        ActionCode = Common.ActionCode.RoleMove;
        base.Awake();
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer >= interval && isReady)
        {
            timer = 0;
            SendRequest(new PlayerData()
            {
                x = LocalRoleTransform.position.x,
                y = LocalRoleTransform.position.y,
                z = LocalRoleTransform.position.z,
                rotationX = LocalRoleTransform.eulerAngles.x,
                rotationY = LocalRoleTransform.eulerAngles.y,
                rotationZ = LocalRoleTransform.eulerAngles.z,
                forward = playerMove.forward
            });
        }
    }

    public void SendRequest(PlayerData playerData)
    {
        base.SendRequest(new RoleMoveRequest()
        {
            PlayerData = playerData
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
