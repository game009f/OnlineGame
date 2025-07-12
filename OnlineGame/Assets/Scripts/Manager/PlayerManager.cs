using Common.Model;
using Common.Proto.Request;
using Common.Proto.Response;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerManager : BaseManager
{
    private GameObject currentRoleGameObject;

    public Dictionary<string, RoleData> dictRoleData = new Dictionary<string, RoleData>();
    private Transform rolePositions;
    //消息队列
    public ConcurrentQueue<BaseResponse> requests = new ConcurrentQueue<BaseResponse>();
    public PlayerManager(GameFacade facade) : base(facade)
    {
    }

    public override void OnInit()
    {
        rolePositions = GameObject.Find("RolePositions").transform;
    }

    public override void Update()
    {
        if (requests.Count > 0)
        {
            HandleResponses();
        }
    }
    /// <summary>
    /// 初始化数据
    /// </summary>
    public void InitData()
    {
        //如果存在旧数据
        foreach (var v in dictRoleData)
        {
            v.Value.RoleReturn();
        }
        dictRoleData = new Dictionary<string, RoleData>();
    }

    public void EnqueueResponse(BaseResponse request)
    {
        this.requests.Enqueue(request);
    }

    private void HandleResponses()
    {
        while (requests.Count > 0)
        {
            BaseResponse response;
            requests.TryDequeue(out response);
            RoleData roleData;
            if (response is RoomCreateResponse)
            {
                RoomCreateResponse rcr = response as RoomCreateResponse;
                roleData = new RoleData(rcr.PlayerData, rolePositions, true);
                currentRoleGameObject = roleData.RoleObject;
                facade.CameraMgr.FollowRole();
                dictRoleData.Add(rcr.PlayerData.name, roleData);
            }
            else if (response is RoomJoinResponse)
            {
                RoomJoinResponse pjr = response as RoomJoinResponse;
                roleData = new RoleData(pjr.PlayerData, rolePositions, false);
                dictRoleData.Add(pjr.PlayerData.name, roleData);
            }
            else if (response is RoomQuitResponse)
            {
                RoomQuitResponse rqr = response as RoomQuitResponse;
                dictRoleData[rqr.PlayerData.name].RoleReturn();
                dictRoleData.Remove(rqr.PlayerData.name);
            }
            else if (response is RoleMoveResponse)
            {
                RoleMoveResponse rmr = response as RoleMoveResponse;
                PlayerData pd = rmr.PlayerData;
                dictRoleData[pd.name].RoleObject.transform.DOLocalMove(new Vector3(pd.x, pd.y, pd.z), 0.06f);
                dictRoleData[pd.name].RoleObject.transform.DOLocalRotate(new Vector3(pd.rotationX, pd.rotationY, pd.rotationZ), 0.06f);
                //dictRoleData[pd.name].RoleObject.transform.eulerAngles = new Vector3(pd.rotationX, pd.rotationY, pd.rotationZ);
                dictRoleData[pd.name].animator.SetFloat("Forward", pd.forward);
            }
            else if (response is RoleAttackResponse)
            {
                RoleAttackResponse rar = response as RoleAttackResponse;
                ArrowData ad = rar.ArrowData;
                Vector3 pos = new Vector3(ad.x, ad.y, ad.z);
                Quaternion rotation = new Quaternion(ad.rotationX, ad.rotationY, ad.rotationZ, ad.rotationW);
                GameObject go = ObjectPool.Get(ad.prefabName, pos, rotation) as GameObject;
                go.GetComponent<Arrow>().isLocal = true;
            }
        }
    }

    public GameObject GetCurrentRoleGameObject()
    {
        return currentRoleGameObject;
    }


    public void Shoot(string prefabName, Vector3 pos, Quaternion rotation)
    {
        facade.PlayNormalSound(AudioManager.Sound_Timer);
        GameObject go = ObjectPool.Get(prefabName, pos, rotation) as GameObject;
        go.GetComponent<Arrow>().isLocal = true;
        //发送请求
        facade.SendRequest(new RoleAttackRequest()
        {
            ArrowData = new ArrowData()
            {
                x = pos.x,
                y = pos.y,
                z = pos.z,
                rotationX = rotation.x,
                rotationY = rotation.y,
                rotationZ = rotation.z,
                rotationW = rotation.w,
                prefabName = prefabName
            }
        });
        //GameObject.Instantiate(arrowPrefab, pos, rotation).GetComponent<Arrow>().isLocal = true;
        //发送请求
        //shootRequest.SendRequest(arrowPrefab.GetComponent<Arrow>().roleType, pos, rotation.eulerAngles);
    }

    public void RemoteShoot(RoleData rt, Vector3 pos, Vector3 rotation)
    {
    }
}
