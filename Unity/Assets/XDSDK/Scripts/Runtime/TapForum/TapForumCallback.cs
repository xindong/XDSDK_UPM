using UnityEngine;
using System.Collections;

namespace com.taptap.sdk
{
	
	public abstract class TapForumCallback
	{
		//public abstract void OnQueryAppBoardStatusSuccess (int count);

		//public abstract void OnQueryAppBoardStatusFailed (string msg);

		public abstract void OnForumAppear();
		public abstract void OnForumDisappear();

	}
}
