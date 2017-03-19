using UnityEngine;
using System.Collections;
using strange.extensions.mediation.impl;

namespace CanvasUI
{
    public class UIMediator : Mediator
    {
        [Inject]
        public UIView view { get; set; }


        public override void OnRegister()
        {
            view.init();
        }

        public override void OnRemove()
        {
            base.OnRemove();
        }

        private void OnUserLogIn()
        {

        }

        private void OnSwitchProcedure(int signalID, string parameter)
        {

        }

        private void OnClassRoomOperate(int signalID, string parameter)
        {

        }




    }
}