# <center> XDSDK-Unity </center>

### 目录
* <a href="#1">Unity 环境设置及 SDK 资源配置</a>
	* <a href="#1.1">1.添加 XDSDK 依赖</a>
	* <a href="#1.2">2..配置 XDSDK</a>
* <a href="#2">API</a>
	* <a href="#2.1">1.实现并设置回调方法</a>
	* <a href="#2.2">2.配置登录选项</a>
	* <a href="#2.3">3.初始化SDK</a>
	* <a href="#2.4">4.登录</a>
	* <a href="#2.5">5.角色信息</a>
	* <a href="#2.6">6.用户中心</a>
	* <a href="#2.7">7.支付</a>
	* <a href="#2.8">8.登出</a>
	* <a href="#2.9">9.游客升级</a>
	* <a href="#2.10">10.实名认证</a>
	* <a href="#2.11">11.游客升级</a>
	* <a href="#2.12">12.客服</a>
	* <a href="#2.13">13.TapDB</a>
	* <a href="#2.14">14.防沉迷设置</a>
	* <a href="#2.15">15.TapTap 论坛</a>
	* <a href="#2.16">16.XDLive 直播</a>
	* <a href="#2.17">17.打开协议页面</a>
	* <a href="#2.18">18.地区判断</a>
	* <a href="#2.19">19.账户注销</a>
* <a href="#3">原生平台设置</a>
	* <a href="#3.1">1.Android 平台设置</a>
	* <a href="#3.2">2.iOS 平台设置</a>
* <a href="#4">服务端对接</a>
	* <a href="#4.1">1.获取用户信息</a>
	* <a href="#4.2">2.处理支付回调</a>
* <a href="#5">广告渠道配置</a>
	* <a href="#5.1">1.广告部分说明</a>
	* <a href="#5.2">2.今日头条分包SDK接入说明 ( 仅Android )</a>
	* <a href="#5.3">3.腾讯广点通分包SDK接入说明 ( 仅Android )</a>


<a name="1"></a>

## Unity 环境设置及 SDK 资源配置

### 前提条件

* 安装Unity **Unity 2018.3** 或更高版本

* IOS 编译最低版本为 **10** 或更高版本

* Android 编译设置 **Minimum API 21** 或更高版本, Target API 建议使用 30 以下

<a name="1.1"></a>

### 1. 添加 XDSDK 依赖

**已接入旧版 SDK 的游戏需要先删除旧版 SDK 的所有文件，具体文件列表参考 [旧版SDK文件列表](./旧版SDK文件列表.md)**  

 新版 XDSDK 使用 UPM 方式导入 SDK 资源，配置方式有三种，选择一种即可，如下所示：

#### 1.1 修改对应配置文件

```json
//在YourProjectPath/Packages/manifest.json中添加以下代码
"dependencies":{
        "com.xd.sdk":"https://github.com/xindong/XDSDK_UPM.git#3.4.5“,
        "com.tds.sdk":"https://github.com/xindong/TAPSDK_UPM.git#1.2.1"
    }
```

#### 1.2 通过 Unity PackageManager 导入

在 Unity 菜单栏中选择 Window -> Package Manager, 选择添加方式为 `Add package from git URL` , 在地址栏中输入 `https://github.com/xindong/TAPSDK_UPM.git#1.2.1` 和 `https://github.com/xindong/XDSDK_UPM.git#3.4.5` 即可。

> 注意：不同 Unity 版本 UI 显示可能略有差别

#### 1.3 通过本地源码导入

