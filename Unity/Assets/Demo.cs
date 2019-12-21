using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;
using System;
using com.taptap.sdk;

public class Demo : MonoBehaviour
{

	public static IEnumerator DelayToInvokeDo(Action action, float delaySeconds)
	{
		yield return new WaitForSeconds(delaySeconds);
		action();
	}

    void Update()
    {

    }

    void OnGUI()
    {
        GUIStyle myButtonStyle = new GUIStyle(GUI.skin.button)
        {
            fontSize = 50
        };

        GUI.depth = 0;

        if (GUI.Button(new Rect(50, 100, 300, 100), "设置回调", myButtonStyle))
        {
            xdsdk.XDSDK.SetCallback(new XDSDKHandler());
            com.xdsdk.xdtrafficcontrol.XDTrafficControl.Instance.SetCallback(new XDTCHandler());
            string[] entries = { "QQ_LOGIN", "XD_LOGIN","GUEST_LOGIN" };
            xdsdk.XDSDK.SetLoginEntries(entries);
            com.xdsdk.xdtrafficcontrol.XDTrafficControlListener.Init();
        }

        if (GUI.Button(new Rect(50, 300, 300, 100), "初始化", myButtonStyle))
        {
			// xdsdk.XDSDK.InitSDK("evnn72tle1sgkgo", 1, "UnityXDSDK", "0.0.0", true);
			xdsdk.XDSDK.InitSDK("2isp77irl1c0gc4", 1, "UnityXDSDK", "0.0.0", true);

#if !UNITY_EDITOR && !UNITY_STANDALONE_OSX && !UNITY_STANDALONE_WIN
            TapTapSDK.Instance.InitAppBoard ();
            com.xdsdk.xdtrafficcontrol.XDTrafficControlListener.Init();
#endif
        }

        if (GUI.Button (new Rect (50, 500, 300, 100), "登录", myButtonStyle)){

			xdsdk.XDSDK.Login ();
			com.xdsdk.xdlive.XDLiveListener.Init ();
		}

		if (GUI.Button (new Rect (50, 700, 300, 100), "用户中心", myButtonStyle)){
            xdsdk.XDSDK.OpenUserBindView ();
		}

		if (GUI.Button (new Rect (50, 900, 300, 100), "支付", myButtonStyle)){


			Dictionary<string, string> info = new Dictionary<string,string>();
			info.Add("OrderId", "1234567890123456789012345678901234567890");
			info.Add("Product_Price", "1");
			info.Add("EXT", "abcd|efgh|1234|5678");
			info.Add("Sid", "2");
			info.Add("Role_Id", "3");
			info.Add("Product_Id", "com.xd.ro.3d.EP04_Card73");
			info.Add("Product_Name", "648大礼包");
			xdsdk.XDSDK.Pay (info);

		}

		if (GUI.Button (new Rect (50, 1100, 300, 100), "登出", myButtonStyle)){
			xdsdk.XDSDK.Logout ();
		}

		if (GUI.Button (new Rect (50, 1300, 300, 100), "是否登录", myButtonStyle)){
			Debug.Log (xdsdk.XDSDK.IsLoggedIn ());
		}

		if (GUI.Button (new Rect (50, 1500, 300, 100), "SDK版本", myButtonStyle)){
            //Debug.Log (xdsdk.XDSDK.GetSDKVersion());
            com.xdsdk.xdtrafficcontrol.XDTrafficControl.Instance.Check("cf1j5axm7hckw48");
        }

		if (GUI.Button (new Rect (50, 1700, 300, 100), "token", myButtonStyle)){
			Debug.Log (xdsdk.XDSDK.GetAccessToken ());
		}


		if (GUI.Button (new Rect (400, 100, 300, 100), "隐藏微信", myButtonStyle)){
//            com.xdsdk.xdtrafficcontrol.XDTrafficControl.Instance.Check("appid001");
			com.xdsdk.xdlive.XDLive.Instance.OpenXDLive("dhsjolxyls840co");
			StartCoroutine(DelayToInvokeDo(() =>
				{
					Dictionary<string, object> parameters = new Dictionary<string, object>();
					parameters.Add("type", "alert");
					parameters.Add("id", "0");
					Dictionary<string, object> config = new Dictionary<string, object>();
					config.Add("image", "/images/invitation.png");
					config.Add("content", "您收到奥特曼的[size=16][color=#ffb100]私聊[/color][/size]是否返回游戏查看");
					List<object> buttons = new List<object>();
					Dictionary<string, object> no = new Dictionary<string, object>();
					no.Add("key", "no");
					no.Add("content", "忽略");
					no.Add("type", "");
					buttons.Add(no);
					Dictionary<string, object> yes = new Dictionary<string, object>();
					yes.Add("key", "yes");
					yes.Add("content", "去查看");
					yes.Add("type", "primary");
					buttons.Add(yes);
					config.Add("buttons", buttons);
					Dictionary<string, object> checkbox = new Dictionary<string, object>();
					checkbox.Add("content", "5分钟内不再提示");
					checkbox.Add("value", true);
					config.Add("checkbox", checkbox);
					parameters.Add("config", config);
					com.xdsdk.xdlive.XDLive.Instance.InvokeFunc(parameters, (params1)=>{
						Debug.Log ("Receive result from" + params1);
						com.xdsdk.xdlive.XDLive.Instance.CloseXDLive();
					});
				}, 8.0f));
        }

		if (GUI.Button (new Rect (400, 300, 300, 100), "隐藏QQ", myButtonStyle)){
			com.xdsdk.xdlive.XDLive.Instance.OpenXDLive("1");
		}

		if (GUI.Button (new Rect (400, 500, 300, 100), "显示VeryCD", myButtonStyle)){
			xdsdk.XDSDK.ShowVC();
		}

		if (GUI.Button (new Rect (400, 700, 300, 100), "隐藏游客登录", myButtonStyle)){
			xdsdk.XDSDK.HideGuest();
		}

		if (GUI.Button (new Rect (400, 900, 300, 100), "QQ Web", myButtonStyle)){
			xdsdk.XDSDK.SetQQWeb();
		}

		if (GUI.Button (new Rect (400, 1100, 300, 100), "微信 Web", myButtonStyle)){
			xdsdk.XDSDK.SetWXWeb();
		}
			
		if (GUI.Button (new Rect (400, 1300, 300, 100), "退出", myButtonStyle)){
			xdsdk.XDSDK.Exit();
		}

		if (GUI.Button (new Rect (400, 1500, 300, 100), "用户反馈", myButtonStyle)){
			xdsdk.XDSDK.UserFeedback();
		}

		if (GUI.Button (new Rect (400, 1700, 300, 100), "TapTap登录", myButtonStyle)){
			xdsdk.XDSDK.HideTapTap();
		}

		if (GUI.Button (new Rect (750, 100, 300, 100), "微信分享文字", myButtonStyle)){
			Dictionary<string, string> content = new Dictionary<string, string> ();
			content.Add ("title", "***title***");
			content.Add ("description", "***description***");
			content.Add ("text", "***text***");
			content.Add ("scene", "SESSION");
			content.Add ("shareType", "TEXT");
			xdsdk.XDSDK.Share (content);
		}

		if (GUI.Button (new Rect (750, 300, 300, 100), "微信分享图片", myButtonStyle)){
			Dictionary<string, string> content = new Dictionary<string, string> ();
			content.Add ("title", "***title***");
			content.Add ("description", "***description***");
			content.Add ("thumbPath", "/storage/emulated/0/2.png");
			content.Add ("imageUrl", "/storage/emulated/0/2.png");
			content.Add ("scene", "SESSION");
			content.Add ("shareType", "IMAGE");
			xdsdk.XDSDK.Share (content);
		}

		if (GUI.Button (new Rect (750, 500, 300, 100), "微信分享音乐", myButtonStyle)){
			Dictionary<string, string> content = new Dictionary<string, string> ();
			content.Add ("title", "***title***");
			content.Add ("description", "***description***");
			content.Add ("thumbPath", "/storage/emulated/0/2.png");
			content.Add ("musicUrl", "http://staff2.ustc.edu.cn/~wdw/softdown/index.asp/0042515_05.ANDY.mp3");
			content.Add ("scene", "SESSION");
			content.Add ("shareType", "MUSIC");
			xdsdk.XDSDK.Share (content);
		}

		if (GUI.Button (new Rect (750, 700, 300, 100), "微信分享视频", myButtonStyle)){
			Dictionary<string, string> content = new Dictionary<string, string> ();
			content.Add ("title", "***title***");
			content.Add ("description", "***description***");
			content.Add ("thumbPath", "/storage/emulated/0/2.png");
			content.Add ("videoUrl", "xd.com");
			content.Add ("scene", "SESSION");
			content.Add ("shareType", "VIDEO");
			xdsdk.XDSDK.Share (content);
		}

		if (GUI.Button (new Rect (750, 900, 300, 100), "微信分享链接", myButtonStyle)){
			Dictionary<string, string> content = new Dictionary<string, string> ();
			content.Add ("title", "***title***");
			content.Add ("description", "***description***");
			content.Add ("thumbPath", "/storage/emulated/0/2.png");
			content.Add ("webpageUrl", "xd.com");
			content.Add ("scene", "SESSION");
			content.Add ("shareType", "WEB");
			xdsdk.XDSDK.Share (content);
		}

		if (GUI.Button (new Rect (750, 1100, 300, 100), "实名认证", myButtonStyle)){
			xdsdk.XDSDK.OpenRealName();
		}

		if (GUI.Button (new Rect (750, 1300, 300, 100), "论坛", myButtonStyle)){
			TapTapSDK.Instance.OpenTapTapForum("58881");
		}

		if (GUI.Button (new Rect (750, 1500, 300, 100), "公告", myButtonStyle)){
			// TapTapSDK.Instance.OpenAppBoard("58881");
			xdsdk.XDSDK.OnStop();
		}

		if (GUI.Button (new Rect (750, 1700, 300, 100), "直播", myButtonStyle)) {
			// Debug.Log (xdsdk.XDSDK.GetAdChannelName());

			xdsdk.XDSDK.OnResume();

			// Dictionary<string, string> info = new Dictionary<string,string>();
			// info.Add("OrderId", "1234567890123456789012345678901234567890");
			// info.Add("Product_Price", "1");
			// info.Add("EXT", "abcd|efgh|1234|5678");
			// info.Add("Sid", "2");
			// info.Add("Role_Id", "3");
			// info.Add("Product_Id", "4");
			// info.Add("Product_Name", "648大礼包");
			// info.Add ("transactionIdentifier", "123456789");
			// xdsdk.XDSDK.RestorePay (info);
            // com.xdsdk.xdlive.XDLive.Instance.OpenXDLive("1","xcc://events/redpoint?path=videos.307");
		}

		if (GUI.Button (new Rect (750, 1900, 300, 100), "退出", myButtonStyle)) {
			#if UNITY_EDITOR

			#else
			Application.Quit();
			#endif
		}



	}
}
