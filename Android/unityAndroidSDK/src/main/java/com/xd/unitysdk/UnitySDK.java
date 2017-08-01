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

import java.util.Map;

public class UnitySDK{


    public static void initSDK(String appid, int aOrientation) {
        XDSDK.setCallback(xdCallback);
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

    private static final XDCallback xdCallback = new XDCallback() {
        @Override
        public void onInitSucceed() {
            UnityPlayer.UnitySendMessage("XDSDK", "onInitSucceed", "");
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


}

