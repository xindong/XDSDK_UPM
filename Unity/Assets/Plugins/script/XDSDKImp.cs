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


			#elif UNITY_ANDROID && !UNITY_EDITOR
			AndroidJavaClass jc = new AndroidJavaClass("com.xd.unitysdk.UnitySDK");
			jc.CallStatic ("hideGuest");
			#endif
		}

		public void HideWX(){
			#if UNITY_IOS && !UNITY_EDITOR


			#elif UNITY_ANDROID && !UNITY_EDITOR
			AndroidJavaClass jc = new AndroidJavaClass("com.xd.unitysdk.UnitySDK");
			jc.CallStatic ("hideWX");
			#endif
		}

		public void HideQQ(){
			#if UNITY_IOS && !UNITY_EDITOR


			#elif UNITY_ANDROID && !UNITY_EDITOR
			AndroidJavaClass jc = new AndroidJavaClass("com.xd.unitysdk.UnitySDK");
			jc.CallStatic ("hideQQ");
			#endif
		}

		public void ShowVC(){
			#if UNITY_IOS && !UNITY_EDITOR


			#elif UNITY_ANDROID && !UNITY_EDITOR
			AndroidJavaClass jc = new AndroidJavaClass("com.xd.unitysdk.UnitySDK");
			jc.CallStatic ("showVC");
			#endif
		}

		public void SetQQWeb(){
			#if UNITY_IOS && !UNITY_EDITOR


			#elif UNITY_ANDROID && !UNITY_EDITOR
			AndroidJavaClass jc = new AndroidJavaClass("com.xd.unitysdk.UnitySDK");
			jc.CallStatic ("setQQWeb");
			#endif
		}

		public void SetWXWeb(){
			#if UNITY_IOS && !UNITY_EDITOR


			#elif UNITY_ANDROID && !UNITY_EDITOR
			AndroidJavaClass jc = new AndroidJavaClass("com.xd.unitysdk.UnitySDK");
			jc.CallStatic ("setWXWeb");
			#endif
		}

		public string GetSDKVersion(){
			#if UNITY_IOS && !UNITY_EDITOR


			#elif UNITY_ANDROID && !UNITY_EDITOR
			AndroidJavaClass jc = new AndroidJavaClass("com.xd.unitysdk.UnitySDK");
			return jc.CallStatic<string> ("getSDKVersion");
			#endif
			return "0.0.0";
		}

		public void Init(string appid, int aOrientation){
			#if UNITY_IOS && !UNITY_EDITOR


			#elif UNITY_ANDROID && !UNITY_EDITOR
			AndroidJavaClass jc = new AndroidJavaClass("com.xd.unitysdk.UnitySDK");
			jc.CallStatic ("initSDK","a4d6xky5gt4c80s", 1);
			#endif
		}

		public void Login(){

			#if UNITY_IOS && !UNITY_EDITOR


			#elif UNITY_ANDROID && !UNITY_EDITOR
			AndroidJavaClass jc = new AndroidJavaClass("com.xd.unitysdk.UnitySDK");
			jc.CallStatic ("login");
			#endif
		}

		public string GetAccessToken(){
			#if UNITY_IOS && !UNITY_EDITOR


			#elif UNITY_ANDROID && !UNITY_EDITOR
			AndroidJavaClass jc = new AndroidJavaClass("com.xd.unitysdk.UnitySDK");
			return jc.CallStatic<string> ("getAccessToken");
			#endif
			return "";
		}

		public bool IsLoggedIn(){
			#if UNITY_IOS && !UNITY_EDITOR


			#elif UNITY_ANDROID && !UNITY_EDITOR
			AndroidJavaClass jc = new AndroidJavaClass("com.xd.unitysdk.UnitySDK");
			return jc.CallStatic<bool> ("isLoggedIn");
			#endif
			return true;
		}

		public bool OpenUserCenter(){
			#if UNITY_IOS && !UNITY_EDITOR


			#elif UNITY_ANDROID && !UNITY_EDITOR
			AndroidJavaClass jc = new AndroidJavaClass("com.xd.unitysdk.UnitySDK");
			return jc.CallStatic<bool> ("openUserCenter");
			#endif
			return false;
		}

		public bool Pay(Dictionary<string, string> info){

			#if UNITY_IOS && !UNITY_EDITOR

			#elif UNITY_ANDROID && !UNITY_EDITOR
			AndroidJavaClass jc = new AndroidJavaClass("com.xd.unitysdk.UnitySDK");
			return jc.CallStatic<bool> ("pay", DicToMap(info));
			#endif
			return false;

		}

		public void Logout(){
			#if UNITY_IOS && !UNITY_EDITOR


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




