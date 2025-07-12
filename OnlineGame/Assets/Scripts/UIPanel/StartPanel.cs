using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartPanel : BasePanel
{
    private Button button;

    private void Start()
    {
        button = transform.Find("LoginButton").GetComponent<Button>();
        button.onClick.AddListener(OnLoginClick);
    }

    public void OnLoginClick()
    {
        PlayClickSound();
        uiMgr.PushPanel(UIPanelType.Login);
    }
}
