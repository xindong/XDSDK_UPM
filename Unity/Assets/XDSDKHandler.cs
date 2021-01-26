using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class XDSDKHandler : xdsdk.XDCallback {

	public override void OnInitSucceed (){
		Debug.Log ("OnInitSucceed");
	}

	public override void OnInitFailed (string msg){
		Debug.Log ("OnInitFailed:" + msg);
	}

	public override void OnLoginSucceed (string token){
		Debug.Log ("OnLoginSucceed:" + token);
	}

	public override void OnLoginFailed (string msg){
		Debug.Log ("OnLoginFailed:" + msg);
	}

	public override void OnLoginCanceled (){
		Debug.Log ("OnLoginCanceled");
	}

	public override void OnGuestBindSucceed (string token){
		Debug.Log ("OnGuestBindSucceed:" + token);
	}

    public override void OnGuestBindFailed(string msg){
        Debug.Log("OnGuestBindFailed:" + msg);
    }

    public override void OnLogoutSucceed (){
		Debug.Log ("OnLogoutSucceed");

	}

	public override void OnPayCompleted (){
		Debug.Log ("OnPayCompleted");
	}

	public override void OnPayFailed (string msg){
		Debug.Log ("OnPayFailed: " + msg);
	}


	public override void OnPayCanceled (){
		Debug.Log ("OnPayCanceled");
	}

	public override void OnExitConfirm (){
		Debug.Log ("OnExitConfirm");
	}

	public override void OnExitCancel (){
		Debug.Log ("OnExitCancel");
	}

	public override void OnWXShareSucceed (){
		Debug.Log ("OnWXShareSucceed");
	}

	public override void OnWXShareFailed (string msg){
		Debug.Log ("OnWXShareFailed ： " + msg);
	}

	public override void OnRealNameSucceed(){
		Debug.Log ("OnRealNameSucceed");
	}

	public override void OnRealNameFailed(string msg){
		Debug.Log ("OnRealNameFailed ： " + msg);

	}

    public override void OnProtocolAgreed()
    {
		Debug.Log("OnProtocolAgreed  " );
	}

    public override void OnProtocolOpenSucceed()
    {
		Debug.Log("OnProtocolOpenSucceed " );
	}

    public override void OnProtocolOpenFailed(string msg)
    {
		Debug.Log("OnProtocolOpenFail ： " + msg);
	}

    public override void RestoredPayment(List<Dictionary<string,string>> resultList){
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
