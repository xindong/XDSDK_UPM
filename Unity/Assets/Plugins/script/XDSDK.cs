using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System;

namespace xdsdk
{
	public class XDSDK : MonoBehaviour
	{
		void Start () {
			this.name = "XDSDK";
		}
		
		public static void SetCallback(XDCallback callback){
			XDSDKImp.GetInstance ().SetCallback (callback);
        }
			
		public static void HideGuest(){
			XDSDKImp.GetInstance ().HideGuest ();
		}

		public static void HideWX(){
			XDSDKImp.GetInstance ().HideWX ();
		}

		public static void HideQQ(){
			XDSDKImp.GetInstance ().HideQQ ();
		}

		public static void ShowVC(){
			XDSDKImp.GetInstance ().ShowVC ();
		}

		public static void SetQQWeb(){
			XDSDKImp.GetInstance ().SetQQWeb ();
		}

		public static void SetWXWeb(){
			XDSDKImp.GetInstance ().SetWXWeb ();
		}

		public static string GetSDKVersion(){
			return XDSDKImp.GetInstance ().GetSDKVersion ();

		}
        
		public static void InitSDK(string appid, int aOrientation){
			XDSDKImp.GetInstance ().InitSDK (appid, aOrientation);
        }

		public static void Login(){
			XDSDKImp.GetInstance ().Login ();
		}

		public static string GetAccessToken(){
			return XDSDKImp.GetInstance ().GetAccessToken ();
		
		}

		public static bool IsLoggedIn(){
			return XDSDKImp.GetInstance ().IsLoggedIn ();
		
		}

		public static bool OpenUserCenter(){
			return XDSDKImp.GetInstance ().OpenUserCenter ();
		
		}

		public static bool Pay(Dictionary<string, string> info){
			return XDSDKImp.GetInstance ().Pay (info);
		
		}

		public static void Logout(){
			XDSDKImp.GetInstance ().Logout ();
		}

		public static void Exit(){
			XDSDKImp.GetInstance ().Exit ();
		}



		//callback
		public void OnInitSucceed (){
			XDSDKImp.GetInstance ().GetXDCallback ().OnInitSucceed ();
		}

		public void OnInitFailed (string msg){
			XDSDKImp.GetInstance ().GetXDCallback ().OnInitFailed (msg);
		}

		public void OnLoginSucceed (string token){
			XDSDKImp.GetInstance ().GetXDCallback ().OnLoginSucceed (token);
		}

		public void OnLoginFailed (string msg){
			XDSDKImp.GetInstance ().GetXDCallback ().OnLoginFailed (msg);
		}

		public void OnLoginCanceled (){
			XDSDKImp.GetInstance ().GetXDCallback ().OnLoginCanceled ();
		}

		public void OnGuestBindSucceed (string token){
			XDSDKImp.GetInstance ().GetXDCallback ().OnGuestBindSucceed (token);
		}

		public void OnLogoutSucceed (){
			XDSDKImp.GetInstance ().GetXDCallback ().OnLogoutSucceed ();
		}

		public void OnPayCompleted (){
			XDSDKImp.GetInstance ().GetXDCallback ().OnPayCompleted ();
		}

		public void OnPayFailed (string msg){
			XDSDKImp.GetInstance ().GetXDCallback ().OnPayFailed (msg);
		}

		public void OnPayCanceled (){
			XDSDKImp.GetInstance ().GetXDCallback ().OnPayCanceled ();
		}

		public void OnExitConfirm (){
			XDSDKImp.GetInstance ().GetXDCallback ().OnExitConfirm ();
		}

		public void OnExitCancel (){
			XDSDKImp.GetInstance ().GetXDCallback ().OnExitCancel ();
		}

	}
}
