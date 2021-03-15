﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;
using System;
using com.taptap.sdk;
#if PLATFORM_ANDROID
using UnityEngine.Android;
#endif

using UnityNative.Toasts;


public class Demo : MonoBehaviour
{
	private String amount = "1";
	private String product_id = "product_id";
	private String sceneId = "taprl0137852001";
	private string imagePath = "";
	IUnityNativeToasts toast = UnityNativeToasts.Create();

	public static IEnumerator DelayToInvokeDo(Action action, float delaySeconds)
	{
		yield return new WaitForSeconds(delaySeconds);
		action();
	}


	void Start()
	{
	}

    void Update()
    {

    }

	void OnApplicationPause(bool pauseStatus)
    {
        if (pauseStatus)
        {
			xdsdk.XDSDK.OnStop();
        }
        else
        {
			xdsdk.XDSDK.OnResume();
        }
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
            xdsdk.XDSDK.SetCallback(new XDSDKHandler(toast));
            com.xdsdk.xdtrafficcontrol.XDTrafficControl.Instance.SetCallback(new XDTCHandler(toast));
			TapSDK.TDSMoment.SetCallback((code,msg) =>
			{
                switch (code)
                {
					case 20000:
						toast.ShowShortToast("获取小红点：" + msg);
						break;
					case 20100:
						toast.ShowShortToast("获取小红点失败：" + msg);
						break;
				}
			});
            //string[] entries = { "QQ_LOGIN", "XD_LOGIN", "GUEST_LOGIN", "WX_LOGIN", "APPLE_LOGIN" };

            //xdsdk.XDSDK.SetLoginEntries(entries);
        }

        if (GUI.Button(new Rect(50, 200, 300, 100), "横屏初始化", myButtonStyle))
        {
			//ios d4bjgwom9zk84wk evnn72tle1sgkgo a4d6xky5gt4c80s
#if UNITY_ANDROID && !UNITY_EDITOR
			xdsdk.XDSDK.InitSDK("a4d6xky5gt4c80s", 0, "XDSDK android", "androidversion", true, true);
#else
			xdsdk.XDSDK.InitSDK("d4bjgwom9zk84wk", 0, "XDSDK ios", "iosversion", true, true);
#endif

			// xdsdk.XDSDK.InitSDK("2isp77irl1c0gc4", 1, "UnityXDSDK", "0.0.0", true);

		}

        if (GUI.Button (new Rect (50, 300, 300, 100), "登录", myButtonStyle)){

			xdsdk.XDSDK.Login ();
			//com.xdsdk.xdlive.XDLiveListener.Init ();
		}

		if (GUI.Button (new Rect (50, 400, 300, 100), "用户中心", myButtonStyle)){
			xdsdk.XDSDK.OpenUserCenter();
		}

		if (GUI.Button (new Rect (50, 500, 300, 100), "支付", myButtonStyle)){


			Dictionary<string, string> info = new Dictionary<string,string>();
			info.Add("OrderId", "1234567890123456789012345678901234567890");
			info.Add("Product_Price", "1");
			info.Add("EXT", "abcd|efgh|1234|5678");
			info.Add("Sid", "2");
			info.Add("Role_Id", "3");
			info.Add("Product_Id", "com.xd.sdkdemo1.stone300");
			info.Add("Product_Name", "648大礼包");
			xdsdk.XDSDK.Pay (info);

		}

		if (GUI.Button (new Rect (50, 600, 300, 100), "登出", myButtonStyle)){
			xdsdk.XDSDK.ClearRole();
			xdsdk.XDSDK.Logout();
		}

		if (GUI.Button (new Rect (50, 700, 300, 100), "是否登录", myButtonStyle)){
			Debug.Log (xdsdk.XDSDK.IsLoggedIn ());
		}

		if (GUI.Button (new Rect (50, 800, 300, 100), "版本号", myButtonStyle)){
			//Debug.Log(xdsdk.XDSDK.GetSDKVersion());
			//com.xdsdk.xdtrafficcontrol.XDTrafficControl.Instance.Check("cf1j5axm7hckw48");

			toast.ShowShortToast("当前版本：" + xdsdk.XDSDK.GetSDKVersion());

		}

		if (GUI.Button (new Rect (50, 900, 300, 100), "token", myButtonStyle)){
			Debug.Log (xdsdk.XDSDK.GetAccessToken ());
		}

