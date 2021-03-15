using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using xdsdk;
using UnityNative.Toasts;

public class XDSDKHandler : xdsdk.XDCallback {
	IUnityNativeToasts toast;

	public XDSDKHandler(IUnityNativeToasts toast)
    {
		this.toast = toast;
    }

	public override void OnInitSucceed (){
		Debug.Log ("OnInitSucceed");
		toast.ShowShortToast("OnInitSucceed");
	}

	public override void OnInitFailed (string msg){
		Debug.Log ("OnInitFailed:" + msg);
		toast.ShowShortToast("OnInitFailed msg = " + msg);
	}

	public override void OnLoginSucceed (string token){
		Debug.Log ("OnLoginSucceed:" + token);
		toast.ShowShortToast("OnLoginSucceed:" + token);
	}

	public override void OnLoginFailed (string msg){
		Debug.Log ("OnLoginFailed:" + msg);
		toast.ShowShortToast("OnLoginFailed:" + msg);
	}

	public override void OnLoginCanceled (){
		Debug.Log ("OnLoginCanceled");
		toast.ShowShortToast("OnLoginCanceled");
	}

	public override void OnGuestBindSucceed (string token){
		Debug.Log ("OnGuestBindSucceed:" + token);
		toast.ShowShortToast("OnGuestBindSucceed: " + token);
	}

    public override void OnGuestBindFailed(string msg){
        Debug.Log("OnGuestBindFailed:" + msg);
		toast.ShowShortToast("OnGuestBindFailed:" + msg);
    }

    public override void OnLogoutSucceed (){
		Debug.Log ("OnLogoutSucceed");
		toast.ShowShortToast("OnLogoutSucceed");

	}

	public override void OnPayCompleted (){
		Debug.Log ("OnPayCompleted");
		toast.ShowShortToast("OnPayCompleted");
	}

	public override void OnPayFailed (string msg){
		Debug.Log ("OnPayFailed: " + msg);
		toast.ShowShortToast("OnPayFailed: " + msg);
	}


	public override void OnPayCanceled (){
		Debug.Log ("OnPayCanceled");
		toast.ShowShortToast("OnPayCanceled");
	}

	public override void OnExitConfirm (){
		Debug.Log ("OnExitConfirm");
		toast.ShowShortToast("OnExitConfirm");
	}

	public override void OnExitCancel (){
		Debug.Log ("OnExitCancel");
		toast.ShowShortToast("OnExitCancel");
	}

	public override void OnWXShareSucceed (){
		Debug.Log ("OnWXShareSucceed");
		toast.ShowShortToast("OnWXShareSucceed");
	}

	public override void OnWXShareFailed (string msg){
		Debug.Log ("OnWXShareFailed ： " + msg);
		toast.ShowShortToast("OnWXShareFailed ： " + msg);
	}

	public override void OnRealNameSucceed(){
		Debug.Log ("OnRealNameSucceed");
		toast.ShowShortToast("OnRealNameSucceed");
	}

	public override void OnRealNameFailed(string msg){
		Debug.Log ("OnRealNameFailed ： " + msg);
		toast.ShowShortToast("OnRealNameFailed ： " + msg);

	}

    public override void OnProtocolAgreed()
    {
		Debug.Log("OnProtocolAgreed  " );
		toast.ShowShortToast("OnProtocolAgreed");
	}

    public override void OnProtocolOpenSucceed()
    {
		Debug.Log("OnProtocolOpenSucceed " );
		//toast.ShowShortToast("OnProtocolOpenSucceed");
	}

    public override void OnProtocolOpenFailed(string msg)
    {
		Debug.Log("OnProtocolOpenFail ： " + msg);
		//toast.ShowShortToast("OnProtocolOpenFail");
	}

    public override void RestoredPayment(List<Dictionary<string,string>> resultList){
		toast.ShowShortToast("");
		Debug.Log ("RestoredPayment resultList： ");
		foreach (Dictionary<string,string> dictionary in resultList)
			{
				foreach (KeyValuePair<string, string> kvp in dictionary)
				{
						Debug.Log(kvp.Key + kvp.Value);
				}
			}


	}
}
