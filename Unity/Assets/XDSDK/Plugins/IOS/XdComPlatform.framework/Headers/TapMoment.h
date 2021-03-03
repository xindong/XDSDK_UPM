//
//  Moment.h
//  XdComPlatform
//
//  Created by ritchie on 2021/2/23.
//  Copyright © 2021 X.D. Network Inc. All rights reserved.
//

#import <Foundation/Foundation.h>
#import "TapPostMomentData.h"

/// 动态发布成功
FOUNDATION_EXTERN int const TM_RESULT_CODE_POST_SUCCEED;
/// 动态发布失败
FOUNDATION_EXTERN int const TM_RESULT_CODE_POST_FAILED;
/// 关闭动态发布页面
FOUNDATION_EXTERN int const TM_RESULT_CODE_POST_CLOSED;


/// 获取新消息成功
FOUNDATION_EXTERN int const TM_RESULT_CODE_NEW_MSG_SUCCEED;
/// 获取新消息失败
FOUNDATION_EXTERN int const TM_RESULT_CODE_NEW_MSG_FAILED;

/// 好友动态页面打开
FOUNDATION_EXTERN int const TM_RESULT_CODE_MOMENT_PAGE_OPEBED;
/// 好友动态页面关闭
FOUNDATION_EXTERN int const TM_RESULT_CODE_MOMENT_PAGE_CLOSED;

/// 取消关闭所有动态界面（弹框点击取消按钮）
FOUNDATION_EXTERN int const TM_RESULT_CODE_MOMENT_CLOSE_CANCELLED;
/// 确认关闭所有动态界面（弹框点击确认按钮）
FOUNDATION_EXTERN int const TM_RESULT_CODE_MOMENT_CLOSE_CONFIRMED;

/// 内嵌登录成功
FOUNDATION_EXTERN int const TM_RESULT_CODE_MOMENT_LOGIN_SUCCEED;



NS_ASSUME_NONNULL_BEGIN

typedef NS_ENUM (NSInteger, TapMomentOrientation) {
    TapMomentOrientationDefault   = -1,
    TapMomentOrientationLandscape = 0,
    TapMomentOrientationPortrait  = 1,
};

@protocol TapMomentDelegate <NSObject>

@optional
- (void)onMomentCallbackWithCode:(NSInteger)code msg:(NSString *)msg;

@end

@interface TapMomentConfig : NSObject
@property (nonatomic, assign) TapMomentOrientation orientation;
+ (TapMomentConfig *)createWithOrientation:(TapMomentOrientation)orientation;
@end

@interface TapMoment : NSObject

+ (void)initSDKWithClientId:(NSString *)clientId;

+ (void)setDelegate:(id <TapMomentDelegate>)delegate;

+ (NSString *)getSdkVersion;

+ (NSString *)getSdkVersionCode;

+ (void)openTapMomentWithConfig:(TapMomentConfig *)config;

/// 打开好友个人中心
/// @param config MomentConfig
/// @param userId openId
+ (void)openUserCenterWithConfig:(TapMomentConfig *)config userId:(NSString *)userId;

/// 关闭所有内嵌窗口
/// @param title 弹框的 title
/// @param content 弹框的 content
/// @param showConfirm 是否显示确认弹窗
/// @warning 传空或者空串不弹框
+ (void)closeMomentWithTitle:(NSString *)title
                     content:(NSString *)content
                 showConfirm:(BOOL)showConfirm;

/// 直接关闭所有内嵌窗口不弹二次确认窗口
+ (void)closeMoment;

/// 获取新动态数量
/// @warning 结果在 `Delegate` 下的 `onMomentCallbackWithCode:msg:`, code == TM_RESULT_CODE_NEW_MSG_SUCCEED时，`msg` 即为消息数量
+ (void)fetchNewMessage;

/// 发布动态
/// @param content 发布的动态内容
/// @param config 配置
/// @warning `moment`参数：动态内容为视频请选择`VideoMomentData`, 动态内容为图片时请选择 ImageMomentData`。
+ (void)openPostMomentPageWithContent:(TapPostMomentData *_Nonnull)content
                               config:(TapMomentConfig *_Nonnull)config;
@end







NS_ASSUME_NONNULL_END
