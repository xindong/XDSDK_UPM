using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;  
using System.Runtime.InteropServices;

public class XDPlatform : MonoBehaviour {

	void Start()
	{

	}

	void Update () {
		
	}
		
	void OnGUI() {

		if (GUI.Button (new Rect (80, 80, 100, 100), "初始化")) {

			XD_Init ();
		}

		if(GUI.Button(new Rect(280,80,100,100),"登录")) {  

			XD_Login ();
		}  

		if(GUI.Button(new Rect(480,80,100,100),"登出")) {  

			XD_Logout ();
		}


		if (GUI.Button (new Rect (80, 200, 100, 100), "用户中心")) {

			XD_OpenUserCenter ();
		}

		if(GUI.Button(new Rect(280,200,100,100),"支付")) {  

			XD_Pay ("XDSDKPoint","XDSDKPoint","1","Sid","Role_Id","Order_Id","EXT");
		}  

		if(GUI.Button(new Rect(480,200,100,100),"账号ID")) {  

			XD_GetAccessToken ();
		}



		if (GUI.Button (new Rect (80, 320, 100, 100), "版本号")) {

			Debug.Log ("Init XDSDK");
			XD_SDKVersion ();
		}

		if(GUI.Button(new Rect(280,320,100,100),"隐藏游客")) {  

			XD_HideGuest ();
		}  

		if(GUI.Button(new Rect(480,320,100,100),"隐藏QQ")) {  

			XD_HideQQ ();
		}



		if (GUI.Button (new Rect (80, 440, 100, 100), "隐藏WX")) {

			XD_hideWeiChat ();
		}

		if(GUI.Button(new Rect(280,440,100,100),"显示VC")) {  

			XD_ShowVC ();
		}  

		if(GUI.Button(new Rect(480,440,100,100),"QQWeb")) {  

			XD_SetQQWeb ();
		}

		if (GUI.Button (new Rect (80, 560, 100, 100), "WXWeb")) {

			XD_SetWXWeb ();
		}
			
	}

	public static void XD_Init (){

		initSDK ();
	}

	public static void XD_Login (){

		login ();
	}

	public static void XD_Logout(){

		logout ();
	}

	public static void XD_OpenUserCenter(){

		openUserCenter ();
	}

	public static void XD_Pay(string proudct_name, string product_id, string product_price, string sid, string role_id, string orderid, string ext){

		pay (proudct_name,product_id,product_price,sid,role_id,orderid,ext);
	}

	public static void XD_SDKVersion(){

		sdk_debug_msg (getSDKVersion());
	}

	public static void XD_GetAccessToken(){

		getAccessToken ();

		sdk_debug_msg (getAccessToken ());
	}

	public static void XD_HideGuest(){

		hideGuest ();
	}

	public static void XD_HideQQ(){

		hideQQ ();
	}

	public static void XD_hideWeiChat(){

		hideWeiChat ();
	}

	public static void XD_ShowVC(){

		showVC ();
	}

	public static void XD_SetQQWeb(){

		setQQWeb ();
	}

	public static void XD_SetWXWeb(){

		setWXWeb ();
	}

	[DllImport("__Internal")]
	private static extern void sdk_debug_msg(string msg);

	[DllImport("__Internal")]
	private static extern void initSDK();

	[DllImport("__Internal")]
	private static extern void login();

	[DllImport("__Internal")]
	private static extern void logout();

	[DllImport("__Internal")]
	private static extern void openUserCenter();

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



	public void onInitSucceed(){

		Debug.Log ("onInitSucceed");

		sdk_debug_msg ("onInitSucceed");
	}

	public void onInitFailed(string error_msg){

		Debug.Log ("onInitFailed");

		sdk_debug_msg ("onInitFailed");
	}

	public void onLoginSucceed(string access_token){

		Debug.Log ("onLoginSucceed");

		sdk_debug_msg ("onLoginSucceed");
	}

	public void onLoginFailed(string error_msg){

		Debug.Log ("onLoginFailed");

		sdk_debug_msg ("onLoginFailed");
	}

	public void onLogoutSucceed(){

		Debug.Log ("onLogoutSucceed");

		sdk_debug_msg ("onLogoutSucceed");
	}

	public void onLoginCanceled(){

		Debug.Log ("onLoginCanceled");

		sdk_debug_msg ("onLoginCanceled");
	}

	public void onGuestBindSucceed(string access_token){

		Debug.Log ("onGuestBindSucceed");

		sdk_debug_msg ("onGuestBindSucceed");
	}

	public void onPayCompleted(){

		Debug.Log ("onPayCompleted");

		sdk_debug_msg ("onPayCompleted");
	}

	public void onPayFailed(string error_msg){

		Debug.Log ("onPayFailed");

		sdk_debug_msg ("onPayFailed");
	}

	public void onPayCanceled(){

		Debug.Log ("onPayCanceled");

		sdk_debug_msg ("onPayCanceled");
	}
}
