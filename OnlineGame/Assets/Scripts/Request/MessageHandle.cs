using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;
using Common.Proto.Response;

public class MessageHandle : BaseHandle
{
    private MessagePanel messagePanel;
    public override void Awake()
    {
        ActionCode = ActionCode.Message;
        messagePanel = GetComponent<MessagePanel>();
        base.Awake();
    }

    public override void OnResponse(BaseResponse response)
    {
        messagePanel.ShowMessageSync(response.Content);
    }
}
