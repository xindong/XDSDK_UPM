using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/**
 * Created by sunyi on 30/11/2017.
 */


namespace com.taptap.sdk
{
	public class AndroidImpl : TapTapSDKImpl
	{
		public override void OpenTapTapForum(string appid){
			AndroidJavaClass jc = new AndroidJavaClass ("com.taptap.forum.TapTapSdk");
			jc.CallStatic("setListener", new TapForumCallback(TapTapSDK.Instance.GetCallback()));
			AndroidJavaClass playerClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
			AndroidJavaObject activityObject = playerClass.GetStatic<AndroidJavaObject>("currentActivity");
			jc.CallStatic ("openTapTapForum", activityObject, appid);
		}

		class TapForumCallback : AndroidJavaProxy
        {
			TapCallback tapCallback;
			public TapForumCallback(TapCallback tapCallback) : base("com.taptap.forum.TapTapSdk$TapTapSdkListener")
            {
				this.tapCallback = tapCallback;
            }

			public override AndroidJavaObject Invoke(string methodName, object[] javaArgs)
            {
                switch (methodName)
                {
					case "onForumAppear":
						tapCallback.OnForumAppear();
						break;
					case "onForumDisappear":
						tapCallback.OnForumDisappear();
						break;

				}
				return null;
            }
		}
	}
}

