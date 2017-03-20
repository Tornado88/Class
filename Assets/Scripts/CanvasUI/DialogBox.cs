using UnityEngine;
using System.Collections;
using strange.extensions.mediation.impl;
using System.Collections.Generic;

namespace CanvasUI
{
    public class DialogBox : View
    {
        [Inject]
        public DialogBoxSignal dialogBoxSignal { get; set; }
        //在此类内部设置窗口请求类的队列。然后依次处理
        public List<DialogBoxMsg> dialogList=null;


        protected override void Start()
        {
            dialogList = null;
            dialogBoxSignal.AddListener(OnDialogBoxSignal);
        }


        private void OnDialogBoxSignal(DialogBoxMsg msg)
        {
            if (dialogList == null)
                dialogList = new List<DialogBoxMsg>();

            dialogList.Add(msg);
        }


        #region

        private Rect windowRect = new Rect((Screen.width - 200) / 2+200, (Screen.height - 100) / 2, 200, 110);
        //private bool show = false;

        void OnGUI()
        {
            if (dialogList!=null && dialogList.Count>0)
            {
                windowRect = GUI.Window(0, windowRect, DialogWindow, dialogList[0].tittle);//"Game Over"
            }
        }

        void DialogWindow(int windowID)
        {
            float y = 20;
            float x = 5;
            GUI.Label(new Rect(x, y, windowRect.width, 60), dialogList[0].msg);
            y += 60;

            //并排的3个按钮 
            x = 5;
            if (GUI.Button(new Rect(x, y, (windowRect.width - 10) / 3 - 5, 20), "Yes"))
            {
                if (dialogList[0].resultSignal != null)
                {
                    DialogBoxResult dbr = new DialogBoxResult(DialogBoxResult.OperateStyle.Yes);
                    dialogList[0].resultSignal.Dispatch(dbr);
                }
                dialogList.RemoveAt(0);
            }
            x += (windowRect.width - 10) / 3 - 5 + 5;

            if (GUI.Button(new Rect(x, y, (windowRect.width - 10) / 3 - 5, 20), "No"))
            {
                if (dialogList[0].resultSignal != null)
                {
                    DialogBoxResult dbr = new DialogBoxResult(DialogBoxResult.OperateStyle.No);
                    dialogList[0].resultSignal.Dispatch(dbr);
                }
                dialogList.RemoveAt(0);
            }
            x += (windowRect.width - 10) / 3 - 5 + 5;

            if (GUI.Button(new Rect(x, y, (windowRect.width - 10) / 3 - 5, 20), "Cancel"))
            {
                if (dialogList[0].resultSignal != null)
                {
                    DialogBoxResult dbr = new DialogBoxResult(DialogBoxResult.OperateStyle.Cancel);
                    dialogList[0].resultSignal.Dispatch(dbr);
                }
                dialogList.RemoveAt(0);
            }
        }


        #endregion
    }
}