# <center> XDSDK-Unity3d </center>

## 1.Unity

<p style="color:red"> namespace均为xdsdk </p>

### 1.1.导入SDK资源

<p> 下载“XDSDK For Unity 3D”版本的SDK包。将其中的XDSDKForUnity.unitypackage文件导入到Unity工程里面（如果无法导入，请再次确认文件是放置于非中文路径下的），如下图所示： </p>

<img src="img/1.png"></img>

目录或文件 | 用途
--- |---
Plugins/script/XDSDK.cs | 心动SDK接口
Plugins/script/XDCallback.cs | 心动SDK回调
Plugins/script/XDSDKImp.cs | 心动SDK调用原生方法
Plugins/Android/libs | 心动SDK Android依赖库
Plugins/Android/res | 心动SDK Android资源文件
Plugins/iOS/libRMStore | 心动SDK iOS支付库组件
Plugins/iOS/libWeChatSDK | 心动SDK iOS微信依赖库
Plugins/iOS/libXDSDKiOSWrapper | 心动SDK iOS桥接组件
Library/TencentOpenAPI.framework | 心动SDK iOSQQ登录库文件
Library/XdComPlatform.framework | 心动SDK iOS核心依赖库 
Library/XDStore.framework | 心动SDK iOS支付组件


<p> 导入后，将Plugins/script下面的XDSDKListener.cs脚本文件拖动到长生命周期的对象中进行脚本挂接。 </p>



### 1.2.实现并设置回调方法

<p> 脚本需要实现的回调请参考以下代码： </p>

```
public class XDSDKHandler : XDCallback {

    //初始化成功回调
    public override void OnInitSucceed (){

    }
	
    //初始化失败回调
    public override void OnInitFailed (string msg){

    }

    //登录成功回调
    public override void OnLoginSucceed (string token){

    }

    //登录失败回调
    public override void OnLoginFailed (string msg){

    }
	
    //登录取消回调
    public override void OnLoginCanceled (){

    }

    //游客绑定成功回调
    public override void OnGuestBindSucceed (string token){

    }

    //登出回调
    public override void OnLogoutSucceed (){

    }

    //支付完成回调
    public override void OnPayCompleted (){

    }

    //支付失败回调
    public override void OnPayFailed (string msg){

    }

    //支付取消回调
    public override void OnPayCanceled (){  

    }

    //Android 退出回调
    public override void OnExitConfirm (){

    }

    //Android 取消退出回调
    public override void OnExitCancel (){

    }

    //微信分享成功回调（可选）
    public override void OnWXShareSucceed (){

    }

    //微信分享失败回调（可选）
    public override void OnWXShareFailed (){

    }

}
```

<p>设置回调方法 </p>

```
XDSDK.SetCallback (new XDSDKHandler ());
```

### 1.3.配置登录选项

<p>心动SDK提供配置QQ、微信登录、游客登录的显示与隐藏以及登录方式。</p>

<p>如不进行配置，SDK将默认显示QQ、微信、游客登录。QQ和微信的登录方式默认为App授权登录，心动SDK会根据QQ和微信的安装情况，将QQ和微信登录方式切换为Web登录，或者不提供对应的登录功能。</p>

<p style="color:red">请勿直接复制以下代码，根据游戏实际需求选择调用以下API</p>

```
XDSDK.HideGuest()	//隐藏游客登录
XDSDK.HideWX()		//隐藏微信登录
XDSDK.HideQQ()		//隐藏QQ登录
XDSDK.ShowVC()		//显示VeryCD登录（此接口供老游戏兼容，新游戏不建议调用）
XDSDK.SetQQWeb()	//设置QQ为Web登录方式
XDSDK.SetWXWeb()	//设置微信为Web 扫码登录方式
```

### 1.4.初始化SDK

初始化心动SDK，调用该接口是调用其它功能接口的必要条件。

```
/**
 * @param appid 心动AppID
 * @param aOrientation 屏幕方向，0表示横屏，1表示竖屏
 */
public static void InitSDK(string appid, int aOrientation)
```

<p>示例代码：</p>

```
XDSDK.InitSDK ("xxxxxx", 1);
```

<p>调用该接口会触发下列回调。</p>
<p style="color:red">其他接口请在获取到初始化成功回调之后进行调用。</p>

类别 | 回调方法
--- |---
初始化成功 | public void OnInitSucceed()
初始化失败 | public void OnInitFailed(string msg)


