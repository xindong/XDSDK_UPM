using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityNative.Toasts;


public class XDTCHandler : com.xdsdk.xdtrafficcontrol.XDTrafficControl.XDTrafficControlCallback
{
    IUnityNativeToasts toast;

    public XDTCHandler(IUnityNativeToasts toast)
    {
        this.toast = toast;
    }
    public override void OnQueueingFinished()
    {
        Debug.Log("OnQueueingFinished");
        toast.ShowShortToast("OnQueueingFinished");
    }

    public override void OnQueueingFailed(string msg)
    {
        Debug.Log("OnQueueingFailed:" + msg);
        toast.ShowShortToast("OnQueueingFailed:" + msg);

    }

    public override void OnQueueingCanceled()
    {
        Debug.Log("XDTrafficControlCancel");
        toast.ShowShortToast("XDTrafficControlCancel");

    }
}
