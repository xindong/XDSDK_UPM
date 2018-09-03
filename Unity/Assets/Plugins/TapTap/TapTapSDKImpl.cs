﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Created by sunyi on 30/11/2017.
 */


namespace com.taptap.sdk
{
	public abstract class TapTapSDKImpl
	{
		public abstract void OpenTapTapForum(string appid);

		public abstract void InitAppBoard();

		public abstract void QueryAppBoardStatus();

		public abstract void OpenAppBoard(string appid);
	}

}
