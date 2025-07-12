using System.Collections;
using System.Collections.Generic;
using Common.Proto.Request;
using Common.Proto.Response;
using UnityEngine;

public class RoomListHandle : BaseHandle
{
    private RoomListPanel roomListPanel;
    public override void Awake()
    {
        ActionCode = Common.ActionCode.RoomList;
        roomListPanel = GetComponent<RoomListPanel>();
        base.Awake();
    }

    public void SendRequest()
    {
        base.SendRequest(new BaseRequest()
        {
            RequestCode = Common.RequestCode.Room,
            ActionCode = Common.ActionCode.RoomList
        });
    }

    public override void OnResponse(BaseResponse response)
    {
        roomListPanel.OnRoomListResponse(response);
    }
}
