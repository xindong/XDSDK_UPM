using UnityEngine;
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
		
		public static XDSDKImp GetInstance() {
			if (_instance == null) {
				_instance = new XDSDKImp ();
			}
			return _instance;
		}

		public void SetCallback(XDCallback callback){
			xdCallback = callback;
		}

		public XDCallback GetXDCallback(){
			return xdCallback;
		}

		public void HideGuest(){
			#if UNITY_IOS && !UNITY_EDITOR
			hideGuest();

			#elif UNITY_ANDROID && !UNITY_EDITOR
			AndroidJavaClass jc = new AndroidJavaClass("com.xd.unitysdk.UnitySDK");
			jc.CallStatic ("hideGuest");
			#endif
		}

		public void HideWX(){
			#if UNITY_IOS && !UNITY_EDITOR
			hideWeiChat();

			#elif UNITY_ANDROID && !UNITY_EDITOR
			AndroidJavaClass jc = new AndroidJavaClass("com.xd.unitysdk.UnitySDK");
			jc.CallStatic ("hideWX");
			#endif
		}

		public void HideQQ(){
			#if UNITY_IOS && !UNITY_EDITOR
			hideQQ();

			#elif UNITY_ANDROID && !UNITY_EDITOR
			AndroidJavaClass jc = new AndroidJavaClass("com.xd.unitysdk.UnitySDK");
			jc.CallStatic ("hideQQ");
			#endif
		}

		public void ShowVC(){
			#if UNITY_IOS && !UNITY_EDITOR
			showVC();

			#elif UNITY_ANDROID && !UNITY_EDITOR
			AndroidJavaClass jc = new AndroidJavaClass("com.xd.unitysdk.UnitySDK");
			jc.CallStatic ("showVC");
			#endif
		}

		public void SetQQWeb(){
			#if UNITY_IOS && !UNITY_EDITOR
			setQQWeb();

			#elif UNITY_ANDROID && !UNITY_EDITOR
			AndroidJavaClass jc = new AndroidJavaClass("com.xd.unitysdk.UnitySDK");
			jc.CallStatic ("setQQWeb");
			#endif
		}

		public void SetWXWeb(){
			#if UNITY_IOS && !UNITY_EDITOR
			setWXWeb();

			#elif UNITY_ANDROID && !UNITY_EDITOR
			AndroidJavaClass jc = new AndroidJavaClass("com.xd.unitysdk.UnitySDK");
			jc.CallStatic ("setWXWeb");
			#endif
		}

		public void ShowTapTap(){
			#if UNITY_IOS && !UNITY_EDITOR


			#elif UNITY_ANDROID && !UNITY_EDITOR
			AndroidJavaClass jc = new AndroidJavaClass("com.xd.unitysdk.UnitySDK");
			jc.CallStatic ("showTapTap");
			#endif
		}

		public string GetSDKVersion(){
			#if UNITY_IOS && !UNITY_EDITOR
			return getSDKVersion();

			#elif UNITY_ANDROID && !UNITY_EDITOR
			AndroidJavaClass jc = new AndroidJavaClass("com.xd.unitysdk.UnitySDK");
			return jc.CallStatic<string> ("getSDKVersion");
			#endif
			return "0.0.0";
		}

		public void InitSDK(string appid, int aOrientation){
			#if UNITY_IOS && !UNITY_EDITOR
			initSDK(appid,aOrientation);

			#elif UNITY_ANDROID && !UNITY_EDITOR
			AndroidJavaClass jc = new AndroidJavaClass("com.xd.unitysdk.UnitySDK");
			jc.CallStatic ("initSDK",appid, aOrientation);
			#endif
		}

		public void Login(){

			#if UNITY_IOS && !UNITY_EDITOR
			login();

			#elif UNITY_ANDROID && !UNITY_EDITOR
			AndroidJavaClass jc = new AndroidJavaClass("com.xd.unitysdk.UnitySDK");
			jc.CallStatic ("login");
			#endif
		}

		public string GetAccessToken(){
			#if UNITY_IOS && !UNITY_EDITOR
			return getAccessToken();

			#elif UNITY_ANDROID && !UNITY_EDITOR
			AndroidJavaClass jc = new AndroidJavaClass("com.xd.unitysdk.UnitySDK");
			return jc.CallStatic<string> ("getAccessToken");
			#endif
			return "";
		}

		public bool IsLoggedIn(){
			#if UNITY_IOS && !UNITY_EDITOR
			return isLoggedIn();

			#elif UNITY_ANDROID && !UNITY_EDITOR
			AndroidJavaClass jc = new AndroidJavaClass("com.xd.unitysdk.UnitySDK");
			return jc.CallStatic<bool> ("isLoggedIn");
			#endif
			return true;
		}

		public bool OpenUserCenter(){
			#if UNITY_IOS && !UNITY_EDITOR
			return openUserCenter();

			#elif UNITY_ANDROID && !UNITY_EDITOR
			AndroidJavaClass jc = new AndroidJavaClass("com.xd.unitysdk.UnitySDK");
			return jc.CallStatic<bool> ("openUserCenter");
			#endif
			return false;
		}

		public bool UserFeedback(){
			#if UNITY_IOS && !UNITY_EDITOR


			#elif UNITY_ANDROID && !UNITY_EDITOR
			AndroidJavaClass jc = new AndroidJavaClass("com.xd.unitysdk.UnitySDK");
			jc.CallStatic ("userFeedback");
			#endif
			return false;
		}

		public bool Pay(Dictionary<string, string> info){

			#if UNITY_IOS && !UNITY_EDITOR
			pay(info["Product_Name"],info["Product_Id"],info["Product_Price"],info["Sid"],info["Role_Id"],info["OrderId"],info["EXT"]);

			#elif UNITY_ANDROID && !UNITY_EDITOR
			AndroidJavaClass jc = new AndroidJavaClass("com.xd.unitysdk.UnitySDK");
			return jc.CallStatic<bool> ("pay", DicToMap(info));
			#endif
			return false;

		}

		public void Logout(){
			#if UNITY_IOS && !UNITY_EDITOR
			logout();

			#elif UNITY_ANDROID && !UNITY_EDITOR
			AndroidJavaClass jc = new AndroidJavaClass("com.xd.unitysdk.UnitySDK");
			jc.CallStatic ("logout");

			#endif
		}

		public void Exit(){
			#if UNITY_IOS && !UNITY_EDITOR


			#elif UNITY_ANDROID && !UNITY_EDITOR
			AndroidJavaClass jc = new AndroidJavaClass("com.xd.unitysdk.UnitySDK");
			jc.CallStatic ("exit");
			#endif
		}

		public void Share(Dictionary<string, string> content){
			#if UNITY_IOS && !UNITY_EDITOR

			share(content["text"],content["bText"],content["scene"],content["shareType"],content["title"],content["description"],content["thumbPath"],content["imageUrl"],content["musicUrl"],
			content["musicLowBandUrl"],content["musicDataUrl"],content["musicLowBandDataUrl"],content["videoUrl"],content["videoLowBandUrl"],content["webpageUrl"]);

			#elif UNITY_ANDROID && !UNITY_EDITOR
			AndroidJavaClass jc = new AndroidJavaClass("com.xd.unitysdk.UnitySDK");
			jc.CallStatic ("shareToWX", DicToMap(content));
			#endif
		}
						

		[DllImport("__Internal")]
		private static extern void initSDK(string appid, int aOrientation);

		[DllImport("__Internal")]
		private static extern void login();

		[DllImport("__Internal")]
		private static extern bool isLoggedIn();

		[DllImport("__Internal")]
		private static extern void logout();

		[DllImport("__Internal")]
		private static extern bool openUserCenter();

		[DllImport("__Internal")]
		private static extern void pay(string proudct_name, string product_id, string product_price, string sid, string role_id, string orderid, string ext);

		[DllImport("__Internal")]
		private static extern string getSDKVersion();

		[DllImport("__Internal")]
		private static extern string getAccessToken();

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
		private static extern void share (string text, string bText, string scene, string shareType, string title,string description, string thumbPath, 
			string imageUrl, string musicUrl, string musicLowBandUrl, string musicDataUrl, string musicLowBandDataUrl, string videoUrl,string videoLowBandUrl,
			string webPageUrl
		);


		#if UNITY_IOS && !UNITY_EDITOR

		#elif UNITY_ANDROID && !UNITY_EDITOR
		public static AndroidJavaObject DicToMap(Dictionary<string, string> dictionary)
		{
			if(dictionary == null)
			{
				return null;
			}
			AndroidJavaObject map = new AndroidJavaObject("java.util.HashMap");
			foreach(KeyValuePair<string, string> pair in dictionary)
			{
				map.Call<string>("put", pair.Key, pair.Value);
			}
			return map;
		}
		#endif

	}

}




