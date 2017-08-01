using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class XDSDKHandler : xdsdk.XDCallback {

	public override void OnInitSucceed (){
		
		sdk_debug_msg ("OnInitSucceed");
	}

	public override void OnInitFailed (string msg){
		Debug.Log ("onInitFailed:" + msg);

		sdk_debug_msg ("OnInitFailed");
	}

	public override void OnLoginSucceed (string token){
		Debug.Log ("onLoginSucceed:" + token);

		sdk_debug_msg ("OnLoginSucceed");
	}

	public override void OnLoginFailed (string msg){
		Debug.Log ("onLoginFailed:" + msg);

		sdk_debug_msg ("OnLoginFailed");
	}

	public override void OnLoginCanceled (){
		Debug.Log ("onLoginCanceled");
		sdk_debug_msg ("OnLoginCanceled");

	}

	public override void OnGuestBindSucceed (string token){
		Debug.Log ("onGuestBindSucceed:" + token);
	
		sdk_debug_msg ("OnGuestBindSucceed");

	}

	public override void OnLogoutSucceed (){
		Debug.Log ("onLogoutSucceed");
	
		sdk_debug_msg ("OnLogoutSucceed");

	}

	public override void OnPayCompleted (){
		Debug.Log ("onPayCompleted");
	
		sdk_debug_msg ("onInitSucceed");
	}

	public override void OnPayFailed (string msg){
		Debug.Log ("onPayFailed: " + msg);
	
		sdk_debug_msg ("onInitSucceed");
	}


	public override void OnPayCanceled (){
		Debug.Log ("onPayCanceled");
	
		sdk_debug_msg ("OnPayCanceled");
	}

	public override void OnExitConfirm (){
		Debug.Log ("onExitConfirm");

		sdk_debug_msg ("OnExitConfirm");
	}

	public override void OnExitCancel (){
		Debug.Log ("onExitCancel");

		sdk_debug_msg ("OnExitCancel");
	}

	[DllImport("__Internal")]
	private static extern void sdk_debug_msg(string msg);

}
