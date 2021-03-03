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

        private static XDLiveCallback callback;
        private XDLiveNativeCallback liveNativeCallback;

        private delegate void XDLiveMessageCallback(string method, string message);
        [AOT.MonoPInvokeCallback(typeof(XDLiveMessageCallback))]
        static void XDLiveCallbackImp(string method, string message)
        {
            Debug.Log(" Unity XDLive ios method = " + method + " msg = " + message);
            if (callback == null) return;
            switch (method)
            {
                case "onXDLiveOpen":
                    callback.OnXDLiveOpen();
                    break;
                case "onXDLiveClosed":
                    callback.OnXDLiveClosed();
                    break;
            }
        }

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

            if(liveNativeCallback != null){
                getAgent().CallStatic("setCallback",liveNativeCallback);
            }
            getAgent().CallStatic ("openXDLive", getUnityActivity(),appid);
#endif
        }

        public void OpenXDLive(string appid,string uri)
        {
#if UNITY_IOS && !UNITY_EDITOR 
            openXDLiveWithUri(appid,uri);

#elif UNITY_ANDROID && !UNITY_EDITOR
             if(liveNativeCallback != null){
                getAgent().CallStatic("setCallback",liveNativeCallback);
            }
            getAgent().CallStatic ("openXDLive", getUnityActivity(),appid,uri);
#endif
        }

        public void OpenXDLive(string appid,string uri,int orientation)
        {
#if UNITY_IOS && !UNITY_EDITOR
            openXDLiveWithUriAndOrientation(appid,uri,orientation);

#elif UNITY_ANDROID && !UNITY_EDITOR
            if(liveNativeCallback != null){
                getAgent().CallStatic("setCallback",liveNativeCallback);
            }
            getAgent().CallStatic ("openXDLive", getUnityActivity(),appid,uri,orientation);
#endif
        }

        public void CloseXDLive()
		{
#if UNITY_IOS && !UNITY_EDITOR
			closeXDLive();

#elif UNITY_ANDROID && !UNITY_EDITOR
		
			getAgent().CallStatic ("closeXDLive");
#endif
        }

        public void InvokeFunc(Dictionary<string, object> parameters, Action<Dictionary<string, object>> callback)
		{
			string unityCallbackID = Guid.NewGuid().ToString();
			string paramString = MiniJSON.Json.Serialize(parameters);

#if UNITY_IOS && !UNITY_EDITOR
			invokeFunc(unityCallbackID, paramString);
#elif UNITY_ANDROID && !UNITY_EDITOR
			getAgent().CallStatic ("invokeFun", unityCallbackID, paramString,new XDLiveInvokeCallback(callback));
#endif
        }

        public void SetCallback(XDLiveCallback callback)
        {
            XDLive.callback = callback;
            this.liveNativeCallback = new XDLiveNativeCallback(callback);
#if UNITY_IOS && !UNITY_EDITOR
            XDLiveSetCallback(XDLiveCallbackImp);
#endif
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
        [DllImport("__Internal")]
        private static extern void XDLiveSetCallback(XDLiveMessageCallback callback);
#elif UNITY_ANDROID && !UNITY_EDITOR
	public static string JAVA_CLASS = "com.xindong.xdlive.XDLive";
	private static string UNTIFY_CLASS = "com.unity3d.player.UnityPlayer";
	private static AndroidJavaClass agent = null;
	private static AndroidJavaObject unityActivity = null;

	private static AndroidJavaClass getAgent() {
		if (agent == null) {
			agent = new AndroidJavaClass(JAVA_CLASS);
		}
		return agent;
	}

	private static AndroidJavaObject getUnityActivity(){
		if (unityActivity == null) {
			unityActivity = new AndroidJavaClass(UNTIFY_CLASS).GetStatic<AndroidJavaObject>("currentActivity");
		}
		return unityActivity;
	}
#endif

        class XDLiveNativeCallback : AndroidJavaProxy
        {
            XDLiveCallback callback;

            public XDLiveNativeCallback(XDLiveCallback callback)  : base ("com.xindong.xdlive.XDLive$XDLiveCallback") {
                this.callback = callback;
            }

            public override AndroidJavaObject Invoke(string methodName, object[] javaArgs)
            {
                switch (methodName)
                {
                    case "onXDLiveOpen":
                        callback.OnXDLiveOpen();
                        break;
                    case "onXDLiveClosed":
                        callback.OnXDLiveClosed();
                        break;
                }
                return null;
            }
        }

        class XDLiveInvokeCallback : AndroidJavaProxy
        {
            Action<Dictionary<string, object>> callback;

            public XDLiveInvokeCallback(Action<Dictionary<string, object>> callback) : base("com.xindong.xdlive.XDLive$FunResultStringListener")
            {
                this.callback = callback;
            }

            public override AndroidJavaObject Invoke(string methodName, object[] javaArgs)
            {
                string result = (string)javaArgs[0];
                if (result != null)
                {
                    Dictionary<string, object> resultDict = MiniJSON.Json.Deserialize(result) as Dictionary<string, object>;
                    if (resultDict.ContainsKey("unity_callback_id"))
                    {
                        string unityCallbackID = resultDict["unity_callback_id"] as string;

                        callback(resultDict);
                    }
                }
                return null;
            }
        }
    }
}