### 1.5.登录

调用该接口进行登录。

```
public static void Login()
```

示例代码
```
XDSDK.Login();
```

<p>调用该接口会触发下列回调。</p>
<p style="color:red">获取、查看用户信息以及支付接口请在获取到登录成功回调之后调用。</p>

类别 | 回调方法
--- | ---
登录成功 | void OnLoginSucceed(string token)
登录失败 | void OnLoginFailed(string msg)
登录取消 | void OnLoginCanceled()

### 1.6.获取Access Token

调用该接口获取当前登录用户的access token。

```
public static string GetAccessToken()
```

代码示例
```
XDSDK.GetAccessToken()
```

### 1.7.获取当前登录状态

```
public static bool IsLoggedIn()
```
代码示例
```
XDSDK.IsLoggedIn()
```

### 1.8.打开用户中心

调用该接口打开用户中心界面，用户可以在该界面进行登出和登录操作，游戏注意正确处理回调。在未登录状态，无法打开用户中心。在用户中心中，用户可进行登出操作，此时交互界面将消失。游戏需要提供引导用户重新进行登录的操作界面。

```
/**
* @return false表示尚未登录，重复调用默认为成功
*/
public static bool OpenUserCenter()
```
代码示例
```
XDSDK.OpenUserCenter()
```

### 1.9.发起支付

调用该接口发起支付。
<p style='color:red'>不保证在任何情况下都能收到回调，请勿直接使用SDK返回的支付结果作为最终判定订单状态的依据。</p>

```
/**
* @param info 支付相关信息，注意key和value都是字符串类型
*/
public static bool Pay(Dictionary<string, string> info)
```
其中info的字段如下。

参数 | 必须 |说明
--- | --- |--- 
Product_Name | 是 |商品名称，建议以游戏名称开头，方便财务对账
Product_Id | 是 | 商品ID
Product_Price | 是 | 商品价格（单位分）
Sid | 是 |所在服务器ID，不能有特殊字符，服务端支付回调会包含该字段
Role_Id | 否 | 支付角色ID，服务端支付回调会包含该字段
OrderId | 否 | 游戏侧订单号，服务端支付回调会包含该字段
EXT | 否 |额外信息，最长512个字符，服务端支付回调会包含该字段。

调用该接口会触发下列回调。

类别 | 回调方法
--- | ---
支付完成 | public void OnPayCompleted()
支付失败 | public void OnPayFailed(string msg) 
支付取消 | public void OnPayCanceled()

示例代码

```
Dictionary<string, string> info = new Dictionary<string,string>();
info.Add("OrderId", "1234567890123456789012345678901234567890");
info.Add("Product_Price", "1");
info.Add("EXT", "abcd|efgh|1234|5678");
info.Add("Sid", "2");
info.Add("Role_Id", "3");
info.Add("Product_Id", "4");
info.Add("Product_Name", "648大礼包");
XDSDK.Pay (info);
```

### 1.10.登出

需要注销当前登录用户时调用，该操作不会出现登录界面。

```
public static void Logout() 
```
调用该接口会触发下列回调

类别 | 回调方法
--- | ---
登出成功 | public void OnLogoutSucceed() 

示例代码
```
XDSDK.Logout();
```

### 1.11.游客升级

当游客账号升级成功时,会触发下列回调。<br/>
后续如需使用token，务必使用回调给的新token。但已生效的会话无需处理。

类别 | 回调方法
--- | ---
游客升级成功 | public void OnGuestBindSucceed(string token)

### 1.12.退出

调用该方法时，弹出确认框供用户选择是否退出。

```
public static void Exit()
```

示例代码

```
XDSDK.Exit();
```

调用该接口会触发下列回调

类别 | 回调方法
--- | ---
确认退出 | public void OnExitConfirm() 
取消退出 | public void OnExitCancel() 

### 1.13.微信分享

调用心动SDK的微信分享功能。

参数说明


通用参数 | 是否必须 | 类型 | 说明 
--- | --- | --- | ---
text | 否 | string | 分享文字内容 
bText | 是 | string | 分享内容是否为多媒体内容（1文本内容，0多媒体内容）
scene | 是 | string | 分享场景（SESSION对话，TIMELINE朋友圈，FAVOURITE收藏）
shareType | 是 | string | 分享类型（TEXT，IMAGE，MUSIC，VIDEO，WEB）
title | 否 | string | 分享标题 
description | 否 | string | 分享描述信息 
thumbPath | 否 | string | 缩略图路径 （安卓不支持Url，请填写文件路径。）
meidaTagName | 否 | string
messageExt | 否 | string 
messageAction | 否 | string

