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
			openTapTapForum(appid);
		}

		[DllImport("__Internal")]
		private static extern void openTapTapForum (string appid);
	}
}

