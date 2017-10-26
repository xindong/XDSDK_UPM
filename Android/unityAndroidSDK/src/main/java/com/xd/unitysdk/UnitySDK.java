package com.xd.unitysdk;

import android.app.Activity;
import android.content.Intent;
import android.util.Log;

import com.unity3d.player.UnityPlayer;
import com.unity3d.player.UnityPlayerActivity;
import com.xd.xdsdk.ExitCallback;
import com.xd.xdsdk.XDCallback;
import com.xd.xdsdk.XDCore;
import com.xd.xdsdk.XDSDK;
import com.xd.xdsdk.share.XDWXShare;
import com.xd.xdsdk.share.XDWXShareObject;

import java.util.Map;
import java.util.StringTokenizer;

public class UnitySDK{


    public static void initSDK(String appid, int aOrientation) {
        XDSDK.setCallback(xdCallback);
        XDWXShare.setWXShareCallBack(wxShareCallback);
        XDSDK.initSDK(UnityPlayer.currentActivity, appid, aOrientation);
    }

    public static String getSDKVersion(){
        return XDSDK.getSDKVersion();
    }

    public static void login() {
        XDSDK.login();
    }

    public static String getAccessToken() {
        return XDSDK.getAccessToken();
    }

    public static boolean isLoggedIn() {
        return XDSDK.isLoggedIn();
    }

    public static boolean openUserCenter() {
        return XDSDK.openUserCenter();
    }

    public static boolean pay(Map<String, String> info) {
        return XDSDK.pay(info);
    }

    public static void logout() {
        XDSDK.logout();
    }

    public static void exit() {
        XDSDK.exit(exitCallback);
    }

    public static void hideGuest() {
        XDSDK.hideGuest();
    }

    public static void hideWX() {
        XDSDK.hideWX();
    }

    public static void hideQQ() {
        XDSDK.hideQQ();
    }

    public static void showVC() {
        XDSDK.showVC();
    }

    public static void setQQWeb() {
        XDSDK.setQQWeb();
    }

    public static void setWXWeb() {
        XDSDK.setWXWeb();
    }

    public static void showTapTap() {
        XDSDK.showTapTap();
    }

    public static void userFeedback(){
        XDSDK.userFeedback();
    }

    public static void shareToWX(Map<String, String> content) {
        XDWXShareObject wxShareObject = new XDWXShareObject();
        wxShareObject.setTitle(content.get("title"));
        wxShareObject.setDescription(content.get("description"));
        wxShareObject.setThumb(content.get("thumbPath"));
        if(content.get("scene").equals("SESSION")){
            wxShareObject.setScene(XDWXShareObject.SCENE_SESSION);
        }else if (content.get("scene").equals("TIMELINE")){
            wxShareObject.setScene(XDWXShareObject.SCENE_TIMELINE);
        }else if (content.get("scene").equals("FAVOURITE")){
            wxShareObject.setScene(XDWXShareObject.SCENE_FAVOURITE);
        }
        if (content.get("shareType").equals("TEXT")){
            wxShareObject.setText(content.get("text"));
            wxShareObject.setType(XDWXShareObject.TYPE_TEXT);
        }else if (content.get("shareType").equals("IMAGE")){
            wxShareObject.setImage(content.get("imageUrl"));
            wxShareObject.setType(XDWXShareObject.TYPE_IMAGE);
        }else if (content.get("shareType").equals("MUSIC")){
            wxShareObject.setMusicUrl(content.get("musicUrl"));
            wxShareObject.setType(XDWXShareObject.TYPE_MUSIC);
        }else if (content.get("shareType").equals("VIDEO")){
            wxShareObject.setVideoUrl(content.get("videoUrl"));
            wxShareObject.setType(XDWXShareObject.TYPE_VIDEO);
        }else if (content.get("shareType").equals("WEB")){
            wxShareObject.setWebPageUrl(content.get("webpageUrl"));
            wxShareObject.setType(XDWXShareObject.TYPE_WEB);
        }
        XDWXShare.share(wxShareObject);
    }

    private static final XDCallback xdCallback = new XDCallback() {
        @Override
        public void onInitSucceed() {
            UnityPlayer.UnitySendMessage("XDSDK", "OnInitSucceed", "");
        }

        @Override
        public void onInitFailed(String s) {
            UnityPlayer.UnitySendMessage("XDSDK", "OnInitFailed", s);
        }

        @Override
        public void onLoginSucceed(String s) {
            UnityPlayer.UnitySendMessage("XDSDK", "OnLoginSucceed", s);
        }

        @Override
        public void onLoginFailed(String s) {
            UnityPlayer.UnitySendMessage("XDSDK", "OnLoginFailed", s);
        }

        @Override
        public void onLoginCanceled() {
            UnityPlayer.UnitySendMessage("XDSDK", "OnLoginCanceled", "");
        }

        @Override
        public void onGuestBindSucceed(String s) {
            UnityPlayer.UnitySendMessage("XDSDK", "OnGuestBindSucceed", s);
        }

        @Override
        public void onLogoutSucceed() {
            UnityPlayer.UnitySendMessage("XDSDK", "OnLogoutSucceed", "");
        }

        @Override
        public void onPayCompleted() {
            UnityPlayer.UnitySendMessage("XDSDK", "OnPayCompleted", "");
        }

        @Override
        public void onPayFailed(String s) {
            UnityPlayer.UnitySendMessage("XDSDK", "OnPayFailed", s);
        }

        @Override
        public void onPayCanceled() {
            UnityPlayer.UnitySendMessage("XDSDK", "OnPayFailed", "");
        }
    };

    private static final ExitCallback exitCallback = new ExitCallback() {
        @Override
        public void onConfirm() {
            UnityPlayer.UnitySendMessage("XDSDK", "OnExitConfirm", "");
        }

        @Override
        public void onCancle() {
            UnityPlayer.UnitySendMessage("XDSDK", "OnExitCancel", "");
        }
    };

    private static final XDWXShare.XDWXShareCallback wxShareCallback= new XDWXShare.XDWXShareCallback() {
        @Override
        public void onWXShareSucceed() {
            UnityPlayer.UnitySendMessage("XDSDK", "OnWXShareSucceed", "");
        }

        @Override
        public void onWXShareFailed(String msg) {
            UnityPlayer.UnitySendMessage("XDSDK", "OnWXShareFailed", msg);
        }
    };




}

