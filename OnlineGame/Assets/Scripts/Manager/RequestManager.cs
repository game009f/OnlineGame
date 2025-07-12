using Common;
using Common.Proto.Response;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class RequestManager : BaseManager
{
    public Dictionary<ActionCode, BaseHandle> RequestDict = new Dictionary<ActionCode, BaseHandle>();

    public RequestManager(GameFacade facade) : base(facade)
    {
    }

    public void AddHandle(BaseHandle handle)
    {
        RequestDict.Add(handle.ActionCode, handle);
    }

    public void RemoveHandle(BaseHandle handle)
    {
        RequestDict.Remove(handle.ActionCode);
    }

    public void OnResponese(BaseResponse responseMessage)
    {
        if (RequestDict[responseMessage.ActionCode] == null)
        {
            Debug.Log("ActionCode:"+ responseMessage.ActionCode + "未实现");
            return;
        }
        RequestDict[responseMessage.ActionCode]?.OnResponse(responseMessage);
    }

}