		if (GUI.Button(new Rect(50, 1000, 300, 100), "竖屏初始化", myButtonStyle))
		{
#if UNITY_ANDROID && !UNITY_EDITOR
			xdsdk.XDSDK.InitSDK("a4d6xky5gt4c80s", 1, "XDSDK android", "androidversion", true, true);
#else
			xdsdk.XDSDK.InitSDK("d4bjgwom9zk84wk", 1, "XDSDK ios", "iosversion", true, true);
#endif
		}


		if (GUI.Button (new Rect (400, 100, 300, 100), "5s关闭直播", myButtonStyle)){
			//            com.xdsdk.xdtrafficcontrol.XDTrafficControl.Instance.Check("appid001");
			//com.xdsdk.xdlive.XDLive.Instance.OpenXDLive("dhsjolxyls840co");
			//StartCoroutine(DelayToInvokeDo(() =>
			//	{
			//		Dictionary<string, object> parameters = new Dictionary<string, object>();
			//		parameters.Add("type", "alert");
			//		parameters.Add("id", "0");
			//		Dictionary<string, object> config = new Dictionary<string, object>();
			//		config.Add("image", "/images/invitation.png");
			//		config.Add("content", "您收到奥特曼的[size=16][color=#ffb100]私聊[/color][/size]是否返回游戏查看");
			//		List<object> buttons = new List<object>();
			//		Dictionary<string, object> no = new Dictionary<string, object>();
			//		no.Add("key", "no");
			//		no.Add("content", "忽略");
			//		no.Add("type", "");
			//		buttons.Add(no);
			//		Dictionary<string, object> yes = new Dictionary<string, object>();
			//		yes.Add("key", "yes");
			//		yes.Add("content", "去查看");
			//		yes.Add("type", "primary");
			//		buttons.Add(yes);
			//		config.Add("buttons", buttons);
			//		Dictionary<string, object> checkbox = new Dictionary<string, object>();
			//		checkbox.Add("content", "5分钟内不再提示");
			//		checkbox.Add("value", true);
			//		config.Add("checkbox", checkbox);
			//		parameters.Add("config", config);
			//		com.xdsdk.xdlive.XDLive.Instance.InvokeFunc(parameters, (params1)=>{
			//			Debug.Log ("Receive result from" + params1);
			//			com.xdsdk.xdlive.XDLive.Instance.CloseXDLive();
			//		});
			//	}, 8.0f));
			Invoke("closeLive", 5.0f);
        }

		if (GUI.Button (new Rect (400, 200, 300, 100), "自动登录", myButtonStyle)){
			xdsdk.XDSDK.AutoLogin();
		}

		if (GUI.Button(new Rect(400, 300, 500, 100), "苹果登录(仅支持IOS)", myButtonStyle))
		{
			xdsdk.XDSDK.AppleLogin();
		}
		if (GUI.Button (new Rect (400, 400, 300, 100), "TapTap登录", myButtonStyle)){
			xdsdk.XDSDK.TapTapLogin();
		}

		if (GUI.Button (new Rect (400, 500, 300, 100), "游客登录", myButtonStyle)){
			xdsdk.XDSDK.GuestLogin();
		}

		if (GUI.Button (new Rect (400, 600, 300, 100), "设置角色", myButtonStyle)){
			xdsdk.XDSDK.SetRole("roleId", "roleName", "https:/img.tapimg.com/market/lcs/db4187014ddc175f2b9dadf23160a88a_360.png?imageMogr2/auto-orient/strip");
		}
			
		if (GUI.Button (new Rect (400, 700, 300, 100), "退出", myButtonStyle)){
			xdsdk.XDSDK.Exit();
		}

		if (GUI.Button (new Rect (400, 800, 300, 100), "用户反馈", myButtonStyle)){
			xdsdk.XDSDK.UserFeedback();
		}

		if (GUI.Button (new Rect (400, 900, 300, 100), "隐藏taptap", myButtonStyle)){
			xdsdk.XDSDK.HideTapTap();
		}

		if (GUI.Button(new Rect(400,1000,500,100),"获取地区Code",myButtonStyle)){
			TDSCommon.TDSCommon.GetRegionCode((isMainland)=>
			{
				Debug.Log("isMainland:" + isMainland);
			});
		}

		if (GUI.Button (new Rect (750, 100, 300, 100), "微信分享文字", myButtonStyle)){
			Dictionary<string, string> content = new Dictionary<string, string> ();
            
            // https://developer.apple.com/documentation/foundation/nsstring/1409420-boolvalue?preferredLanguage=occ
            content.Add ("bText", "Y");
            
			content.Add ("title", "***title***");
			content.Add ("description", "***description***");
			content.Add ("text", "***text***");
			content.Add ("scene", "SESSION");
			content.Add ("shareType", "TEXT");
			xdsdk.XDSDK.Share (content);
		}

