

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System;


namespace xdsdk
{
	public interface XDSDKInterface
	{

		//设置回调方法
		void SetCallback(XDCallback callback);

		//隐藏游客登录	
		void HideGuest();

		//隐藏微信登录
		void HideWX();

		//隐藏QQ登录
		void HideQQ();

		//显示VeryCD登录（此接口供老游戏兼容，新游戏不建议调用）
		void ShowVC();

		//设置QQ为Web登录方式
		void SetQQWeb();

		//设置微信为Web 扫码登录方式
		void SetWXWeb();


		//显示TapTap登录
		void HideTapTap();


		//设置登录顺序
		void SetLoginEntries(string[] entries);


		//获取SDK版本
		string GetSDKVersion();


		//获取当前包的渠道名（安卓）
		string GetAdChannelName();


		//初始化心动SD
		void InitSDK(string appid, int aOrientation, string channel, string version, bool enableTapdb);

		//初始化心动SD
		void InitSDK(string appid, int aOrientation, string channel, string version, bool enableTapdb, bool enableMoment);

		//登录
		void Login();

		//获取Access Token
		string GetAccessToken();

		//获取当前登录状态
		bool IsLoggedIn();


		//打开用户中心
		bool OpenUserCenter();


		//打开实名认证界面
		void OpenRealName();


		void OpenUserBindView();


		//打开用户反馈
		void UserFeedback();

		//发起支付
		bool Pay(Dictionary<string, string> info);


		//恢复支付
		void RestorePay(Dictionary<string, string> info);


		//登出
		void Logout();

		//退出
		void Exit();


		//分享至微信
		void Share(Dictionary<string, string> content);


		//设置用户等级
		void SetLevel(int level);

		//设置游戏服务器地址
		void SetServer(string server);


		void SetRole(string roleId, string roleName, string roleAvatar);


		void ClearRole();


		// 自定义登录方式
		void AutoLogin();


		void TapTapLogin();


		void AppleLogin();

		void GuestLogin();


		void GameStop();


		void GameResume();


		// onresume 安卓
		void OnResume();


		// onstop 安卓
		void OnStop();



		void OpenProtocol(XDSDK.ProtocolType type);


		void OpenUserMoment(XDMomentConfig config, string xdId);
      
	}
}
