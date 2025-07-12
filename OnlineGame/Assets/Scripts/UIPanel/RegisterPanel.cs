using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using Common.Proto.Response;

public class RegisterPanel : BasePanel
{
    private InputField usernameIF;
    private InputField passwordIF;
    private InputField rePasswordIF;
    private RegisterHandle registerHandle;

    private void Start()
    {
        registerHandle = GetComponent<RegisterHandle>();

        usernameIF = transform.Find("UsernameInput").GetComponent<InputField>();
        passwordIF = transform.Find("PasswordInput").GetComponent<InputField>();
        rePasswordIF = transform.Find("RepeatPasswordInput").GetComponent<InputField>();

        transform.Find("RegisterButton").GetComponent<Button>().onClick.AddListener(OnRegisterClick);
        transform.Find("CloseButton").GetComponent<Button>().onClick.AddListener(OnCloseClick);
    }

    private void OnRegisterClick()
    {
        PlayClickSound();
        if (string.IsNullOrEmpty(usernameIF.text))
        {
            uiMgr.ShowMessage("用户名不能为空 "); return;
        }
        if (string.IsNullOrEmpty(passwordIF.text))
        {
            uiMgr.ShowMessage("密码不能为空 "); return;
        }
        if (string.IsNullOrEmpty(rePasswordIF.text))
        {
            uiMgr.ShowMessage("重复密码不能为空 "); return;
        }
        if (!passwordIF.text.Equals(rePasswordIF.text))
        {
            uiMgr.ShowMessage("重复密码必须和密码保持一致"); return;
        }
        registerHandle.SendRequest(usernameIF.text, passwordIF.text);
    }

    public void OnRegisterResponse(BaseResponse response)
    {
        if (response.ResponseCode == Common.ResponseCode.Success)
        {
            uiMgr.PopPanelSync();
            uiMgr.ShowMessageSync("注册成功!");
        }
    }

    private void OnCloseClick()
    {
        PlayClickSound();
        uiMgr.PopPanel();
    }

    public override void EnterAnimation()
    {
        gameObject.SetActive(true);
        transform.localScale = Vector3.zero;
        transform.DOScale(1, 0.2f);
        transform.localPosition = new Vector3(1000, 0, 0);
        transform.DOLocalMove(Vector3.zero, 0.2f);
    }
    public override void HideAnimation()
    {
        transform.DOScale(0, 0.3f);
        transform.DOLocalMoveX(1000, 0.2f).OnComplete(() => gameObject.SetActive(false));
    }
}
