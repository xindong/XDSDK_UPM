# ChangeLog

## 3.1.0 

### Dependencies

* TapSDK version --> 1.0.8

### BugFix

* 修复 iOS 内嵌动态旋转问题

## 3.0.0 

### 1. 功能变更
* Android / iOS 添加非游客账号绑定 Tap 账号回调 onBindTaptapSucceed
* Android / iOS 修改初始化接口，添加是否使用动态 enableMoment 字段
* Android / iOS 添加对于已实名用户调用打开实名窗口时返回实名失败回调
* Android / iOS 修改单独绑定手机号时接口配置

### 2. 文件变更
新版 XDSDK 使用 UPM 方式接入，所以需将之前的 XDSDK 相关文件全部删除，重新使用 Package Manager 接入，具体需要删除的文件请参考 [旧版SDK文件列表](./Documentation/旧版SDK文件列表.md)

  
