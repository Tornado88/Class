using UnityEngine;
using System.Collections;
using strange.extensions.context.impl;
using strange.extensions.context.api;
using strange.extensions.command.api;
using strange.extensions.command.impl;

public class ClassMVCSContext : MVCSContext
{
    public ClassMVCSContext(MonoBehaviour view):base(view)
    { }

    public ClassMVCSContext(MonoBehaviour view, ContextStartupFlags flags):base(view,flags)
    { }

    protected override void addCoreComponents()
    {
        base.addCoreComponents();
        injectionBinder.Unbind<ICommandBinder>();
        injectionBinder.Bind<ICommandBinder>().To<SignalCommandBinder>().ToSingleton();
    }


    public override IContext Start()
    {
        base.Start();
        StartSignal startSignal = (StartSignal)injectionBinder.GetInstance<StartSignal>();
        startSignal.Dispatch();
        return this;
    }

    protected override void mapBindings()
    {
        base.mapBindings();
        //View 绑定到 Mediator
        UIView.MediationMapbinding(mediationBinder);
        
        //signal 绑定到 Command
        commandBinder.Bind<StartSignal>().To<StartCommand>().Once();
        commandBinder.Bind<LogInSignal>().To<LogInCommand>();
        commandBinder.Bind<LogOutSignal>().To<LogOutCommand>();
        commandBinder.Bind<ProcedureSignal>().To<ProcedureCommand>();
        commandBinder.Bind<MicControlSignal>().To<MicControlCommand>();

        //在injectionBinder上绑定服务 一般将类绑定到接口上 对于某些公用的信号使用单例模式。 injectionBinder
        //injectionBinder.Bind<LogInSignal>().ToSingleton();
        //injectionBinder.Bind<LogOutSignal>().To<LogOutCommand>();
        //injectionBinder.Bind<ProcedureSignal>().To<ProcedureCommand>();
        //injectionBinder.Bind<MicControlSignal>().To<MicControlCommand>();
    }
}
