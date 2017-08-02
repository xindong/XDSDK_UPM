using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class XDSDKHandler : xdsdk.XDCallback {

	public override void OnInitSucceed (){
		Debug.Log ("OnInitSucceed");
		
		sdk_debug_msg ("OnInitSucceed");
	}

	public override void OnInitFailed (string msg){
		Debug.Log ("OnInitFailed:" + msg);

		sdk_debug_msg ("OnInitFailed");
	}

	public override void OnLoginSucceed (string token){
		Debug.Log ("OnLoginSucceed:" + token);

		sdk_debug_msg ("OnLoginSucceed");
	}

	public override void OnLoginFailed (string msg){
		Debug.Log ("OnLoginFailed:" + msg);

		sdk_debug_msg ("OnLoginFailed");
	}

	public override void OnLoginCanceled (){
		Debug.Log ("OnLoginCanceled");

		sdk_debug_msg ("OnLoginCanceled");

	}

	public override void OnGuestBindSucceed (string token){
		Debug.Log ("OnGuestBindSucceed:" + token);
	
		sdk_debug_msg ("OnGuestBindSucceed");

	}

	public override void OnLogoutSucceed (){
		Debug.Log ("OnLogoutSucceed");
	
		sdk_debug_msg ("OnLogoutSucceed");

	}

	public override void OnPayCompleted (){
		Debug.Log ("OnPayCompleted");
	
		sdk_debug_msg ("onInitSucceed");
	}

	public override void OnPayFailed (string msg){
		Debug.Log ("OnPayFailed: " + msg);
	
		sdk_debug_msg ("onInitSucceed");
	}


	public override void OnPayCanceled (){
		Debug.Log ("OnPayCanceled");
	
		sdk_debug_msg ("OnPayCanceled");
	}

	public override void OnExitConfirm (){
		Debug.Log ("OnExitConfirm");

		sdk_debug_msg ("OnExitConfirm");
	}

	public override void OnExitCancel (){
		Debug.Log ("OnExitCancel");

		sdk_debug_msg ("OnExitCancel");
	}

	[DllImport("__Internal")]
	private static extern void sdk_debug_msg(string msg);

}
