using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float forward = 0;
    private float speed = 3;
    private Animator anim;
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    float h, v;
    void Update()
    {
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Grounded") == false) return;

        h = Input.GetAxis("Horizontal");
        v = Input.GetAxis("Vertical");

        if (Mathf.Abs(h) > 0 || Mathf.Abs(v) > 0)
        {
            transform.Translate(new Vector3(h, 0, v) * speed * Time.deltaTime, Space.World);
            transform.rotation = Quaternion.LookRotation(new Vector3(h, 0, v));
            forward = Mathf.Max(Mathf.Abs(h), Mathf.Abs(v));
            anim.SetFloat("Forward", forward);
        }
    }

    public void Destroy()
    {
        Destroy(this);
    }
}
