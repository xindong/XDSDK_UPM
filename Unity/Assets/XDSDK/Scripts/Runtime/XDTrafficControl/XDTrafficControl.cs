using UnityEngine;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System;



namespace com.xdsdk.xdtrafficcontrol
{
    public sealed class XDTrafficControl
    {

        private static volatile XDTrafficControl instance;
        private static object syncRoot = new System.Object();

        private static XDTrafficControlCallback callback;

        private delegate void XDTrafficCallback(string method, string message);
        [AOT.MonoPInvokeCallback(typeof(XDTrafficCallback))]
        static void XDTrafficCallbackImp(string method, string message)
        {
            Debug.Log(" Unity trafficControl ios method = " + method + " msg = " + message);
            if (callback == null) return;
            switch (method)
            {
                case "XDTrafficControlFinished":
                    callback.OnQueueingFinished();
                    break;
                case "XDTrafficControlFailed":
                    callback.OnQueueingFailed(message);
                    break;
                case "XDTrafficControlCanceled":
                    callback.OnQueueingCanceled();
                    break;
            }
        }

        public XDTrafficControl()
        {
        }

        public static XDTrafficControl Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                        {
                            instance = new XDTrafficControl();
                        }
                    }
                }
                return instance;
            }
        }


        public void Check(string appid)
        {
#if UNITY_IOS && !UNITY_EDITOR
            xdtcCheck(appid);

#elif UNITY_ANDROID && !UNITY_EDITOR
            AndroidJavaObject activity = new AndroidJavaClass("com.unity3d.player.UnityPlayer").GetStatic<AndroidJavaObject>("currentActivity");
            AndroidJavaClass jc = new AndroidJavaClass("com.xd.sdk.trafficcontrol.XDTrafficControl");
            jc.CallStatic ("check", activity,appid);
#endif

        }

        public void SetCallback(XDTrafficControlCallback callback)
        {
#if UNITY_IOS && !UNITY_EDITOR
            XDTrafficSetCallback(XDTrafficCallbackImp);
#elif UNITY_ANDROID && !UNITY_EDITOR
            AndroidJavaClass jc = new AndroidJavaClass("com.xd.sdk.trafficcontrol.XDTrafficControl");
            jc.CallStatic ("setCallback",new XDTrafficCallbackAnd(callback));
#endif

            XDTrafficControl.callback = callback;
        }

        public XDTrafficControlCallback GetCallback()
        {
            return callback;
        }

        public abstract class XDTrafficControlCallback
        {
            public abstract void OnQueueingFinished();
            public abstract void OnQueueingFailed(string msg);
            public abstract void OnQueueingCanceled();
        }

#if UNITY_IOS && !UNITY_EDITOR
        [DllImport("__Internal")]
        private static extern void XDTrafficSetCallback(XDTrafficCallback callback);

        [DllImport("__Internal")]
        private static extern void xdtcCheck(string appid);
#endif

        class XDTrafficCallbackAnd : AndroidJavaProxy
        {
            XDTrafficControlCallback callback;

            public XDTrafficCallbackAnd(XDTrafficControlCallback callback):base("com.xd.sdk.trafficcontrol.XDTrafficControl$Callback")
            {
                this.callback = callback;
            }

            public override AndroidJavaObject Invoke(string methodName, object[] javaArgs)
            {
                if (this.callback == null) return null;
                switch (methodName)
                {
                    case "onQueueingFinished":
                        callback.OnQueueingFinished();
                        break;
                    case "onQueueingFailed":
                        string error = "";
                        try
                        {
                            error = (string)javaArgs[0];
                        }catch(Exception e)
                        {
                            error = "unknow error";
                        }
                         callback.OnQueueingFailed(error);
                        break;
                    case "onQueueingCanceled":
                        callback.OnQueueingCanceled();
                        break;
                }
                return null;
            }
        }
    }
}
