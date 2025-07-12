using Common.Model;
using Common.Proto.Response;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomListPanel : BasePanel
{
    public Button closeButton;
    public Button createButton;
    public Button refreshButton;
    public Transform roomLayout;
    public RoomListHandle RoomListHandle;
    public RoomCreateHandle RoomCreateHandle;
    public RoomJoinHandle RoomJoinHandle;

    private List<RoomInfo> rooms = null;
    //是否需要刷新房间列表
    private bool IsRefreshRoomList = false;
    private void Start()
    {
        RoomListHandle = GetComponent<RoomListHandle>();
        RoomCreateHandle = GetComponent<RoomCreateHandle>();
        RoomJoinHandle = GetComponent<RoomJoinHandle>();
        //可以直接在unity里面拖控件赋值
        //closeButton = transform.Find("RoomList/CloseButton").GetComponent<Button>();
        closeButton.onClick.AddListener(OnCloseClick);
        createButton.onClick.AddListener(OnCreateRoom);
        refreshButton.onClick.AddListener(OnRoomList);

    }

    public override void OnEnter()
    {
        base.OnEnter();
        OnRoomList();
    }

    public override void OnResume()
    {
        base.OnResume();
        OnRoomList();
    }

    public void Update()
    {
        if (IsRefreshRoomList == true)
        {
            IsRefreshRoomList = false;
            foreach (Transform child in roomLayout.transform)
            {
                ObjectPool.Return(child.gameObject);
            }
            if (rooms != null)
            {
                foreach (var item in rooms)
                {
                    AddRoomItem(item);
                }
            }
        }
    }

    public void AddRoomItem(RoomInfo roomInfo)
    {
        GameObject instPanel = ObjectPool.Get("RoomItem", transform.position, transform.rotation) as GameObject;
        instPanel.transform.SetParent(roomLayout, false);
        instPanel.transform.localScale = Vector3.one;
        RoomItem ri = instPanel.GetComponent<RoomItem>();
        ri.roomListPanel = this;
        ri.uiMgr = uiMgr;
        ri.SetRoom(roomInfo);
    }

    public void OnRoomList()
    {
        PlayClickSound();
        if (RoomListHandle == null)
            RoomListHandle = GetComponent<RoomListHandle>();
        RoomListHandle.SendRequest();
    }

    public void OnRoomListResponse(BaseResponse response)
    {
        RoomListResponse response1 = response as RoomListResponse;
        rooms = response1.RoomList;
        IsRefreshRoomList = true;
    }

    public void OnCreateRoom()
    {
        RoomCreateHandle.SendRequest();
    }

    public void OnCreateRoomResponse(BaseResponse response)
    {
        RoomCreateResponse roomCreateResponse = response as RoomCreateResponse;
        //进入游戏
        uiMgr.PushPanelSync(UIPanelType.RoomQuit);
        PlayerData data = roomCreateResponse.PlayerData;
        Debug.Log(string.Format("房主{0} 初始位置位置{1}：{2}：{3} 类型：{4}", data.name, data.x, data.y, data.z, data.roleType.ToString()));

        facade.ShowMessageSync(string.Format("房主{0} 初始位置位置{1}：{2}：{3} 类型：{4}", data.name, data.x, data.y, data.z, data.roleType.ToString()));
    }

    public void OnRoomJoin(RoomInfo roomInfo)
    {
        RoomJoinHandle.SendRequest(roomInfo);
    }

    public void OnRoomJoinResponse(BaseResponse response)
    {
        if (response.ResponseCode == Common.ResponseCode.Success)
        {
            RoomJoinResponse jr = response as RoomJoinResponse;
            PlayerData data = jr.PlayerData;
            Debug.Log("玩家" + jr.PlayerData.name + "加入房间" + data.roleType.ToString());
            facade.ShowMessageSync("玩家" + jr.PlayerData.name + "加入房间" + data.roleType.ToString());
        }
        else
        {
            facade.ShowMessageSync(response.Content);
        }
    }

    public void OnSelfRoomJoinResponse(BaseResponse response)
    {
        //进入游戏
        uiMgr.PushPanelSync(UIPanelType.RoomQuit);
        Debug.Log(response.Content);
    }

    public void OnCloseClick()
    {
        PlayClickSound();
        uiMgr.PopPanelSync();
    }

    public override void EnterAnimation()
    {
        transform.localScale = Vector3.zero;
        transform.DOScale(1, 0.2f);
        transform.localPosition = new Vector3(-1000, 0, 0);
        transform.DOLocalMove(Vector3.zero, 0.2f);
    }
    public override void HideAnimation()
    {
        transform.DOScale(0, 0.3f);
        //transform.DOLocalMoveX(1000, 0.2f).OnComplete(() => gameObject.SetActive(false));
        transform.DOLocalMoveX(-1000, 0.2f);
    }
}
