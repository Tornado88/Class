using UnityEngine;
using System.Collections;
using strange.extensions.signal.impl;
using strange.extensions.command.impl;
using strange.extensions.context.api;
using strange.extensions.injector.api;
using strange.extensions.command.api;

public class GlobalSignalManager
{
    static public void GlobalSignalBinder(ICrossContextInjectionBinder injectionBinder, ICommandBinder commandBinder)
    {
        //将公用的信号使用单例模式。 
        injectionBinder.Bind<LogInSignal>().ToSingleton();
        injectionBinder.Bind<LogedInSignal>().ToSingleton();
        injectionBinder.Bind<LogOutSignal>().ToSingleton();
        injectionBinder.Bind<ProcedureSignal>().ToSingleton();
        injectionBinder.Bind<MicControlSignal>().ToSingleton();

    }
}


#region UIsingal
public class LogInSignal : Signal<LogInInfo> {}
public class LogedInSignal : Signal<LogInResult> { }
public class LogOutSignal : Signal {}
public class ProcedureSignal : Signal<ProcedureInfo> { }
public class MicControlSignal : Signal<MicControlInfo> { }



public class LogInResult
{
    public bool isConnected;
    public bool isLogedIn;
    public string msg;
    public TransferCommand.UserStyle userStyle;
}

public class LogInInfo
{
    public LogInInfo(string name1,string severIP)
    {
        name = name1;
        severIpAddress = severIP;
    }
    public string name;
    public string severIpAddress;
}
public class ProcedureInfo
{
    public int thing;
}

public class MicControlInfo
{
    public int thing;
}


#endregion





