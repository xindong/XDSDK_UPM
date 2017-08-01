using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XDSDKHandler : xdsdk.XDCallback {

	public override void OnInitSucceed (){
		Debug.Log ("onInitSucceed");
	}

	public override void OnInitFailed (string msg){
		Debug.Log ("onInitFailed:" + msg);
	}

	public override void OnLoginSucceed (string token){
		Debug.Log ("onLoginSucceed:" + token);
	}

	public override void OnLoginFailed (string msg){
		Debug.Log ("onLoginFailed:" + msg);
	}

	public override void OnLoginCanceled (){
		Debug.Log ("onLoginCanceled");
	}

	public override void OnGuestBindSucceed (string token){
		Debug.Log ("onGuestBindSucceed:" + token);
	}

	public override void OnLogoutSucceed (){
		Debug.Log ("onLogoutSucceed");
	}

	public override void OnPayCompleted (){
		Debug.Log ("onPayCompleted");
	}

	public override void OnPayFailed (string msg){
		Debug.Log ("onPayFailed: " + msg);
	}


	public override void OnPayCanceled (){
		Debug.Log ("onPayCanceled");
	}

	public override void OnExitConfirm (){
		Debug.Log ("onExitConfirm");
	}

	public override void OnExitCancel (){
		Debug.Log ("onExitCancel");
	}

}
