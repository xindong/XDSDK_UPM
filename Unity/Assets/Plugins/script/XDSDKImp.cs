#define USE_UNITY_XDSDK

using UnityEngine;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System;



namespace xdsdk
{
    public class XDSDKImp
    {
        private static XDSDKImp _instance;

        public XDCallback xdCallback;

        public static XDSDKImp GetInstance()
        {
            if (_instance == null)
            {
                _instance = new XDSDKImp();
            }
            return _instance;
        }

        public void SetCallback(XDCallback callback)
        {
            xdCallback = callback;

#if UNITY_STANDALONE_WIN && !UNITY_EDITOR && !USE_UNITY_XDSDK

			UnitySetCallback(new XDSDKListener.UniversalCallbackDelegate(XDSDKListener.UniversalCallback));

#elif (UNITY_EDITOR || UNITY_STANDALONE_OSX || UNITY_STANDALONE_WIN ) && FOR_RO
            Unity.XDSDK.SetCallback((Unity.ResultCode code, string data) =>
            {
                if (xdCallback != null)
                {
                    switch (code)
                    {
                        case Unity.ResultCode.InitSucceed:
                            xdCallback.OnInitSucceed();
                            break;
                        case Unity.ResultCode.InitFailed:
                            xdCallback.OnInitFailed(data);
                            break;
                        case Unity.ResultCode.LoginSucceed:
                            xdCallback.OnLoginSucceed(data);
                            break;
                        case Unity.ResultCode.LoginCanceled:
                            xdCallback.OnLoginCanceled();
                            break;
                        case Unity.ResultCode.LoginFailed:
                            xdCallback.OnLoginFailed(data);
                            break;
                        case Unity.ResultCode.PayCompleted:
                            xdCallback.OnPayCompleted();
                            break;
                        case Unity.ResultCode.PayCanceled:
                            xdCallback.OnPayCanceled();
                            break;
                        case Unity.ResultCode.PayFailed:
                            xdCallback.OnPayFailed(data);
                            break;
                        case Unity.ResultCode.RealNameSucceed:
                            xdCallback.OnRealNameSucceed();
                            break;
                        case Unity.ResultCode.RealNameFailed:
                            xdCallback.OnRealNameFailed(data);
                            break;
                        default:
                            break;
                    }
                }
            });

#endif
        }

        public XDCallback GetXDCallback()
        {
            return xdCallback;
        }

        public void HideGuest()
        {
#if UNITY_IOS && !UNITY_EDITOR
			hideGuest();

#elif UNITY_ANDROID && !UNITY_EDITOR
			AndroidJavaClass jc = new AndroidJavaClass("com.xd.unitysdk.UnitySDK");
			jc.CallStatic ("hideGuest");
#endif
        }

        public void HideWX()
        {
#if UNITY_IOS && !UNITY_EDITOR
			hideWeiChat();

#elif UNITY_ANDROID && !UNITY_EDITOR
			AndroidJavaClass jc = new AndroidJavaClass("com.xd.unitysdk.UnitySDK");
			jc.CallStatic ("hideWX");
#endif
        }

        public void HideQQ()
        {
#if UNITY_IOS && !UNITY_EDITOR
			hideQQ();

#elif UNITY_ANDROID && !UNITY_EDITOR
			AndroidJavaClass jc = new AndroidJavaClass("com.xd.unitysdk.UnitySDK");
			jc.CallStatic ("hideQQ");
#endif
        }

        public void ShowVC()
        {
#if UNITY_IOS && !UNITY_EDITOR
			showVC();

#elif UNITY_ANDROID && !UNITY_EDITOR
			AndroidJavaClass jc = new AndroidJavaClass("com.xd.unitysdk.UnitySDK");
			jc.CallStatic ("showVC");
#endif
        }

        public void SetQQWeb()
        {
#if UNITY_IOS && !UNITY_EDITOR
			setQQWeb();

#elif UNITY_ANDROID && !UNITY_EDITOR
			AndroidJavaClass jc = new AndroidJavaClass("com.xd.unitysdk.UnitySDK");
			jc.CallStatic ("setQQWeb");
#endif
        }

