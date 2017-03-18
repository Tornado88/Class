using UnityEngine;
using System.Collections;
using strange.extensions.signal.impl;
using strange.extensions.command.impl;
using strange.extensions.context.api;

#region UIsingal
public class LogInSignal : Signal<LogInInfo> {}
public class LogOutSignal : Signal {}
public class ProcedureSignal : Signal<ProcedureInfo> { }
public class MicControlSignal : Signal<MicControlInfo> { }

public class LogInInfo
{
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

    public override void Execute()
    {

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





