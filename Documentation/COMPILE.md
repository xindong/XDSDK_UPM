## iOS 编译

### 1. XDSDK-Info.plist 文件描述

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

如果不需要 Apple 登陆、applinks、WeChat 以及 tencent 相关功能，可以从 XDSDK-Info.plist 中进行删除。

### 2. XDSDKXCodePostProcess.cs 编译流程

    注意：该脚本执行顺序为 [PostProcessBuildAttribute(99)], 以下使用 '脚本' 指代 XDSDKXCodeProcess.cs 

#### 2.1 导入资源文件
* 脚本 会在 XCode 输出工程的目录下新建 XDSDKResource 文件夹，根据游戏方导入 XDSDK 的方式不同，脚本会筛选 ProjectPath/Library/PackageCache/com.xd.sdk@{commitHashCode}   或者 ProjectPath/XDSDK 目录下的资源文件并且添加。

#### 2.2 修改 Info.plist 
* LSApplicationQueriesSchemes  添加 tapsdk、mqq、mqqapi 等第三方 schemes
* URLTypes 添加 XD-Id、WeChat Id 、Tencent Id 、TapTap
* 添加相关权限

#### 2.3 修改 UnityAppController.mm 
* 添加第三方登陆回调

### 3. XDSDK XCode 工程 CheckList

- [ ] Unity-iPhone 中包含 TDSCommonResource.bundle，TDSMomentResource.bundle， XDSDKResource.bundle
- [ ] Unity-iPhone 或者 UnityFramework 中包含 TapSDK.framework，TDSCommon.framework, XdComPlatform.framework 
- [ ] LSApplicationQueriesSchemes 中包含 
```    
    tapsdk
    mqq
    mqqapi
    wtloginmqq2
    mqqopensdkapiV4
    mqqopensdkapiV3
    mqqopensdkapiV2
    mqqwpa 
    mqqOpensdkSSoLogin
    mqqgamebindinggroup 
    mqqopensdkfriend 
    mqzone 
    weixin 
    wechat 
    weixinULAPI
```
- [ ] UnityAppController.mm 中包含以下相关代码
```
#import <XdComPlatform/XDCore.h>
#import <TapSDK/TapLoginHelper.h>

[TapLoginHelper handleTapTapOpenUrl];
[XDCore HandleXDOpenURL:url];
[XDCore handleOpenUniversalLink:userActivity];
```
