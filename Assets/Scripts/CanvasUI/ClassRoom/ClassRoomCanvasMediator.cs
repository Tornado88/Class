using UnityEngine;
using System.Collections;
using strange.extensions.mediation.impl;

namespace CanvasUI
{
    public class ClassRoomCanvasMediator : Mediator
    {
        [Inject]
        public LogedInSignal globalLogedInSignal { get; set; }

        [Inject]
        public ClassRoomCanvasView view { get; set; }

        override public void OnRegister()
        {
            view.init();
            globalLogedInSignal.AddListener(OnLogedIn);
        }

        private void OnLogedIn(LogInResult lir)
        {
            if (lir.isLogedIn)
            {
                view.Show();
            } 
           
        }

        override public void OnRemove()
        {
        }


    }
}
