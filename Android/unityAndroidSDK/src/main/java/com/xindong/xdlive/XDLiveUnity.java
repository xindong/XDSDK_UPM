package com.xindong.xdlive;

import android.util.Log;

import com.unity3d.player.UnityPlayer;
import com.xd.xdsdk.ExitCallback;
import com.xd.xdsdk.XDCallback;
import com.xd.xdsdk.XDSDK;
import com.xd.xdsdk.share.XDWXShare;
import com.xd.xdsdk.share.XDWXShareObject;

import org.json.JSONException;
import org.json.JSONObject;

import java.util.Map;

public class XDLiveUnity {

    static XDLive.XDLiveCallback callback = new XDLive.XDLiveCallback() {
        @Override
        public void onXDLiveOpen() {
            UnityPlayer.UnitySendMessage("XDLiveListener", "OnXDLiveOpen", "");
        }

        @Override
        public void onXDLiveClosed() {
            UnityPlayer.UnitySendMessage("XDLiveListener", "OnXDLiveClosed", "");
        }
    };

    public static void OpenXDLive(String appid) {
        XDLive.setCallback(XDLiveUnity.callback);
        XDLive.openXDLive(UnityPlayer.currentActivity, appid);
    }

    public static void OpenXDLive(String appid, String url) {
        XDLive.setCallback(callback);
        XDLive.openXDLive(UnityPlayer.currentActivity, appid, url);
    }

    public static void OpenXDLive(String appid, String url,int orientation) {
        XDLive.setCallback(callback);
        XDLive.openXDLive(UnityPlayer.currentActivity, appid, url,orientation);
    }

    public static void CloseXDLive() {
        XDLive.closeXDLive();
    }

    public static void InvokeFunc(final String unityCallbackID, String params) {
        try {
            JSONObject paramsJsonObject = new JSONObject(params);
            XDLive.invokeFunc(paramsJsonObject, new XDLive.FuncResultListener() {
                @Override
                public void onResult(JSONObject resultJsonObject) {
                    try {
                        resultJsonObject.put("unity_callback_id", unityCallbackID);
                        UnityPlayer.UnitySendMessage("XDLiveListener", "InvokeFuncCallback", resultJsonObject.toString());
                    } catch (JSONException e) {
                        e.printStackTrace();
                    }
                }
            });
        } catch (JSONException e) {
            e.printStackTrace();
            JSONObject result = new JSONObject();
            try {
                result.put("unity_callback_id", unityCallbackID);
                result.put("status", -1);
                result.put("msg", "illegal params");
                UnityPlayer.UnitySendMessage("XDLiveListener", "InvokeFuncCallback", result.toString());
            } catch (JSONException e1) {
                e1.printStackTrace();
            }

        }
    }
}