        public void SetWXWeb()
        {
#if UNITY_IOS && !UNITY_EDITOR
			setWXWeb();

#elif UNITY_ANDROID && !UNITY_EDITOR
			AndroidJavaClass jc = new AndroidJavaClass("com.xd.unitysdk.UnitySDK");
			jc.CallStatic ("setWXWeb");
#endif
        }

        public void HideTapTap()
        {
#if UNITY_IOS && !UNITY_EDITOR
			hideTapTap();

#elif UNITY_ANDROID && !UNITY_EDITOR
			AndroidJavaClass jc = new AndroidJavaClass("com.xd.unitysdk.UnitySDK");
			jc.CallStatic ("hideTapTap");
#endif
        }

        public void SetLoginEntries(string[] entries)
        {
#if UNITY_IOS && !UNITY_EDITOR
			setLoginEntries(entries,entries.Count());

#elif UNITY_STANDALONE_WIN && !UNITY_EDITOR && !USE_UNITY_XDSDK

			UnitySetLoginEntries(entries, entries.Count());

#elif (UNITY_EDITOR || UNITY_STANDALONE_OSX || UNITY_STANDALONE_WIN) && FOR_RO

            Unity.XDSDK.SetLoginEntries(entries);

#elif UNITY_ANDROID && !UNITY_EDITOR
			AndroidJavaClass jc = new AndroidJavaClass("com.xd.unitysdk.UnitySDK");
			jc.CallStatic ("setLoginEntries", CSStringArrayToJavaStringArray(entries));
#endif
        }

        public string GetSDKVersion()
        {
#if UNITY_IOS && !UNITY_EDITOR
			return getXDSDKVersion();

#elif UNITY_STANDALONE_WIN && !UNITY_EDITOR && !USE_UNITY_XDSDK
			return UnityGetSdkVersion();

#elif (UNITY_EDITOR || UNITY_STANDALONE_OSX || UNITY_STANDALONE_WIN) && FOR_RO
            return Unity.XDSDK.VERSION;

#elif UNITY_ANDROID && !UNITY_EDITOR
			AndroidJavaClass jc = new AndroidJavaClass("com.xd.unitysdk.UnitySDK");
			return jc.CallStatic<string> ("getSDKVersion");

#else
            return "0.0.0";
#endif

        }

        public void InitSDK(string appid, int aOrientation, string channel, string version, bool enableTapDB)
        {
#if UNITY_IOS && !UNITY_EDITOR
			initXDSDK(appid,aOrientation,channel,version,enableTapDB);

#elif UNITY_STANDALONE_WIN && !UNITY_EDITOR && !USE_UNITY_XDSDK

			UnityInit(appid);

#elif (UNITY_EDITOR || UNITY_STANDALONE_OSX || UNITY_STANDALONE_WIN)  && FOR_RO
            Unity.XDSDK.Init(appid);

#elif UNITY_ANDROID && !UNITY_EDITOR
			AndroidJavaClass jc = new AndroidJavaClass("com.xd.unitysdk.UnitySDK");
			jc.CallStatic ("initSDK",appid,aOrientation,channel,version,enableTapDB);
#endif
        }

        public void Login()
        {

#if UNITY_IOS && !UNITY_EDITOR
			xdLogin();

#elif UNITY_STANDALONE_WIN && !UNITY_EDITOR && !USE_UNITY_XDSDK

			UnityLogin();

#elif (UNITY_EDITOR || UNITY_STANDALONE_OSX || UNITY_STANDALONE_WIN)  && FOR_RO
            Unity.XDSDK.Login();
#elif UNITY_ANDROID && !UNITY_EDITOR
			AndroidJavaClass jc = new AndroidJavaClass("com.xd.unitysdk.UnitySDK");
			jc.CallStatic ("login");
#endif
        }

