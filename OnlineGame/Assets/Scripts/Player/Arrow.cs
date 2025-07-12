using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public bool isLocal = false;
    public int speed = 35;
    //爆炸粒子
    public GameObject explosionEffect;
    private Rigidbody rgd;
    void Start()
    {
        rgd = GetComponent<Rigidbody>();
    }

    
    void Update()
    {
        rgd.MovePosition(transform.position + transform.forward * speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == GameFacade.Instance.PlayerMgr.GetCurrentRoleGameObject()) return;
        if (other.tag == "Player")
        {
            GameFacade.Instance.PlayNormalSound(AudioManager.Sound_ShootPerson);
            if (isLocal)
            {
                //bool playerIsLocal = other.GetComponent<PlayerInfo>().isLocal;
                //if (isLocal != playerIsLocal)
                //{
                //    //发送射中目标请求
                //    //GameFacade.Instance.SendAttack(Random.Range(10, 20));
                //}
            }
        }
        else
        {
            GameFacade.Instance.PlayNormalSound(AudioManager.Sound_Miss);
        }
        GameObject obj = ObjectPool.Get("explosionEffect", transform.position, transform.rotation) as GameObject;
        ParticleSystem ps = obj.GetComponent<ParticleSystem>();
        // ParticleSystem.MainModule mm =  ps.main;
        // mm.startColor= new ParticleSystem.MinMaxGradient(Color.blue);
        //GameObject.Instantiate(explosionEffect, transform.position, transform.rotation);
        GameObject.Destroy(this.gameObject);
    }
}
