﻿using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System;

public class PortraitInteraction : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler,IPointerClickHandler {

    Transform popPanel = null;
    MicControl micControl = null;
	// Use this for initialization
	void Start () {
        popPanel = transform.FindChild("PopPanel");
        popPanel.localScale = Vector3.zero;
        micControl = GetComponentInChildren<MicControl>();
    }
	

    public void OnPointerEnter(PointerEventData eventData)
    {
        popPanel.localScale = Vector3.one;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        popPanel.localScale = Vector3.zero;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(eventData.clickCount == 2)
        {
            micControl.Toggle();
        }
        else if (eventData.clickCount == 1)
        {
        }
    }
}
