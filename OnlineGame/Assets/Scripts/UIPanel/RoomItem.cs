using Common.Proto.Response;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomItem : MonoBehaviour
{
    public Text idText;
    public Text roomText;
    public Button joinButton;
    public RoomInfo roomInfo;
    public UIManager uiMgr;
    public RoomListPanel roomListPanel;

    void Start()
    {
        if (joinButton != null)
        {
            joinButton.onClick.AddListener(OnJoinButtonClick);
        }
    }

    public void SetRoom(RoomInfo room)
    {
        this.roomInfo = room;
        this.idText.text = room.Id.ToString();
        roomText.text = room.Name;
    }


    void OnJoinButtonClick()
    {
        roomListPanel.OnRoomJoin(roomInfo);
    }
}
