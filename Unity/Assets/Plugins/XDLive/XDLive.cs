using UnityEngine;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System;



namespace com.xdsdk.xdlive
{
    public sealed class XDLive
    {

        private static volatile XDLive instance;
        private static object syncRoot = new System.Object();
        private static volatile bool inited = false;

        private XDLiveCallback callback;

        public XDLive()
        {
        }

        public static XDLive Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                        {
                            instance = new XDLive();
                        }
                    }
                }
                return instance;
            }
        }


        public void OpenXDLive(string appid)
        {
#if UNITY_IOS && !UNITY_EDITOR
            openXDLive(appid);

#elif UNITY_ANDROID && !UNITY_EDITOR
            AndroidJavaClass jc = new AndroidJavaClass("com.xindong.xdlive.XDLiveUnity");
            jc.CallStatic ("OpenXDLive", appid);
#endif

        }

        public void SetCallback(XDLiveCallback callback)
        {
            this.callback = callback;
        }

        public XDLiveCallback GetCallback(){
            return callback;
        }

        public abstract class XDLiveCallback
        {
            public abstract void OnXDLiveClosed();
            public abstract void OnXDLiveOpen();
        }

#if UNITY_IOS && !UNITY_EDITOR
        [DllImport("__Internal")]
        private static extern void openXDLive(string appid);
#endif
    }
}
