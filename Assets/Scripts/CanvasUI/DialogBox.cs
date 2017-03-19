using UnityEngine;
using System.Collections;
namespace CanvasUI
{
    public class DialogBox : MonoBehaviour
    {
        string tittle="haha";
        string msg="msg";
        string button1="sdf";
        string button2;
        string button3;

        private void Start()
        {
            show = false;
        }

        private Rect windowRect = new Rect((Screen.width - 200) / 2, (Screen.height - 300) / 2, 200, 300);
        private bool show = false;

        void OnGUI()
        {
            if (show)
            {
                windowRect = GUI.Window(0, windowRect, DialogWindow, tittle);//"Game Over"
            }
        }

        void DialogWindow(int windowID)
        {
            float y = 20;
            GUI.Label(new Rect(5, y, windowRect.width, 20), msg);//"Again?"

            if (button1 != null && button1 != "")
            {
                y += 20;
                if (GUI.Button(new Rect(5, y, windowRect.width - 10, 20), button1))//"Restart"
                {
                    show = false;
                }
            }

            if (button2 != null && button2 != "")
            {
                y += 20;
                if (GUI.Button(new Rect(5, y, windowRect.width - 10, 20), button2))
                {
                    show = false;
                }
            }

            if (button3 != null && button3 != "")
            {
                y += 20;
                if (GUI.Button(new Rect(5, y, windowRect.width - 10, 20), button3))
                {
                    show = false;
                }
            }
        }

    }
}