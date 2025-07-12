using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MessagePanel : BasePanel
{
    public Text text;
    private float showTime = 1;
    private string message = null;

    private void Start()
    {
        uiMgr.InjectMsgPanel(this);
        text.enabled = false;
    }

    private void Update()
    {
        if (message != null)
        {
            ShowMessage(message);
            message = null;
        }
    }

    public void ShowMessageSync(string msg)
    {
        message = msg;
    }
    public void ShowMessage(string msg)
    {
        text.text = msg;
        EnterAnimation();
        Invoke("HideAnimation", showTime);
    }
   
    public override void EnterAnimation()
    {
        text.enabled = true;
        transform.SetAsLastSibling();
        text.CrossFadeAlpha(1, 0.2f, false);
    }
    public override void HideAnimation()
    {
        text.CrossFadeAlpha(0, 1, false);
    }
}