        public string GetAccessToken()
        {
#if UNITY_IOS && !UNITY_EDITOR
			return getXDAccessToken();

#elif UNITY_STANDALONE_WIN && !UNITY_EDITOR && !USE_UNITY_XDSDK

			return UnityGetAccessToken();

#elif (UNITY_EDITOR || UNITY_STANDALONE_OSX || UNITY_STANDALONE_WIN)  && FOR_RO
            return Unity.XDSDK.GetAccessToken();

#elif UNITY_ANDROID && !UNITY_EDITOR
			AndroidJavaClass jc = new AndroidJavaClass("com.xd.unitysdk.UnitySDK");
			return jc.CallStatic<string> ("getAccessToken");
#else
            return "";
#endif

        }

        public bool IsLoggedIn()
        {
#if UNITY_IOS && !UNITY_EDITOR
			return isXdLoggedIn();

#elif UNITY_STANDALONE_WIN && !UNITY_EDITOR && !USE_UNITY_XDSDK

			return UnityIsLoggedIn();

#elif (UNITY_EDITOR || UNITY_STANDALONE_OSX || UNITY_STANDALONE_WIN)  && FOR_RO
            return !string.IsNullOrEmpty(Unity.XDSDK.GetAccessToken());

#elif UNITY_ANDROID && !UNITY_EDITOR
			AndroidJavaClass jc = new AndroidJavaClass("com.xd.unitysdk.UnitySDK");
			return jc.CallStatic<bool> ("isLoggedIn");
#else
            return true;
#endif
        }

        public bool OpenUserCenter()
        {
#if UNITY_IOS && !UNITY_EDITOR
			return openUserCenter();

#elif UNITY_ANDROID && !UNITY_EDITOR
			AndroidJavaClass jc = new AndroidJavaClass("com.xd.unitysdk.UnitySDK");
			return jc.CallStatic<bool> ("openUserCenter");
#endif
            return false;
        }

        public bool openMobileVerify()
        {
#if UNITY_IOS && !UNITY_EDITOR
			return openMobileVerifyView();

#elif UNITY_ANDROID && !UNITY_EDITOR
			AndroidJavaClass jc = new AndroidJavaClass("com.xd.unitysdk.UnitySDK");
			jc.CallStatic ("openMobileVerifyView");
                        return true;
#endif
            return false;
        }

        public void UserFeedback()
        {
#if UNITY_IOS && !UNITY_EDITOR
			userFeedback();

#elif UNITY_ANDROID && !UNITY_EDITOR
			AndroidJavaClass jc = new AndroidJavaClass("com.xd.unitysdk.UnitySDK");
			jc.CallStatic ("userFeedback");
#endif
        }


        public void OpenRealName()
        {
#if UNITY_IOS && !UNITY_EDITOR
			openRealName();

#elif UNITY_STANDALONE_WIN && !UNITY_EDITOR && !USE_UNITY_XDSDK

			UnityOpenRealName();

#elif (UNITY_EDITOR || UNITY_STANDALONE_OSX || UNITY_STANDALONE_WIN)  && FOR_RO
            Unity.XDSDK.OpenRealName();

#elif UNITY_ANDROID && !UNITY_EDITOR
			AndroidJavaClass jc = new AndroidJavaClass("com.xd.unitysdk.UnitySDK");
			jc.CallStatic("openRealName");
#endif
        }

        public void OpenUserBindView()
        {
#if UNITY_IOS && !UNITY_EDITOR
            openUserBindView();

#elif UNITY_ANDROID && !UNITY_EDITOR
            AndroidJavaClass jc = new AndroidJavaClass("com.xd.unitysdk.UnitySDK");
            jc.CallStatic("openUserBindView");
#endif
        }

