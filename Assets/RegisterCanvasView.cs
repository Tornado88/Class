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
        canv = GetComponent<Canvas>();
        Show();

        userName = transform.FindChild("UserNameInputField").FindChild("Text").GetComponentInChildren<Text>();
        severIP = transform.FindChild("SeverIPInputField").FindChild("Text").GetComponentInChildren<Text>();
        registerButton = transform.FindChild("RegisterButton").GetComponentInChildren<Button>();
        if (userName == null || severIP == null || registerButton==null) 
        {
            Debug.LogError("Error: cant find userNameInputField or SeverIpInputField or registerButton in RegisterCanvasView!");
        }
        registerButton.onClick.AddListener(OnRegisterButtonClick);
    }

    private void OnRegisterButtonClick()
    {
        LogInInfo li= new LogInInfo(userName.text,severIP.text);
        logInButtonClickSignal.Dispatch(li);
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
