using Common;
using Common.Proto.Request;
using Common.Proto.Response;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseHandle : MonoBehaviour
{
    public ActionCode ActionCode = ActionCode.None;
    protected GameFacade facade;

    protected GameFacade Facade
    {
        get
        {
            if (facade == null)
                facade = GameFacade.Instance;
            return facade;
        }
    }

    public virtual void Awake()
    {
        Facade.AddHandle(this);
    }

    public virtual void OnDestroy()
    {
        GameFacade.Instance.RemoveHandle(this);
    }

    protected void SendRequest(BaseRequest data)
    {
        facade.SendRequest(data);
    }

    public virtual void OnResponse(BaseResponse response)
    {
        Facade.ShowMessageSync(response.Content);
    }
}
