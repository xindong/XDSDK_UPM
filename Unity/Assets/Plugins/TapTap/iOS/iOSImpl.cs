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
			ttOpenTapTapForum(appid);
		}

		public override void InitAppBoard(){
			ttInitAppBoard();
		}

		public override void QueryAppBoardStatus(){
			ttQueryAppBoardStatus();
		}

		public override void OpenAppBoard(string appid){
			ttOpenAppBoard(appid);
		}

		[DllImport("__Internal")]
		private static extern void ttOpenTapTapForum (string appid);

		[DllImport("__Internal")]
		private static extern void ttInitAppBoard ();

		[DllImport("__Internal")]
		private static extern void ttQueryAppBoardStatus ();

		[DllImport("__Internal")]
		private static extern void ttOpenAppBoard (string appid);
	}
}

