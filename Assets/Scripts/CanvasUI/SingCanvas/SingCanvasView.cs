using UnityEngine;
using System.Collections;
using strange.extensions.mediation.impl;

namespace CanvasUI
{
    public class SingCanvasView : View
    {

        bool isHide = false;
        Canvas canv;
        private void Start()
        {
            canv = GetComponent<Canvas>();
            Hide();
        }

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

        public void init()
        {

        }
    }
}
