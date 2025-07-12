using Common.Proto.Response;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomQuitPanel : BasePanel
{
    public Button quitButton;
    public RoomQuitHandle RoomQuitHandle;
    
    void Start()
    {
        quitButton.onClick.AddListener(OnCloseClick);
    }

    public void OnCloseClick()
    {
        RoomQuitHandle?.SendRequest();
        uiMgr.PopPanelSync();
        facade.CameraMgr.WalkthroughScene();
        facade.PlayerMgr.InitData();
    }

    public void OnRoomQuitResponse(BaseResponse response)
    {
        RoomQuitResponse rqr = response as RoomQuitResponse;
        string msg = "玩家" + rqr.PlayerData.name + "退出房间";
        Debug.Log(msg);
        facade.ShowMessageSync(msg);
    }
}
