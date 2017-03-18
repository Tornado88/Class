using UnityEngine;
using System.Collections;
using strange.extensions.mediation.impl;
using strange.extensions.mediation.api;

public class UIView : View
{


    static public void MediationMapbinding(IMediationBinder mediatorBinder)
    {
        //进行mediator绑定
        mediatorBinder.Bind<UIView>().To<UIMediator>();
        mediatorBinder.Bind<SingCanvasView>().To<SingCanvasMediator>();
        mediatorBinder.Bind<RegisterCanvasView>().To<RegisterCanvasMediator>();
        mediatorBinder.Bind<ProcedureCanvasView>().To<ProcedureCanvasMediator>();
        mediatorBinder.Bind<ClassRoomCanvasView>().To<ClassRoomCanvasMediator>();
    }

    
    internal void init()
    {
        //初始化子物体状态
        
        //将本层设置的信号付给子物体的Mediator
    }



	
    
}
