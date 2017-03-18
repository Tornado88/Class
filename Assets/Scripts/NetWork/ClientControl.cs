using UnityEngine;
using System.Collections;
using System.Text;
using System.Collections.Generic;

public class ClientControl : MonoBehaviour
{

    Client client;
    void Start()
    {
        client = new Client("127.0.0.1", 11000);
        client.ConnectingServer();
            
    }

    // Update is called once per frame
    void Update()
    {
        if ( client.connectState==ConnectState.connected)
        {
            //TransferCommand command=new TransferCommand();
            //command.userID = 114;
            //command.ObjID = 564;
            //command.moveStyle = TransferCommand.MoveStyle.GoRight;
            //command.interaction = new Interaction(Interaction.ActionStyle.like, 334);
            //command.threeDInfo = new ThreeDInfo(new Vector3(0.1f, 0.2f, 0.3f), new Vector3(0.4f, 0.5f, 0.6f), new Vector3(0.7f, 0.8f, 0.9f));
            //client.SendData(command);

            List<TransferCommand> clist = client.GetReceivedTransferCommand();
            if (clist == null)
                return;
            Debug.Log("receiveCommandNum:" + clist.Count);
            foreach (TransferCommand c in clist)
            {
                Debug.Log("userID:" + c.userID);
                Debug.Log("ObjID:" + c.ObjID);
                Debug.Log("ObjID position:" + c.threeDInfo.position.x +","+ c.threeDInfo.position.y + "," + c.threeDInfo.position.z  );
                Debug.Log("ObjID rotation:" + c.threeDInfo.rotation.x +","+ c.threeDInfo.rotation.y + "," + c.threeDInfo.rotation.z  );
                Debug.Log("ObjID scale:" + c.threeDInfo.scale.x + "," + c.threeDInfo.scale.y + "," + c.threeDInfo.scale.z);
                Debug.Log("ObjID move:" + c.moveStyle.ToString());
                Debug.Log("ObjID actionStyle:" + c.interaction.actionStyle.ToString());
                Debug.Log("ObjID actionObj:" + c.interaction.interactObjID);
            }
        }
        else if ( client.connectState == ConnectState.disConnected)
        {
            client.ReConnectServer();
        }
    }

    void OnApplicationQuit()
    {
        client.Close();
    }
}
