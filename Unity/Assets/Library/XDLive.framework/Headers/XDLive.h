//
// Created by 孙毅 on 2018/9/27.
// Copyright (c) 2018 xd. All rights reserved.
//

#import <Foundation/Foundation.h>

#define  XDLIVE_VERSION @"0.1.2"

typedef void (^XDLFuncResultCallback)(NSDictionary *result);

@protocol XDLiveDelegate <NSObject>
-(void) onXDLiveOpen;
-(void) onXDLiveClosed;
@end


@interface XDLive : NSObject

+ (void) setDelegate:(id<XDLiveDelegate>)delegate;

+ (void) openXDLive:(NSString *)appid;

+ (void) closeXDLive;

+ (void) invokeFunc:(NSDictionary *)params callback:(XDLFuncResultCallback) callback;

@end
