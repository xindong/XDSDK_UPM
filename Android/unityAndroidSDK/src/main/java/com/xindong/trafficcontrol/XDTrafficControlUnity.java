package com.xindong.trafficcontrol;

import android.util.Log;

import com.unity3d.player.UnityPlayer;
import com.unity3d.player.UnityPlayerActivity;
import com.xd.sdk.trafficcontrol.XDTrafficControl;
import com.xindong.xdlive.XDLive;

public class XDTrafficControlUnity {
    public static void init(){
        XDTrafficControl.setCallback(new XDTrafficControl.Callback() {
            @Override
            public void onQueueingFinished() {
                Log.e("XDTrafficControlUnity", "onQueueingSucceed");
                UnityPlayer.UnitySendMessage("XDTrafficControlListener", "XDTrafficControlFinished", "");
            }

            @Override
            public void onQueueingFailed(String s) {
                Log.e("XDTrafficControlUnity", "onQueueingFailed");
                UnityPlayer.UnitySendMessage("XDTrafficControlListener", "XDTrafficControlFailed", s);
            }

            @Override
            public void onQueueingCanceled() {
                Log.e("XDTrafficControlUnity", "onQueueingCanceled");
                UnityPlayer.UnitySendMessage("XDTrafficControlListener", "XDTrafficControlCanceled", "");
            }
        });
    }

    public static void check(String appid) {
        XDTrafficControl.check(UnityPlayer.currentActivity, appid);
    }
}

