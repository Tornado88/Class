using UnityEngine;
using System.Collections;
using strange.extensions.mediation.impl;

public class RegisterCanvasMediator : Mediator
{

    [Inject]
    public RegisterCanvasView view { get; set; }



	
	
	// Update is called once per frame
	void Update () {
	
	}

    override public void OnRegister()
    {
        view.init();
        view.logInButtonClickSignal.AddListener(OnLogIn);
    }

    internal void OnLogIn(LogInInfo li)
    {
        view.Hide();
    }

    override public void OnRemove()
    {
    }
}
