using UnityEngine;
using System.Collections;
using strange.extensions.mediation.impl;
using strange.extensions.context.api;
using UnityEditor;

public class RegisterCanvasMediator : Mediator
{

    [Inject]
    public RegisterCanvasView view { get; set; }
    [Inject]
    public LogInSignal globalLogInSignal { get; set; }
    [Inject]
    public LogedInSignal globalLogedInSignal { get; set; }


    override public void OnRegister()
    {
        view.init();
        view.logInButtonClickSignal.AddListener(OnLogIn);
    }

    internal void OnLogIn(LogInInfo li)
    {
        //Debug.Log("OnLogIn: " + li.name + ":" + li.severIpAddress);
        globalLogedInSignal.AddListener(OnLogedIn);
        globalLogInSignal.Dispatch(li);
        
    }

    internal void OnLogedIn(LogInResult lir)
    {
        //Debug.Log("RegisterCanvasMediator OnLogedIn" + isLogedIn);
        if(lir.isLogedIn && lir.isConnected)//登录成功
        {
            view.Hide();
            OnRemove();
        }
        else if(lir.isConnected)//连接成功
        {
            EditorUtility.DisplayDialog("用户名称不正确", "请您输入正确的用户名，或联系管理员", "OK", "Cancel");
        }
        else//连接不成功
        {
            Debug.LogError("Error: cant LogedIn ");
            //后面在此弹出一个提示窗口
            EditorUtility.DisplayDialog("服务器IP不正确", "请您输入正确服务器IP，或联系管理员", "OK", "Cancel");
        }
    }

    override public void OnRemove()
    {
        //去除信号
        globalLogedInSignal.RemoveListener(OnLogedIn);
        //去除菜单
    }
}
