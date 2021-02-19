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

		public delegate void FuncResult(int num);

        private static volatile XDLive instance;
        private static object syncRoot = new System.Object();

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

        public void OpenXDLive(string appid,string uri)
        {
#if UNITY_IOS && !UNITY_EDITOR
            openXDLiveWithUri(appid,uri);

#elif UNITY_ANDROID && !UNITY_EDITOR
            AndroidJavaClass jc = new AndroidJavaClass("com.xindong.xdlive.XDLiveUnity");
            jc.CallStatic ("OpenXDLive", appid,uri);
#endif
        }

        public void OpenXDLive(string appid,string uri,int orientation)
        {
#if UNITY_IOS && !UNITY_EDITOR
            openXDLiveWithUriAndOrientation(appid,uri,orientation);

#elif UNITY_ANDROID && !UNITY_EDITOR
            AndroidJavaClass jc = new AndroidJavaClass("com.xindong.xdlive.XDLiveUnity");
            jc.CallStatic ("OpenXDLive", appid,uri,orientation);
#endif
        }

		public void CloseXDLive()
		{
			#if UNITY_IOS && !UNITY_EDITOR
			closeXDLive();

			#elif UNITY_ANDROID && !UNITY_EDITOR
			AndroidJavaClass jc = new AndroidJavaClass("com.xindong.xdlive.XDLiveUnity");
			jc.CallStatic ("CloseXDLive");
			#endif
		}

		public void InvokeFunc(Dictionary<string, object> parameters, Action<Dictionary<string, object>> callback)
		{
			string unityCallbackID = Guid.NewGuid().ToString();
			string paramString = MiniJSON.Json.Serialize(parameters);
			XDLiveListener listener = GameObject.Find("XDLiveListener").GetComponent<XDLiveListener>();
			listener.AddCallback (unityCallbackID, callback);

#if UNITY_IOS && !UNITY_EDITOR
			invokeFunc(unityCallbackID, paramString);
#elif UNITY_ANDROID && !UNITY_EDITOR
			AndroidJavaClass jc = new AndroidJavaClass("com.xindong.xdlive.XDLiveUnity");
			jc.CallStatic ("InvokeFunc", unityCallbackID, paramString);
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
        [DllImport("__Internal")]
        private static extern void openXDLiveWithUri(string appid,string uri);
        [DllImport("__Internal")]
        private static extern void openXDLiveWithUriAndOrientation(string appid,string uri,int orientation);
		[DllImport("__Internal")]
		private static extern void closeXDLive();
		[DllImport("__Internal")]
		private static extern void invokeFunc(string unityCallbackID, string parameters);
#endif
    }
}
