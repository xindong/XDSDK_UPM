//
//  XDCallback.h
//  XdComPlatform
//
//  Created by 杜凯强 on 2017/6/13.
//  Copyright © 2017年 dyy. All rights reserved.
//

#import <Foundation/Foundation.h>


/**
 3.0 以上版本 心动SDK回调事件
 */
@protocol XDCallback <NSObject>

@required

/**
 初始化成功
 */
- (void)onInitSucceed;

/**
 初始化失败

 @param error_msg 错误信息
 */
- (void)onInitFailed:(nullable NSString*)error_msg;


/**
 登录成功

 @param access_token Token
 */
- (void)onLoginSucceed:(nonnull NSString*)access_token;



/**
 登录失败

 @param error_msg 错误信息
 */
- (void)onLoginFailed:(nullable NSString*)error_msg;



/**
 登录取消
 */
- (void)onLoginCanceled;


/**
 游客账号升级成功
 */
- (void)onGuestBindSucceed:(nonnull NSString*)access_token;

/**
 游客账号升级失败
 */
- (void)onGuestBindFailed:(nonnull NSString*)errorMsg;


/// 绑定taptap成功
/// @param profile 返回用户信息
- (void)onBindTaptapSucceed:(nonnull NSString *)profile;

/**
 登出成功
 */
- (void)onLogoutSucceed;


/**
 支付完成
 */
- (void)onPayCompleted;


/**
 支付失败

 @param error_msg 错误信息
 */
- (void)onPayFailed:(nullable NSString*)error_msg;


/**
 支付取消
 */
- (void)onPayCanceled;

- (void)onRealNameSucceed;

- (void)onRealNameFailed:(nullable NSString*)error_msg;

/// 有未完成的订单回调，比如：礼包码.注意：多个未完成订单会在一个数组中一起回调。（只会在登录状态下回调）
/// @param paymentInfos 订单信息数组。
/// 单个未完成订单信息包含：     transactionIdentifier ：订单标识 ，恢复购买时需要回传
///                                                       productIdentifier ：商品ID，
///                                                                     quantity：商品数量
- (void)restoredPayment:(nonnull NSArray*)paymentInfos;


/// 打开协议页面成功失败
- (void)onOpenProtocolSuccess;
- (void)onOpenProtocolFail;
- (void)onAgreeProtocol;

@end
