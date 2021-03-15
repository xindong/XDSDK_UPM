using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/**
 * Created by sunyi on 30/11/2017.
 */


namespace com.taptap.sdk
{
	public sealed class TapForumSDK
	{
		private static volatile TapForumSDK instance;
		private static object syncRoot = new System.Object ();

		private TapForumSDKImpl taptapSDKimpl;

		public TapForumCallback tapCallback;
	

		private TapForumSDK()
		{
		}

		public static TapForumSDK Instance {
			get {
				if (instance == null) {
					lock (syncRoot) {
						if (instance == null)
							instance = new TapForumSDK();
					}
				}
				return instance;
			}
		}

		public void SetCallback(TapForumCallback callback){
			tapCallback = callback;
		}

		public TapForumCallback GetCallback(){
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
	}
}