		if (GUI.Button (new Rect (750, 200, 300, 100), "排队", myButtonStyle)){
#if UNITY_ANDROID && !UNITY_EDITOR
			com.xdsdk.xdtrafficcontrol.XDTrafficControl.Instance.Check("cf1j5axm7hckw48");
#else
			com.xdsdk.xdtrafficcontrol.XDTrafficControl.Instance.Check("cf1j5axm7hckw48");
#endif


        }

		//      if (GUI.Button (new Rect (750, 300, 300, 100), "微信分享音乐", myButtonStyle)){
		//	Dictionary<string, string> content = new Dictionary<string, string> ();
		//	content.Add ("title", "***title***");
		//	content.Add ("description", "***description***");
		//	content.Add ("thumbPath", "/storage/emulated/0/2.png");
		//	content.Add ("musicUrl", "http://staff2.ustc.edu.cn/~wdw/softdown/index.asp/0042515_05.ANDY.mp3");
		//	content.Add ("scene", "SESSION");
		//	content.Add ("shareType", "MUSIC");
		//	xdsdk.XDSDK.Share (content);
		//}

		//if (GUI.Button (new Rect (750, 400, 300, 100), "微信分享视频", myButtonStyle)){
		//	Dictionary<string, string> content = new Dictionary<string, string> ();
		//	content.Add ("title", "***title***");
		//	content.Add ("description", "***description***");
		//	content.Add ("thumbPath", "/storage/emulated/0/2.png");
		//	content.Add ("videoUrl", "xd.com");
		//	content.Add ("scene", "SESSION");
		//	content.Add ("shareType", "VIDEO");
		//	xdsdk.XDSDK.Share (content);
		//}

		sceneId = GUI.TextArea(new Rect(750, 400, 300, 100), sceneId);
		if (GUI.Button(new Rect(750, 500, 300, 100), "场景化入口", myButtonStyle))
		{
			TapSDK.TDSMoment.OpenSceneEntryMoment(TapSDK.Orientation.ORIENTATION_DEFAULT, sceneId);
		}
			//if (GUI.Button (new Rect (750, 500, 300, 100), "微信分享链接", myButtonStyle)){
			//	Dictionary<string, string> content = new Dictionary<string, string> ();
			//	content.Add ("title", "***title***");
			//	content.Add ("description", "***description***");
			//	content.Add ("thumbPath", "/storage/emulated/0/2.png");
			//	content.Add ("webpageUrl", "xd.com");
			//	content.Add ("scene", "SESSION");
			//	content.Add ("shareType", "WEB");
			//	xdsdk.XDSDK.Share (content);
			//}

			if (GUI.Button (new Rect (750, 600, 300, 100), "实名认证", myButtonStyle)){
			xdsdk.XDSDK.OpenRealName();
		}

		if (GUI.Button (new Rect (750, 700, 300, 100), "论坛", myButtonStyle)){
			TapForumSDK.Instance.SetCallback(new TapforumCallbackHandler(toast));
			TapForumSDK.Instance.OpenTapTapForum("58881");
		}

		if (GUI.Button (new Rect (750, 800, 300, 100), "直播", myButtonStyle)) {
			Dictionary<string, string> info = new Dictionary<string,string>();
			info.Add("OrderId", "1234567890123456789012345678901234567890");
			info.Add("Product_Price", "1");
			info.Add("EXT", "abcd|efgh|1234|5678");
			info.Add("Sid", "2");
			info.Add("Role_Id", "3");
			info.Add("Product_Id", "4");
			info.Add("Product_Name", "648大礼包");
			info.Add ("transactionIdentifier", "123456789");
			com.xdsdk.xdlive.XDLive.Instance.SetCallback(new XDLIVECallback(toast));
			// xdsdk.XDSDK.RestorePay (info);
			com.xdsdk.xdlive.XDLive.Instance.OpenXDLive("1","xcc://events/redpoint?path=videos.307");


			// xdsdk.XDSDK.OnResume();
		}

		if (GUI.Button (new Rect (750, 900, 300, 100), "退出", myButtonStyle)) {
#if UNITY_EDITOR

#else
			Application.Quit();
#endif
		}

        if(GUI.Button(new Rect(1200, 100, 350, 100), "防沉迷计时开启", myButtonStyle))
        {
			xdsdk.XDSDK.GameResume();
        }

		if (GUI.Button(new Rect(1200, 200, 350, 100), "防沉迷计时关闭", myButtonStyle))
		{
			xdsdk.XDSDK.GameStop();
		}
		if (GUI.Button(new Rect(1200, 300, 350, 100), "打开动态", myButtonStyle))
		{
			TapSDK.TDSMoment.OpenMoment(TapSDK.Orientation.ORIENTATION_DEFAULT);
		}

