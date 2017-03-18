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
        //对于某些公用的信号使用单例模式。 
        injectionBinder.Bind<LogInSignal>().ToSingleton();
        injectionBinder.Bind<LogedInSignal>().ToSingleton();
        injectionBinder.Bind<LogOutSignal>().ToSingleton();
        injectionBinder.Bind<ProcedureSignal>().ToSingleton();
        injectionBinder.Bind<MicControlSignal>().ToSingleton();

        //将公用信号都设置成单例模式
        commandBinder.Bind<LogInSignal>().To<LogInCommand>();
        commandBinder.Bind<LogedInSignal>().To<LogedInCommand>();
        commandBinder.Bind<LogOutSignal>().To<LogOutCommand>();
        commandBinder.Bind<ProcedureSignal>().To<ProcedureCommand>();
        commandBinder.Bind<MicControlSignal>().To<MicControlCommand>();
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

public class LogInCommand :Command
{
    [Inject(ContextKeys.CONTEXT_VIEW)]
    public GameObject contextView { get; set; }

    [Inject]
    public LogInInfo logInInfo { get; set; }
    [Inject]//(ContextKeys.CONTEXT)
    public LogedInSignal logedInSignal { get; set; }

    public override void Execute()
    {
        Debug.Log("LogInCommand " + logInInfo.name + ":" + logInInfo.severIpAddress);
        //运行实际的登录程序 并触发登录成功信号
        //logedInSignal.Dispatch(false);
    }
}

public class LogedInCommand : Command
{
    [Inject(ContextKeys.CONTEXT_VIEW)]
    public GameObject contextView { get; set; }
    [Inject]
    public bool isLogedIn {get; set;}

    public override void Execute()
    {
        Debug.Log("LogedInCommand:" + isLogedIn);

    }

}

public class LogOutCommand : Command
{
    [Inject(ContextKeys.CONTEXT_VIEW)]
    public GameObject contextView { get; set; }

    public override void Execute()
    {
    }
}

public class ProcedureCommand:Command
{
    [Inject(ContextKeys.CONTEXT_VIEW)]
    public GameObject contextView { get; set; }

    [Inject]
    public ProcedureInfo procedureInfo { get; set; }

    public override void Execute()
    {
    }
}

public class MicControlCommand : Command
{
    [Inject(ContextKeys.CONTEXT_VIEW)]
    public GameObject contextView { get; set; }

    [Inject]
    public MicControlInfo controlInfo { get; set; }

    public override void Execute()
    {
    }
}

#endregion





