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

        private XDTrafficControlCallback callback;

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
            AndroidJavaClass jc = new AndroidJavaClass("com.xindong.trafficcontrol.XDTrafficControlUnity");
            jc.CallStatic ("check", appid);
#endif

        }

        public void SetCallback(XDTrafficControlCallback callback)
        {
#if UNITY_IOS && !UNITY_EDITOR

#elif UNITY_ANDROID && !UNITY_EDITOR
            AndroidJavaClass jc = new AndroidJavaClass("com.xindong.trafficcontrol.XDTrafficControlUnity");
            jc.CallStatic ("init");
#endif

            this.callback = callback;
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
        private static extern void xdtcCheck(string appid);
#endif
    }
}
