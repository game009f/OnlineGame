using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common.Proto.Request;
using Common.Proto.Response;

public class RoomQuitHandle : BaseHandle
{
    private RoomQuitPanel roomQuitPanel;
    public override void Awake()
    {
        ActionCode = Common.ActionCode.RoomQuit;
        roomQuitPanel = GetComponent<RoomQuitPanel>();
        base.Awake();
    }

    public void SendRequest()
    {
        base.SendRequest(new BaseRequest()
        {
            RequestCode = Common.RequestCode.Room,
            ActionCode = Common.ActionCode.RoomQuit
        });
    }

    public override void OnResponse(BaseResponse response)
    {
        roomQuitPanel.OnRoomQuitResponse(response);
        //有玩家退出
        facade.PlayerMgr.EnqueueResponse(response);
    }
}
