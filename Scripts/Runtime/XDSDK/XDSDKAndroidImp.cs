#define USE_UNITY_XDSDK

using UnityEngine;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System;

namespace xdsdk
{
    public class XDSDKAndroidImp : XDSDKInterface
    {
        private static XDSDKAndroidImp _instance;

        private XDCallback xdCallback;
        private XDSDKExitCallback xdExitCallback;

        public static XDSDKAndroidImp GetInstance()
        {
            if (_instance == null)
            {
                _instance = new XDSDKAndroidImp();
            }
            return _instance;
        }

   
        public void SetCallback(XDCallback callback)
        {
            Debug.Log(" Unity XDSDKAndroidImp setCallback");
            xdCallback = callback;
            XDWXShareCallback xDWXShareCallback = new XDWXShareCallback(callback);
#if UNITY_ANDROID && !UNITY_EDITOR
     
        getAgent().CallStatic("setCallback",new XDSDKCallbackHandler(callback));

        AndroidJavaClass share = new AndroidJavaClass("com.xd.xdsdk.share.XDWXShare");
        share.CallStatic("setWXShareCallBack",xDWXShareCallback);
#endif
        }

        public XDCallback GetXDCallback()
        {
            return xdCallback;
        }
       
        public void HideGuest()
        {
#if UNITY_ANDROID && !UNITY_EDITOR
			getAgent().CallStatic ("hideGuest");
#endif
        }

        public void HideWX()
        {

#if UNITY_ANDROID && !UNITY_EDITOR
			getAgent().CallStatic ("hideWX");
#endif
        }

        public void HideQQ()
        {

#if UNITY_ANDROID && !UNITY_EDITOR
			getAgent().CallStatic ("hideQQ");
#endif
        }

        public void ShowVC()
        {
#if UNITY_ANDROID && !UNITY_EDITOR
		getAgent().CallStatic ("showVC");
#endif
        }

        public void SetQQWeb()
        {
#if UNITY_ANDROID && !UNITY_EDITOR
			getAgent().CallStatic ("setQQWeb");
#endif
        }

        public void SetWXWeb()
        {
#if UNITY_ANDROID && !UNITY_EDITOR
			getAgent().CallStatic ("setWXWeb");
#endif
        }

        public void HideTapTap()
        {
#if UNITY_ANDROID && !UNITY_EDITOR
			getAgent().CallStatic ("hideTapTap");
#endif
        }

        public void SetLoginEntries(string[] entries)
        {

#if UNITY_ANDROID && !UNITY_EDITOR
			getAgent().CallStatic ("setLoginEntries", CSStringArrayToJavaStringArray(entries));
#endif
        }

        public string GetSDKVersion()
        {
#if UNITY_ANDROID && !UNITY_EDITOR
			return getAgent().CallStatic<string> ("getSDKVersion");

#else
            return "0.0.0";
#endif

        }

        public string GetAdChannelName()
        {
#if UNITY_ANDROID && !UNITY_EDITOR
            AndroidJavaClass playerClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            AndroidJavaObject activityObject = playerClass.GetStatic<AndroidJavaObject>("currentActivity");
	
			return getAgent().CallStatic<string> ("getAdChannelName",activityObject);

#else
            return "";
#endif

        }



        public void InitSDK(string appid, int aOrientation, string channel, string version, bool enableTapDB)
        {
#if UNITY_ANDROID && !UNITY_EDITOR
            AndroidJavaClass playerClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            AndroidJavaObject activityObject = playerClass.GetStatic<AndroidJavaObject>("currentActivity");
		
			getAgent().CallStatic ("initSDK",activityObject,appid,aOrientation,channel,version,enableTapDB);
#endif
        }

        public void InitSDK(string appid, int aOrientation, string channel, string version, bool enableTapDB, bool enableMoment)
        {
#if UNITY_ANDROID && !UNITY_EDITOR
            AndroidJavaClass playerClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            AndroidJavaObject activityObject = playerClass.GetStatic<AndroidJavaObject>("currentActivity");
		
			getAgent().CallStatic ("initSDK",activityObject,appid,aOrientation,channel,version,enableTapDB,enableMoment);
#endif
        }

