using UnityEngine;
using System.Collections;

public class TmpHideDisplayButtion : MonoBehaviour {

    public bool isOpen;
    void Start()
    {
        if(isOpen)
        {
            transform.localScale = new Vector3(1,1,1);
        }
        else
        {
            transform.localScale = new Vector3(0, 0, 0);
        }
    }

    public void HideDisplay()
    {
        isOpen = !isOpen;
        if (isOpen)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else
        {
            transform.localScale = new Vector3(0, 0, 0);
        }
    }
}
