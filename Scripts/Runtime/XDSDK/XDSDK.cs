/*
 * XDSDK Version 3.3.0
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
			getXDSDKImp().SetCallback(callback);
        }
		//隐藏游客登录	
		public static void HideGuest(){
			getXDSDKImp().HideGuest ();
		}
		//隐藏微信登录
		public static void HideWX(){
			getXDSDKImp().HideWX ();
		}
		//隐藏QQ登录
		public static void HideQQ(){
			getXDSDKImp().HideQQ ();
		}
		//显示VeryCD登录（此接口供老游戏兼容，新游戏不建议调用）
		public static void ShowVC(){
			getXDSDKImp().ShowVC ();
		}
		//设置QQ为Web登录方式
		public static void SetQQWeb(){
			getXDSDKImp().SetQQWeb ();
		}
		//设置微信为Web 扫码登录方式
		public static void SetWXWeb(){
			getXDSDKImp().SetWXWeb ();
		}

		//显示TapTap登录
		public static void HideTapTap(){
			getXDSDKImp().HideTapTap ();
		}

		//设置登录顺序
		public static void SetLoginEntries(string[] entries){
			getXDSDKImp().SetLoginEntries (entries);
		}
	
		//获取SDK版本
		public static string GetSDKVersion(){
			return getXDSDKImp().GetSDKVersion ();

		}

		//获取当前包的渠道名（安卓）
		public static string GetAdChannelName(){
			return getXDSDKImp().GetAdChannelName ();
		}

		//初始化心动SDK
		public static void InitSDK(string appid, int aOrientation, string channel, string version, bool enableTapdb){
			getXDSDKImp().InitSDK (appid, aOrientation, channel, version, enableTapdb);
        }

		//初始化心动SDK
		public static void InitSDK(string appid, int aOrientation, string channel, string version, bool enableTapdb, bool enableMoment)
		{
			getXDSDKImp().InitSDK(appid, aOrientation, channel, version, enableTapdb, enableMoment);
		}

		//登录
		public static void Login(){
			getXDSDKImp().Login ();
		}
		//获取Access Token
		public static string GetAccessToken(){
			return getXDSDKImp().GetAccessToken ();
		}
		//获取当前登录状态
		public static bool IsLoggedIn(){
			return getXDSDKImp().IsLoggedIn ();
		}

		//打开用户中心
		public static bool OpenUserCenter(){
			return getXDSDKImp().OpenUserCenter ();
		}

		//打开实名认证界面
		public static void OpenRealName(){
			getXDSDKImp().OpenRealName();
		}

        public static void OpenUserBindView() {
			getXDSDKImp().OpenUserBindView();
        }

        //打开用户反馈
        public static void UserFeedback(){
			getXDSDKImp().UserFeedback ();
		}
		//发起支付
		public static bool Pay(Dictionary<string, string> info){
			return getXDSDKImp().Pay (info);
		}

		//恢复支付
		public static void RestorePay(Dictionary<string, string> info){
			getXDSDKImp().RestorePay (info);
		}

		//登出
		public static void Logout(){
			getXDSDKImp().Logout ();
		}

		//退出
		public static void Exit(){
			getXDSDKImp().Exit ();
		}

		//分享至微信
		public static void Share(Dictionary<string, string> content){
			getXDSDKImp().Share (content);
		}

		//设置用户等级
		public static void SetLevel(int level){
			getXDSDKImp().SetLevel (level);
		}

		//设置游戏服务器地址
		public static void SetServer(string server){
			getXDSDKImp().SetServer (server);
		}

		 public static void SetRole(string roleId,string roleName,string roleAvatar)
        {
			getXDSDKImp().SetRole(roleId, roleName, roleAvatar);
        }

        public static void ClearRole()
        {
			getXDSDKImp().ClearRole();

		}

		// 自定义登录方式
		public static void AutoLogin() {
			getXDSDKImp().AutoLogin();
		}

		public static void TapTapLogin() {
			getXDSDKImp().TapTapLogin();
		}

		public static void AppleLogin() {
			getXDSDKImp().AppleLogin();
		}

		public static void GuestLogin() {
			getXDSDKImp().GuestLogin();
		}

		public static void GameStop() {
			getXDSDKImp().GameStop();
		} 

		public static void GameResume () {
			getXDSDKImp().GameResume();
		}

		// onresume 安卓
		public static void OnResume(){
			getXDSDKImp().OnResume();
		}

		// onstop 安卓
		public static void OnStop(){
			getXDSDKImp().OnStop();
		}

        
        public static void OpenProtocol(ProtocolType type)
        {
			getXDSDKImp().OpenProtocol(type);
        }

        public static void OpenUserMoment(XDMomentConfig config, string xdId)
        {
			getXDSDKImp().OpenUserMoment(config, xdId);
        }

		private static XDSDKInterface getXDSDKImp()
        {
#if UNITY_STANDALONE_WIN && !UNITY_EDITOR && !USE_UNITY_XDSDK

			 return (XDSDKInterface)XDSDKImp.GetInstance();

#elif UNITY_STANDALONE_WIN && !UNITY_EDITOR && !USE_UNITY_XDSDK

  return (XDSDKInterface)XDSDKImp.GetInstance();

#elif (UNITY_EDITOR || UNITY_STANDALONE_OSX || UNITY_STANDALONE_WIN) && PC_VERSION

 return (XDSDKInterface)XDSDKImp.GetInstance();

#elif UNITY_IOS && !UNITY_EDITOR

return (XDSDKInterface)XDSDKIOSImp.GetInstance();

#elif UNITY_ANDROID && !UNITY_EDITOR

return (XDSDKInterface)XDSDKAndroidImp.GetInstance();

#else
			return (XDSDKInterface)XDSDKImp.GetInstance();
#endif

		}
	}
}