		product_id = GUI.TextArea(new Rect(1200, 400, 350, 100), product_id);
        amount = GUI.TextArea(new Rect(1200, 500, 350, 100), amount);

        if(GUI.Button(new Rect(1200, 600, 350, 100), "自定义支付",myButtonStyle)){
			Dictionary<string, string> info = new Dictionary<string, string>();
			info.Add("OrderId", "1234567890123456789012345678901234567890");
			info.Add("Product_Price", amount);
			info.Add("EXT", "abcd|efgh|1234|5678");
			info.Add("Sid", "2");
			info.Add("Role_Id", "3");
			info.Add("Product_Id", product_id);
			info.Add("Product_Name", "648大礼包");
			xdsdk.XDSDK.Pay(info);
		}

		if (GUI.Button(new Rect(1200, 700, 350, 100), "用户协议", myButtonStyle))
		{
			xdsdk.XDSDK.OpenProtocol(xdsdk.XDSDK.ProtocolType.PROTOCOL_TYPE_USER);
		}
		if (GUI.Button(new Rect(1200, 800, 350, 100), "游戏协议", myButtonStyle))
		{
			xdsdk.XDSDK.OpenProtocol(xdsdk.XDSDK.ProtocolType.PROTOCOL_TYPE_GAME);
		}
		if (GUI.Button(new Rect(1200, 900, 350, 100), "隐私协议", myButtonStyle))
		{
			xdsdk.XDSDK.OpenProtocol(xdsdk.XDSDK.ProtocolType.PROTOCOL_TYPE_PRIVACY);
		}

		if (GUI.Button(new Rect(1200, 1000, 350, 100), "好友动态", myButtonStyle))
		{
			//xdsdk.XDSDK.GuestLogin();
			xdsdk.XDMomentConfig momentConfig = new xdsdk.XDMomentConfig();
			momentConfig.SetOrientation(-1);
			xdsdk.XDSDK.OpenUserMoment(momentConfig, "199949426");

		}

		imagePath = GUI.TextArea(new Rect(1600, 100, 350, 100), imagePath);

		if (GUI.Button(new Rect(1600, 200, 350, 100), "发布动态", myButtonStyle))
		{
			string[] imagePaths = imagePath.Split(',');
			TapSDK.TDSMoment.PublishMoment(TapSDK.Orientation.ORIENTATION_DEFAULT, imagePaths, "描述测试");
		}

		if (GUI.Button(new Rect(1600, 300, 350, 100), "获取小红点数据", myButtonStyle))
		{
			TapSDK.TDSMoment.GetNoticeData();

		}

		if (GUI.Button(new Rect(1600, 400, 350, 100), "10s直接关闭", myButtonStyle))
		{
			Invoke("closeMoment", 10.0f);

		}
		if (GUI.Button(new Rect(1600, 500, 350, 100), "设置XD登录", myButtonStyle))
		{
            string[] entries = { "QQ_LOGIN", "XD_LOGIN", "GUEST_LOGIN", "WX_LOGIN", "APPLE_LOGIN" };

            xdsdk.XDSDK.SetLoginEntries(entries);

        }
	}

    void closeLive()
	{
		com.xdsdk.xdlive.XDLive.Instance.CloseXDLive();
	}

	void closeMoment()
	{
		TapSDK.TDSMoment.CloseMoment();
	}
	class XDLIVECallback : com.xdsdk.xdlive.XDLive.XDLiveCallback
    {
		IUnityNativeToasts toast;

		public XDLIVECallback(IUnityNativeToasts toast)
        {
			this.toast = toast;
        }

        override
		public void OnXDLiveClosed()
	{
			Debug.Log(" xdlive close=========");
			toast.ShowShortToast("xdlive close=========");


	}

	override
	public void OnXDLiveOpen()
	{
			Debug.Log(" xdlive open==========");
			toast.ShowShortToast("xdlive open==========");
	}
}

    class TapforumCallbackHandler : TapForumCallback
    {
		IUnityNativeToasts toast;

		public TapforumCallbackHandler(IUnityNativeToasts toast)
		{
			this.toast = toast;
		}
		public override void OnForumAppear()
        {
			Debug.Log(" XDForum open==========");
			toast.ShowShortToast(" XDForum open======");
		}

        public override void OnForumDisappear()
        {
			Debug.Log(" XDForum close==========");
			toast.ShowShortToast("XDForum close=========");
		}
    }

}
