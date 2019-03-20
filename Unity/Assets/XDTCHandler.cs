using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XDTCHandler : com.xdsdk.xdtrafficcontrol.XDTrafficControl.XDTrafficControlCallback
{

    public override void OnQueueingFinished()
    {
        Debug.Log("OnQueueingFinished");
    }

    public override void OnQueueingFailed(string msg)
    {
        Debug.Log("OnQueueingFailed:" + msg);
    }

    public override void OnQueueingCanceled()
    {
        Debug.Log("XDTrafficControlCancel");
    }
}
