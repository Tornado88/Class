using UnityEngine;
using System.Collections;
using System;

public class CameraAspect : MonoBehaviour{


    float targetaspect = 16.0f / 9.0f;//16.0f / 9.0f;
    float windowaspect;
    float scaleheight;
    Camera mainCam =null;

    void Start()
    {
        windowaspect = (float)Screen.width / (float)Screen.height;
        scaleheight = windowaspect / targetaspect;
        mainCam = GetComponent<Camera>();

        // if scaled height is less than current height, add letterbox
        if (scaleheight < 1.0f)
        {
            Rect rect = mainCam.rect;

            rect.width = 1.0f;
            rect.height = scaleheight;
            rect.x = 0;
            rect.y = (1.0f - scaleheight) / 2.0f;

            mainCam.rect = rect;
        }
        else // add pillarbox
        {
            float scalewidth = 1.0f / scaleheight;

            Rect rect = mainCam.rect;

            rect.width = scalewidth;
            rect.height = 1.0f;
            rect.x = (1.0f - scalewidth) / 2.0f;
            rect.y = 0;

            mainCam.rect = rect;
        }
    }

    // Update is called once per frame
    void Update () {
        //todo 后面考虑将修改长宽比的功能放到 couroutine中每0.5s执行一次
        windowaspect = (float)Screen.width / (float)Screen.height;
        scaleheight = windowaspect / targetaspect;

        if (scaleheight < 1.0f)
        {
            Rect rect = mainCam.rect;

            rect.width = 1.0f;
            rect.height = scaleheight;
            rect.x = 0;
            rect.y = (1.0f - scaleheight) / 2.0f;

            mainCam.rect = rect;
        }
        else
        {
            float scalewidth = 1.0f / scaleheight;

            Rect rect = mainCam.rect;
            rect.width = scalewidth;
            rect.height = 1.0f;
            rect.x = (1.0f - scalewidth) / 2.0f;
            rect.y = 0;

            mainCam.rect = rect;
        }
    }


}
