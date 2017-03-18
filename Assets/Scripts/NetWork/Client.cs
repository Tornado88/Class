using UnityEngine;
using System.Collections;
using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using ProtoBuf;
using System.Collections.Generic;

public class Client : IClient
{
    
    public Client(string severIP, int port)
    {
        severIPAdress = severIP;
        severPort = port;
        connectState = ConnectState.disConnected;
    }

    private ConnectState isConnected_ = ConnectState.disConnected;
    public ConnectState connectState
    {
        get
        { return isConnected_; }
        set
        { isConnected_ = value; }
    }

    private string severIPAdress_=null;
    private IPAddress severIP=null;
    public string severIPAdress
    {
        get
        { return severIPAdress_; }

        set
        {
            if (IPAddress.TryParse(value, out severIP))
            {
                severIPAdress_ = value;
            }
            else
            {
                severIPAdress_ = "";
                severIP = null;
            }
          }
    }

    private int severPort_=8800;
    public int severPort
    {
        get
        { return severPort_; }

        set
        { severPort_ = value; }
    }

    private Socket client=null;
    private TcpClient tcpClient = null;
    private NetworkStream stream = null;
    public bool ConnectingServer()
    {
        if (severIP == null )
        {
            Debug.Log("Error: ConnectingServer server IP is inilligal!");
            return false;
        }
        else
        {
            IPEndPoint remoteEP = new IPEndPoint(severIP, severPort);
            client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            client.BeginConnect(remoteEP, new AsyncCallback(ConnectedCallback), client);
            connectState = ConnectState.isConnecting;
            return true;
        }
    }

    //连接成功的回调 开线程来进行数据接收
    private void ConnectedCallback(IAsyncResult ar)
    {
        if (client.Connected == false)
        {
            connectState = ConnectState.disConnected;
            return;
        }
            
        connectState = ConnectState.connected;
        receiving = true;
        tcpClient = new TcpClient();
        tcpClient.Client = client;
        stream = tcpClient.GetStream();
        receiveThread = new Thread(RecieveBytes);
        receiveThread.Start();
    }

    public void Close()
    {
        receiving = false;
        if(client!=null)
            client.Close();
        Debug.Log("Socket Close!!");
        client = null;
        stream = null;
        connectState = ConnectState.disConnected;
        if (receiveThread!=null)
            receiveThread.Abort();
    }

    private Thread receiveThread;
    private bool receiving = false;
    private List<TransferCommand> receiveList = new List<TransferCommand>(); 
    private bool isCombineReceiveMsg = true;
    private void RecieveBytes()
    {
        List<TransferCommand> tmpList = new List<TransferCommand>();
        while(receiving)
        {
            TransferCommand command = ProtoBuf.Serializer.DeserializeWithLengthPrefix<TransferCommand>(stream, PrefixStyle.Base128);
            tmpList.Add(command);
            //如果没有在处理接收到的信息
            isCombineReceiveMsg = true;
            if (processRecvMsg == false)
            {
                receiveList.AddRange(tmpList);
                isCombineReceiveMsg = false;
                tmpList.Clear();
            }
        }
    }

    public bool ReConnectServer()
    {
        //如果连接未断开 则断开连接
        if (client != null)
            client.Close();
        connectState = ConnectState.disConnected;
        receiving = false;
        if(receiveThread!=null)
            receiveThread.Abort();
        receiveThread = null;
        return ConnectingServer();
    }

    private bool processRecvMsg = false;
    public List<TransferCommand> GetReceivedTransferCommandList()
    {
        processRecvMsg = true;
        List<TransferCommand> returnList = null;
        //如果没有在合并接收信息
        if (isCombineReceiveMsg==false)
        {
            if (receiveList.Count > 0)
            {
                returnList = receiveList;
                receiveList = new List<TransferCommand>();
            }
        }
        //不管在这个update中有没有处理信息 都要释放processRecvMsg以防互锁
        processRecvMsg = false;
        return returnList;
    }
    public TransferCommand GetReceivedTransferCommand()
    {
        processRecvMsg = true;
        TransferCommand returnObj = null;
        //如果没有在合并接收信息
        if (isCombineReceiveMsg == false)
        {
            if (receiveList.Count > 0)
            {
                returnObj = receiveList[0];
                receiveList.Remove(returnObj);
            }
        }
        //不管在这个update中有没有处理信息 都要释放processRecvMsg以防互锁
        processRecvMsg = false;
        return returnObj;
    }
    //command是null的时候不进行发送。
    public void SendData(TransferCommand command)
    {
        if (stream != null && client!=null &&client.Connected)
        {
            if (stream.CanWrite && command != null)
            {
                ProtoBuf.Serializer.SerializeWithLengthPrefix<TransferCommand>(stream, command, PrefixStyle.Base128);
            }
        }
        else if((client == null || client.Connected == false) && connectState != ConnectState.isConnecting)
        {
            connectState = ConnectState.disConnected;
            ReConnectServer();
        }
    }
}
