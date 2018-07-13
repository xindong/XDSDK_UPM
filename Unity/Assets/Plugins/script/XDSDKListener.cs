using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace xdsdk{
	
	public class XDSDKListener : MonoBehaviour {

		private static int OnInitSucceedCode = 1;
		private static int OnInitFailedCode = 2;
		private static int OnLoginSucceedCode = 3;
		private static int OnLoginFailedCode = 4;
		private static int OnLoginCanceledCode = 5;
		private static int OnGuestBindSucceedCode = 6;
		private static int OnRealNameSucceedCode = 7;
		private static int OnRealNameFailedCode = 8;
		private static int OnLogoutSucceedCode = 9;
		private static int OnPayCompletedCode = 10;
		private static int OnPayFailedCode = 11;
		private static int OnPayCanceledCode = 12;
		private static int OnExitConfirmCode = 13;
		private static int OnExitCancelCode = 14;
		private static int OnWXShareSucceedCode = 15;
		private static int OnWXShareFailedCode = 16;

		void Start () {
			this.name = "XDSDK";
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

		public void OnRealNameSucceed (){
			XDSDKImp.GetInstance().GetXDCallback().OnRealNameSucceed();
		}

		public void OnRealNameFailed(string msg){
			XDSDKImp.GetInstance().GetXDCallback().OnRealNameFailed(msg);
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

		public void OnWXShareSucceed (){
			XDSDKImp.GetInstance ().GetXDCallback ().OnWXShareSucceed ();
		}

		public void OnWXShareFailed (string msg){
			XDSDKImp.GetInstance ().GetXDCallback ().OnWXShareFailed (msg);
		}

		#elif UNITY_STANDALONE_WIN && !UNITY_EDITOR

		public static delegate void UniversalCallbackDelegate(int code, string msg);

		public static void UniversalCallback(int code, string msg) {
			switch (code)
			{
			case OnInitSucceedCode:
				XDSDKImp.GetInstance ().GetXDCallback ().OnInitSucceed ();
				break;
			case OnInitFailedCode:
				XDSDKImp.GetInstance ().GetXDCallback ().OnInitFailed (msg);
				break;
			case OnLoginSucceedCode:
				XDSDKImp.GetInstance ().GetXDCallback ().OnLoginSucceed (msg);
				break;
			case OnLoginFailedCode:
				XDSDKImp.GetInstance ().GetXDCallback ().OnLoginFailed (msg);
				break;
			case OnLoginCanceledCode:
				XDSDKImp.GetInstance ().GetXDCallback ().OnLoginCanceled ();
				break;
			case OnGuestBindSucceedCode:
				XDSDKImp.GetInstance ().GetXDCallback ().OnGuestBindSucceed (msg);
				break;
			case OnRealNameSucceedCode:
				XDSDKImp.GetInstance().GetXDCallback().OnRealNameSucceed();
				break;
			case OnRealNameFailedCode:
				XDSDKImp.GetInstance().GetXDCallback().OnRealNameFailed(msg);
				break;
			case OnLogoutSucceedCode:
				XDSDKImp.GetInstance ().GetXDCallback ().OnLogoutSucceed ();
				break;
			case OnPayCompletedCode:
				XDSDKImp.GetInstance ().GetXDCallback ().OnPayCompleted ();
				break;
			case OnPayFailedCode:
				XDSDKImp.GetInstance ().GetXDCallback ().OnPayFailed (msg);
				break;
			case OnPayCanceledCode:
				XDSDKImp.GetInstance ().GetXDCallback ().OnPayCanceled ();
				break;
			case OnExitConfirmCode:
				XDSDKImp.GetInstance ().GetXDCallback ().OnExitConfirm ();
				break;
			case OnExitCancelCode:
				XDSDKImp.GetInstance ().GetXDCallback ().OnExitCancel ();
				break;
			case OnWXShareSucceedCode:
				XDSDKImp.GetInstance ().GetXDCallback ().OnWXShareSucceed ();
				break;
			case OnWXShareFailedCode:
				XDSDKImp.GetInstance ().GetXDCallback ().OnWXShareFailed (msg);
				break;
			case default:
				break;
			}
		}

		#endif

	}
}
