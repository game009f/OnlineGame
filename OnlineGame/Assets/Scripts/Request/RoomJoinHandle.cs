using System.Collections;
using System.Collections.Generic;
using Common.Proto.Request;
using Common.Proto.Response;
using UnityEngine;

public class RoomJoinHandle : BaseHandle
{
    public RoomListPanel roomListPanel;
    public override void Awake()
    {
        ActionCode = Common.ActionCode.RoomJoin;
        roomListPanel = GetComponent<RoomListPanel>();
        base.Awake();
    }

    public void SendRequest(RoomInfo roomInfo)
    {
        base.SendRequest(new RoomJoinRequest()
        {
            RoomId = roomInfo.Id
        });
    }

    public override void OnResponse(BaseResponse response)
    {
        roomListPanel.OnRoomJoinResponse(response);
        facade.PlayerMgr.EnqueueResponse(response);
    }
}
