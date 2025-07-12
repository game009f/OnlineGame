using UnityEngine;
using System.Collections;

public class BasePanel : MonoBehaviour
{
    protected UIManager uiMgr;
    protected GameFacade facade;
    public UIManager UIMgr
    {
        set { uiMgr = value; }
    }
    public GameFacade Facade
    {
        set { facade = value; }
    }

    protected void PlayClickSound()
    {
        facade.PlayNormalSound(AudioManager.Sound_ButtonClick);
    }

    /// <summary>
    /// 界面被显示出来
    /// </summary>
    public virtual void OnEnter()
    {
        EnterAnimation();
    }

    /// <summary>
    /// 界面暂停
    /// </summary>
    public virtual void OnPause()
    {
        HideAnimation();
    }

    /// <summary>
    /// 界面继续
    /// </summary>
    public virtual void OnResume()
    {
        EnterAnimation();
    }

    /// <summary>
    /// 界面不显示,退出这个界面，界面被关闭
    /// </summary>
    public virtual void OnExit()
    {
        HideAnimation();
    }

    /// <summary>
    /// 界面展示动画
    /// </summary>
    public virtual void EnterAnimation()
    {
        gameObject.SetActive(true);
    }

    /// <summary>
    /// 界面隐藏动画
    /// </summary>
    public virtual void HideAnimation()
    {
        gameObject.SetActive(false);
    }
}
