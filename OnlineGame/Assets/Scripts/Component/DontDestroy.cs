using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 防止对象被销毁
/// </summary>
public class DontDestroy : MonoBehaviour
{
    void Start()
    {
        DontDestroyOnLoad(transform.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