以下参数，请根据不同的分享类型（shareType参数）进行设置

图片分享 | 是否必须 | 类型| 说明 
--- | --- | --- | ---
imageUrl | 是| string | 分享图片Url （安卓不支持Url，请填写文件路径）
<br/>


音乐分享 | 是否必须 | 类型| 说明
--- | --- | --- | ---
musicUrl | 是 | string | 音乐链接 （请填写音乐路径。例：https://www.xd.com/music.mp3）
musicLowBandUrl | 否 | string | 低带宽音乐链接
musicDataUrl | 否 | string | 音乐数据链接
musicLowBandDataUrl | 否 | string | 低带宽状音乐数据链接
<br/>

视频分享 | 是否必须 | 类型| 说明
--- | --- | --- | ---
videoUrl | 是 | string | 视频链接
videoLowBandUrl | 否 | string | 低带宽视频链接
<br/>


网页分享参数 | 是否必须 | 类型| 说明
--- | --- | --- | ---
webpageUrl | 是 | string | 网页链接

示例代码

```
//分享文字
Dictionary<string, string> content = new Dictionary<string, string> ();
content.Add ("title", "***title***");  //标题
content.Add ("description", "***description***"); //描述
content.Add ("text", "***text***"); //内容
content.Add ("scene", "SESSION"); //scene场景值
content.Add ("shareType", "TEXT");  //分享类型 文字
xdsdk.XDSDK.Share (content);

//分享图片
Dictionary<string, string> content = new Dictionary<string, string> ();
content.Add ("title", "***title***");  //标题
content.Add ("description", "***description***");  //描述
content.Add ("thumbPath", "/storage/emulated/0/2.png"); //预览图路径
content.Add ("imageUrl", "/storage/emulated/0/2.png"); //图片路径
content.Add ("scene", "SESSION"); //scene场景值
content.Add ("shareType", "IMAGE");  //分享类型 图片
xdsdk.XDSDK.Share (content);

//分享音乐
Dictionary<string, string> content = new Dictionary<string, string> ();
content.Add ("title", "***title***"); //标题
content.Add ("description", "***description***"); //描述
content.Add ("thumbPath", "/storage/emulated/0/2.png"); //预览图路径
content.Add ("musicUrl", "http://staff2.ustc.edu.cn/~wdw/softdown/index.asp/0042515_05.ANDY.mp3");  //音乐url
content.Add ("scene", "SESSION");  //scene场景值
content.Add ("shareType", "MUSIC");  //分享类型 音乐
xdsdk.XDSDK.Share (content);

//分享视频
Dictionary<string, string> content = new Dictionary<string, string> ();
content.Add ("title", "***title***"); //标题
content.Add ("description", "***description***"); //描述
content.Add ("thumbPath", "/storage/emulated/0/2.png"); //预览图路径
content.Add ("videoUrl", "https://www.xd.com");  //视频url
content.Add ("scene", "SESSION");  //scene场景值
content.Add ("shareType", "VIDEO");  //分享类型 视频
xdsdk.XDSDK.Share (content);

//分享网页
Dictionary<string, string> content = new Dictionary<string, string> ();
content.Add ("title", "***title***"); //标题
content.Add ("description", "***description***"); //描述
content.Add ("thumbPath", "/storage/emulated/0/2.png"); //预览图路径
content.Add ("webpageUrl", "https://www.xd.com");  //视频url
content.Add ("scene", "SESSION");  //scene场景值
content.Add ("shareType", "WEB");  //分享类型 网页
xdsdk.XDSDK.Share (content);
```


调用分享接口会触发下列回调

类别 | 回调方法
--- | ---
分享成功 | public void OnWXShareSucceed() 
分享失败 | public void OnWXShareFailed(string msg)

<p style = "color : red"> 
注意：分享过程中，若用户选择留在微信，则不会产生分享回调。游戏不应当完全依赖分享回调。
</p> 


## 2.Android

### 2.1.按需要修改AndroidManifest

<p> 可以根据需要,参照如下示例修改Plugins/Android/AndroidManifest.xml。
</p>

