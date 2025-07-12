using Common;
using Common.Model;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class RoleData
{
    public bool isLocal = false;
    public GameObject RoleObject { get; private set; }
    public Animator animator;
    public RoleData(PlayerData playerData, Transform spawnPos, bool isLocal)
    {
        if (playerData.roleType == RoleType.Blue)
            RoleObject = ObjectPool.Get("Hunter_BLUE", spawnPos.position, spawnPos.rotation) as GameObject;
        else
            RoleObject = ObjectPool.Get("Hunter_RED", spawnPos.position, spawnPos.rotation) as GameObject;
        this.isLocal = isLocal;
        if (isLocal)
        {
            PlayerMove rm = RoleObject.AddComponent<PlayerMove>();
            PlayerAttack ra = RoleObject.AddComponent<PlayerAttack>();
            PlayerInfo ri = RoleObject.AddComponent<PlayerInfo>();
            ri.isLocal = isLocal;
            ri.playerData = playerData;
            RoleMoveHandle rmh = RoleObject.AddComponent<RoleMoveHandle>();
            rmh.LocalRoleTransform = RoleObject.transform;
            rmh.playerMove = rm;
            rmh.isReady = true;
            RoleAttackHandle rah = RoleObject.AddComponent<RoleAttackHandle>();
        }
        animator = RoleObject.GetComponent<Animator>();
    }

    /// <summary>
    ///回收角色对象 
    /// </summary>
    public void RoleReturn()
    {
        if (isLocal)
        {
            RoleObject.GetComponent<PlayerMove>().Destroy();
            RoleObject.GetComponent<PlayerAttack>().Destroy();
            RoleObject.GetComponent<PlayerInfo>().Destroy();
            RoleObject.GetComponent<RoleMoveHandle>().Destroy();
            RoleObject.GetComponent<RoleAttackHandle>().Destroy();
        }
        ObjectPool.Return(RoleObject);
    }
}
