using UnityEngine;
using System.Collections;
using strange.extensions.mediation.impl;

namespace CanvasUI
{
    public class ProcedureCanvasMediator : Mediator
    {
        [Inject]
        public ProcedureCanvasView view { get; set; }
        [Inject]
        public LogedInSignal globalLogedInSignal { get; set; }

        override public void OnRegister()
        {
            view.init();
            globalLogedInSignal.AddListener(OnLogedIn);
            
        }

        private void OnLogedIn(LogInResult lir)
        {
            if(lir.isLogedIn)
            {
                if (lir.userStyle == TransferCommand.UserStyle.Teacher)
                {
                    view.Show();
                }
                else
                {
                    view.Hide();
                }
            }
        }

        override public void OnRemove()
        {
        }


    }
}

