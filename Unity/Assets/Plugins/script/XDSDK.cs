/*
 * XDSDK Version 2.3.8
 * 
 * 
 * 
 */


using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System;

namespace xdsdk
{
	public class XDSDK
	{
        public enum ProtocolType
        {
            PROTOCOL_TYPE_USER,
            PROTOCOL_TYPE_GAME,
            PROTOCOL_TYPE_PRIVACY
        }

		//设置回调方法
		public static void SetCallback(XDCallback callback){
			XDSDKImp.GetInstance ().SetCallback (callback);
        }
		//隐藏游客登录	
		public static void HideGuest(){
			XDSDKImp.GetInstance ().HideGuest ();
		}
		//隐藏微信登录
		public static void HideWX(){
			XDSDKImp.GetInstance ().HideWX ();
		}
		//隐藏QQ登录
		public static void HideQQ(){
			XDSDKImp.GetInstance ().HideQQ ();
		}
		//显示VeryCD登录（此接口供老游戏兼容，新游戏不建议调用）
		public static void ShowVC(){
			XDSDKImp.GetInstance ().ShowVC ();
		}
		//设置QQ为Web登录方式
		public static void SetQQWeb(){
			XDSDKImp.GetInstance ().SetQQWeb ();
		}
		//设置微信为Web 扫码登录方式
		public static void SetWXWeb(){
			XDSDKImp.GetInstance ().SetWXWeb ();
		}

		//显示TapTap登录
		public static void HideTapTap(){
			XDSDKImp.GetInstance ().HideTapTap ();
		}

		//设置登录顺序
		public static void SetLoginEntries(string[] entries){
			XDSDKImp.GetInstance ().SetLoginEntries (entries);
		}
	
		//获取SDK版本
		public static string GetSDKVersion(){
			return XDSDKImp.GetInstance ().GetSDKVersion ();

		}

		//获取当前包的渠道名（安卓）
		public static string GetAdChannelName(){
			return XDSDKImp.GetInstance ().GetAdChannelName ();
		}

		//初始化心动SDK
		public static void InitSDK(string appid, int aOrientation, string channel, string version, bool enableTapdb){
			XDSDKImp.GetInstance ().InitSDK (appid, aOrientation, channel, version, enableTapdb);
			com.taptap.sdk.TapTapListener.Init ();
            com.xdsdk.xdlive.XDLiveListener.Init();
        }

		//登录
		public static void Login(){
			XDSDKImp.GetInstance ().Login ();
		}
		//获取Access Token
		public static string GetAccessToken(){
			return XDSDKImp.GetInstance ().GetAccessToken ();
		}
		//获取当前登录状态
		public static bool IsLoggedIn(){
			return XDSDKImp.GetInstance ().IsLoggedIn ();
		}

		//打开用户中心
		public static bool OpenUserCenter(){
			return XDSDKImp.GetInstance ().OpenUserCenter ();
		}

		//打开实名认证界面
		public static void OpenRealName(){
			XDSDKImp.GetInstance().OpenRealName();
		}

        public static void OpenUserBindView() {
            XDSDKImp.GetInstance().OpenUserBindView();
        }

        //打开用户反馈
        public static void UserFeedback(){
			XDSDKImp.GetInstance ().UserFeedback ();
		}
		//发起支付
		public static bool Pay(Dictionary<string, string> info){
			return XDSDKImp.GetInstance ().Pay (info);
		}

		//恢复支付
		public static void RestorePay(Dictionary<string, string> info){
			XDSDKImp.GetInstance ().RestorePay (info);
		}

		//登出
		public static void Logout(){
			XDSDKImp.GetInstance ().Logout ();
		}

		//退出
		public static void Exit(){
			XDSDKImp.GetInstance ().Exit ();
		}

		//分享至微信
		public static void Share(Dictionary<string, string> content){
			XDSDKImp.GetInstance ().Share (content);
		}

		//设置用户等级
		public static void SetLevel(int level){
			XDSDKImp.GetInstance ().SetLevel (level);
		}

		//设置游戏服务器地址
		public static void SetServer(string server){
			XDSDKImp.GetInstance ().SetServer (server);
		}

		 public static void SetRole(string roleId,string roleName,string roleAvatar)
        {
			XDSDKImp.GetInstance().SetRole(roleId, roleName, roleAvatar);
        }

        public static void ClearRole()
        {
			XDSDKImp.GetInstance().ClearRole();

		}

		// 自定义登录方式
		public static void AutoLogin() {
			XDSDKImp.GetInstance().AutoLogin();
		}

		public static void TapTapLogin() {
			XDSDKImp.GetInstance().TapTapLogin();
		}

		public static void AppleLogin() {
			XDSDKImp.GetInstance().AppleLogin();
		}

		public static void GuestLogin() {
			XDSDKImp.GetInstance().GuestLogin();
		}

		public static void GameStop() {
			XDSDKImp.GetInstance ().GameStop();
		} 

		public static void GameResume () {
			XDSDKImp.GetInstance ().GameResume();
		}

		// onresume 安卓
		public static void OnResume(){
			XDSDKImp.GetInstance ().OnResume();
		}

		// onstop 安卓
		public static void OnStop(){
			XDSDKImp.GetInstance ().OnStop();
		}

        
        public static void OpenProtocol(ProtocolType type)
        {
			XDSDKImp.GetInstance().OpenProtocol(type);
        }

        public static void OpenUserMoment(XDMomentConfig config, string xdId)
        {
			XDSDKImp.GetInstance().OpenUserMoment(config, xdId);
        }
	}
}
