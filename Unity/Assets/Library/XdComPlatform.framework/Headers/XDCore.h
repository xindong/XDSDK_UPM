//
//  XDCore.h
//  XdComPlatform
//
//  Created by w99wen on 14-11-10.
//  Copyright (c) 2014年 XINDONG Network. All rights reserved.
//

#import "XDCallback.h"

#ifndef XdComPlatform_XDCore_h

#define XdComPlatform_XDCore_h

/**
 心动SDK 4.3.3
 */
@interface XDCore : NSObject


# pragma mark - XDSDK4.0

/**
 获取SDK版本信息
 
 @return 版本信息
 */
+ (nonnull NSString*)getSDKVersion;


/// 初始化支付相关功能
+ (void)setupXDStore;
/**
 设置回调协议

 @param delegate <XDCallback>
 */
+ (void)setCallBack:(nonnull id<XDCallback>)delegate;


/**
 自定义登录入口。共五种，其中主要两种，次要两种。
 1、默认显示为：苹果、TapTap、微信、QQ、游客
 
 2、各登录方式对应名称如下：
 微信登录：WX_LOGIN，
 QQ登录：QQ_LOGIN，
 taptap登录：TAPTAP_LOGIN，
 游客登录：GUEST_LOGIN，
 心动登录：XD_LOGIN,
 苹果登录：APPLE_LOGIN,
 
 3、例，传入的数组。
 @[@"WX_LOGIN",@"QQ_LOGIN",@"TAPTAP_LOGIN",@"GUEST_LOGIN"]

 注：
    1）传入为空或nil，则当做隐藏所有按钮，直接显示心动登录。
    2）最多只能显示5种登录方式。
    3) 登录按钮TapTap和心动登录不能同时显示

 @param entries 入口类型，带顺序
 */
+ (void)setLoginEntries:(nullable NSArray *)entries;


/**
 隐藏游客登录功能
 */
+ (void)hideGuest;


/**
 隐藏微信登录功能
 */
+ (void)hideWX;


/**
 隐藏QQ登录功能
 */
+ (void)hideQQ;


/**
 显示VeryCD登录功能
 */
+ (void)showVC;


/**
 强制QQ为Web登录方式
 */
+ (void)setQQWeb;


/**
 强制微信为web登录方式
 */
+ (void)setWXWeb;


/**
 隐藏TapTap登录
 */
+ (void)hideTapTap;


/**
 初始化sdk，包含心动SDK，TapDB统计SDK

 @param appid 心动appid
 @param orientation 屏幕方向
 @param channel 渠道号
 @param version 版本号
 @param enableTapdb 是否开启TapDB
 */
+ (void)init:(nonnull NSString *)appid
 orientation:(int)orientation
     channel:(nonnull NSString *)channel
     version:(nonnull NSString *)version
 enableTapdb:(BOOL)enableTapdb;


/**
 登录
 */
+ (void)login;


/*
 手动调用登录方式时，先调用autologin
 */
+ (void)autoLogin;

+ (void)taptapLogin;
+ (void)appleLogin;
+ (void)guestLogin;


/**
 用户反馈
 */
+ (void)userFeedback;


/**
 获取Token

 @return Token
 */
+ (nullable NSString *)getAccessToken;


/**
 获取登录状态

 @return （YES，已登录）（NO，未登录）
 */
+ (BOOL)isLoggedIn;


/**
 打开用户中心

 @return （YES，打开成功）（NO，已经打开）
 */
+ (BOOL)openUserCenter;

/**
 打开游客升级

 @return 是否打开成功
 */
+ (BOOL)openUserBindView;


/**
 打开实名认证窗口
 */
+ (void)openRealName;


/**
 主动展示用户协议窗口，不提供给游戏，只作DEMO使用
 */
+ (void)openAgreementView;


/**
 进行支付

 @param prodectInfo 订单信息
 @return （YES，流程正常）（NO，尚未登录或重复调用）
 */
+ (BOOL)requestProduct:(nonnull NSDictionary *)prodectInfo;

/**
 恢复支付
 
 @param prodectInfo 订单信息
 @return （YES，流程正常）（NO，尚未登录或重复调用）
 */
+ (BOOL)restoreProduct:(nonnull NSDictionary *)prodectInfo;


/**
 登出用户
 */
+ (void)logout;


/**
 处理跳转app回调
 
 @param url 回调的URL
 @return TRUE-回调成功，FALSE-回调失败
 */
+ (BOOL)HandleXDOpenURL:(nonnull NSURL *)url;


/// 处理跳转回调
/// @param userActivity 回调内容
+ (BOOL)handleOpenUniversalLink:(nullable NSUserActivity *)userActivity;

#pragma mark - 防沉迷
/// 恢复时长统计
+ (void)gameResume;

/// 停止时长统计
+ (void)gameStop;


/*
 检查防沉迷剩余时间，暂时只支持联网数据查询
 由于客户端数据定时发送，结果可能有最多两分钟时长误差
 成年人或不受防沉迷用户，结果可能为空
 */
+ (NSString *_Nullable)checkAntiAddictionResult;

#pragma mark - 角色相关
+ (void)setRole:(NSString *_Nullable)roleId roleName:(NSString *_Nullable)roleName roleAvatar:(NSString *_Nonnull)avatarUrl;

+ (void)clearRole;


#pragma mark - TapDB相关

/**
 设置玩家区服

 @param server server
 */
+ (void)setServer:(nonnull NSString *)server;


/**
 设置玩家等级

 @param level 等级
 */
+ (void)setLevel:(NSInteger)level;

#pragma mark - 广告相关
/**
 自定义广告参数。如测试新参数。 需在SDK初始化前调用

@param appId 心动appId
@param params 自定义第三方广告SDK的初始化参数，会覆盖后台参数

"adPlatform":广告平台，0: 所有平台 1: 广点通 2：头条

"appID":appid
"appName":appName
"channel":channel                  App发布的渠道名（建议内测版用local_test，正式版用App Store，灰度版用发布的渠道名，如pp

"actionSetId":UserActionSetId
"secretKey":secretKey

例，覆盖头条广告平台参数:
NSMutableDictionary *params = [NSMutableDictionary dictionary];
[params setValue:@1 forKey:@"adPlatform"];
[params setValue:@"123455" forKey:@"appID"];
[params setValue:@"testName" forKey:@"appName"];
[params setValue:@"APP Store" forKey:@"channel"];

*/
+ (void)customAdParams:(nullable NSDictionary *)params;



/// 打开用户动态个人中心
/// @param xdId 心动 id
/// @param config json 字符串固定格式
+ (void)openTapMomentUserCenterWithId:(NSString *_Nonnull)xdId config:(NSString *_Nonnull)config;

/// 打开协议页面
/// @param type 0用户协议， 1游戏协议， 2隐私协议
+ (void)openProtocolWithType:(NSInteger)type;

@end

#endif