        public void Login()
        {
#if UNITY_ANDROID && !UNITY_EDITOR
			getAgent().CallStatic ("login");
#endif
        }

        public string GetAccessToken()
        {
#if UNITY_ANDROID && !UNITY_EDITOR
			return getAgent().CallStatic<string> ("getAccessToken");
#else
            return "";
#endif

        }

        public bool IsLoggedIn()
        {
#if UNITY_ANDROID && !UNITY_EDITOR
			return getAgent().CallStatic<bool> ("isLoggedIn");
#else
            return true;
#endif
        }

        public bool OpenUserCenter()
        {
#if UNITY_ANDROID && !UNITY_EDITOR
			return getAgent().CallStatic<bool> ("openUserCenter");
#endif
            return false;
        }

        public void UserFeedback()
        {
#if UNITY_ANDROID && !UNITY_EDITOR
		getAgent().CallStatic ("userFeedback");
#endif
        }


        public void OpenRealName()
        {
#if UNITY_ANDROID && !UNITY_EDITOR
		getAgent().CallStatic("openRealName");
#endif
        }

        public void OpenUserBindView()
        {
#if UNITY_ANDROID && !UNITY_EDITOR
          getAgent().CallStatic("openUserBindView");
#endif
        }

        public bool Pay(Dictionary<string, string> info)
        {
#if UNITY_ANDROID && !UNITY_EDITOR
			return getAgent().CallStatic<bool> ("pay", DicToMap(info));
#endif
            return false;

        }

        public void RestorePay(Dictionary<string, string> info)
        {
#if UNITY_ANDROID && !UNITY_EDITOR

#endif
        }

        public void Logout()
        {
#if UNITY_ANDROID && !UNITY_EDITOR
			getAgent().CallStatic ("logout");

#endif
        }

        public void Exit()
        {
            if(xdExitCallback == null)
            {
                xdExitCallback = new XDSDKExitCallback(GetXDCallback());
            }
#if UNITY_ANDROID && !UNITY_EDITOR
		getAgent().CallStatic ("exit",xdExitCallback);
#endif
        }

        public void Share(Dictionary<string, string> content)
        {
#if UNITY_ANDROID && !UNITY_EDITOR
            AndroidJavaClass share = new AndroidJavaClass("com.xd.xdsdk.share.XDWXShare");
			share.CallStatic ("shareToWX", DicToMap(content));
#endif
        }

        public void SetLevel(int level)
        {
#if UNITY_ANDROID && !UNITY_EDITOR
		getAgent().CallStatic ("setLevel", level);
#endif
        }

        public void SetServer(string server)
        {
#if UNITY_ANDROID && !UNITY_EDITOR
			getAgent().CallStatic ("setServer", server);
#endif
        }

        public void SetRole(string roleId, string roleName, string roleAvatar)
        {
            if (roleAvatar == null)
            {
                roleAvatar = "";
            }
#if UNITY_ANDROID && !UNITY_EDITOR

          getAgent().CallStatic("setRole",roleId,roleName,roleAvatar);
#endif
        }

        public void ClearRole()
        {
#if UNITY_ANDROID && !UNITY_EDITOR

           getAgent().CallStatic("clearRole");
#endif
        }

        public void AutoLogin()
        {
#if UNITY_ANDROID && !UNITY_EDITOR
             getAgent().CallStatic("autoLogin");
#endif
        }

        public void TapTapLogin()
        {
#if UNITY_ANDROID && !UNITY_EDITOR
           getAgent().CallStatic("taptapLogin");
                // TODO
#endif
        }

        public void AppleLogin()
        {
#if UNITY_ANDROID && !UNITY_EDITOR

                // TODO
#endif
        }

        public void GuestLogin()
        {
#if UNITY_ANDROID && !UNITY_EDITOR
              getAgent().CallStatic("guestLogin");
#endif
        }

        public void GameStop() {
#if UNITY_ANDROID && !UNITY_EDITOR
                        // TODO
          getAgent().CallStatic("gameStoped");
#endif
        }

        public void GameResume() {
#if UNITY_ANDROID && !UNITY_EDITOR
                        // TODO
            getAgent().CallStatic("gameStarted");
#endif
        }

