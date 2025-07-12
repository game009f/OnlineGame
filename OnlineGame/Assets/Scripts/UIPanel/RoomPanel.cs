using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomPanel : BasePanel
{
    public Button ReadyButton;
    public Button CloseButton;

    public void Start()
    {
        CloseButton.onClick.AddListener(OnCloseClick);
        ReadyButton.onClick.AddListener(OnReadyClick);
    }

    public override void OnEnter()
    {
        base.OnEnter();
    }

    public void OnCloseClick()
    {
        PlayClickSound();
        uiMgr.PopPanel();
    }

    public void OnReadyClick()
    { 
    }
}
