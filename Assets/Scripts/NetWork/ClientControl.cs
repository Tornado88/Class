using UnityEngine;
using System.Collections;
using System.Text;
using System.Collections.Generic;
using strange.extensions.mediation.impl;

public class ClientControl : View
{
    [Inject]
    public LogInSignal globalLogInSignal { get; set; }
    [Inject]
    public LogedInSignal globalLogedInSignal { get; set; }

    Client client=null;
    public void OnFoundClient(LogInInfo logInfo)
    {
        //如果上次连接过 但是没有成功 则关闭client重新建立
        if(client!=null)
        {
            client.Close();
            client = null;
        }

        client = new Client(logInfo.severIpAddress, 11000);//"127.0.0.1", 11000
        client.ConnectingServer();
        //打开一个协程检视client的链接状态
        StartCoroutine("WaitClientConnected");
        //如果连接没成功则返回false
        if(client.connectState!=ConnectState.connected)
        {
            client.Close();//关闭client连接
            LogInResult lir = new LogInResult();
            lir.isLogedIn = lir.isConnected = false;
            globalLogedInSignal.Dispatch(lir);
        }
        //如果连接超时 或者连接成功触发 LogedInSignal
    }

    private IEnumerator WaitClientConnected()
    {
        for (int i=0;i<10;i++)
        {
            if (client.connectState != ConnectState.isConnecting)
                yield return new WaitForSeconds(0.15f);
        }
    }


    void OnApplicationQuit()
    {
        client.Close();
    }



    private void Start()
    {
        globalLogInSignal.AddListener(OnFoundClient);
    }

    // Update is called once per frame
    void Update_depracated()
    {
        if (client.connectState == ConnectState.connected)
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
                Debug.Log("ObjID position:" + c.threeDInfo.position.x + "," + c.threeDInfo.position.y + "," + c.threeDInfo.position.z);
                Debug.Log("ObjID rotation:" + c.threeDInfo.rotation.x + "," + c.threeDInfo.rotation.y + "," + c.threeDInfo.rotation.z);
                Debug.Log("ObjID scale:" + c.threeDInfo.scale.x + "," + c.threeDInfo.scale.y + "," + c.threeDInfo.scale.z);
                Debug.Log("ObjID move:" + c.moveStyle.ToString());
                Debug.Log("ObjID actionStyle:" + c.interaction.actionStyle.ToString());
                Debug.Log("ObjID actionObj:" + c.interaction.interactObjID);
            }
        }
        else if (client.connectState == ConnectState.disConnected)
        {
            client.ReConnectServer();
        }
    }
}
