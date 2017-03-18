using UnityEngine;
using System.Collections;
using System;
public enum ConnectState
{
    connected,
    isConnecting,
    disConnected
}

public interface IClient  {

    ConnectState connectState { get; set; }

    string severIPAdress { get; set; }
    int severPort { get; set; }

    //连接服务器 需要用协程的方式来做
    bool ConnectingServer();
    bool ReConnectServer();

    void SendData(TransferCommand command);

    void RecieveBytes();
}
