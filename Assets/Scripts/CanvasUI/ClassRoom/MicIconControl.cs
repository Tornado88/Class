using UnityEngine;
using System.Collections;

namespace CanvasUI
{
    public class MicIconControl : MonoBehaviour
    {

        Transform forbidden = null;
        void Start()
        {
            forbidden = transform.FindChild("Forbidden");
            forbidden.localScale = Vector3.one;
        }

        bool isUse = false;
        public void Toggle()
        {
            if (isUse)
            {
                ForbiddenMic();
            }
            else
            {
                UseMic();
            }
        }

        public void UseMic()
        {
            forbidden.localScale = Vector3.zero;
            isUse = true;
        }

        public void ForbiddenMic()
        {
            forbidden.localScale = Vector3.one;
            isUse = false;
        }
    }
}