        public bool Pay(Dictionary<string, string> info)
        {

#if UNITY_IOS && !UNITY_EDITOR
			xdPay(info.ContainsKey("Product_Name") ? info["Product_Name"] : "",
					info.ContainsKey("Product_Id") ? info["Product_Id"] : "",
					info.ContainsKey("Product_Price") ? info["Product_Price"] : "",
					info.ContainsKey("Sid") ? info["Sid"] : "",
					info.ContainsKey("Role_Id") ? info["Role_Id"] : "",
					info.ContainsKey("OrderId") ? info["OrderId"] : "",
					info.ContainsKey("EXT") ? info["EXT"] : "");

#elif UNITY_STANDALONE_WIN && !UNITY_EDITOR && !USE_UNITY_XDSDK

			UnityPay(info.ContainsKey("Product_Name") ? info["Product_Name"] : "",
					info.ContainsKey("Product_Id") ? info["Product_Id"] : "",
					info.ContainsKey("Product_Price") ? int.Parse(info["Product_Price"]) : 0,
					info.ContainsKey("Sid") ? info["Sid"] : "",
					info.ContainsKey("Role_Id") ? info["Role_Id"] : "",
					info.ContainsKey("OrderId") ? info["OrderId"] : "",
					info.ContainsKey("EXT") ? info["EXT"] : "");

#elif (UNITY_EDITOR || UNITY_STANDALONE_OSX || UNITY_STANDALONE_WIN)  && FOR_RO
            Unity.XDSDK.Pay(info);

#elif UNITY_ANDROID && !UNITY_EDITOR
			AndroidJavaClass jc = new AndroidJavaClass("com.xd.unitysdk.UnitySDK");
			return jc.CallStatic<bool> ("pay", DicToMap(info));
#endif
            return false;

        }

        public void RestorePay(Dictionary<string, string> info)
        {
#if UNITY_IOS && !UNITY_EDITOR
	xdRestorePay(info.ContainsKey("Product_Name") ? info["Product_Name"] : "",
					info.ContainsKey("Product_Id") ? info["Product_Id"] : "",
					info.ContainsKey("Product_Price") ? info["Product_Price"] : "",
					info.ContainsKey("Sid") ? info["Sid"] : "",
					info.ContainsKey("Role_Id") ? info["Role_Id"] : "",
					info.ContainsKey("OrderId") ? info["OrderId"] : "",
					info.ContainsKey("EXT") ? info["EXT"] : "",
                                        info.ContainsKey("transactionIdentifier") ? info["transactionIdentifier"] : "");
#elif UNITY_ANDROID && !UNITY_EDITOR

#endif
        }

        public void Logout()
        {
#if UNITY_IOS && !UNITY_EDITOR
			xdLogout();

#elif UNITY_STANDALONE_WIN && !UNITY_EDITOR  && !USE_UNITY_XDSDK

			UnityLogout();

#elif (UNITY_EDITOR || UNITY_STANDALONE_OSX || UNITY_STANDALONE_WIN)  && FOR_RO
            Unity.XDSDK.Logout();

#elif UNITY_ANDROID && !UNITY_EDITOR
			AndroidJavaClass jc = new AndroidJavaClass("com.xd.unitysdk.UnitySDK");
			jc.CallStatic ("logout");

#endif
        }

        public void Exit()
        {
#if UNITY_IOS && !UNITY_EDITOR


#elif UNITY_ANDROID && !UNITY_EDITOR
			AndroidJavaClass jc = new AndroidJavaClass("com.xd.unitysdk.UnitySDK");
			jc.CallStatic ("exit");
#endif
        }

        public void Share(Dictionary<string, string> content)
        {
#if UNITY_IOS && !UNITY_EDITOR

			xdShare(content["text"],content["bText"],content["scene"],content["shareType"],content["title"],content["description"],content["thumbPath"],content["imageUrl"],content["musicUrl"],
			content["musicLowBandUrl"],content["musicDataUrl"],content["musicLowBandDataUrl"],content["videoUrl"],content["videoLowBandUrl"],content["webpageUrl"]);

#elif UNITY_ANDROID && !UNITY_EDITOR
			AndroidJavaClass jc = new AndroidJavaClass("com.xd.unitysdk.UnitySDK");
			jc.CallStatic ("shareToWX", DicToMap(content));
#endif
        }

        public void SetLevel(int level)
        {
#if UNITY_IOS && !UNITY_EDITOR

			setLevel(level);

#elif UNITY_ANDROID && !UNITY_EDITOR
			AndroidJavaClass jc = new AndroidJavaClass("com.xd.unitysdk.UnitySDK");
			jc.CallStatic ("setLevel", level);
#endif
        }