在 ProjectPath 目录下新建 XDSDK、TapSDK 两个文件夹，拷贝 [XDSDK源码](https://github.com/xindong/XDSDK_UPM.git#3.4.5) 、[TapSDK源码](https://github.com/xindong/TAPSDK_UPM.git#1.2.1) 到指定目录中。

>注意：ProjectPath/XDSDK，Project/TapSDK 目录均为大小写敏感，且 XDSDK 、TapSDK 会对该目录下的资源文件进行脚本依赖。

```json
//在YourProjectPath/Packages/manifest.json中添加以下代码
"dependencies":{
        "com.xd.sdk":"file:..//XDSDK“,
        "com.tds.sdk":"file:..//TapSDK",
    }
```

<a name="1.2"></a>

### 2. 配置 XDSDK

#### 2.1 Android 配置

编辑 Assets/Plugins/Android/AndroidManifest.xml 文件, 样例如下。
> **注意：游戏需要将`包名` 字段改为游戏的包名**

```xml
<manifest xmlns:android="http://schemas.android.com/apk/res/android"
  xmlns:tools="http://schemas.android.com/tools"
    package="com.xd.sdklib"
    android:versionCode="1"
    android:versionName="1.0" >
    
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

        <!-- 微信登录 -->
        <activity-alias
            android:name="包名.wxapi.WXEntryActivity"
            android:exported="true"
            android:targetActivity="com.xd.sdklib.helper.WXEntryActivity"/>

        <!-- 微信支付 -->
        <activity-alias
            android:name="包名.wxapi.WXPayEntryActivity"
            android:exported="true"
            android:targetActivity="com.pingplusplus.android.PaymentActivity" />
  </application>


</manifest>

```


#### 2.2 IOS 配置

在 Assets / Plugins / iOS / Resource 目录下创建 XDSDK-Info.plist 文件,复制以下代码并且替换其中以下内容。

> 注意：目录名要区分大小写

* wechat：微信 ClientId
* tecent：腾讯 ClientId
* XD：XD ClientId
* Apple\_SignIn\_Enable：true 需要Apple登陆
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
在 Assets / Plugins / iOS / Resource 目录下创建 TDS-Info.plist 文件，复制以下代码并且替换其中的 TapTap 的 ClientId 和 授权文案

```xml
<?xml version="1.0" encoding="UTF-8"?>
<!DOCTYPE plist PUBLIC "-//Apple//DTD PLIST 1.0//EN" "http://www.apple.com/DTDs/PropertyList-1.0.dtd">
<plist version="1.0">
<dict>
    <key>taptap</key>
    <dict>
        <key>client_id</key>
        <string>ClientId</string>
    </dict>
    <key>NSPhotoLibraryUsageDescription</key>
    <string>App 需要你的同意，才能访问相册 </string>
    <key>NSCameraUsageDescription</key>
    <string>App 需要你的同意，才能访问相机 </string>
    <key>NSMicrophoneUsageDescription</key>
    <string>App 需要你的同意，才能访问麦克风 </string>
    <key>NSUserTrackingUsageDescription</key>
    <string>App需要追踪你的信息</string>
</dict>
</plist>
```

<a name="2"></a>

## API

<a name="2.1"></a>

### 1. 实现并设置回调方法

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

    //游客绑定失败回调（可选）
    public override void OnGuestBindFailed(string msg){

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
    
    //实名认证成功
    public override void OnRealNameSucceed (){

    }
    
    //实名认证失败
    public override void OnRealNameFailed (string error_msg){

    }
    
/// 有未完成的订单回调，比如：礼包码.注意：多个未完成订单会在一个数组中一起回调。（只会在登录状态下回调）
/// @param resultList 订单信息List。
/// 单个未完成订单信息包含：     TransactionIdentifier ：订单标识 ，恢复购买时需要回传
///                             		 Product_Id ：商品ID，
///                                        Quantity：商品数量
public override void RestoredPayment(List<Dictionary<string,string>> resultList){
    }
    
    //用户同意所有协议
    public override void OnProtocolAgreed() {}

	//打开协议成功
    public override void OnProtocolOpenSucceed() {}
    
	//打开协议失败
    public override void OnProtocolOpenFailed(string msg) {}
    
    //非游客匿名账号绑定 Tap 账号成功回调（游客会返回升级成功回调）
    //返回 msg 为对应 tap 信息, 包括 name,openid,unionid,avatar 字段
    public override void OnBindTaptapSucceed(Dictionary<string,string> msg) {}

}
```

<p>设置回调方法 </p>

```
XDSDK.SetCallback (new XDSDKHandler ());
```
<a name="2.2"></a>

### 2. 配置登录选项

**如果游戏之前登录入口已包含 心动 或 QQ 、微信等其他的账号类型，则使用`自定义登录顺序`设置，否则使用`自定义登录入口`。**

建议一般在调用 SDK 初始化前进行登录入口设置。

#### 2.1 自定义登录顺序

自定义登录入口。共五种，其中主要两种，次要两种。
默认显示为：

iOS: 微信、QQ、游客、苹果、TapTap

安卓: 微信、TapTap、游客、苹果、QQ  

各登录方式对应名称如下：
 
-  微信登录：WX_LOGIN，
-  taptap登录：TAPTAP_LOGIN，
-  QQ登录：QQ_LOGIN，
-  游客登录：GUEST_LOGIN，
-  心动登录：XD_LOGIN
-  苹果登录：APPLE_LOGIN

<p style="color:red">由于苹果审核要求，iOS13 显示第三方登录的同时必须显示苹果登录(建议位置靠前)</p>

<p style="color:red">SDK 在iOS12及以下、安卓中使用web实现苹果登录，但是若游戏iOS版未上架appstore，无法使用web版苹果登录，游戏需在iOS应用上架之后，才打开iOS12系统及以下和安卓的苹果登录入口。</p>

例如传入的数组 {"APPLE\_LOGIN","WX\_LOGIN","TAPTAP\_LOGIN","GUEST\_LOGIN","QQ\_LOGIN"}  

> 注：(1) 最多只能显示5种登录方式。(2) 5个登录按钮中 TapTap 和心动登录不能同时显示

```
// 自定义登录按钮及顺序
XDSDK.SetLoginEntries({"APPLE_LOGIN","WX_LOGIN","TAPTAP_LOGIN","GUEST_LOGIN","QQ_LOGIN"});
```

#### 2.2 自定义登录入口

登录入口 UI 由游戏绘制，不再使用 SDK 页面。绘制登录按钮所需要的 UI 素材由 SDK 提供，素材地址：[XDSDK登录方式素材](XDSDK登录方式(大陆)_2020.zip)  

使用该方式登录时, SDK 提供了对应的自定义登录接口（iOS:AutoLogin/TapTapLogin/AppleLogin/GuestLogin,Android:AutoLogin/TapTapLogin），用户在点击游戏绘制的登录入口后，游戏调用对应登录接口即可。  

<a name="自定义登录入口调用方式"></a>
**登录调用方法 ：** 游戏首先隐藏登录入口，调用 AutoLogin，根据登录回调看是否自动登录成功。如果自动登录失败，其中回调信息为『自动登录失败』，则显示登录界面，然后根据用户点击不同登录按钮调用对应登录接口如 TapTapLogin 等。

**（在使用自定义绘制按钮登录时，如果用户因防沉迷而点击切换账号按钮时，会返回登录取消的回调）**

自定义登录接口如下：

```
// 自动登录（若有上次登录记录，则会直接登录）
public static bool AutoLogin() 
// TapTap登录
public static void TapTapLogin() 
// 苹果登录
public static void AppleLogin() 
// 游客登录
public static void GuestLogin() 

```

示例：

<img src="unity_img/5.jpg"></img>  

<a name="2.3"></a>

### 3. 初始化SDK

初始化心动SDK应尽量提前，调用该接口是调用其它功能接口的必要条件。

```
/**
 * @param appid 心动AppID
 * @param aOrientation 屏幕方向，0表示横屏，1表示竖屏
 * @param channel 渠道号
 * @param version 版本号
 * @param enableTapDB 是否开启TapDB
 * @param enableMoment 是否使用动态
 */
public static void InitSDK(string appid, int aOrientation, string channel, string version, bool enableTapDB, bool enableMoment)
```

<p>示例代码：</p>

```
XDSDK.InitSDK ("xxxxxx", 1, "xx", "xx", true, true);
```

<p>调用该接口会触发下列回调。</p>
<p style="color:red">其他接口请在获取到初始化成功回调之后进行调用。</p>

类别 | 回调方法
--- |---
初始化成功 | public void OnInitSucceed()
初始化失败 | public void OnInitFailed(string msg)
用户同意协议 | public void OnProtocolAgreed()


<p style="color:red">注意：</p>
<p style="color:red">已经单独接入TapDB的项目请勿开启内置TapDB统计功能</p>
<p style="color:red">其他注意事项请参考文档TapDB部分或与平台联系</p>

<a name="2.4"></a>

### 4. 登录

> 注意：**游戏应在收到同意协议的回调（OnProtocolAgreed）之后再进行登录操作**  


如果游戏使用 **自定义登录顺序** 来配置登录入口，即使用 SDK 登录页，登录时调用如下接口：

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


如果游戏使用 **自定义登录入口** 配置登录入口，即游戏自己绘制登录入口 UI，请参考[自定义登录入口调用方式](#自定义登录入口调用方式)  ，调用后收到的回调与上述相同。

#### 4.1 获取当前登录状态
游戏需要判断当前用户是否登录成功时，调用该接口：

```
public static bool IsLoggedIn()
```
代码示例
```
XDSDK.IsLoggedIn()
```

#### 4.2 获取 AccessToken

用户 **登录成功** 后，如果游戏需要获取当前用户的登录 token ，可调用如下接口：

```
public static string GetAccessToken()
```

代码示例
```
XDSDK.GetAccessToken()
```


<a name="2.5"></a>

### 5. 角色信息

如果游戏使用 **自定义登录顺序** 来配置登录入口，该设置可跳过。  
对于使用 **自定义登录入口** 的游戏，为了在用户中心显示当前用户的角色信息，可调用如下接口：
	
```
//角色登录成功之后，设置当前角色信息；角色登出后，清空当前角色信息
	public static void SetRole(string roleId, string roleName, string roleAvatar)
	
	public static void ClearRole()
```

<a name="2.6"></a>

### 6. 用户中心

调用该接口打开用户中心界面，用户可以在该界面进行游客升级和登出操作，游戏注意正确处理回调。在未登录状态，无法打开用户中心。  
在用户中心中，用户可进行登出操作，此时交互界面将消失。游戏需要提供引导用户重新进行登录的操作界面。

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
<a name="2.7"></a>

### 7. 支付

<p style='color:red'> SDK 不保证在任何情况下都能收到准确回调，请勿直接使用SDK返回的支付结果作为最终判定订单状态的依据，以后端通知结果为准。</p>

为了收到支付回调，IOS 需要在应用启动后就设置好支付相关功能。
```
- (BOOL)application:(UIApplication*)application didFinishLaunchingWithOptions:(NSDictionary*)launchOptions
{
	...
	// 初始化支付
	[XDCore setupXDStore];
}
```


#### 7.1 发起支付

调用该接口发起支付。

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
Role_Id | 是 | 支付角色ID，服务端支付回调会包含该字段
OrderId | 否 | 游戏侧订单号，服务端支付回调会包含该字段
EXT | 否 |额外信息，最长512个字符，服务端支付回调会包含该字段。可用于标记区分充值回调地址，如需使用该功能，请联系平台进行配置。代码示例：info.Add("EXT", "{\"payCallbackCode\":2}");

#### 7.2 恢复支付（ 仅 IOS ）

如果之前发生掉单情况，当玩家再次打开游戏，SDK 会再次向苹果请求一次收据信息。如有掉单的收据，且玩家登录成功后，则通知游戏，游戏来收到通知后来决定是否要补发道具。
<p style = "color:red">
游戏内购流程图：
</p>
<img src="unity_img/3.png"></img>

<p style = "color:red">
注意: 如果有遗留未完成订单，在接收到恢复订单回调后，(若单个用户可能拥有多个账号，可以请求用户确认后）调用恢复订单接口。
</p>

说明：

在掉单的情况下，SDK只能获取订单的基本信息，如商品ID、苹果侧订单ID（非游戏生成order_id）和商品数量，不能直接对应到用户，所以无法直接兑换商品。

游戏在收到掉单回调之后，可以弹窗请用户确认是否需要恢复商品，如需要，则用回调提供的已有参数，加上其他需要的参数，如角色ID，服务器ID等，使用恢复订单接口恢复该商品。

<p style = "color:red">
SDK会提供测试包，供游戏使用沙盒测试（购买之后会造成掉单），游戏调用恢复接口如果成功到账则为测试成功。
</p>

游戏内弹窗示例(文案做相应修改)：
<img src="unity_img/4.jpg"></img>

回调方法

```
/// 有未完成的订单回调，比如：礼包码.注意：多个未完成订单会在一个数组中一起回调。（只会在登录状态下回调）
/// @param resultList 订单信息List。
/// 单个未完成订单信息包含：     TransactionIdentifier ：订单标识 ，恢复购买时需要回传	
///                          	 	   Product_Id ：商品ID，
///                                        Quantity：商品数量
public override void RestoredPayment(List<Dictionary<string,string>> resultList){
    }
```


恢复订单接口

```
/**
* @param info 支付相关信息，注意key和value都是字符串类型
*/
public static bool RestorePay(Dictionary<string, string> info)

```
其中info的字段如下。

参数 | 必须 |说明
--- | --- |--- 
TransactionIdentifier | 是 | 需要恢复的订单标识，SDK恢复订单回调中包含
Product_Name | 是 |商品名称，建议以游戏名称开头，方便财务对账
Product_Id | 是 | 商品ID，到AppStore购买的商品
Product_Price | 是 | 商品价格（单位分），对于AppStore支付，该字段没有用处，但是需要传递真实金额，有多处需要用到
Sid | 是 |所在服务器ID，不能有特殊字符，服务端支付回调会包含该字段
Role_Id | 是 | 支付角色ID，服务端支付回调会包含该字段
OrderId | 否 | 游戏侧订单号，服务端支付回调会包含该字段
EXT | 否 | 额外信息，最长512个字符，服务端支付回调会包含该字段。可用于标记区分充值回调地址，如需使用该功能，请联系平台进行配置。代码示例：[prdInfo setObject:@"{\\"payCallbackCode\\":1}" forKey:@"EXT"];

#### 7.3 支付结果

调用发起支付和恢复支付接口会触发下列回调。

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
info.Add("EXT", "{\"payCallbackCode\":2}");（）
XDSDK.Pay (info);
```

<a name="2.8"></a>

### 8. 登出


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
<a name="2.9"></a>

### 9. 游客升级

**如果游戏只有 Tap 登录入口，该接口可忽略。**  
当游客账号升级成功时,会触发下列回调。<br/>
后续如需使用token，务必使用回调给的新token。但已生效的会话无需处理。

类别 | 回调方法
--- | ---
游客升级成功 | public void OnGuestBindSucceed(string token)

<a name="2.10"></a>

### 10. 实名认证

当用户为未实名玩家是，调用该方法弹出实名认证窗口。对于已实名用户，调用该接口会返回实名失败。

```
public static void OpenRealName()
```

示例代码

```
XDSDK.OpenRealName();
```

调用该接口会触发下列回调

类别 | 回调方法
--- | ---
确认退出 | public void OnRealNameSucceed() 
取消退出 | public void OnRealNameFailed(string error_msg)
 
<p style = "color : red"> 
注意：实名认证结果以服务端authoriz_state参数为准（见4.1）。
</p> 

<a name="2.11"></a>

### 11. 游客升级

对于游客账号，如果需要绑定 Tap 或其他类型账号时（根据游戏登录入口决定），调用如下接口：

```
public void OpenUserBindView()
```

示例代码

```
XDSDK.OpenUserBindView ();
```

调用该接口会触发下列回调

类别 | 回调方法
--- | ---
成功 | public void OnGuestBindSucceed (string token)
失败 | public void OnGuestBindFailed(string msg)

<a name="2.12"></a>

### 12. 客服


需要展示客服窗口时调用如下接口：

```
public static void UserFeedback()
```

示例代码

```
XDSDK.UserFeedback();
```

<a name="2.13"></a>

### 13. TapDB

如果需要调用 TapDB 中设置等级和服务端地址，可调用如下接口：

```
public static void SetLevel(int level)

public static void SetServer(string server)

```

示例代码

```
XDSDK.SetLevel(100);

XDSDK.SetServer("xxxxxxx");

```
<p style = "color : red"> 
注意：</br>
1.新版本SDK中，已经集成了TapDB的onStart、setUser方法，游戏客户端不需要再重复集成。</br>
2.提供了setLevel，setServer方法供游戏调用，充值统计由服务端完成。</br>
3.已接入TapDB的游戏在集成SDK时仅需要移除onStart和setUser方法，其余保持不变。</br>
4.尚未接入TapDB的游戏，可根据需要自行接入SDK中尚未包含的TapDB SDK的其他方法。</br>
5.如果需要自行接入TapDB，请在SDK初始化方法中enableTapDB参数填写false。
</p> 

为统计数据更加精确，SDK 提供了一个可选的第三方库`数美`，如果游戏需要数美的设备数据，可以下载依赖库 [数美](https://res.xdcdn.net/tapdb/Android/xdwl/xdwl-pri1-release.aar) 并将其添加到项目中。

<a name="2.14"></a>

### 14. 防沉迷设置

SDK 中提供停止计时和恢复计时接口。游戏根据需求自行调用。

```
public static void GameStop();

public static void GameResume ();

```

Android 防沉迷和部分统计数据依赖于 XDSDK.OnResume 和 XDSDK.OnStop 接口，所以游戏应确保这两个接口接入正常，在 unity 中接入示例：

```
void OnApplicationPause(bool pauseStatus)
    {
        if (pauseStatus)
        {
			xdsdk.XDSDK.OnStop();
        }
        else
        {
			xdsdk.XDSDK.OnResume();
        }
    }
```

<a name="2.15"></a>

### 15. TapTap 论坛

如果游戏使用 Tap 内部论坛，需在脚本类中导入 `namespace com.taptap.sdk`，相关功能接口如下

```
//设置回调
public void SetCallback(TapForumCallback callback)
//打开论坛, 其中 appid 游戏论坛ID，与TapTap开发者ID不同
public void OpenTapTapForum(string appid)

```

示例

```
TapForumSDK.Instance.SetCallback(new TapforumCallbackExample());
TapForumSDK.Instance.OpenTapTapForum("58001");

class TapforumCallbackExample : TapForumCallback
	{

		public override void OnForumAppear()
		{
			Debug.Log(" XDForum open==========");
	
		}

		public override void OnForumDisappear()
		{
			Debug.Log(" XDForum close==========");
		}
	}
```
其中 IOS 还需配置 info.plist，内容如下： 

```
<key>NSPhotoLibraryUsageDescription</key>   
<string>XXXX需要您的同意,才能访问相册</string>
<key>NSCameraUsageDescription</key>   
<string>XXXX需要您的同意,才能访问相机</string>
<key>NSAppTransportSecurity</key>
<dict>
<key>NSAllowsArbitraryLoads</key>
<true/>
</dict>
```

<a name="2.16"></a>

### 16. XDLive 直播

XDSDK 中包含了游戏直播相关组件，游戏第一次使用需要联系平台相关人员开通，获取 appid 和授权直播源管理后台。相关接口如下：

```
//设置回调监听
public void SetCallback(XDLiveCallback callback);

//打开论坛, appid: 申请到的appid
public void OpenXDLive(string appid);

//打开论坛，并跳转到指定页  appid: 申请到的appid ,uri: 自定义地址
public void OpenXDLive(string appid,string uri);

//打开论坛，并跳转到指定屏幕方向的指定页  appid: 申请到的appid ,uri: 自定义地址，orientation: 指定屏幕方向（支持竖屏游戏强制设置横屏）
public void OpenXDLive(string appid,string uri,int orientation);
```

示例：

```
com.xdsdk.xdlive.XDLive.Instance.SetCallback(new XDLIVECallbackE());
com.xdsdk.xdlive.XDLive.Instance.OpenXDLive("123");

class XDLIVECallbackE : com.xdsdk.xdlive.XDLive.XDLiveCallback
	{

		override
		public void OnXDLiveClosed()
		{
			Debug.Log(" xdlive close=========");
		}

		override
		public void OnXDLiveOpen()
		{
			Debug.Log(" xdlive open==========");
		}
	}

```

<a name="2.17"></a>

### 17. 打开协议页面

游戏需要打开协议的内容时，可以调用该接口，示例如下：

```
xdsdk.XDSDK.OpenProtocol(xdsdk.XDSDK.ProtocolType.PROTOCOL_TYPE_GAME);
```
参数为协议类型，包括  PROTOCOL\_TYPE\_USER(用户协议),PROTOCOL\_TYPE\_GAME(游戏协议),PROTOCOL\_TYPE\_PRIVACY(隐私协议)

<a name="2.18"></a>

### 18. 地区判断

如果需要判断当前地区是否在大陆内，可调用如下接口：

```c#

using TDSCommon;

TDSCommon.TDSCommon.GetRegionCode((isMainland)=>
{
    //true 大陆 false 非大陆
});

```


<a name="2.19"></a>

### 19. 账号注销 （仅iOS支持）

如果需要判断当前地区是否在大陆内，可调用如下接口：

```c#

xdsdk.XDSDK.AccountCancellation();

```

<a name="3"></a>

## 原生平台设置

<a name="3.1"></a>

### 1. Android 平台设置

### 1.1 Android 11 适配

游戏打包时根据 targetSdkVersion 版本不同需要进行适配。  
> targetSdkVersion 指在 unity 中通过 File -> Build Settings -> Player Settings -> Other Settings 中 Target API Level 设置的版本  

如果游戏打包配置时 targetSdkVersion < 30 , 暂不需要配置，可跳过。  
如果 targetSdkVersion >= 30 , 需按如下步骤进行配置。

#### (1) 添加 AndroidManifest 配置  

 在 AndroidManifest.xml 中添加如下内容：
 
 ```
 <manifest ...>
  <queries>
        <package android:name="com.tencent.mm" />
        <package android:name="com.tencent.mobileqq"/>
        <package android:name="com.taptap" />
        <package android:name="com.taptap.pad" />
        <package android:name="com.taptap.global" />
 </queries>
  <!-- 如果游戏需要游客登录或访问非游戏的其他目录文件，则需添加如下权限 -->
 <!-- <uses-permission android:name="android.permission.MANAGE_EXTERNAL_STORAGE"/>
 ...
 </manifest>
 ```
 
#### (2) Unity 配置修改

根据不同 Unity 版本修改对应编译配置，具体可参考[Unity 适配 Android 11](https://developers.google.com/ar/develop/unity/android-11-build)。  

如果当前版本小于 2019.3.patch 7, 当编译提示 launcher 与 main library 的 gradle 版本不一致错误信息时，需要将 SDK 目录中的 Android 附加 / Android 11适配目录 中 launcherTemplate.gradle 文件复制到项目 Assets / plugins / Android 目录中 、GradleTemplateFixer.cs 复制到 Assets / Editor 目录中。在编译前需选择Unity菜单 Tools -> Build -> Gradle Template Fix (Preprocess build）。如果游戏除了当前项目外还需要编译其他项目，在编译完成后还需选择 Tools -> Build -> Gradle Template Fix (Postprocess build）否则不需要其他操作。

<a name="3.2"></a>

### 2. iOS 平台设置

Unity 导出 Xcode 工程时，**如下相关配置已通过脚本自动设置，但不同版本导出时可能会有差别，实际内容以下方配置内容为准，如果因配置不同导致编译运行异常，请参照对应配置修改**。

#### 2.1 导入SDK文件

从心动平台处获取SDK资源文件，其中主要的文件或目录用途如下。

目录或文件 | 用途
--- | ---
XDSDKResource.bundle | 心动SDK需要或依赖的资源文件，需要保证所有文件都被添加到了Xcode的“Copy Bundle Resources”中


将以上文件导入Xcode工程。

#### 2.2 添加系统依赖库

```
libz.tbd
libsqlite3.0.tbd
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
libc++.tbd
// 2.1.0
AuthenticationServices.framework
```

#### 2.3 设置 URL Types

需要在Xcode中设置多个URL Types，URL Types主要是需要设置URL Schemes，其它选项可任意填写。按照下面表格的内容填写，注意替换其中的各项AppID。

URL Schemes | 用途 |示例 |备注
---|---|---|---|
XD-{心动AppID}|用于支付宝支付后跳回|XD-ci2dos1ktzsca4f
{微信AppID}| 用于微信授权登录后跳回|wx19f231d77ac408d9
tencent{QQ AppID}|用于QQ授权登录后跳回|tencent317081|如果给到的心动AppID没有对应的QQ AppID，可以不配置该项
tt{TapTap AppID}|用户TapTap授权登录后跳回|tt123456

#### 2.4 配置 info.plist

修改项目的info.plist，在<dict>节点中添加下列内容。修改的内容主要为了保证QQ和微信登录能够正常运行。

```
<key>LSApplicationQueriesSchemes</key>
<array>
<string>tapsdk</string>
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
// 2.1.0
<string>weixinULAPI</string>
</array>
<key>NSAppTransportSecurity</key>
<dict>
<key>NSAllowsArbitraryLoads</key>
<true/>
</dict>

```


#### 2.5 配置工程capability

更新SDK 2.1.0之后，
<p style="color:red">需要在xcode工程中添加两项capability：Associated Domains \ Sign in with apple.</p>

如下图:

<img src="unity_img/4.png"></img>

Associated Domains添加一项，格式为『applinks + 域名』如「applinks:www.xd.com」。
<p style="color:red">域名为游戏官网地址，请联系平台方确认</p>

#### 2.6 处理第三方应用跳回事件

<p style="color:red">
在UnityAppController.mm中增加如下两个方法，如果已经存在这些方法，在其中追加相应的处理代码即可。请务必添加下列代码，否则将影响第三方登录的授权回调。
</p>

<p style="color:red">SDK 2.1.0 新增universalLink处理</p>

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

- (BOOL)application:(UIApplication *)application continueUserActivity:(NSUserActivity *)userActivity restorationHandler:(void (^)(NSArray<id<UIUserActivityRestoring>> * _Nullable))restorationHandler {
    return [XDCore handleOpenUniversalLink:userActivity];
}
```

#### 2.7 Buid Settings

Enable Bitcode = NO

<p style="color:red">在编译选项‘Other Linker Flags’中加入「-ObjC」和 「-all_load」。</p>

**Swift Language Version 设置为 Swift 5.0**

**BuildSetting中,Always Embed Swift Standard Libraries设置为YES**

**BuildSetting中,Runpath Search Paths 确保添加 @executable_path/Frameworks**

**如果新版本Untiy导出后有多个Target,只需在主target配置即可。**

 完成以上配置即可进行编译打包。

<a name="4"></a>

## 服务端对接

<a name="4.1"></a>

### 1. 获取用户信息

游戏服务端使用客户端获取的access token，按照下面的方式获取用户信息。

```
接口：https://api.xd.com/v1/user
method：GET
参数：access_token
请求示例：https://api.xd.com/v1/user?access_token=1234
成功判断：返回的HTTP Code为200时表示成功，否则失败
返回数据格式：application/json
返回值示例：
{"id":"1",
"id_card":"1111",
"name":"xdname",
"friendly_name":"xdfriendly_name",
"client_id":"abc",
"phone":"1",
"safety":false,
"site":"1"}
id：用户的ID，注意类型是字符串
id_card：加密的身份证号，可能为空
name：用户的账号名称
friendly_name：用户的昵称，如果游戏想要展现用户名称，建议使用该字段
client_id：该用户在该游戏登录时使用的心动AppID
phone： 绑定的手机号码
safety： 账号是否安全/通过设备二次验证（true：安全，false：不安全）
site：账号类型, 0 => vc账号，1 => 心动账号，3 => qq账号，8 => 微信账号，9 => TapTap账号，注意类型是字符串
authoriz_state：0/1/2/3/4（实名状态,0未实名，>0 都表示已实名认证）
```
应用场景 | 参数以及使用方法
--- | ---
判断是否游客账号 | 游客账号的name和id相同
判断是否通过二次验证 | safety（0未实名，>0 都表示已实名认证）
判断账号类型 | site（0 => vc账号，1 => 心动账号，3 => qq账号，8 => 微信账号，9 => TapTap账号，注意类型是字符串）
判断是否实名认证 | authoriz_state（0未实名，>0 都表示已实名认证）

<a name="4.2"></a>

### 2. 处理支付回调

游戏服务端需要提供一个能够处理支付回调的接口，这个接口是申请心动AppID时需要的。处理逻辑中，需要使用一个密钥进行加密验证，该密钥即为心动AppKey。
当心动平台处有充值成功时，心动服务端会通知到支付回调接口，信息如下。

```
method：POST
数据格式：application/x-www-form-urlencoded

```

<p style="color:red">
请勿信任透传参数
</p>

字段如下。

字段 | 类型 | 描述
--- | --- | ---
order_id | number | 心动平台的订单号，相同订单号表示是同一笔支付
payment | string | 支付方式，appstore或其它（若回调无该字段，则默认为appstore）
sub_payment | string | 子支付方式，Production、Sandbox或其他，为Sandbox表示苹果沙盒充值（值可能为空）
user_id | string | 充值用户ID，注意类型是字符串
client_id | string | 充值的心动AppID
app | string | 同client_id
app_id | string | 游戏客户端调用充值时传递的Sid字段
app\_order_id | string | 游戏客户端调用充值时传递的OrderId字段
role_id | string | 游戏客户端调用充值时传递的Role_Id字段
product_id | string | 支付购买的商品ID
gold | number | 支付实际所付金额，单位元。（仅在客户端使用非AppStore支付方式支付时才有该字段）
ext | string | 游戏客户端调用充值时传递的EXT字段
timestamp | number | 时间戳，1970年到当前时间的秒数
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

<a name="5"></a>

## 广告渠道配置

<a name="5.1"></a>

### 1. 广告部分说明

XDSDK内部集成了部分主要渠道广告 SDK，包括今日头条巨量广告平台 SDK，和腾讯广点通 SDK。必要事件（如注册）会在 SDK 内部发送，充值事件由 XDSDK 服务端发送事件到相应平台。游戏不用做额外对接工作。

需要接入相应平台的SDK请联系 XDSDK 后端配置广告参数。

#### 1.1	iOS

今日头条：TTTracker.framework，版本2.0.6

广点通：GDTActionSDK.framework，版本1.4.9

游戏打包时加入对应的SDK即可。

注意：

1. 广点通SDK在添加到Link Binary With Libraries之后，还必须添加到Embedded Contend中。

2. build setting 中Other Link Flag 中添加-ObjC。


#### 1.2	Android

今日头条：版本2.0.6

广点通：版本1.4.9

需要使用广告包时，将XDAdAction.aar加入工程。并添加新权限

```
   <uses-permission android:name="android.permission.READ_PHONE_STATE"/>
```


若需要出多个渠道包，由市场联系XDSDK，提供所需文件。

SDK提供接口，获取当前包的渠道名。（单接TapDB时可以使用改接口获取渠道）

```
//获取当前包的渠道名（安卓）
public static string GetAdChannelName()

```

<a name="5.2"></a>
### 2. 今日头条分包SDK接入说明 ( 仅Android )
> **_NOTE:_** 由于今日头条的相关规定，若游戏需要在今日头条进行广告投放，则「强制接入」此 SDK。具体需求请与广告组联系。

使用今日头条的分包时，将 humesdk-1.0.0.aar 包加入工程

##### 渠道号读取
* 无需初始化

调用API读取渠道号
```
//获取当前包的渠道名（安卓）
public static string GetAdChannelName()
```

<a name="5.3"></a>
### 3. 腾讯广点通分包 SDK 接入说明 ( 仅Android )
因腾讯广点通分包 SDK 的限制，需要添加远程依赖，需要手动添加远程依赖属性，另 XDAdAction 库已升级到 2.0 版本，添加 XDAdAction_v2.0.aar 包导入到 app module 级别的 libs 文件夹中，
在 app 级的 build.gradle 文件中添加腾讯广告系统分包依赖包：

```groovy

// 腾讯广点通分包 SDK
implementation 'com.tencent.vasdolly:helper:3.0.4'
```

附：[腾讯广点通分包 SDK 接入原始文档](https://docs.qq.com/doc/DQkNBeVR4UmFNd2pO)




