using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

//遍历场景中所有物体的尝试：有两种方法1、按照深度搜索遍历所有的物体。2、从scene中最顶层的物体中提取同一类型的component。选第二种方法虽然空间复杂度大一些，但是时间复杂度小。
public class IterateAllObjects : MonoBehaviour {

	// Use this for initialization
	void Start () {
        List<GameObject> rootObjects = new List<GameObject>();
        Scene scene = SceneManager.GetActiveScene();
        scene.GetRootGameObjects(rootObjects);
        
        Debug.Log("root GameObj Num:"+rootObjects.Count);
        int num = 0;
        for (int i = 0; i < rootObjects.Count; ++i)
        {
            num += rootObjects[i].GetComponentsInChildren<Transform>().GetLength(0);
            IteratePrintObjects(rootObjects[i], "root");
        }
        print("GameObj Num:" + num);
    }

    void IteratePrintObjects(GameObject go, string prefixStr)
    {
        prefixStr = prefixStr + " :: " + go.name;
        Debug.Log(prefixStr);
        for (int j = 0; j < go.transform.childCount; j++)
        {
            IteratePrintObjects(go.transform.GetChild(j).gameObject, prefixStr);
        }
    }
	

}