```
<?xml version="1.0" encoding="utf-8"?>
<manifest
    xmlns:android="http://schemas.android.com/apk/res/android"
    package="com.xd.sdkdemo"
    xmlns:tools="http://schemas.android.com/tools"
    android:installLocation="preferExternal"
    android:versionCode="1"
    android:versionName="1.0">
    <supports-screens
        android:smallScreens="true"
        android:normalScreens="true"
        android:largeScreens="true"
        android:xlargeScreens="true"
        android:anyDensity="true"/>

    <uses-permission android:name="android.permission.INTERNET" />
    <uses-permission android:name="android.permission.ACCESS_NETWORK_STATE" />
    <uses-permission android:name="android.permission.ACCESS_WIFI_STATE" />
    <uses-permission android:name="android.permission.WRITE_EXTERNAL_STORAGE" />

    <application
        android:theme="@style/UnityThemeSelector"
        android:icon="@drawable/app_icon"
        android:label="@string/app_name"
        android:debuggable="true">
        <activity android:name="com.unity3d.player.UnityPlayerActivity"
                  android:label="@string/app_name">
            <intent-filter>
                <action android:name="android.intent.action.MAIN" />
                <category android:name="android.intent.category.LAUNCHER" />
            </intent-filter>
            <meta-data android:name="unityplayer.UnityActivity" android:value="true" />
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
            android:theme="@android:style/Theme.Translucent.NoTitleBar.Fullscreen" />
        <activity
            android:name="com.xd.sdklib.helper.WXEntryActivity"
            android:theme="@android:style/Theme.Translucent.NoTitleBar.Fullscreen" />

        <!-- 微信登录 -->
        <activity-alias
            android:name=".wxapi.WXEntryActivity"
            android:label="@string/app_name"
            android:exported="true"
            android:targetActivity="com.xd.sdklib.helper.WXEntryActivity"/>

        <!-- Ping++ SDK -->
        <activity
            android:name="com.pingplusplus.android.PaymentActivity"
            android:configChanges="orientation|screenSize"
            android:launchMode="singleTop"
            android:theme="@android:style/Theme.Translucent.NoTitleBar.Fullscreen" />

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


        <!-- QQ登录 -->
        <activity
            android:name="com.tencent.tauth.AuthActivity"
            android:noHistory="true"
            android:launchMode="singleTask" >
        </activity>

        <activity
            android:name="com.tencent.connect.common.AssistActivity"
            android:theme="@android:style/Theme.Translucent.NoTitleBar"
            android:configChanges="orientation|keyboardHidden|screenSize" />
    </application>
</manifest>
```

### 2.2.生成APK

<p>在Unity中生成APK，或将工程导出至Android Studio进行打包。

<img src="img/2.png"></img>

### 2.3.接入微信分享

<p>心动SDK目前集成了微信分享功能，需要注意下面几点。</p>

<p style="color:red">需要将工程导出至Android Studio进行操作</p>

<p style="color:red">使用心动SDK提供的分享功能，可略过该部分。</p>
<p style="color:red">如果游戏仍需要自行接入微信分享，需进行以下处理。</p>
<p style="color:red">微信分享的微信AppID必须使用心动提供的微信AppID，否则会导致微信登录失败。</p>

<p>接入其它SDK提供的微信分享功能时，会被要求在项目中增加一个类“{游戏包名}.wxapi.WXEntryActivity”，这个类可能是复制SDK提供的一个类，或者继承SDK的一个类。无论如何，将其修改为另一个名字，比如“{游戏包名}.wxapi.MYWXEntryActivity”。</p>
<p>修改AndroidManifest中，Activity的配置内容</p>

```
<!-- 微信登录，仅在游戏不接入其它微信分享SDK时使用该方法 -->
<activity-alias
    android:name=".wxapi.WXEntryActivity"
    android:exported="true"
    android:targetActivity="com.xd.sdklib.helper.WXEntryActivity"/>

<!-- 微信登录和分享，游戏接入了其它微信分享SDK时必须使用该方法，并按照文章后面的内容进行处理
<activity
    android:name=".wxapi.WXEntryActivity"
    android:label="@string/app_name"
    android:exported="true"/>
-->
```

<p>在游戏项目中增加“{游戏包名}.wxapi.WXEntryActivity”类，内容如下，注意替换代码中花括号括起来的需要游戏自定的内容。</p>

