using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using Common.Proto.Request;
using Common.Proto.Response;

public class LoginPanel : BasePanel
{
    private Button closeButton;
    private Button loginButton;
    private Button registerButton;
    private InputField usernameIF;
    private InputField passwordIF;
    private LoginHandle loginRequest;
    private void Start()
    {
        loginRequest = GetComponent<LoginHandle>();
        closeButton = transform.Find("CloseButton").GetComponent<Button>();
        closeButton.onClick.AddListener(OnCloseClick);
        loginButton = transform.Find("LoginButton").GetComponent<Button>();
        loginButton.onClick.AddListener(OnLoginClick);
        registerButton = transform.Find("RegisterButton").GetComponent<Button>();
        registerButton.onClick.AddListener(OnRegisterClick);

        usernameIF = transform.Find("UsernameInput").GetComponent<InputField>();
        passwordIF = transform.Find("PasswordInput").GetComponent<InputField>();
    }

    public void OnCloseClick()
    {
        PlayClickSound();
        uiMgr.PopPanel();
    }

    public void OnLoginClick()
    {
        PlayClickSound();
        string msg = "";
        if (string.IsNullOrEmpty(usernameIF.text))
        {
            msg += "用户名不能为空 ";
        }
        if (string.IsNullOrEmpty(passwordIF.text))
        {
            msg += "密码不能为空 ";
        }
        if (msg != "")
        {
            uiMgr.ShowMessage(msg); return;
        }
        loginRequest.SendRequest(usernameIF.text, passwordIF.text);
    }

    public void OnLoginResponse(BaseResponse response)
    {
        if(response.ResponseCode == Common.ResponseCode.Success)
            uiMgr.PushPanelSync(UIPanelType.RoomList);
    }

    public void OnRegisterClick()
    {
        PlayClickSound();
        uiMgr.PushPanel(UIPanelType.Regsiter);
    }

    public override void EnterAnimation()
    {
        transform.localScale = Vector3.zero;
        transform.DOScale(1, 0.2f);
        transform.localPosition = new Vector3(1000, 0, 0);
        transform.DOLocalMove(Vector3.zero, 0.2f);
    }
    public override void HideAnimation()
    {
        transform.DOScale(0, 0.3f);
        //transform.DOLocalMoveX(1000, 0.2f).OnComplete(() => gameObject.SetActive(false));
        transform.DOLocalMoveX(1000, 0.2f);
    }
}
