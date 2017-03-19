using UnityEngine;
using System.Collections;
using strange.extensions.context.impl;
using strange.extensions.context.api;
using strange.extensions.command.api;
using strange.extensions.command.impl;
using CanvasUI;

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

        GlobalSignalManager.GlobalSignalBinder(injectionBinder, commandBinder);


        //signal 绑定到 Command
        commandBinder.Bind<StartSignal>().To<StartCommand>().Once();
        

        //在injectionBinder上绑定服务或model (一般绑定的都是接口) 
    }
}