        public void OnResume() {

#if UNITY_ANDROID && !UNITY_EDITOR
		AndroidJavaObject activity = getUnityClass().GetStatic<AndroidJavaObject>("currentActivity");
		getAgent().CallStatic("onResume", activity);

#else
#endif

        }

        public void OnStop() {

#if UNITY_ANDROID && !UNITY_EDITOR

		AndroidJavaObject activity = getUnityClass().GetStatic<AndroidJavaObject>("currentActivity");
		getAgent().CallStatic("onStop", activity);
#else
#endif

        }

        public void OpenProtocol(XDSDK.ProtocolType type)
        {
            int protocolType = Convert.ToInt32(type);
#if UNITY_ANDROID && !UNITY_EDITOR
        getAgent().CallStatic("openProtocol",protocolType);
#endif
        }

        public void OpenUserMoment(XDMomentConfig config, string xdId)
        {
            string configString = config.GetConfigString();
#if UNITY_ANDROID && !UNITY_EDITOR
             getAgent().CallStatic("openUserMoment",configString,xdId);
#endif
        }

#if UNITY_ANDROID && !UNITY_EDITOR
	public static string JAVA_CLASS = "com.xd.xdsdk.XDSDK";
	private static string UNTIFY_CLASS = "com.unity3d.player.UnityPlayer";
	private static AndroidJavaClass agent = null;
	private static AndroidJavaClass unityClass = null;

	private static AndroidJavaClass getAgent() {
		if (agent == null) {
			agent = new AndroidJavaClass(JAVA_CLASS);
		}
		return agent;
	}

	private static AndroidJavaClass getUnityClass(){
		if (unityClass == null) {
			unityClass = new AndroidJavaClass(UNTIFY_CLASS);
		}
		return unityClass;
	}

        private AndroidJavaObject CSStringArrayToJavaStringArray(string [] values) {
            AndroidJavaClass arrayClass  = new AndroidJavaClass("java.lang.reflect.Array");
            AndroidJavaObject arrayObject = arrayClass.CallStatic<AndroidJavaObject>("newInstance",
                                             new AndroidJavaClass("java.lang.String"),
                                             values.Count());
            for (int i=0; i<values.Count(); ++i) {
                arrayClass.CallStatic("set",
                arrayObject,
                i,
                new AndroidJavaObject("java.lang.String",
                values[i]));
            }
            return arrayObject;
        }
        public static AndroidJavaObject DicToMap(Dictionary<string, string> dictionary)
        {
            if(dictionary == null)
            {
                return null;
            }
            AndroidJavaObject map = new AndroidJavaObject("java.util.HashMap");
            foreach(KeyValuePair<string, string> pair in dictionary)
            {
                
                safeCallStringMethod(map,"put",new string[2] { pair.Key,pair.Value});
                // map.Call<string>("put", pair.Key, pair.Value);
            }
            return map;
        }


        public static string safeCallStringMethod(AndroidJavaObject javaObject, string methodName, params object[] args)
        {
#if UNITY_2018_2_OR_NEWER
            if (args == null) args = new object[] {null};
            IntPtr methodID = AndroidJNIHelper.GetMethodID<string>(javaObject.GetRawClass(), methodName, args, false);
            jvalue[] jniArgs = AndroidJNIHelper.CreateJNIArgArray(args);
 
            try
            {
                IntPtr returnValue = AndroidJNI.CallObjectMethod(javaObject.GetRawObject(), methodID, jniArgs);
                if (IntPtr.Zero != returnValue)
                {
                    var val = AndroidJNI.GetStringUTFChars(returnValue);
                    AndroidJNI.DeleteLocalRef(returnValue);
                    return val;
                }
            }
            finally
            {
                AndroidJNIHelper.DeleteJNIArgArray(args, jniArgs);
            }
 
            return null;
#else
            return  javaObject.Call<string>(methodName, args);
#endif
        }
#endif
    }

    class XDSDKCallbackHandler : AndroidJavaProxy
    {
        XDCallback callback;

        public XDSDKCallbackHandler(XDCallback callback) : base("com.xd.xdsdk.XDCallback")
        {
            this.callback = callback;
        }

