using UnityEngine;
using System.Collections;
using strange.extensions.mediation.impl;
using UnityEngine.UI;
using strange.extensions.signal.impl;
using System.Net;

namespace CanvasUI
{
    public class RegisterCanvasView : View
    {
        [Inject]
        public DialogBoxSignal dialogBoxSignal { get; set; }

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
            if (userName == null || severIP == null || registerButton == null)
            {
                Debug.LogError("Error: cant find userNameInputField or SeverIpInputField or registerButton in RegisterCanvasView!");
            }
            registerButton.onClick.AddListener(OnRegisterButtonClick);
        }

        private void OnRegisterButtonClick()
        {
            //检测下IP的合法性
            IPAddress address;
            if (IPAddress.TryParse(severIP.text, out address))
            {
                LogInInfo li = new LogInInfo(userName.text, severIP.text);
                logInButtonClickSignal.Dispatch(li);
            }
            else
            {
                DialogBoxMsg msg = new DialogBoxMsg();
                msg.tittle = "服务器IP不正确";
                msg.msg = "请您输入正确的IP地址";
                msg.resultSignal = null;
                dialogBoxSignal.Dispatch(msg);
            }
        }


        #region 菜单的隐藏显示控制
        public bool isHide = false;  
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
}
