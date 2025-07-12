using Common.Proto.Request;
using Common.Proto.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class RoomCreateHandle : BaseHandle
{
    public RoomListPanel roomListPanel;
    public override void Awake()
    {
        ActionCode = Common.ActionCode.RoomCreate;
        roomListPanel = GetComponent<RoomListPanel>();
        base.Awake();
    }

    public void SendRequest()
    {
        base.SendRequest(new BaseRequest()
        {
            RequestCode = Common.RequestCode.Room,
            ActionCode = Common.ActionCode.RoomCreate
        });
    }

    public override void OnResponse(BaseResponse response)
    {
        roomListPanel.OnCreateRoomResponse(response);
        facade.PlayerMgr.EnqueueResponse(response);
    }
}

