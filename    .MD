### QQ

腾讯已禁止外部移动平台使用Web用户名密码登录，所以心动SDK的setQQWeb将不再支持，未安装QQ时弹起的Web页面为QQSDK内置。

平台 | QQ安装 | 表现
--- | --- | --- |
iOS | 是 | 跳转
iOS | 否 | Web用户名密码或扫码
Android | 是 | 跳转
Android | 否 | Web扫码


#### 微信

App Store 严格来说不允许登录方式中使用扫码登录，所以iPhone未安装微信时隐藏，iPad未安装时弹出扫码。如采用这种默认策略被拒审。再尝试审核时调用隐藏微信，过审后打开。

平台/机型 | 微信安装 | 表现
--- | --- | --- |
iPhone | 是 | 跳转
iPhone | 否 | 隐藏
iPad | 是 | 跳转
iPad | 否 | Web扫码
Android | 是 | 跳转
Android | 否 | Web扫码

setWXWeb后在非隐藏的情况下，会变为扫码


