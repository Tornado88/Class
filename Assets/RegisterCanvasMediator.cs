using UnityEngine;
using System.Collections;
using strange.extensions.mediation.impl;
using strange.extensions.context.api;

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

    internal void OnLogedIn(bool isLogedIn)
    {
        Debug.Log("RegisterCanvasMediator OnLogedIn" + isLogedIn);
        if(isLogedIn)
        {
            view.Hide();
            OnRemove();
        }
        else
        {
            Debug.LogError("Error: cant LogedIn ");
        }
    }

    override public void OnRemove()
    {
        //去除信号
        globalLogedInSignal.RemoveListener(OnLogedIn);
        //去除菜单
    }
}
