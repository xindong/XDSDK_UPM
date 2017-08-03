using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Demo : MonoBehaviour {


	void Update()
	{

	}

	void OnGUI()
	{
		GUIStyle myButtonStyle = new GUIStyle(GUI.skin.button);
		myButtonStyle.fontSize = 50;

		if (GUI.Button (new Rect (100, 100, 300, 100), "设置回调", myButtonStyle)){
			xdsdk.XDSDK.SetCallback (new XDSDKHandler ());
		}

		if (GUI.Button (new Rect (100, 300, 300, 100), "初始化", myButtonStyle)){
			xdsdk.XDSDK.InitSDK ("a4d6xky5gt4c80s", 1);
		}

		if (GUI.Button (new Rect (100, 500, 300, 100), "登录", myButtonStyle)){
			xdsdk.XDSDK.Login ();
		}

		if (GUI.Button (new Rect (100, 700, 300, 100), "用户中心", myButtonStyle)){
			xdsdk.XDSDK.OpenUserCenter ();
		}

		if (GUI.Button (new Rect (100, 900, 300, 100), "支付", myButtonStyle)){
			Dictionary<string, string> info = new Dictionary<string,string>();
			info.Add("OrderId", "1234567890123456789012345678901234567890");
			info.Add("Product_Price", "1");
			info.Add("EXT", "abcd|efgh|1234|5678");
			info.Add("Sid", "2");
			info.Add("Role_Id", "3");
			info.Add("Product_Id", "4");
			info.Add("Product_Name", "648大礼包");
			xdsdk.XDSDK.Pay (info);
		}

		if (GUI.Button (new Rect (100, 1100, 300, 100), "登出", myButtonStyle)){
			xdsdk.XDSDK.Logout ();
		}

		if (GUI.Button (new Rect (100, 1300, 300, 100), "是否登录", myButtonStyle)){
			Debug.Log (xdsdk.XDSDK.IsLoggedIn ());
		}

		if (GUI.Button (new Rect (100, 1500, 300, 100), "SDK版本", myButtonStyle)){
			Debug.Log (xdsdk.XDSDK.GetSDKVersion());
		}

		if (GUI.Button (new Rect (100, 1700, 300, 100), "token", myButtonStyle)){
			Debug.Log (xdsdk.XDSDK.GetAccessToken ());
		}


		if (GUI.Button (new Rect (500, 100, 300, 100), "隐藏微信", myButtonStyle)){
			xdsdk.XDSDK.HideWX();
		}

		if (GUI.Button (new Rect (500, 300, 300, 100), "隐藏QQ", myButtonStyle)){
			xdsdk.XDSDK.HideQQ();
		}

		if (GUI.Button (new Rect (500, 500, 300, 100), "显示VeryCD", myButtonStyle)){
			xdsdk.XDSDK.ShowVC();
		}

		if (GUI.Button (new Rect (500, 700, 300, 100), "隐藏游客登录", myButtonStyle)){
			xdsdk.XDSDK.HideGuest();
		}

		if (GUI.Button (new Rect (500, 900, 300, 100), "QQ Web", myButtonStyle)){
			xdsdk.XDSDK.SetQQWeb();
		}

		if (GUI.Button (new Rect (500, 1100, 300, 100), "微信 Web", myButtonStyle)){
			xdsdk.XDSDK.SetWXWeb();
		}
			
		if (GUI.Button (new Rect (500, 1300, 300, 100), "退出", myButtonStyle)){
			xdsdk.XDSDK.Exit();
		}





	}
}
