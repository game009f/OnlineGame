using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public GameObject arrowPrefab;
    private Animator anim;
    private Transform leftHandTrans;

    private Vector3 shootDir;

    void Start()
    {
        anim = GetComponent<Animator>();
        leftHandTrans = transform.Find("Bip001/Bip001 Pelvis/Bip001 Spine/Bip001 Neck/Bip001 L Clavicle/Bip001 L UpperArm/Bip001 L Forearm/Bip001 L Hand");
    }

    void Update()
    {
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Grounded") == false) return;
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            bool isCollider = Physics.Raycast(ray, out hit);
            if (isCollider)
            {
                Vector3 targetPoint = hit.point;
                targetPoint.y = transform.position.y;
                shootDir = targetPoint - transform.position;
                transform.rotation = Quaternion.LookRotation(shootDir);
                anim.SetBool("Crouch", true);
                Invoke("Shoot", 0.1f);
            }
        }
        else
        {
            anim.SetBool("Crouch", false);
        }
    }

    private void Shoot()
    {
        PlayerInfo pi = transform.GetComponent<PlayerInfo>();
        if (pi.playerData.roleType == Common.RoleType.Blue)
        {
            GameFacade.Instance.PlayerMgr.Shoot("Arrow_Blue", leftHandTrans.position, Quaternion.LookRotation(shootDir));
        }
        else
        {
            GameFacade.Instance.PlayerMgr.Shoot("Arrow_Red", leftHandTrans.position, Quaternion.LookRotation(shootDir));
        }
    }

    public void Destroy()
    {
        Destroy(this);
    }
}
