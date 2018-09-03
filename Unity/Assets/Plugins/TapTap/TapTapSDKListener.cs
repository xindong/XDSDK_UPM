using System.Collections;
using System.Collections.Generic;
using System.Text;
using System;
using UnityEngine;


namespace com.taptap.sdk {
	public class TapTapListener : MonoBehaviour {
		public static volatile bool inited = false;

		public static void Init () {
			if (!inited) {
				inited = true;
				GameObject gameObject = new GameObject ();
				gameObject.name = "TapTapUnity";
				gameObject.AddComponent<TapTapListener> ();
				GameObject.DontDestroyOnLoad (gameObject);
			}
		}

		void OnQueryAppBoardStatusSuccess (string count){
			if (TapTapSDK.Instance.GetCallback() != null) {
				int numValue;
				bool parsed = Int32.TryParse(count, out numValue);
				if (parsed) {
					TapTapSDK.Instance.GetCallback().OnQueryAppBoardStatusSuccess (numValue);
				}
			}
				
		}

		void OnQueryAppBoardStatusFailed (string error){
			if (TapTapSDK.Instance.GetCallback() != null) {
				TapTapSDK.Instance.GetCallback().OnQueryAppBoardStatusFailed (error);
			}
		}
	
	}
}
	