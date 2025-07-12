using Common.Network;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Common.Proto.Request;
using Common.Proto.Response;

public class GameFacade : MonoBehaviour
{
    UIManager UIMgr;
    public AudioManager AudioMgr;
    public PlayerManager PlayerMgr;
    public CameraManager CameraMgr;
    RequestManager RequestMgr;
    Client client;

    static GameFacade instance;
    public static GameFacade Instance
    {
        get
        {
            return instance;
        }
    }

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(this.gameObject); return;
        }
        instance = this;
    }

    void Start()
    {
        InitManager();
    }

    private void Update()
    {
        UpdateManager();
    }

    private void OnDestroy()
    {
        DestroyManager();
    }

    void InitManager()
    {
        UIMgr = new UIManager(this);
        AudioMgr = new AudioManager(this);
        PlayerMgr = new PlayerManager(this);
        CameraMgr = new CameraManager(this);
        RequestMgr = new RequestManager(this);
        UIMgr.OnInit();
        AudioMgr.OnInit();
        PlayerMgr.OnInit();
        CameraMgr.OnInit();
        RequestMgr.OnInit();

        client = new Client("127.0.0.1", 8888);
        client.MessageHandler += (msg) =>
        {
            BaseResponse rm = msg as BaseResponse;
            Debug.Log(String.Format("ActionCode:{0} ResponseCode:{1}    Content:{2}", rm.ActionCode, rm.ResponseCode, rm.Content));
            RequestMgr.OnResponese(msg);
        };
        client.Disconnect += () =>
        {
            Debug.Log("连接断开");
            //do
            //{
            //    client.TryConnect();
            //}
            //while (!client.Connected);
            //Debug.Log("重新连接成功");
            //client.Start();
        };
        client.ExceptionHandle += (e) =>
        {
            Debug.Log(e.ToString());
        };
        client.Start();
    }

    private void DestroyManager()
    {
        UIMgr.OnDestroy();
        AudioMgr.OnDestroy();
        PlayerMgr.OnDestroy();
        CameraMgr.OnDestroy();
        RequestMgr.OnDestroy();
        client.Close();
    }

    private void UpdateManager()
    {
        UIMgr.Update();
        AudioMgr.Update();
        PlayerMgr.Update();
        CameraMgr.Update();
        RequestMgr.Update();
    }

    public void AddHandle(BaseHandle baseRequest)
    {
        RequestMgr.AddHandle(baseRequest);
    }

    public void RemoveHandle(BaseHandle baseRequest)
    {
        RequestMgr.RemoveHandle(baseRequest);
    }

    public void SendRequest(BaseRequest requestMessage)
    {
        client.Send(requestMessage);
    }

    public void ShowMessage(string msg)
    {
        UIMgr.ShowMessage(msg);
    }

    public void ShowMessageSync(string msg)
    {
        UIMgr.ShowMessageSync(msg);
    }

    public void PlayBgSound(string soundName)
    {
        AudioMgr.PlayBgSound(soundName);
    }
    public void PlayNormalSound(string soundName)
    {
        AudioMgr.PlayNormalSound(soundName);
    }

}
