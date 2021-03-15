using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;


/**
 * Created by sunyi on 30/11/2017.
 */

namespace com.taptap.sdk {
	public class iOSImpl : TapForumSDKImpl {

        private delegate void XDForumMessageCallback(string method, string message);
        [AOT.MonoPInvokeCallback(typeof(XDForumMessageCallback))]
        static void XDForumCallbackImp(string method, string message)
        {
            Debug.Log(" Unity ios tapForum method = " + method + " msg= " + message);
            TapForumCallback tapCallback = TapForumSDK.Instance.GetCallback();
            if (tapCallback == null) return;
            switch (method)
            {
                case "onForumDisappear":
                    tapCallback.OnForumDisappear();
                    break;
                case "onForumAppear":
                    tapCallback.OnForumAppear();
                    break;
            }
        }

        public override void OpenTapTapForum(string appid){
#if UNITY_IOS && !UNITY_EDITOR
            XDForumSetCallback(XDForumCallbackImp);    
			XDSDKOpenTapTapForum(appid);
#endif
        }

#if UNITY_IOS && !UNITY_EDITOR

        [DllImport("__Internal")]
		private static extern void XDSDKOpenTapTapForum (string appid);
         [DllImport("__Internal")]
         private static extern void XDForumSetCallback(XDForumMessageCallback callback);
#endif
    }
}