        public override AndroidJavaObject Invoke(string methodName, object[] javaArgs)
        {
            Debug.Log(" XDCALLBACK :" + methodName);
            string extra = "";
            if(javaArgs.Length > 0)
            {
                extra = (string)javaArgs[0];
            }
            switch (methodName)
            {
                case "onInitSucceed":
                    Debug.Log(" xdsdk set init success callback = " + callback.ToString());
                    callback.OnInitSucceed();
                    break;
                case "onInitFailed":
                    callback.OnInitFailed(extra);
                    break;
                case "onLoginSucceed":
                    callback.OnLoginSucceed(extra);//token
                    break;
                case "onLoginFailed":
                    callback.OnLoginFailed(extra);
                    break;
                case "onLoginCanceled":
                    callback.OnLoginCanceled();
                    break;
                case "onGuestBindSucceed":
                    callback.OnGuestBindSucceed(extra);
                    break;
                case "onGuestBindFailed":
                    callback.OnGuestBindFailed(extra);
                    break;
                case "onRealNameSucceed":
                    callback.OnRealNameSucceed();
                    break;
                case "onRealNameFailed":
                    callback.OnRealNameFailed(extra);
                    break;
                case "onLogoutSucceed":
                    callback.OnLogoutSucceed();
                    break;
                case "onPayCompleted":
                    callback.OnPayCompleted();
                    break;
                case "onPayFailed":
                    callback.OnPayFailed(extra);
                    break;
                case "onPayCanceled":
                    callback.OnPayCanceled();
                    break;
                case "restoredPayment":
                    string payment = extra;
                    Debug.Log("RestoredPayment ： " + payment);

                    List<object> resultList = (List<object>)Json.Deserialize(payment);
                    List<Dictionary<string, string>> paymentInfoList = new List<Dictionary<string, string>>();
                    foreach (Dictionary<string, object> dict in resultList)
                    {
                        Dictionary<string, string> onePaymentInfo = new Dictionary<string, string>();

                        foreach (KeyValuePair<string, object> kvp in dict)
                        {
                            onePaymentInfo[kvp.Key] = (string)kvp.Value;
                        }

                        paymentInfoList.Add(onePaymentInfo);
                    }
                    callback.RestoredPayment(paymentInfoList);
                    break;
                case "onExitConfirm":
                    callback.OnExitConfirm();
                    break;
                case "onExitCancel":
                    callback.OnExitCancel();
                    break;
                case "onWXShareSucceed":
                    callback.OnWXShareSucceed();
                    break;
                case "onWXShareFailed":
                    callback.OnWXShareFailed(extra);
                    break;
                case "onProtocolAgreed":
                    callback.OnProtocolAgreed();
                    break;
                case "onProtocolOpenSucceed":
                    callback.OnProtocolOpenSucceed();
                    break;
                case "onProtocolOpenFailed":
                    callback.OnProtocolOpenFailed(extra);
                    break;
                case "onBindTaptapSucceed":
                    Dictionary<string, string> tapInfo = (Dictionary<string, string>)Json.Deserialize(extra);
                    callback.OnBindTaptapSucceed(tapInfo);
                    break;


            }

            return null;
        }
    }

    class XDSDKExitCallback : AndroidJavaProxy
    {
        XDCallback callback;

        public XDSDKExitCallback(XDCallback callback) : base("com.xd.xdsdk.ExitCallback")
        {
            this.callback = callback;
        }

        public override AndroidJavaObject Invoke(string methodName, object[] javaArgs)
        {
            switch (methodName)
            {
                case "onConfirm":
                    callback.OnExitConfirm();
                    break;
                case "onCancle":
                    callback.OnExitCancel();
                    break;
            }
            return null;

        }
    }

    class XDWXShareCallback : AndroidJavaProxy
    {
        XDCallback callback;

        public XDWXShareCallback(XDCallback callback) : base("com.xd.xdsdk.share.XDWXShare$XDWXShareCallback")
        {
            this.callback = callback;
        }

        public override AndroidJavaObject Invoke(string methodName, object[] javaArgs)
        {
            switch (methodName)
            {
                case "onWXShareSucceed":
                    callback.OnWXShareSucceed();
                    break;
                case "onWXShareFailed":
                    callback.OnWXShareFailed((string)javaArgs[0]);
                    break;
            }
            return null;
        }

    }
}
