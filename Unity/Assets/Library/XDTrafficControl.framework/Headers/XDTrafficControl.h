//
//  XDTrafficControl.h
//  XDTrafficControl
//
//  Created by Jinya Tu on 2019/2/18.
//  Copyright © 2019 xd. All rights reserved.
//

#import <Foundation/Foundation.h>

/// ===== 排队系统回调代理 =====
@protocol XDTrafficControlDelegate <NSObject>
@required

/**
 无需排队或者排队结束，可以进入游戏
 */
- (void)onQueuingFinished;

/**
 排队取消
 */
- (void)onQueuingCancelled;

/**
 排队服务请求失败

 @param errorMessage 失败信息
 */
- (void)onQueuingFailed:(NSString *)errorMessage;

@end


/// ===== XDTrafficControl =====
@interface XDTrafficControl : NSObject

/**
 获取SDK版本信息
 
 @return 版本信息
 */
+ (nonnull NSString *)getSDKVersion;

/**
 设置排队服务回调代理

 @param delegate 接受回调的对象
 */
+ (void)setDelegate:(id<XDTrafficControlDelegate>)delegate;

/**
 请求回调

 @param appId 游戏AppId
 */
+ (void)check:(NSString *)appId;

@end