        public void SetServer(string server)
        {

#if UNITY_IOS && !UNITY_EDITOR

			setServer(server);

#elif UNITY_ANDROID && !UNITY_EDITOR
			AndroidJavaClass jc = new AndroidJavaClass("com.xd.unitysdk.UnitySDK");
			jc.CallStatic ("setServer", server);
#endif
        }



#if UNITY_IOS && !UNITY_EDITOR
        [DllImport("__Internal")]
        private static extern void initSDK(string appid, int aOrientation);

        [DllImport("__Internal")]
        private static extern void initXDSDK(string appid, int aOrientation, string channel, string version, bool enableTapdb);

        [DllImport("__Internal")]
        private static extern void setLevel(int level);

        [DllImport("__Internal")]
        private static extern void setServer(string server);

        [DllImport("__Internal")]
        private static extern void xdLogin();

        [DllImport("__Internal")]
        private static extern bool isXdLoggedIn();

        [DllImport("__Internal")]
        private static extern void xdLogout();

        [DllImport("__Internal")]
        private static extern bool openUserCenter();

        [DllImport("__Internal")]
        private static extern bool openMobileVerifyView();

        [DllImport("__Internal")]
        private static extern void xdPay(string proudct_name, string product_id, string product_price, string sid, string role_id, string orderid, string ext);

        [DllImport("__Internal")]
        private static extern void xdRestorePay(string proudct_name, string product_id, string product_price, string sid, string role_id, string orderid, string ext,string transactionIdentifier);

        [DllImport("__Internal")]
        private static extern string getXDSDKVersion();

        [DllImport("__Internal")]
        private static extern string getXDAccessToken();

        [DllImport("__Internal")]
        private static extern void hideGuest();

        [DllImport("__Internal")]
        private static extern void hideQQ();

        [DllImport("__Internal")]
        private static extern void hideWeiChat();

        [DllImport("__Internal")]
        private static extern void showVC();

        [DllImport("__Internal")]
        private static extern void setQQWeb();

        [DllImport("__Internal")]
        private static extern void setWXWeb();

        [DllImport("__Internal")]
        private static extern void setLoginEntries(string[] entries,int length);

        [DllImport("__Internal")]
        private static extern void userFeedback();

        [DllImport("__Internal")]
        private static extern void hideTapTap();

        [DllImport("__Internal")]
        private static extern void openRealName();

        [DllImport("__Internal")]
        private static extern void xdShare (string text, string bText, string scene, string shareType, string title,string description, string thumbPath,
        string imageUrl, string musicUrl, string musicLowBandUrl, string musicDataUrl, string musicLowBandDataUrl, string videoUrl,string videoLowBandUrl,
        string webPageUrl
        );

        [DllImport("__Internal")]
        private static extern void openUserBindView();


#elif UNITY_STANDALONE_WIN && !UNITY_EDITOR

        [DllImport("XDSDK")]
        private static extern void UnitySetCallback(XDSDKListener.UniversalCallbackDelegate universalCallback);

        [DllImport("XDSDK")]
        private static extern void UnitySetLoginEntries(string[] entries, int length);

        [DllImport("XDSDK")]
        private static extern void UnityInit(string appid);

        [DllImport("XDSDK")]
        private static extern void UnityLogin();

        [DllImport("XDSDK")]
        private static extern void UnityLogout();

        [DllImport("XDSDK")]
        private static extern string UnityGetAccessToken();

        [DllImport("XDSDK")]
        private static extern bool UnityIsLoggedIn();

        [DllImport("XDSDK")]
        private static extern void UnityOpenRealName();

        [DllImport("XDSDK")]
        private static extern string UnityGetSdkVersion();

        [DllImport("XDSDK")]
        private static extern void UnityPay(string proudct_name, string product_id, int product_price, string sid, string role_id, string order_id, string ext);

#endif

#if UNITY_IOS && !UNITY_EDITOR

#elif UNITY_ANDROID && !UNITY_EDITOR
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
}
