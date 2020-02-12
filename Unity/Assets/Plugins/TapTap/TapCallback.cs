using UnityEngine;
using System.Collections;

namespace com.taptap.sdk
{
	
	public abstract class TapCallback
	{
		public abstract void OnQueryAppBoardStatusSuccess (int count);

		public abstract void OnQueryAppBoardStatusFailed (string msg);

	}
}
