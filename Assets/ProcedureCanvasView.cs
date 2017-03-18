using UnityEngine;
using System.Collections;
using strange.extensions.mediation.impl;

public class ProcedureCanvasView : View
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

	
	// Update is called once per frame
	void Update () {
	
	}
}
