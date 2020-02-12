using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace xdsdk{
	
	public class XDSDKListener : MonoBehaviour {

		private const int OnInitSucceedCode = 1;
		private const int OnInitFailedCode = 2;
		private const int OnLoginSucceedCode = 3;
		private const int OnLoginFailedCode = 4;
		private const int OnLoginCanceledCode = 5;
		private const int OnGuestBindSucceedCode = 6;
		private const int OnRealNameSucceedCode = 7;
		private const int OnRealNameFailedCode = 8;
		private const int OnLogoutSucceedCode = 9;
		private const int OnPayCompletedCode = 10;
		private const int OnPayFailedCode = 11;
		private const int OnPayCanceledCode = 12;
		private const int OnExitConfirmCode = 13;
		private const int OnExitCancelCode = 14;
		private const int OnWXShareSucceedCode = 15;
		private const int OnWXShareFailedCode = 16;

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

        public void OnGuestBindFailed(string msg)
        {
            XDSDKImp.GetInstance().GetXDCallback().OnGuestBindFailed(msg);
        }

        public void OnRealNameSucceed (){
			XDSDKImp.GetInstance().GetXDCallback().OnRealNameSucceed();
		}

        public void OnRealNameFailed ()
        {
            XDSDKImp.GetInstance().GetXDCallback().OnRealNameFailed("");
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

		public void RestoredPayment (string msg){
			Debug.Log ("RestoredPayment ： " + msg);

			List<object> resultList = (List<object>)XDMiniJSON.Json.Deserialize(msg);
			List<Dictionary<string,string>> paymentInfoList = new List<Dictionary<string,string>>();
			foreach (Dictionary<string,object> dict in resultList)
			{
				Dictionary<string,string> onePaymentInfo = new Dictionary<string,string>();

				foreach (KeyValuePair<string,object> kvp in dict)
				{
					onePaymentInfo[kvp.Key] = (string)kvp.Value;
				}

				paymentInfoList.Add(onePaymentInfo);
			}

			XDSDKImp.GetInstance ().GetXDCallback ().RestoredPayment (paymentInfoList);

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

		#if UNITY_STANDALONE_WIN && !UNITY_EDITOR

		public delegate void UniversalCallbackDelegate(int code, string msg);

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
			default:
				break;
			}
		}

		#endif

	}
}
