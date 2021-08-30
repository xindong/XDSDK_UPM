# ChangeLog

## 3.3.2
* 兼容今日头条分包 SDK 
* 升级 TapSDK UPM 版本 1.1.9

### Dependencies
* TapSDK UPM Version ---> 1.1.9

## 3.3.1

### Feature
* 升级 TapSDK UPM版本 1.1.8

### Dependencies

* TapSDK UPM Version ---> 1.1.8


## 3.3.0 

### Feature
* Android / iOS 将隐私协议验证调整至初始化之前
* Android 更新 OAID 依赖库
* Android 更新防沉迷依赖库


## 3.2.0    
### Feature
* Android / iOS 修改实名提示文案
* Android / iOS 修改不同意隐私和用户等协议时文案显示
* Android / iOS 修改心动注册时默认为未同意协议
* iOS TapDB在 14.5及以上不再申请 IDFA 权限

### Dependencies

* TapSDK Version ---> 1.1.5


## 3.1.2

### BugFix

* 修复 iOS Processor.cs 脚本未添加 TapTap URLTypes 的错误

## 3.1.1

### BugFix

* 修复 iOS TapDB 数据上报错误

### Dependencies

* TapSDK Version ---> 1.0.9

## 3.1.0 

### Feature
* Android / iOS 添加防沉迷 SDK 游戏时长上报功能
* Android / iOS 添加中宣部实名信息认证异常后提示
### BugFix
* iOS 修复动态屏幕旋转异常
### Dependencies
* TapSDK version --> 1.0.8


## 3.0.0 

### 1. 功能变更
* Android / iOS 添加非游客账号绑定 Tap 账号回调 onBindTaptapSucceed
* Android / iOS 修改初始化接口，添加是否使用动态 enableMoment 字段
* Android / iOS 添加对于已实名用户调用打开实名窗口时返回实名失败回调
* Android / iOS 修改单独绑定手机号时接口配置

### 2. 文件变更
新版 XDSDK 使用 UPM 方式接入，所以需将之前的 XDSDK 相关文件全部删除，重新使用 Package Manager 接入，具体需要删除的文件请参考 [旧版SDK文件列表](./Documentation/旧版SDK文件列表.md)

  