```
package {游戏的包名(AndroidManifest.xml中的package)}.wxapi;

import android.app.Activity;
import android.content.Intent;
import android.os.Bundle;
import android.util.Log;

import com.tencent.mm.opensdk.modelbase.BaseReq;
import com.tencent.mm.opensdk.modelbase.BaseResp;
import com.tencent.mm.opensdk.modelmsg.SendAuth;
import com.tencent.mm.opensdk.modelmsg.SendMessageToWX;
import com.tencent.mm.opensdk.openapi.IWXAPI;
import com.tencent.mm.opensdk.openapi.IWXAPIEventHandler;
import com.tencent.mm.opensdk.openapi.WXAPIFactory;
import com.xd.xdsdk.XDCore;

public class WXEntryActivity extends Activity implements IWXAPIEventHandler {

    private IWXAPI api;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        this.api = WXAPIFactory.createWXAPI(this, XDCore.getInstance().getWXAppId(),false);
        this.api.handleIntent(getIntent(), this);
    }
    
    @Override
    protected void onNewIntent(Intent intent) {
        super.onNewIntent(intent);
        
        setIntent(intent);
        api.handleIntent(intent, this);
    }
    
    @Override
    public void onReq(BaseReq arg0) {
    }

    @Override
    public void onResp(BaseResp arg0) {
        if (arg0 instanceof SendMessageToWX.Resp) {
            Intent intent = new Intent();
            String str = getPackageName() + ".wxapi.{被改名的分享类名}" // 替换后的例子"wxapi.MYWXEntryActivity"
            intent.setClassName(getPackageName(), str);
            if (intent.resolveActivity(getPackageManager()) != null) {
                intent.putExtras(getIntent());
                startActivity(intent);
            }
            finish();
            return;
        }

        if (arg0 instanceof SendAuth.Resp) {
	    if (XDPlatform.isLoggedIn() && !XDUser.getUser().isGuester()) return;
            XDPlatform.isIn3rdActivity = false;
            switch (arg0.errCode) {
            case BaseResp.ErrCode.ERR_OK:
                String code = ((SendAuth.Resp)arg0).code;
                Log.d("微信code",code);
                //调用平台登录
                XDCore.getInstance().wxLogin(code, "app");
                break;
            case BaseResp.ErrCode.ERR_USER_CANCEL:
	        XDPlatform.dismissProgressBar();
                Log.d("微信登录","ERR_USER_CANCEL");
                break;
            case BaseResp.ErrCode.ERR_AUTH_DENIED:
	        XDPlatform.dismissProgressBar();
                Log.d("微信登录","ERR_AUTH_DENIED");
                break;
            default:
                break;
            }
        }
        finish();
    }
}
```

## 3.iOS

用Unity导出Xcode工程并打开

### 3.1. 导入SDK文件

从心动平台处获取SDK资源文件，其中主要的文件或目录用途如下。

目录或文件 | 用途
--- | ---
resource | 心动SDK需要或依赖的资源文件，需要保证所有文件都被添加到了Xcode的“Copy Bundle Resources”中


将以上文件导入Xcode工程。

### 3.2. 添加系统依赖库

```
libz.tbd
libsqlite3.tbd
libicucore.tbd
*请核对下列库文件是否已自动添加*
Security.framework 
CFNetwork.framework
UIKit.framework
QuartzCore.framework
Foundation.framework
CoreGraphics.Framework
CoreTelephony.framework
SystemConfiguration.framework
libiconv.tbd
libstdc++.tbd
```

### 3.3. 设置 URL Types

需要在Xcode中设置多个URL Types，URL Types主要是需要设置URL Schemes，其它选项可任意填写。按照下面表格的内容填写，注意替换其中的各项AppID。

URL Schemes | 用途 |示例 |备注
---|---|---|---|
XD-{心动AppID}|用于支付宝支付后跳回|XD-ci2dos1ktzsca4f
{微信AppID}| 用于微信授权登录后跳回|wx19f231d77ac408d9
tencent{QQ AppID}|用于QQ授权登录后跳回|tencent317081|如果给到的心动AppID没有对应的QQ AppID，可以不配置该项

### 3.4. 配置 info.plist

修改项目的info.plist，在<dict>节点中添加下列内容。修改的内容主要为了保证QQ和微信登录能够正常运行。

