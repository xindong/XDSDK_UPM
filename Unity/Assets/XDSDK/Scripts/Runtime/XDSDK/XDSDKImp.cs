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

#elif (UNITY_EDITOR || UNITY_STANDALONE_OSX || UNITY_STANDALONE_WIN) && PC_VERSION
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

        }

        public void HideWX()
        {

        }

        public void HideQQ()
        {

        }

        public void ShowVC()
        {

        }

        public void SetQQWeb()
        {

        }

        public void SetWXWeb()
        {

        }

        public void HideTapTap()
        {

        }

        public void SetLoginEntries(string[] entries)
        {

#if UNITY_STANDALONE_WIN && !UNITY_EDITOR && !USE_UNITY_XDSDK

			UnitySetLoginEntries(entries, entries.Count());

#elif (UNITY_EDITOR || UNITY_STANDALONE_OSX || UNITY_STANDALONE_WIN) && PC_VERSION

            Unity.XDSDK.SetLoginEntries(entries);
#endif
        }

        public string GetSDKVersion()
        {
#if UNITY_STANDALONE_WIN && !UNITY_EDITOR && !USE_UNITY_XDSDK
			return UnityGetSdkVersion();

#elif (UNITY_EDITOR || UNITY_STANDALONE_OSX || UNITY_STANDALONE_WIN) && PC_VERSION
            return Unity.XDSDK.VERSION;
#else
            return "0.0.0";
#endif

        }

        public string GetAdChannelName()
        {

#if UNITY_STANDALONE_WIN && !UNITY_EDITOR && !USE_UNITY_XDSDK
            return "";

#elif (UNITY_EDITOR || UNITY_STANDALONE_OSX || UNITY_STANDALONE_WIN) && PC_VERSION
            return "";

#else
            return "";
#endif

        }



        public void InitSDK(string appid, int aOrientation, string channel, string version, bool enableTapDB)
        {

#if UNITY_STANDALONE_WIN && !UNITY_EDITOR && !USE_UNITY_XDSDK

			UnityInit(appid);

#elif (UNITY_EDITOR || UNITY_STANDALONE_OSX || UNITY_STANDALONE_WIN) && PC_VERSION
            Unity.XDSDK.Init(appid);

#endif
        }

        public void Login()
        {

#if UNITY_STANDALONE_WIN && !UNITY_EDITOR && !USE_UNITY_XDSDK

			UnityLogin();

#elif (UNITY_EDITOR || UNITY_STANDALONE_OSX || UNITY_STANDALONE_WIN) && PC_VERSION
            Unity.XDSDK.Login();
#endif
        }

        public string GetAccessToken()
        {

#if UNITY_STANDALONE_WIN && !UNITY_EDITOR && !USE_UNITY_XDSDK

			return UnityGetAccessToken();

#elif (UNITY_EDITOR || UNITY_STANDALONE_OSX || UNITY_STANDALONE_WIN) && PC_VERSION
            return Unity.XDSDK.GetAccessToken();
#else
            return "";
#endif

        }

        public bool IsLoggedIn()
        {

#if UNITY_STANDALONE_WIN && !UNITY_EDITOR && !USE_UNITY_XDSDK

			return UnityIsLoggedIn();

#elif (UNITY_EDITOR || UNITY_STANDALONE_OSX || UNITY_STANDALONE_WIN) && PC_VERSION
            return !string.IsNullOrEmpty(Unity.XDSDK.GetAccessToken());
#else
            return true;
#endif
        }

        public bool OpenUserCenter()
        {
            return false;
        }

        public void UserFeedback()
        {

        }


        public void OpenRealName()
        {

#if UNITY_STANDALONE_WIN && !UNITY_EDITOR && !USE_UNITY_XDSDK

			UnityOpenRealName();

#elif (UNITY_EDITOR || UNITY_STANDALONE_OSX || UNITY_STANDALONE_WIN) && PC_VERSION
            Unity.XDSDK.OpenRealName();

#endif
        }

        public void OpenUserBindView()
        {

        }

        public bool Pay(Dictionary<string, string> info)
        {

#if UNITY_STANDALONE_WIN && !UNITY_EDITOR && !USE_UNITY_XDSDK

			UnityPay(info.ContainsKey("Product_Name") ? info["Product_Name"] : "",
					info.ContainsKey("Product_Id") ? info["Product_Id"] : "",
					info.ContainsKey("Product_Price") ? int.Parse(info["Product_Price"]) : 0,
					info.ContainsKey("Sid") ? info["Sid"] : "",
					info.ContainsKey("Role_Id") ? info["Role_Id"] : "",
					info.ContainsKey("OrderId") ? info["OrderId"] : "",
					info.ContainsKey("EXT") ? info["EXT"] : "");

#elif (UNITY_EDITOR || UNITY_STANDALONE_OSX || UNITY_STANDALONE_WIN) && PC_VERSION
            Unity.XDSDK.Pay(info);

#endif
            return false;

        }

        public void RestorePay(Dictionary<string, string> info)
        {

        }

        public void Logout()
        {


#if UNITY_STANDALONE_WIN && !UNITY_EDITOR  && !USE_UNITY_XDSDK

			UnityLogout();

#elif (UNITY_EDITOR || UNITY_STANDALONE_OSX || UNITY_STANDALONE_WIN) && PC_VERSION
            Unity.XDSDK.Logout();

#endif
        }

        public void Exit()
        {

        }

        public void Share(Dictionary<string, string> content)
        {

        }

        public void SetLevel(int level)
        {

        }

        public void SetServer(string server)
        {

        }

        public void SetRole(string roleId,string roleName,string roleAvatar)
        {
                if (roleAvatar == null)
                {
                    roleAvatar = "";
                }
        }

        public void ClearRole()
        {

        }

        public void AutoLogin()
        {

        }

        public void TapTapLogin()
        {

        }

        public void AppleLogin()
        {

        }

        public void GuestLogin()
        {

        }

        public void GameStop() {

        }

        public void GameResume() {

        }

        public void OnResume(){
           
#if UNITY_STANDALONE_WIN && !UNITY_EDITOR && !USE_UNITY_XDSDK

#elif (UNITY_EDITOR || UNITY_STANDALONE_OSX || UNITY_STANDALONE_WIN) && PC_VERSION

#else
#endif

	}
	
	public void OnStop(){
            
#if UNITY_STANDALONE_WIN && !UNITY_EDITOR && !USE_UNITY_XDSDK

#elif (UNITY_EDITOR || UNITY_STANDALONE_OSX || UNITY_STANDALONE_WIN) && PC_VERSION

#else
#endif

	}

        public void OpenProtocol(XDSDK.ProtocolType type)
        {
            int protocolType = Convert.ToInt32(type);

        }

        public void OpenUserMoment(XDMomentConfig config, string xdId)
        {
            string configString = config.GetConfigString();

        }


#if UNITY_STANDALONE_WIN && !UNITY_EDITOR

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
 
    }
}
