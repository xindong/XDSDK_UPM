using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;


/**
 * Created by sunyi on 30/11/2017.
 */

namespace com.taptap.sdk {
	public class iOSImpl : TapTapSDKImpl {

		public override void OpenTapTapForum(string appid){
#if UNITY_IOS && !UNITY_EDITOR
			ttOpenTapTapForum(appid);
#endif
        }

        public override void InitAppBoard(){
#if UNITY_IOS && !UNITY_EDITOR
			ttInitAppBoard();
#endif
        }

        public override void QueryAppBoardStatus(){
#if UNITY_IOS && !UNITY_EDITOR
			ttQueryAppBoardStatus();
#endif
        }

        public override void OpenAppBoard(string appid){
#if UNITY_IOS && !UNITY_EDITOR
			ttOpenAppBoard(appid);
#endif
        }

#if UNITY_IOS && !UNITY_EDITOR

        [DllImport("__Internal")]
		private static extern void ttOpenTapTapForum (string appid);

		[DllImport("__Internal")]
		private static extern void ttInitAppBoard ();

		[DllImport("__Internal")]
		private static extern void ttQueryAppBoardStatus ();

		[DllImport("__Internal")]
		private static extern void ttOpenAppBoard (string appid);
#endif
    }
}

