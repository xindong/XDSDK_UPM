//
//  XDLiveWrapper.cpp
//  XDLiveiOSWrapper
//
//  Created by 孙毅 on 2018/10/22.
//  Copyright © 2018 xd. All rights reserved.
//

#include "XDLiveWrapper.h"
#include "XDLive/XDLive.h"


@interface XDLiveWrapper() <XDLiveDelegate>
@end


@implementation XDLiveWrapper

static XDLiveWrapper * instance;

+ (instancetype)defaultInstance{
    
    static dispatch_once_t onceToken;
    dispatch_once(&onceToken, ^{
        instance = [[XDLiveWrapper alloc] init];
    });
    return instance;
}

#if defined(__cplusplus)
extern "C"{
#endif
    extern void UnitySendMessage(const char *, const char *, const char *);
#if defined(__cplusplus)
}
#endif


- (void)onXDLiveClosed {
//    NSLog(@"WRAPPER onXDLiveClosed");
        UnitySendMessage("XDLiveListener", "OnXDLiveClosed" , "");
}

- (void)onXDLiveOpen {
//    NSLog(@"WRAPPER onXDLiveOpen");
        UnitySendMessage("XDLiveListener", "OnXDLiveOpen" , "");
}


#if __cplusplus
extern "C" {
#endif
    void openXDLive(const char * appid){
        [XDLive setDelegate:[XDLiveWrapper defaultInstance]];
        [XDLive openXDLive:[NSString stringWithUTF8String:appid]];
    }
#if __cplusplus
}
#endif




@end
