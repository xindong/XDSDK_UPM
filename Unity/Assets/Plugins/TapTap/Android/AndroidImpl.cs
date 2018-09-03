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
			AndroidJavaClass jc = new AndroidJavaClass ("com.taptap.sdk.TapTapUnity");
			jc.CallStatic ("openTapTapForum", appid);
		}

		public override void InitAppBoard(){
			AndroidJavaClass jc = new AndroidJavaClass ("com.taptap.sdk.TapTapUnity");
			jc.CallStatic ("initAppBoard");
		}

		public override void QueryAppBoardStatus(){
			AndroidJavaClass jc = new AndroidJavaClass ("com.taptap.sdk.TapTapUnity");
			jc.CallStatic ("queryAppBoardStatus");
		}

		public override void OpenAppBoard(string appid){
			AndroidJavaClass jc = new AndroidJavaClass ("com.taptap.sdk.TapTapUnity");
			jc.CallStatic ("openAppBoard", appid);
		}
	}
}

