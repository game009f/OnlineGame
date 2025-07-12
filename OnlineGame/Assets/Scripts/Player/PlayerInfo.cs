using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common.Proto.Request;
using Common.Proto.Response;
using Common.Model;

public class PlayerInfo : MonoBehaviour
{
    public PlayerData playerData;
    public bool isLocal;

    public void Destroy()
    {
        Destroy(this);
    }
}
