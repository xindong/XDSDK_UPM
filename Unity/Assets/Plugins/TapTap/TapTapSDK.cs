using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/**
 * Created by sunyi on 30/11/2017.
 */


namespace com.taptap.sdk
{
	public sealed class TapTapSDK
	{
		private static volatile TapTapSDK instance;
		private static object syncRoot = new System.Object ();

		private TapTapSDKImpl taptapSDKimpl;

		public TapCallback tapCallback;
	

		private TapTapSDK ()
		{
		}

		public static TapTapSDK Instance {
			get {
				if (instance == null) {
					lock (syncRoot) {
						if (instance == null)
							instance = new TapTapSDK ();
					}
				}
				return instance;
			}
		}

		public void SetCallback(TapCallback callback){
			tapCallback = callback;
		}

		public TapCallback GetCallback(){
			return tapCallback;
		}


		public void OpenTapTapForum(string appid){
       		if(taptapSDKimpl == null){
#if UNITY_ANDROID && !UNITY_EDITOR
        		taptapSDKimpl = new AndroidImpl ();
#elif UNITY_IPHONE && !UNITY_EDITOR
        		taptapSDKimpl = new iOSImpl ();
#endif
            }
            if (taptapSDKimpl != null) {
                taptapSDKimpl.OpenTapTapForum(appid);
            }
		}	

		public void InitAppBoard(){
			if(taptapSDKimpl == null){
#if UNITY_ANDROID && !UNITY_EDITOR
				taptapSDKimpl = new AndroidImpl ();
#elif UNITY_IPHONE && !UNITY_EDITOR
				taptapSDKimpl = new iOSImpl ();
#endif
            }
            if(taptapSDKimpl != null) {
                taptapSDKimpl.InitAppBoard();
                TapTapListener.Init();
            }
		}	

		public void QueryAppBoardStatus(){
			if(taptapSDKimpl == null){
#if UNITY_ANDROID && !UNITY_EDITOR
				taptapSDKimpl = new AndroidImpl ();
#elif UNITY_IPHONE && !UNITY_EDITOR
				taptapSDKimpl = new iOSImpl ();
#endif
            }
            if (taptapSDKimpl != null) {
                taptapSDKimpl.QueryAppBoardStatus();
            }
		}	

		public void OpenAppBoard(string appid){
			if(taptapSDKimpl == null){
#if UNITY_ANDROID && !UNITY_EDITOR
				taptapSDKimpl = new AndroidImpl ();
#elif UNITY_IPHONE && !UNITY_EDITOR
				taptapSDKimpl = new iOSImpl ();
#endif
            }
            if(taptapSDKimpl != null) {
                taptapSDKimpl.OpenAppBoard(appid);
            }
		}	
	}
}

