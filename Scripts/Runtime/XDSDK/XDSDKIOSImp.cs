#define USE_UNITY_XDSDK

using UnityEngine;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System;

namespace xdsdk
{
    public class XDSDKIOSImp : XDSDKInterface
    {
        private static XDSDKIOSImp _instance;

        private static XDCallback xdCallback;

        private delegate void XDSDKMessageCallback(string method,string message);
        [AOT.MonoPInvokeCallback(typeof(XDSDKMessageCallback))]
        static void XDSDKCallbackImp(string method,string message)
        {
            Debug.Log(" Unity Ios callback method = " + method + " msg = " + message);
            if (method == null ||  xdCallback == null ) return;
            switch (method)
            {
                case "OnInitSucceed":
                    xdCallback.OnInitSucceed();
                    break;
                case "OnInitFailed":
                    xdCallback.OnInitFailed(message);
                    break;
                case "OnLoginSucceed":
                    xdCallback.OnLoginSucceed(message);
                    break;
                case "OnLoginFailed":
                    xdCallback.OnLoginFailed(message);
                    break;
                case "OnLoginCanceled":
                    xdCallback.OnLoginCanceled();
                    break;
                case "OnGuestBindSucceed":
                    xdCallback.OnGuestBindSucceed(message);
                    break;
                case "OnGuestBindFailed":
                    xdCallback.OnGuestBindFailed(message);
                    break;
                case "OnRealNameSucceed":
                    xdCallback.OnRealNameSucceed();
                    break;
                case "OnRealNameFailed":
                    xdCallback.OnRealNameFailed(message);
                    break;
                case "OnLogoutSucceed":
                    xdCallback.OnLogoutSucceed();
                    break;
                case "OnPayCompleted":
                    xdCallback.OnPayCompleted();
                    break;
                case "OnPayFailed":
                    xdCallback.OnPayFailed(message);
                    break;
                case "OnPayCanceled":
                    xdCallback.OnPayCanceled();
                    break;
                case "RestoredPayment":
                    Debug.Log("RestoredPayment ： " + message);

                    List<object> resultList = (List<object>)Json.Deserialize(message);
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
                    xdCallback.RestoredPayment(paymentInfoList);
                    break;
                case "OnExitConfirm":
                    xdCallback.OnExitConfirm();
                    break;
                case "OnExitCancel":
                    xdCallback.OnExitCancel();
                    break;
                case "OnWXShareSucceed":
                    xdCallback.OnWXShareSucceed();
                    break;
                case "OnWXShareFailed":
                    xdCallback.OnWXShareFailed(message);
                    break;
                case "OnProtocolAgreed":
                    xdCallback.OnProtocolAgreed();
                    break;
                case "OnProtocolOpenSucceed":
                    xdCallback.OnProtocolOpenSucceed();
                    break;
                case "OnProtocolOpenFailed":
                    xdCallback.OnProtocolOpenFailed(message);
                    break;
                case "OnBindTaptapSucceed":
                    Dictionary<string, object> tapInfo = (Dictionary<string, object>)Json.Deserialize(message);
                    Dictionary<string, string> tapInfoString = new Dictionary<string, string>();
                    foreach (KeyValuePair<string, object> kvp in tapInfo)
                    {
                        tapInfoString[kvp.Key] = (string)kvp.Value;
                    }
                    xdCallback.OnBindTaptapSucceed(tapInfoString);
                    break;
            }

        }

        public static XDSDKIOSImp GetInstance()
        {
            if (_instance == null)
            {
                _instance = new XDSDKIOSImp();
            }
            return _instance;
        }

        public void SetCallback(XDCallback callback)
        {
            xdCallback = callback;
#if UNITY_IOS && !UNITY_EDITOR
        XDSDKSetCallback(XDSDKCallbackImp);
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
#endif
        }

        public void HideWX()
        {
#if UNITY_IOS && !UNITY_EDITOR
			hideWeiChat();
#endif
        }

        public void HideQQ()
        {
#if UNITY_IOS && !UNITY_EDITOR
			hideQQ();
#endif
        }

        public void ShowVC()
        {
#if UNITY_IOS && !UNITY_EDITOR
			showVC();
#endif
        }

        public void SetQQWeb()
        {
#if UNITY_IOS && !UNITY_EDITOR
			setQQWeb();
#endif
        }

        public void SetWXWeb()
        {
#if UNITY_IOS && !UNITY_EDITOR
			setWXWeb();
#endif
        }

        public void HideTapTap()
        {
#if UNITY_IOS && !UNITY_EDITOR
			hideTapTap();
#endif
        }

