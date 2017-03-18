using UnityEngine;
using System.Collections;
using strange.extensions.context.impl;

public class MVCSRoot : ContextView {

    private void Awake()
    {
        context = new ClassMVCSContext(this);
    }
}
