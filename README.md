# XDSDK

### 前提条件

* 安装Unity **Unity 2018.3**或更高版本

* IOS **10**或更高版本

* Android 目标为**API21**或更高版本

### 1.添加XDSDK

使用XDSDK的前提条件是依赖 **[TapSDK](https://developer.taptap.com/v2-doc/sdk/tap-unity)**

 **TapSDK** 和 **XDSDK** 的版本关系为强绑定关系。

* 使用Unity Pacakge Manager

```json
//在YourProjectPath/Packages/manifest.json中添加以下代码
"dependencies":{
        "com.xd.sdk":"https://github.com/xindong/XDSDK_UPM.git#1.0.0",
        "com.tds.sdk":"https://github.com/xindong/TAPSDK_UPM.git#1.0.6",
    }
```

### 2.配置XDSDK

#### 2.1 Android 配置

编辑Assets/Plugins/Android/AndroidManifest.xml文件,在Application Tag下添加以下代码。

```xml
    <uses-sdk tools:overrideLibrary="com.bun.miitmdid"/>
    <uses-permission android:name="android.permission.INTERNET" />
    <uses-permission android:name="android.permission.ACCESS_NETWORK_STATE" />
    <uses-permission android:name="android.permission.ACCESS_WIFI_STATE" />
    <uses-permission android:name="android.permission.WRITE_EXTERNAL_STORAGE" />
    
    <application  android:usesCleartextTraffic="true">
        <activity
                         android:name="com.unity3d.player.UnityPlayerActivity"
                         android:launchMode="2"
                         android:screenOrientation="0"
                         android:configChanges="orientation|keyboardHidden|screenSize"
                         android:hardwareAccelerated="false">

                         <intent-filter>

                             <action
                                 android:name="android.intent.action.MAIN" />

                             <category
                                 android:name="android.intent.category.LAUNCHER" />
                         </intent-filter>

        </activity>

        <activity
            android:name="com.xd.sdklib.helper.XDStartView"
            android:theme="@android:style/Theme.Translucent.NoTitleBar.Fullscreen"
            android:configChanges="orientation|keyboardHidden|screenSize" />
        <activity
            android:name="com.xd.sdklib.helper.XDViewActivity"
            android:theme="@android:style/Theme.Translucent.NoTitleBar.Fullscreen"
            android:configChanges="orientation|keyboardHidden|screenSize" />
        <activity
            android:name="com.xd.sdklib.helper.XDPayActivity"
            android:theme="@android:style/Theme.Translucent.NoTitleBar.Fullscreen"
            android:configChanges="orientation|keyboardHidden|screenSize" />
        <activity
            android:name="com.xd.sdklib.helper.XDWebView"
            android:theme="@android:style/Theme.Translucent.NoTitleBar.Fullscreen"
            android:configChanges="orientation|keyboardHidden|screenSize"/>
        <activity
            android:name="com.xd.sdklib.helper.WXEntryActivity"
            android:theme="@android:style/Theme.Translucent.NoTitleBar.Fullscreen" />

        <!-- 微信登录 -->
        <activity-alias
            android:name=".wxapi.WXEntryActivity"
            android:exported="true"
            android:targetActivity="com.xd.sdklib.helper.WXEntryActivity"/>

        <!-- QQ登录 -->
        <activity
            android:name="com.tencent.tauth.AuthActivity"
            android:noHistory="true"
            android:launchMode="singleTask" >
        </activity>

        <!-- TapTap登录 -->
        <activity
            android:name="com.taptap.sdk.TapTapActivity"
            android:exported="false"
            android:screenOrientation="sensorLandscape"
            android:configChanges="keyboard|keyboardHidden|screenLayout|screenSize|orientation"
            android:theme="@android:style/Theme.NoTitleBar" />

        <activity
            android:name="com.taptap.forum.TapTapActivity"
            android:configChanges="keyboard|keyboardHidden|screenLayout|screenSize|orientation"
            android:exported="false"
            android:theme="@android:style/Theme.NoTitleBar.Fullscreen" />
        <activity
            android:name="com.tencent.connect.common.AssistActivity"
            android:theme="@android:style/Theme.Translucent.NoTitleBar"
            android:configChanges="orientation|keyboardHidden|screenSize" />
        <!-- Ping++ SDK -->
        <activity
            android:name="com.pingplusplus.android.PaymentActivity"
            android:configChanges="orientation|keyboardHidden|navigation|screenSize"
            android:launchMode="singleTop"
            android:theme="@android:style/Theme.Translucent.NoTitleBar" />

        <!-- 支付宝 -->
        <activity
            android:name="com.alipay.sdk.app.H5PayActivity"
            android:configChanges="orientation|keyboardHidden|navigation"
            android:exported="false"
            android:screenOrientation="portrait" />
        <activity
            android:name="com.alipay.sdk.auth.AuthActivity"
            android:configChanges="orientation|keyboardHidden|navigation"
            android:exported="false"
            android:screenOrientation="portrait" />

        <!-- 微信支付 -->
        <activity-alias
            android:name=".wxapi.WXPayEntryActivity"
            android:exported="true"
            android:targetActivity="com.pingplusplus.android.PaymentActivity" />
           <uses-library android:name="org.apache.http.legacy" android:required="false"/>
```

#### 2.2 IOS 配置

在Assets/Plugins/IOS/Resource目录下创建XD-Info.plist文件,复制以下代码并且替换其中以下内容。

* wechat：微信 ClientId
* tecent：腾讯 ClientId
* XD：XD ClientId
* Apple_SignIn_Enable：true 需要Apple登陆
* Game_Domain：applinks

```xml
<?xml version="1.0" encoding="UTF-8"?>
<!DOCTYPE plist PUBLIC "-//Apple//DTD PLIST 1.0//EN" "http://www.apple.com/DTDs/PropertyList-1.0.dtd">
<plist version="1.0">
<dict>
    <key>wechat</key>
    <dict>
        <key>client_id</key>
        <string>wxbdfbe5dbd3e3c64b</string>
    </dict>
    <key>tencent</key>
    <dict>
        <key>client_id</key>
        <string>tencent1106148555</string>
    </dict>
    <key>XD</key>
    <dict>
        <key>client_id</key>
        <string>XD-d4bjgwom9zk84wk</string>
    </dict>
    <key>Apple_SignIn_Enable</key>
    <string>false</string>
    <key>Game_Domain</key>
    <string></string>
</dict>
</plist>

```