        public void SetLoginEntries(string[] entries)
        {
#if UNITY_IOS && !UNITY_EDITOR
			setLoginEntries(entries,entries.Count());
#endif
        }

        public string GetSDKVersion()
        {
#if UNITY_IOS && !UNITY_EDITOR
			return getXDSDKVersion();

#else
            return "0.0.0";
#endif

        }

        public string GetAdChannelName()
        {
#if UNITY_IOS && !UNITY_EDITOR
            return "";
#else
            return "";
#endif

        }



        public void InitSDK(string appid, int aOrientation, string channel, string version, bool enableTapDB)
        {
#if UNITY_IOS && !UNITY_EDITOR
			initXDSDK(appid,aOrientation,channel,version,enableTapDB);
#endif
        }

        public void InitSDK(string appid, int aOrientation, string channel, string version, bool enableTapDB, bool enableMoment)
        {
#if UNITY_IOS && !UNITY_EDITOR
			initXDSDKWithMoment(appid,aOrientation,channel,version,enableTapDB,enableMoment);
#endif
        }

        public void Login()
        {

#if UNITY_IOS && !UNITY_EDITOR
			xdLogin();
#endif
        }

        public void AccountCancellation()
        {
#if UNITY_IOS && !UNITY_EDITOR
         accountCancellation();
#endif 
        }
        

        public string GetAccessToken()
        {
#if UNITY_IOS && !UNITY_EDITOR
			return getXDAccessToken();
#else
            return "";
#endif

        }

        public bool IsLoggedIn()
        {
#if UNITY_IOS && !UNITY_EDITOR
			return isXdLoggedIn();
#else
            return true;
#endif
        }

        public bool OpenUserCenter()
        {
#if UNITY_IOS && !UNITY_EDITOR
			return openUserCenter();
#endif
            return false;
        }

        public void UserFeedback()
        {
#if UNITY_IOS && !UNITY_EDITOR
			userFeedback();
#endif
        }


        public void OpenRealName()
        {
#if UNITY_IOS && !UNITY_EDITOR
			openRealName();
#endif
        }

        public void OpenUserBindView()
        {
#if UNITY_IOS && !UNITY_EDITOR
            openUserBindView();
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
                                        info.ContainsKey("TransactionIdentifier") ? info["TransactionIdentifier"] : "");

#endif
        }

        public void Logout()
        {
#if UNITY_IOS && !UNITY_EDITOR
			xdLogout();
#endif
        }

        public void Exit()
        {
#if UNITY_IOS && !UNITY_EDITOR

#endif
        }

        public void Share(Dictionary<string, string> content)
        {
#if UNITY_IOS && !UNITY_EDITOR

                        string text = XDSDKUtil.DictionaryGetStringValue("text",content);
                        string bText = XDSDKUtil.DictionaryGetStringValue("bText",content);
                        string scene = XDSDKUtil.DictionaryGetStringValue("scene",content);
                        string shareType =  XDSDKUtil.DictionaryGetStringValue("shareType",content);
                        string title = XDSDKUtil.DictionaryGetStringValue("title",content);
                        string description = XDSDKUtil.DictionaryGetStringValue("description",content);
                        string thumbPath = XDSDKUtil.DictionaryGetStringValue("thumbPath",content);
                        string imageUrl =XDSDKUtil.DictionaryGetStringValue("imageUrl",content);
                        string musicUrl = XDSDKUtil.DictionaryGetStringValue("musicUrl",content);
                        string musicLowBandUrl = XDSDKUtil.DictionaryGetStringValue("musicLowBandUrl",content);
                        string musicDataUrl = XDSDKUtil.DictionaryGetStringValue("musicDataUrl",content);
                        string musicLowBandDataUrl = XDSDKUtil.DictionaryGetStringValue("musicLowBandDataUrl",content);
                        string videoUrl = XDSDKUtil.DictionaryGetStringValue("videoUrl",content);
                        string videoLowBandUrl = XDSDKUtil.DictionaryGetStringValue("videoLowBandUrl",content);
                        string webpageUrl = XDSDKUtil.DictionaryGetStringValue("webpageUrl",content);

                        Debug.Log("wx Share Dic text:" + text + "\n bText:" + bText + "\n Scene:" + scene + "\n ShareType:" + shareType + "\n title:" + title + "\n description:" + description 
                        + "\n thumbPath:" + thumbPath + "\n imageUrl:" + imageUrl +"\n musicUrl:" + musicUrl + "\n musicLowBandDataUrl:" + musicLowBandDataUrl + "\n videoUrl:" + videoUrl 
                        + "\n videoLowBandUrl:" + videoLowBandUrl + "\n webPageUrl:" + webpageUrl);

                        Debug.Log("start to Wx Share");

			xdShare(text,bText,scene,shareType,title,description,thumbPath,imageUrl,musicUrl,
			musicLowBandUrl,musicDataUrl,musicLowBandDataUrl,videoUrl,videoLowBandUrl,webpageUrl);
#endif
        }

