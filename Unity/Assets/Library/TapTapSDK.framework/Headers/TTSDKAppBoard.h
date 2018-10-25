//
//  TTSDKNotificationCenter.h
//  TapTapSDK
//
//  Created by 孙毅 on 2018/8/7.
//  Copyright © 2018年 易玩. All rights reserved.
//

#import <Foundation/Foundation.h>


typedef void (^TTSDKAppBoardStatusHandler)(NSInteger unreadCount , NSError * error);

@interface TTSDKAppBoard : NSObject

+ (void) setAppBoardStatusHandler:(TTSDKAppBoardStatusHandler) handler;

+ (void) queryAppBoardStatus;

+ (void) markArticleRead : (NSString *) articleID;

+ (void) markArticlesRead:(NSArray<NSString *> *) articleIDArray;

+ (void)fetchLatestAppBoard:(void (^)(NSData *, NSURLResponse *, NSError *)) responseHandler;

@end
