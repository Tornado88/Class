using UnityEngine;
using System.Collections;
using strange.extensions.mediation.impl;
using strange.extensions.context.api;

namespace CanvasUI
{
    public class RegisterCanvasMediator : Mediator
    {

        [Inject]
        public RegisterCanvasView view { get; set; }
        [Inject]
        public LogInSignal globalLogInSignal { get; set; }
        [Inject]
        public LogedInSignal globalLogedInSignal { get; set; }
        [Inject]
        public DialogBoxSignal dialogBoxSignal { get; set; }


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
            if (lir.isLogedIn && lir.isConnected)//登录成功
            {
                view.Hide();
                OnRemove();
            }
            else if (lir.isConnected)//连接成功
            {
                DialogBoxMsg msg = new DialogBoxMsg();
                msg.tittle = "用户名称不存在";
                msg.msg = "请您输入正确的用户名，或联系管理员";
                dialogBoxSignal.Dispatch(msg);
            }
            else//连接不成功 
            {
                DialogBoxMsg msg = new DialogBoxMsg();
                msg.tittle = "服务器IP不存在";
                msg.msg = "请您输入正确服务器IP，或联系管理员";
                msg.resultSignal = new DialogBoxResultSignal();
                msg.resultSignal.AddListener(OnDialogBoxTest);
                dialogBoxSignal.Dispatch(msg);
            }
        }

        private void OnDialogBoxTest(DialogBoxResult lbr)
        {
            Debug.Log("Info: DialogBox result:" + lbr.operateStyle.ToString());
        }

        override public void OnRemove()
        {
            //去除信号
            globalLogedInSignal.RemoveListener(OnLogedIn);
            //去除菜单
        }

    }
}
