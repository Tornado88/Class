using UnityEngine;
using System.Collections;
using strange.extensions.mediation.impl;
using UnityEngine.UI;
using strange.extensions.signal.impl;

public class RegisterCanvasView : View
{
    public Signal<LogInInfo> logInButtonClickSignal = new Signal<LogInInfo>();

    Text userName;
    Text severIP;
    Button registerButton;

    internal void init()
    {
        Start();
        userName = transform.FindChild("UserNameInputField").GetComponentInChildren<Text>();
        severIP = transform.FindChild("SeverIPInputField").GetComponentInChildren<Text>();
        registerButton = transform.FindChild("RegisterButton").GetComponentInChildren<Button>();
        if (userName == null || severIP == null || registerButton==null) 
        {
            Debug.LogError("Error: cant find userNameInputField or SeverIpInputField or registerButton in RegisterCanvasView!");
        }
        registerButton.onClick.AddListener(OnRegisterButtonClick);
    }

    private void OnRegisterButtonClick()
    {
        LogInInfo li=null;
        logInButtonClickSignal.Dispatch(li);
    }

    void Start()
    {
        canv = GetComponent<Canvas>();
        Show();
    }

    #region 菜单的隐藏显示控制
    bool isHide = false;
    Canvas canv;
    public void Hide()
    {
        canv.enabled = false;
        isHide = true;
    }

    public void Show()
    {
        canv.enabled = true;
        isHide = false;
    }

    #endregion


}
