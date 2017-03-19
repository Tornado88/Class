using UnityEngine;
using System.Collections;
using strange.extensions.mediation.impl;

namespace CanvasUI
{
    public class ProcedureCanvasView : View
    {


        public void init()
        {
            canv = GetComponent<Canvas>();
            Hide();
        }

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

    }
}
