package com.xindong.xdlive;

import android.util.Log;

import com.unity3d.player.UnityPlayer;
import com.xd.xdsdk.ExitCallback;
import com.xd.xdsdk.XDCallback;
import com.xd.xdsdk.XDSDK;
import com.xd.xdsdk.share.XDWXShare;
import com.xd.xdsdk.share.XDWXShareObject;

import java.util.Map;

public class XDLiveUnity {
    public static void OpenXDLive(String appid) {
        XDLive.openXDLive(UnityPlayer.currentActivity, appid);
    }
}