```
<key>LSApplicationQueriesSchemes</key>
<array>
<string>mqq</string>
<string>mqqapi</string>
<string>wtloginmqq2</string>
<string>mqqopensdkapiV4</string>
<string>mqqopensdkapiV3</string>
<string>mqqopensdkapiV2</string>
<string>mqqwpa</string>
<string>mqqOpensdkSSoLogin</string>
<string>mqqgamebindinggroup</string>
<string>mqqopensdkfriend</string>
<string>mqzone</string>
<string>weixin</string>
<string>wechat</string>
</array>
<key>NSAppTransportSecurity</key>
<dict>
<key>NSAllowsArbitraryLoads</key>
<true/>
</dict>

```

### 3.5. 处理第三发应用跳回事件

<p style="color:red">
在UnityAppController.mm中增加如下两个方法，如果已经存在这些方法，在其中追加相应的处理代码即可。请务必添加下列代码，否则将影响第三方登录的授权回调。
</p>

```
#import <XdComPlatform/XDCore.h>
```

```
- (BOOL)application:(UIApplication*)application openURL:(NSURL*)url sourceApplication:(NSString*)sourceApplication annotation:(id)annotation{
	
	...
	...
    ...
   return [XDCore HandleXDOpenURL:url];
}

- (BOOL)application:(UIApplication *)app openURL:(NSURL *)url options:(NSDictionary<UIApplicationOpenURLOptionsKey, id> *)options{
    
    return [XDCore HandleXDOpenURL:url];
}
```

### 3.6. Buid Settings

Enable Bitcode = NO

##### 完成以上配置即可进行编译打包。

## 4. 服务端对接

### 4.1	获取用户信息
游戏服务端使用客户端获取的access token，按照下面的方式获取用户信息。

```
接口：https://api.xd.com/v1/user
method：GET
参数：access_token
请求示例：https://api.xd.com/v1/user?access_token=1234
成功判断：返回的HTTP Code为200时表示成功，否则失败
返回数据格式：application/json
返回值示例：
{"id":”a13”,"name":"xdname","friendly_name":"xdfriendly_name","client_id":"abc"}
id：用户的ID，注意类型是字符串
name：用户的账号名称
friendly_name：用户的昵称，如果游戏想要展现用户名称，建议使用该字段
client_id：该用户首次在该游戏登录时使用的心动AppID
```
### 4.2.	处理支付回调
游戏服务端需要提供一个能够处理支付回调的接口，这个接口是申请心动AppID时需要的。处理逻辑中，需要使用一个密钥进行加密验证，该密钥即为心动AppKey。
当心动平台处有充值成功时，心动服务端会通知到支付回调接口，信息如下。

```
method：POST
数据格式：application/x-www-form-urlencoded

```
字段如下。


字段 | 类型 | 描述
--- | --- | ---
order_id | number | 心动平台的订单号，相同订单号表示是同一笔支付
payment | string | 支付方式，appstore或其它（若回调无该字段，则默认为appstore）
user_id | string | 充值用户ID，注意类型是字符串
client_id | string | 充值的心动AppID
app | string | 同client_id
app_id | string | 游戏客户端调用充值时传递的Sid字段
app\_order_id | string | 游戏客户端调用充值时传递的OrderId字段
role_id | string | 游戏客户端调用充值时传递的Role_Id字段
product_id | string | 支付购买的商品ID
gold | number | 支付实际所付金额，单位元。（仅在客户端使用非AppStore支付方式支付时才有该字段）
ext | string | 游戏客户端调用充值时传递的EXT字段
timestamp | number | 时间戳，1990年到当前时间的秒数
sign | string | 签名校验字段，按照下面的方式进行校验

签名算法示例，使用php语言。

```
/**
* @param params 类型array，支付回调时收到的参数
* @param appKey 类型string，心动AppKey
*/
function verify_sign($params, $appKey) {
$tmp = $params;
$sign = $tmp['sign'];
unset($tmp['sign']);
ksort($tmp);

return strcasecmp($sign, md5(http_build_query($tmp) . $appKey)) == 0;
}
```

<p style="color:red">
需要注意
</br>
1、游戏服务端应该按照order_id进行排重，相同order_id仅生效一次。
</br>
2、游戏服务端成功处理了支付回调后，应当返回字符串“success”，如果是一笔已经处理的重复的订单，也应该返回“success”。
</br>
3、只要通过签名校验的回调，都应该视为合法数据，按照如下逻辑发放道具。A.如果payment字段为appstore，即AppStore支付，直接按照product_id字段进行道具发放；B.如果payment字段为其它值，需要验证gold字段和 product_id 字段是否相符，如果相符，按照product_id发放道具，如果不相符，直接按照gold字段折算成对应的游戏货币发放。
</p>