        public void SetLevel(int level)
        {
#if UNITY_IOS && !UNITY_EDITOR

			setLevel(level);
#endif
        }

        public void SetServer(string server)
        {

#if UNITY_IOS && !UNITY_EDITOR

			setServer(server);
#endif
        }

        public void SetRole(string roleId,string roleName,string roleAvatar)
        {
                if (roleAvatar == null)
                {
                    roleAvatar = "";
                }
        #if UNITY_IOS && !UNITY_EDITOR
                XDSDKSetRole(roleId,roleName,roleAvatar);
          
#endif
        }

        public void ClearRole()
        {
        #if UNITY_IOS && !UNITY_EDITOR
                XDSDKClearRole();
          
#endif
        }

        public void AutoLogin()
        {
#if UNITY_IOS && !UNITY_EDITOR
                XDSDKAutoLogin();
#endif
        }

        public void TapTapLogin()
        {
#if UNITY_IOS && !UNITY_EDITOR
                XDSDKTapTapLogin();
#endif
        }

        public void AppleLogin()
        {
        #if UNITY_IOS && !UNITY_EDITOR
                XDSDKAppleLogin();
      
        #endif
        }

        public void GuestLogin()
        {
        #if UNITY_IOS && !UNITY_EDITOR
                XDSDKGuestLogin();
       
        #endif
        }

        public void GameStop() {
#if UNITY_IOS && !UNITY_EDITOR
                        XDSDKGameStop();
#endif
        }

        public void GameResume() {
#if UNITY_IOS && !UNITY_EDITOR
                        XDSDKGameResume();
#endif
        }

        public void OnResume(){
                #if UNITY_IOS && !UNITY_EDITOR

#endif

	}
	
	public void OnStop(){
                             #if UNITY_IOS && !UNITY_EDITOR

#endif

	}

        public void OpenProtocol(XDSDK.ProtocolType type)
        {
            int protocolType = Convert.ToInt32(type);
#if UNITY_IOS && !UNITY_EDITOR
                      OpenProtocol(protocolType);
#endif
        }

        public void OpenUserMoment(XDMomentConfig config, string xdId)
        {
            string configString = config.GetConfigString();
#if UNITY_IOS && !UNITY_EDITOR
                     OpenUserMoment(xdId,configString);
#endif
        }



#if UNITY_IOS && !UNITY_EDITOR

        [DllImport("__Internal")]
        private static extern void XDSDKSetCallback(XDSDKMessageCallback callback);

        [DllImport("__Internal")]
        private static extern void initXDSDK(string appid, int aOrientation, string channel, string version, bool enableTapdb);

        [DllImport("__Internal")]
        private static extern void initXDSDKWithMoment(string appid, int aOrientation, string channel, string version, bool enableTapdb, bool enableMoment);

        [DllImport("__Internal")]
        private static extern void setLevel(int level);

        [DllImport("__Internal")]
        private static extern void setServer(string server);

        [DllImport("__Internal")]
        private static extern void xdLogin();

       [DllImport("__Internal")]
       private static extern void  accountCancellation();

        [DllImport("__Internal")]
        private static extern bool isXdLoggedIn();

        [DllImport("__Internal")]
        private static extern void xdLogout();

        [DllImport("__Internal")]
        private static extern bool openUserCenter();

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

        [DllImport("__Internal")]
        private static extern void XDSDKSetRole(string roleId,string roleName,string avatarUrl);

        [DllImport("__Internal")]
        private static extern void XDSDKClearRole();

        [DllImport("__Internal")]
        private static extern void XDSDKAutoLogin();
        [DllImport("__Internal")]
        private static extern void XDSDKTapTapLogin();
        [DllImport("__Internal")]
        private static extern void XDSDKAppleLogin();
        [DllImport("__Internal")]
        private static extern void XDSDKGuestLogin();
        [DllImport("__Internal")]
        private static extern void XDSDKGameStop();
        [DllImport("__Internal")]
        private static extern void XDSDKGameResume();
        [DllImport("__Internal")]
        private static extern void OpenProtocol(int type);
        [DllImport("__Internal")]
        private static extern void OpenUserMoment(string xdId,string config);

#endif

    }

}
